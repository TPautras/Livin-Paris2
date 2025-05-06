using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using SqlConnector;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }
        public string ClientUsername { get; }
        public ObservableCollection<CartItem> Cart { get; set; } = new ObservableCollection<CartItem>();

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private ExploreViewModel _exploreViewModel;
        
        public ICommand LogoutCommand { get; }
        public ICommand ShowExploreViewCommand { get; }
        public ICommand ShowCartViewCommand { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom}!";

        public ClientViewModel(Personne user, string client)
        {
            ClientUsername = client;
            CurrentUser = user;
            _exploreViewModel = new ExploreViewModel(user, this);
            CurrentView = _exploreViewModel;

            ShowCartViewCommand = new RelayCommand(o =>
            {
                CurrentView = new CartViewModel(Cart, this);
            });
            ShowExploreViewCommand = new RelayCommand(o =>
            {
                CurrentView = _exploreViewModel;
            });
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }
        
        private void ExecuteLogout()
        {
            Window newWindow = new MVVM.View.LoginView{DataContext = new LoginViewModel()};
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }

        public void AddCartItem(CartItem cartItem)
        {
            Cart.Add(cartItem);
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            Cart.Remove(cartItem);
        }

        public void PutCart()
        {
            int cId = new CommandeDataAccess().GetAll().Count + 1;
            Commande c = new Commande
            {
                CuisinierUsername = Cart.First().Cuisinier,
                ClientUsername = ClientUsername,
                CommandeId = new CommandeDataAccess().GetAll().Count+1,
                DateCreation = DateTime.Now,
            };
            new CommandeDataAccess().Insert(c);
            foreach (CartItem cartItem in Cart)
            {
                string rawCartPrix = cartItem.Prix.Replace(" €", "");
                if (!decimal.TryParse(rawCartPrix, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal cartPrice))
                {
                    Debug.WriteLine($"Prix invalide dans le panier : '{rawCartPrix}'");
                    continue;
                }

                var plats = new PlatDataAccess().GetAll();
                foreach (var candidate in plats)
                {
                    Debug.WriteLine($"Candidate : {candidate.CuisinierUsername} / {candidate.PlatPrix}");
                }

                Plat p = plats.FirstOrDefault(x =>
                {
                    if (!decimal.TryParse(x.PlatPrix, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal platPrice))
                        return false;
                    return x.CuisinierUsername == cartItem.Cuisinier && platPrice == cartPrice;
                });

                Debug.Assert(p != null, $"Plat non trouvé: {cartItem.Cuisinier} / {rawCartPrix}");
                if (p == null)
                    continue;

                var creation = new Creation
                {
                    CommandeId = cId,
                    PlatId = p.PlatId,
                    Quantity = cartItem.Quantity,
                };
                new CreationDataAccess().Insert(creation);
                Cart = new ObservableCollection<CartItem>();
            }
        }
    }
}