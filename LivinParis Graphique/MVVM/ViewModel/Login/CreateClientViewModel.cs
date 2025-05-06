using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CreateClientViewModel : BaseViewModel
    {
        public Personne Personne { get; set; }
        private string _username;
        private string _password;
        private CreateClientView _CreateClientView;
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
        
        public CreateClientViewModel(Personne personne, CreateClientView createClientView)
        {
            Personne = personne;
            _CreateClientView = createClientView;
            CreerCommand = new RelayCommand(o => Creer());
        }

        private void Creer()
        {
            Client client = new Client
            {
                PersonneEmail = Personne.PersonneEmail,
                Personne = Personne,
                ClientPassword = _password,
                ClientUsername = _username,
            };
            new ClientDataAccess().Insert(client);
            _CreateClientView.Close();
        }
    }
}