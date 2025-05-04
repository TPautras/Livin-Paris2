using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }
        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Administrateur)";
        
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

        private GestionPersonnesViewModel _gestionPersonnesViewModel;
        
        public ICommand ShowGestionPersonnesCommand { get; }
        public ICommand LogoutCommand { get; }

        public AdminViewModel(Personne user)
        {
            CurrentUser = user;
            
            // Initialiser les view models
            _gestionPersonnesViewModel = new GestionPersonnesViewModel();

            // Définir la vue par défaut
            CurrentView = _gestionPersonnesViewModel;

            // Configurer les commandes
            ShowGestionPersonnesCommand = new RelayCommand(o => CurrentView = _gestionPersonnesViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        // Constructeur par défaut pour le designer
        public AdminViewModel()
        {
            _gestionPersonnesViewModel = new GestionPersonnesViewModel();
            CurrentView = _gestionPersonnesViewModel;

            ShowGestionPersonnesCommand = new RelayCommand(o => CurrentView = _gestionPersonnesViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        private void ExecuteLogout()
        {
            Window newWindow = new MVVM.View.LoginView{DataContext = new LoginViewModel()};
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}