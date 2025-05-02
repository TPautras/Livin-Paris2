using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Admin)";

        // Exemples de propriétés pour l'admin:
        // public ObservableCollection<Personne> AllUsers { get; }
        // public ObservableCollection<Commande> AllCommandes { get; }
        // public ICommand LoadStatsCommand { get; }

        public AdminViewModel(Personne user)
        {
            CurrentUser = user;
            // On pourrait ici charger des données globales, par ex:
            // AllUsers = new ObservableCollection<Personne>(new PersonneDataAccess().GetAll());
            // AllCommandes = new ObservableCollection<Commande>(new CommandeDataAccess().GetAll());
        }
    }
}