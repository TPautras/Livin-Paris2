using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }
        public ObservableCollection<CartItem> Cart { get; } = new ObservableCollection<CartItem>();

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

        public ClientViewModel(Personne user)
        {
            CurrentUser = user;
            _exploreViewModel = new ExploreViewModel(user, this);
            CurrentView = _exploreViewModel;

            ShowCartViewCommand = new RelayCommand(o =>
            {
                CurrentView = new CartViewModel(Cart);
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
    }
}