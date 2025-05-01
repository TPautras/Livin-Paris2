using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CookViewModel : BaseViewModel
    {
        // Informations de l'utilisateur courant
        public Personne CurrentUser { get; }
        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Cuisinier)";

        // Vue actuelle affichée dans la zone de contenu
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

        // ViewModels enfants pour chaque sous-menu
        private VoirPlatsViewModel _voirPlatsViewModel;
        private AjouterPlatViewModel _ajouterPlatViewModel;
        private CreerRecetteViewModel _creerRecetteViewModel;

        // Commandes pour la navigation et la déconnexion
        public ICommand ShowVoirPlatsCommand { get; }
        public ICommand ShowAjouterPlatCommand { get; }
        public ICommand ShowCreerRecetteCommand { get; }
        public ICommand LogoutCommand { get; }

        // Constructeur avec paramètre utilisateur (cuisinier connecté)
        public CookViewModel(Personne user)
        {
            CurrentUser = user;
            // Initialiser les ViewModels enfants
            _voirPlatsViewModel = new VoirPlatsViewModel();
            _ajouterPlatViewModel = new AjouterPlatViewModel();
            _creerRecetteViewModel = new CreerRecetteViewModel();

            // Vue par défaut affichée
            CurrentView = _voirPlatsViewModel;

            // Initialiser les commandes de navigation
            ShowVoirPlatsCommand = new RelayCommand(o => CurrentView = _voirPlatsViewModel);
            ShowAjouterPlatCommand = new RelayCommand(o => CurrentView = _ajouterPlatViewModel);
            ShowCreerRecetteCommand = new RelayCommand(o => CurrentView = _creerRecetteViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        // Constructeur sans paramètre (pour design-time ou tests)
        public CookViewModel()
        {
            // Initialiser les ViewModels enfants
            _voirPlatsViewModel = new VoirPlatsViewModel();
            _ajouterPlatViewModel = new AjouterPlatViewModel();
            _creerRecetteViewModel = new CreerRecetteViewModel();
            CurrentView = _voirPlatsViewModel;

            ShowVoirPlatsCommand = new RelayCommand(o => CurrentView = _voirPlatsViewModel);
            ShowAjouterPlatCommand = new RelayCommand(o => CurrentView = _ajouterPlatViewModel);
            ShowCreerRecetteCommand = new RelayCommand(o => CurrentView = _creerRecetteViewModel);
            LogoutCommand = new RelayCommand(o => ExecuteLogout());
        }

        private void ExecuteLogout()
        {
            // Ferme la fenêtre du cuisinier (déconnexion)
            Application.Current.MainWindow?.Close();
        }
    }
}
