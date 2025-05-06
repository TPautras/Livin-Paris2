using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CompanyViewModel : BaseViewModel
    {
        public Personne CurrentUser { get; }

        public string WelcomeMessage => $"Bienvenue, {CurrentUser.PersonneNom} (Entreprise)";


        public CompanyViewModel(Personne user)
        {
            CurrentUser = user;
        }
    }
}