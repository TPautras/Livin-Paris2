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
        private GestionCommandesViewModel _gestionCommandesViewModel;
        private StatistiquesViewModel _statistiquesViewModel;
        private AutreViewModel _autreViewModel;
        public ICommand ShowGestionCommandesCommand { get; }
        public ICommand ShowGestionPersonnesCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ShowStatistiquesCommand { get; }
        public ICommand ShowAutreCommand { get; }



        public AdminViewModel(Personne user)
        {
            CurrentUser = user;

            _autreViewModel = new AutreViewModel();
            _gestionPersonnesViewModel = new GestionPersonnesViewModel();
            _gestionCommandesViewModel = new GestionCommandesViewModel();
            _statistiquesViewModel = new StatistiquesViewModel();

            CurrentView = _gestionPersonnesViewModel;

            ShowAutreCommand = new RelayCommand(o => CurrentView = _autreViewModel);
            ShowGestionCommandesCommand = new RelayCommand(o => CurrentView = _gestionCommandesViewModel);
            ShowGestionPersonnesCommand = new RelayCommand(o => CurrentView = _gestionPersonnesViewModel);
            ShowStatistiquesCommand = new RelayCommand(o => CurrentView = _statistiquesViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }


        public AdminViewModel()
        {
            _gestionPersonnesViewModel = new GestionPersonnesViewModel();
            _gestionCommandesViewModel = new GestionCommandesViewModel();
            CurrentView = _gestionPersonnesViewModel;
            
            ShowGestionCommandesCommand = new RelayCommand(o => CurrentView = _gestionCommandesViewModel);
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