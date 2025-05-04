using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class GestionCommandesViewModel : BaseViewModel
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

        public ICommand RefreshCommand { get; }
        public ICommand AddCommandeCommand { get; }
        public ICommand ShowCommandeDetailsCommand { get; }

        public GestionCommandesViewModel() {
            _commandeDataAccess = new CommandeDataAccess();
            RefreshCommand = new RelayCommand(o => LoadCommandes());
            AddCommandeCommand = new RelayCommand(o => OpenAddCommande());
            ShowCommandeDetailsCommand = new RelayCommand(o => OpenCommandeDetails(o));

            LoadCommandes();
        }

        private void LoadCommandes() {
            var all = _commandeDataAccess.GetAll();  
            _allCommandes = new ObservableCollection<Commande>(all);
            Commandes = new ObservableCollection<Commande>(_allCommandes);
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

            LoadCommandes();
        }

        private void OpenCommandeDetails(object parameter) {
            if (parameter is Commande cmd) {
                var detailsVM = new CommandeDetailsViewModel(cmd);
                var detailsWindow = new CommandeDetailView { DataContext = detailsVM };
                detailsWindow.ShowDialog();
                LoadCommandes();
            }
        }
    }
}
