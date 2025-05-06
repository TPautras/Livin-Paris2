using System.Windows.Input;
using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public HomeViewModel HomeVM { get; }
        public DiscoveryViewModel DiscoveryVM { get; }

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView 
        { 
            get => _currentView; 
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeViewCommand { get; }
        public ICommand DiscoveryViewCommand { get; }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(_ => CurrentView = HomeVM);
            DiscoveryViewCommand = new RelayCommand(_ => CurrentView = DiscoveryVM);
        }
    }
}