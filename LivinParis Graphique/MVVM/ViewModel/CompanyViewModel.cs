using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CompanyViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Entreprise)";

        // Exemple: liste des commandes passées par l'entreprise, etc.
        // public ObservableCollection<Commande> Commandes { get; }

        public CompanyViewModel(Personne user)
        {
            CurrentUser = user;
            // Initialisation des données spécifiques à l'entreprise si nécessaire.
        }
    }
}