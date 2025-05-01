using System.Collections.ObjectModel;
using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class VoirPlatsViewModel : BaseViewModel
    {
        // Collection de plats (données factices)
        public ObservableCollection<string> Plats { get; set; }

        public VoirPlatsViewModel()
        {
            // Initialiser avec quelques plats factices
            Plats = new ObservableCollection<string>
            {
                "Spaghetti Bolognese",
                "Salade César",
                "Tarte aux pommes"
            };
        }
    }
}