using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class VoirPlatsViewModel : BaseViewModel
    {
        public ObservableCollection<PlatToList> Plats { get; set; }

        public VoirPlatsViewModel()
        {
            List<Plat> plats = new PlatDataAccess().GetAll();
            var platsFiltres = plats
                .Where(p => p.CuisinierUsername == "cuisinier1")
                .Select(p => new PlatToList
                {
                    RecetteNom = new RecetteDataAccess().GetById(p.RecetteId).RecetteNom,
                    PlatPrix = $"{p.PlatPrix}€"
                })
                .ToList();

            Plats = new ObservableCollection<PlatToList>(platsFiltres);
        }
    }

    public class PlatToList
    {
        public string RecetteNom { get; set; }
        public string PlatPrix { get; set; }
    }
}