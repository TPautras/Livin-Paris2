using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public HomeViewModel HomeVm { get; set; }
        public DiscoveryViewModel DiscoveryVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            DiscoveryVm = new DiscoveryViewModel();
            
            CurrentView = HomeVm;
            
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });
            
            DiscoveryViewCommand= new RelayCommand(o =>
            {
                CurrentView = DiscoveryVm;
            });
        }
    }
}