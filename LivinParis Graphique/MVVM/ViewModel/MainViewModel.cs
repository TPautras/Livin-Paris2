using System.Windows.Input;
using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        // ViewModels des vues navigables à l'intérieur de la fenêtre principale (pour un client)
        public HomeViewModel HomeVM { get; }
        public DiscoveryViewModel DiscoveryVM { get; }
        // (On pourrait ajouter d'autres, par ex. FeaturedViewModel)

        // Propriété de vue courante liée au ContentControl&#8203;:contentReference[oaicite:5]{index=5}
        private BaseViewModel _currentView;
        public BaseViewModel CurrentView 
        { 
            get => _currentView; 
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Commandes pour changer de vue via le menu
        public ICommand HomeViewCommand { get; }
        public ICommand DiscoveryViewCommand { get; }
        // public ICommand FeaturedViewCommand { get; } // si nécessaire

        public MainViewModel()
        {
            // Instancier les ViewModels des pages
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            // ...

            // Définir la vue par défaut (page d'accueil client)
            CurrentView = HomeVM;

            // Initialiser les commandes de navigation (les RadioButtons du menu sont liés à ces commandes&#8203;:contentReference[oaicite:6]{index=6}&#8203;:contentReference[oaicite:7]{index=7})
            HomeViewCommand = new RelayCommand(_ => CurrentView = HomeVM);
            DiscoveryViewCommand = new RelayCommand(_ => CurrentView = DiscoveryVM);
            // FeaturedViewCommand = new RelayCommand(_ => CurrentView = ...);
        }
    }
}