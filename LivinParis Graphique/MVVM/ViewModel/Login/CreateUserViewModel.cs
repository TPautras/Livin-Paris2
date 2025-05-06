using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CreateUserViewModel : BaseViewModel
    {
        private CreateUserView _createUserView;
        private string _email;
        private string _nom;
        private string _prenom;
        private string _ville;
        private string _postal;
        private string _rue;
        private string _nRue;
        private string _tel;
        private string _station;
        private string _passwordClient;
        private string _usernameClient;
        private string _passwordCuisinier;
        private string _usernameCuisinier;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; OnPropertyChanged(); }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; OnPropertyChanged(); }
        }

        public string Ville
        {
            get { return _ville; }
            set { _ville = value; OnPropertyChanged(); }
        }

        public string Postal
        {
            get { return _postal; }
            set { _postal = value; OnPropertyChanged(); }
        }

        public string Rue
        {
            get { return _rue; }
            set { _rue = value; OnPropertyChanged(); }
        }

        public string NRue
        {
            get { return _nRue; }
            set { _nRue = value; OnPropertyChanged(); }
        }

        public string Tel
        {
            get { return _tel; }
            set { _tel = value; OnPropertyChanged(); }
        }

        public string Station
        {
            get { return _station; }
            set { _station = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> AvailableStations {get;set;}
        
        public ICommand CreateUserCommand {set; get; }
        
        public CreateUserViewModel()
        {
            CreateUserCommand = new RelayCommand(o => CreateUser());
            AvailableStations = new ObservableCollection<string>{
                "Châtelet", "Gare de Lyon", "République", "Nation", "Bastille",
                "Montparnasse - Bienvenüe", "Saint-Lazare", "Charles de Gaulle - Étoile", "Opéra", "Concorde",
                "Place d'Italie", "Belleville", "Strasbourg - Saint-Denis", "Gare du Nord", "La Motte-Picquet - Grenelle",
                "Franklin D. Roosevelt", "Trocadéro", "Invalides", "Hôtel de Ville", "Arts et Métiers"
            };
            _createUserView = new CreateUserView();
        }

        public void CreateUser()
        {
            Personne personne = new Personne
            {
                PersonneEmail = this.Email,
                PersonneNom = this.Nom,
                PersonnePrenom = this.Prenom,
                PersonneVille = this.Ville,
                PersonneCodePostale = Convert.ToInt32(this.Postal),
                PersonneIsAdmin = false,
                PersonneTelephone = Tel,
                PersonneNomDeLaRue = Rue,
                PersonneNumeroDeLaRue = Convert.ToInt32(NRue),
                PersonneStationDeMetroLaPlusProche = Station
            };
            new PersonneService(new PersonneDataAccess()).Insert(personne);
            var newWindow = new RoleSelectionView { DataContext = new RoleSelectionViewModel(personne) };
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}