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
        private readonly CommandeDataAccess _commandeDataAccess;
        private ObservableCollection<Commande> _allCommandes;

        private ObservableCollection<Commande> _commandes;
        public ObservableCollection<Commande> Commandes {
            get => _commandes;
            set { _commandes = value; OnPropertyChanged(); }
        }

        private string _searchQuery;
        public string SearchQuery {
            get => _searchQuery;
            set {
                _searchQuery = value;
                OnPropertyChanged();
                FilterCommandes();
            }
        }

        private DateTime? _filterDate;
        public DateTime? FilterDate {
            get => _filterDate;
            set {
                _filterDate = value;
                OnPropertyChanged();
                FilterCommandes();
            }
        }

        private Commande _selectedCommande;
        public Commande SelectedCommande {
            get => _selectedCommande;
            set {
                _selectedCommande = value;
                OnPropertyChanged();
            }
        }
        
        private Plat _selectedPlat;
        public Plat SelectedPlat {
            get => _selectedPlat;
            set {
                _selectedPlat = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshPlat { get; }
        public ICommand AddPlatCommand { get; }
        public ICommand ShowPlatDetailsCommand { get; }

        private void LoadPlats() {
            List<Plat> plats = new PlatDataAccess().GetAll();
            foreach (Plat plat in plats)
            {
                if (plat.PlatDateDePeremption < DateTime.Today)
                {
                    Console.WriteLine(plat.PlatDateDePeremption);
                    Plats.Add(new PlatToExplore
                    {
                        Prix = plat.PlatPrix.ToString() + " \u20ac",
                        Cuisinier = plat.CuisinierUsername,
                        Recette = new RecetteDataAccess().GetById(plat.RecetteId).RecetteNom,
                    });
                }
            }
        }

        private void FilterCommandes() {
            if (_allCommandes == null) return;

            IEnumerable<Commande> filtered = _allCommandes;

            if (!string.IsNullOrEmpty(SearchQuery)) {
                string query = SearchQuery.ToLower();
                filtered = filtered.Where(c =>
                    (!string.IsNullOrEmpty(c.ClientUsername) && c.ClientUsername.ToLower().Contains(query)) ||
                    (!string.IsNullOrEmpty(c.CuisinierUsername) && c.CuisinierUsername.ToLower().Contains(query))
                );
            }

            if (FilterDate.HasValue) {
                DateTime dateSel = FilterDate.Value.Date;
                filtered = filtered.Where(c => c.DateCreation.Date == dateSel);
            }

            Commandes = new ObservableCollection<Commande>(filtered);
        }

        private void OpenAddCommande() {
            var newCommandeVM = new CommandeDetailsViewModel(); 
            var detailsWindow = new CommandeDetailView { DataContext = newCommandeVM };
            detailsWindow.ShowDialog();

            LoadPlats();
        }

        private void OpenPlatDetails(object parameter) {
            if (parameter is Commande cmd) {
                var detailsVM = new CommandeDetailsViewModel(cmd);
                var detailsWindow = new CommandeDetailView { DataContext = detailsVM };
                detailsWindow.ShowDialog();
                LoadPlats();
            }
        }
        public ObservableCollection<PlatToExplore> Plats { get; set; } = new ObservableCollection<PlatToExplore>();
        
        private ExploreView _exploreView;

        public ExploreViewModel()
        {
            _commandeDataAccess = new CommandeDataAccess();
            RefreshPlat = new RelayCommand(o => LoadPlats());
            AddPlatCommand = new RelayCommand(o => OpenAddCommande());
            ShowPlatDetailsCommand = new RelayCommand(OpenPlatDetails);

            LoadPlats();
            _exploreView = new ExploreView();
        }
    }
}