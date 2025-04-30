using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CookViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Cuisinier)";

        // Exemple: On pourrait prévoir une liste des commandes en cours, des recettes, etc.
        // public ObservableCollection<Commande> CommandesEnCours { get; }

        public CookViewModel(Personne user)
        {
            CurrentUser = user;
            // Initialiser d'éventuelles collections ou données spécifiques
            // CommandesEnCours = new ObservableCollection<Commande>(...);
        }
    }
}