using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SqlConnector;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.Model
{
    public class CommandesToListCuisinier
    {
        public string Client { get; set; }
        public ObservableCollection<string> Plats { get; set; }
        public string PrixTotal { get; set; }

        public CommandesToListCuisinier(Commande commande)
        {
            this.Client = commande.ClientUsername;

            List<Creation> contients = new CreationDataAccess().GetAll()
                .Where(x => x.CommandeId == commande.CommandeId).ToList();

            List<Plat> plats = new PlatDataAccess().GetAll();
            List<Recette> recettes = new RecetteDataAccess().GetAll();

            var platsCommande = plats
                .Where(p => contients.Any(c => c.PlatId == p.PlatId))
                .ToList();

            this.Plats = new ObservableCollection<string>();

            decimal total = 0;

            foreach (var plat in platsCommande)
            {
                Console.Write(plat.PlatId);
                var recette = recettes.FirstOrDefault(r => r.RecetteId == plat.RecetteId);
                if (recette != null)
                {
                    this.Plats.Add(recette.RecetteNom);
                }
                var prixString = plat.PlatPrix?.Replace("€", "").Trim();

                if (decimal.TryParse(prixString, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal prix))
                {
                    total += prix;
                }

            }
            Console.WriteLine($"Total: {total}");
            this.PrixTotal = total.ToString("0.00") + " €";
        }
    }
}
