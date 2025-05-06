using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ExploreViewModel : BaseViewModel
    {
        public Personne User { get; set; }
        private readonly CommandeDataAccess _commandeDataAccess;
        private readonly ExploreView _exploreView;
        private ObservableCollection<PlatToExplore> _allPlats;

        private ObservableCollection<PlatToExplore> _plats;
        public ObservableCollection<PlatToExplore> Plats
        {
            get => _plats;
            set { _plats = value; OnPropertyChanged(); }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                FilterPlats();
            }
        }

        private PlatToExplore _selectedPlat;
        public PlatToExplore SelectedPlat
        {
            get => _selectedPlat;
            set
            {
                _selectedPlat = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshPlat { get; }
        public ICommand AddPlatCommand { get; }
        public ICommand ShowPlatDetailsCommand { get; }
        public ClientViewModel ClientViewModel;

        public ExploreViewModel(Personne user, ClientViewModel clientViewModel)
        {
            ClientViewModel = clientViewModel;
            User = user;
            _commandeDataAccess = new CommandeDataAccess();
            _exploreView = new ExploreView();
            RefreshPlat = new RelayCommand(o => LoadPlats());
            AddPlatCommand = new RelayCommand(o => OpenAddCommande());
            ShowPlatDetailsCommand = new RelayCommand(o => OpenPlatDetails(o));
            LoadPlats();
        }

        private void LoadPlats()
        {
            var plats = new PlatDataAccess().GetAll();
            var result = new ObservableCollection<PlatToExplore>();

            foreach (var plat in plats)
            {
                if (plat.PlatDateDePeremption < DateTime.Today)
                {
                    result.Add(new PlatToExplore
                    {
                        Prix = plat.PlatPrix.ToString() + " \u20ac",
                        Cuisinier = plat.CuisinierUsername,
                        Recette = new RecetteDataAccess().GetById(plat.RecetteId).RecetteNom,
                        RecetteEntiere = new RecetteDataAccess().GetById(plat.RecetteId),
                    });
                }
            }

            Plats = result;
            _allPlats = result;
        }

        private void FilterPlats()
        {
            if (_allPlats == null) return;

            IEnumerable<PlatToExplore> filtered = _allPlats;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                string query = SearchQuery.ToLower();
                filtered = filtered.Where(p =>
                    (!string.IsNullOrEmpty(p.Cuisinier) && p.Cuisinier.ToLower().Contains(query)) ||
                    (!string.IsNullOrEmpty(p.Recette) && p.Recette.ToLower().Contains(query))
                );
            }

            Plats = new ObservableCollection<PlatToExplore>(filtered);
        }

        private void OpenAddCommande()
        {
            var newCommandeVM = new CommandeDetailsViewModel();
            var detailsWindow = new CommandeDetailView { DataContext = newCommandeVM };
            detailsWindow.ShowDialog();
            LoadPlats();
        }

        private void OpenPlatDetails(object parameter)
        {
            if (parameter is PlatToExplore plat)
            {
                var platDetailView = new PlatDetailView
                {
                    DataContext = new PlatDetailViewModel(plat, User, this)
                };
                platDetailView.ShowDialog();
                LoadPlats();
            }
        }

        public void AddToCart(string recetteNom, string prix, Cuisinier cuisinier)
        {
            ClientViewModel.AddCartItem(
                new CartItem
                {
                    Cuisinier = cuisinier.CuisinierUsername,
                    Prix = prix,
                    RecetteName = recetteNom
                });
        }
    }
}
