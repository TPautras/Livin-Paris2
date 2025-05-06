using LivinParis_Graphique.Core;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CreateCuisinierViewModel : BaseViewModel
    {
        public Personne Personne { get; set; }
        private string _username;
        private string _password;

        public CreateCuisinierViewModel(Personne personne)
        {
            
        }
    }
}