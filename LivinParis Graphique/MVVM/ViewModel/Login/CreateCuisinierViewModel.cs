using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CreateCuisinierViewModel : BaseViewModel
    {
        public Personne Personne { get; set; }
        private string _username;
        private string _password;
        private CreateCuisinierView _createCuisinierView;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand CreerCommand { get; set; }
        
        public CreateCuisinierViewModel(Personne personne, CreateCuisinierView createCuisinierView)
        {
            Personne = personne;
            _createCuisinierView = createCuisinierView;
            CreerCommand = new RelayCommand(o => Creer());
        }

        private void Creer()
        {
            Cuisinier cuisinier = new Cuisinier
            {
                PersonneEmail = Personne.PersonneEmail,
                Personne = Personne,
                CuisinierPassword = _password,
                CuisinierUsername = _username,
            };
            new CuisinierDataAccess().Insert(cuisinier);
            _createCuisinierView.Close();
        }
    }
}