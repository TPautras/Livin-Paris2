using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        // On peut intégrer le MainViewModel pour la navigation du client
        public MainViewModel MainVM { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom}!";

        public ClientViewModel(Personne user)
        {
            CurrentUser = user;
            MainVM = new MainViewModel();
            // Si besoin, on pourrait personnaliser MainVM en fonction du client, 
            // par ex. filtrer les données à afficher dans HomeViewModel, etc.
        }
    }
}