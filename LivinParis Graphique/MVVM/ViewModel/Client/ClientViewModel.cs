using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        public MainViewModel MainVM { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom}!";

        public ClientViewModel(Personne user)
        {
            CurrentUser = user;
            MainVM = new MainViewModel();
        }
    }
}