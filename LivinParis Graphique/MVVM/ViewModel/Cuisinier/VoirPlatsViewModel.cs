using System;
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

        public VoirPlatsViewModel(Personne user)
        {
            if (user != null)
            {
                Cuisinier c = new CuisinierDataAccess().GetByEmail(user.PersonneEmail);
                if (c != null)
                {
                    List<Plat> plats = new PlatDataAccess().GetAll();
                    var platsFiltres = plats
                        .Where(p => p.CuisinierUsername == c.CuisinierUsername)
                        .Select(p => new PlatToList
                        {
                            RecetteNom = new RecetteDataAccess().GetById(p.RecetteId).RecetteNom,
                            PlatPrix = $"{p.PlatPrix}€"
                        })
                        .ToList();

                    Plats = new ObservableCollection<PlatToList>(platsFiltres);
                }
            }
        }
    }

    public class PlatToList
    {
        public string RecetteNom { get; set; }
        public string PlatPrix { get; set; }
    }
}