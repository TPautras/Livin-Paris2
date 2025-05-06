using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class RoleSelectionViewModel :BaseViewModel
    {
        private RoleSelectionView _roleSelectionView;
        private CreateClientView _createClientView;
        private CreateCuisinierView _createCuisinierView;

        public ICommand ShowCuisinierCreationCommand { get; set; }
        public ICommand ShowClientCreationCommand { get; set; }
        private Visibility _cuisinierVisibility { get; set; } = Visibility.Visible;
        private Visibility _clientVisibility { get; set; } = Visibility.Visible;

        public Visibility CuisinierVisibility
        {
            get { return _cuisinierVisibility; }
            set { value = _cuisinierVisibility; OnPropertyChanged(); }
        }
        public Visibility ClientVisibility
        {
            get { return _clientVisibility; }
            set { value = _clientVisibility; OnPropertyChanged(); }
        }
        public Personne User { get; set; }
        
        public RoleSelectionViewModel(Personne UserInput)
        {
            User = UserInput;
            
            _roleSelectionView = new RoleSelectionView();
            _createClientView = new CreateClientView();
            _createCuisinierView = new CreateCuisinierView();

            ShowClientCreationCommand = new RelayCommand(o => ExecuteViewClient());
            ShowCuisinierCreationCommand = new RelayCommand(o => ExecuteViewCuisinier());
        }

        private void ExecuteViewClient()
        {
            _createClientView = new CreateClientView();
            var vm = new CreateClientViewModel(User, _createClientView);
            _createClientView.DataContext = vm;

            Application.Current.MainWindow = _createClientView;
            ClientVisibility = Visibility.Hidden;
            _createClientView.Show();
            CheckAndCloseIfBothCreated();
        }


        private void ExecuteViewCuisinier()
        {
            _createCuisinierView = new CreateCuisinierView();
            var vm = new CreateCuisinierViewModel(User, _createCuisinierView);
            _createCuisinierView.DataContext = vm;

            Application.Current.MainWindow = _createCuisinierView;
            CuisinierVisibility = Visibility.Hidden;
            _createCuisinierView.Show();
            CheckAndCloseIfBothCreated();
        }

        private void CheckAndCloseIfBothCreated()
        {
            if (ClientVisibility == Visibility.Hidden && CuisinierVisibility == Visibility.Hidden)
            {
                _roleSelectionView.Close();
            }
        }
    }
}