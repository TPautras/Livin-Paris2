using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class VoirCommandesViewModel : BaseViewModel
    {
        public ObservableCollection<CommandesToListCuisinier> Commandes { get; set; }

        public void Refresh()
        {
            Cuisinier c = new CuisinierDataAccess().GetByEmail(CurrentUser.PersonneEmail);
            List<Commande> commandeList = new CommandeDataAccess().GetAll();
            Commandes = new ObservableCollection<CommandesToListCuisinier>();
            if (c != null)
            {
                commandeList = commandeList
                    .Where(p => p.CuisinierUsername == c.CuisinierUsername)
                    .Select(x =>
                    {
                        Commandes.Add(new CommandesToListCuisinier(x));
                        return x;
                    }).ToList();
            }
        }

        private Personne CurrentUser;

        public VoirCommandesViewModel(Personne user)
        {
            CurrentUser = user;
            Refresh();
        }
    }
}