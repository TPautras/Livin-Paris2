using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CookViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }
        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Cuisinier)";
        
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

        private VoirPlatsViewModel _voirPlatsViewModel;
        private AjouterPlatViewModel _ajouterPlatViewModel;
        private CreerRecetteViewModel _creerRecetteViewModel;
        private VoirCommandesViewModel _voirCommandesViewModel;
        
        public ICommand ShowVoirPlatsCommand { get; }
        public ICommand ShowAjouterPlatCommand { get; }
        public ICommand ShowCreerRecetteCommand { get; }
        public ICommand ShowVoirCommandesCommand { get; }
        public ICommand LogoutCommand { get; }

        public CookViewModel(Personne user)
        {
            CurrentUser = user;
            _voirPlatsViewModel = new VoirPlatsViewModel(user);
            _ajouterPlatViewModel = new AjouterPlatViewModel(user, _voirPlatsViewModel);
            _creerRecetteViewModel = new CreerRecetteViewModel();
            _voirCommandesViewModel = new VoirCommandesViewModel(user);

            CurrentView = _voirPlatsViewModel;

            ShowVoirPlatsCommand = new RelayCommand(o => CurrentView = _voirPlatsViewModel);
            ShowAjouterPlatCommand = new RelayCommand(o => CurrentView = _ajouterPlatViewModel);
            ShowCreerRecetteCommand = new RelayCommand(o => CurrentView = _creerRecetteViewModel);
            ShowVoirCommandesCommand = new RelayCommand(o => CurrentView = _voirCommandesViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        public CookViewModel()
        {
            _voirPlatsViewModel = new VoirPlatsViewModel(new Personne());
            _ajouterPlatViewModel = new AjouterPlatViewModel(new Personne(), _voirPlatsViewModel);
            _creerRecetteViewModel = new CreerRecetteViewModel();
            CurrentView = _voirPlatsViewModel;

            ShowVoirPlatsCommand = new RelayCommand(o => CurrentView = _voirPlatsViewModel);
            ShowAjouterPlatCommand = new RelayCommand(o => CurrentView = _ajouterPlatViewModel);
            ShowCreerRecetteCommand = new RelayCommand(o => CurrentView = _creerRecetteViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        private void ExecuteLogout()
        {
            Window newWindow = new LoginView{DataContext = new LoginViewModel()};
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
        }

    }
}
