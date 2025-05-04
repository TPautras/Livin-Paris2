using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DBConnectionLibrary.DataAccess;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class StatistiquesViewModel : BaseViewModel
    {
        private readonly StatsDataAccess _statsDataAccess;

        public ObservableCollection<string> LivraisonParCuisinier { get; set; }
        public double MoyennePrixCommandes { get; set; }
        public double MoyenneDepensesClients { get; set; }

        public ObservableCollection<string> CommandesPeriode { get; set; }
        public ObservableCollection<string> CommandesClient { get; set; }

        public DateTime DateDebut { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DateFin { get; set; } = DateTime.Now;

        public string ClientUsername { get; set; }
        public string OriginePlat { get; set; }

        public ICommand LoadCommandesPeriodeCommand { get; }
        public ICommand LoadCommandesClientCommand { get; }

        public StatistiquesViewModel()
        {
            _statsDataAccess = new StatsDataAccess();
            LoadCommandesPeriodeCommand = new RelayCommand(_ => LoadCommandesPeriode());
            LoadCommandesClientCommand = new RelayCommand(_ => LoadCommandesClient());

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            LivraisonParCuisinier = new ObservableCollection<string>(
                _statsDataAccess.GetLivraisonCountByCuisinier()
                .Select(x => $"{x.Key} : {x.Value} livraisons"));

            MoyennePrixCommandes = _statsDataAccess.GetAverageCommandePrice();
            MoyenneDepensesClients = _statsDataAccess.GetAverageClientSpending();
        }

        private void LoadCommandesPeriode()
        {
            CommandesPeriode = new ObservableCollection<string>(
                _statsDataAccess.GetCommandesBetweenDates(DateDebut, DateFin)
                .Select(id => $"Commande {id}"));
            OnPropertyChanged(nameof(CommandesPeriode));
        }

        private void LoadCommandesClient()
        {
            CommandesClient = new ObservableCollection<string>(
                _statsDataAccess.GetCommandesByClientAndFilters(ClientUsername, OriginePlat, DateDebut, DateFin)
                .Select(id => $"Commande {id}"));
            OnPropertyChanged(nameof(CommandesClient));
        }
    }
}
