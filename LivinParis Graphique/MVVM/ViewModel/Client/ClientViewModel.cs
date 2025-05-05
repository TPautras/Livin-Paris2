using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

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
        private ExploreViewModel _exploreViewModel;
        
        public ICommand LogoutCommand { get; }
        public ICommand ShowExploreViewCommand { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom}!";

        public ClientViewModel(Personne user)
        {
            _exploreViewModel = new ExploreViewModel();
            CurrentUser = user;
            
            CurrentView = _exploreViewModel;

            ShowExploreViewCommand = new RelayCommand(o => CurrentView = _exploreViewModel);
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