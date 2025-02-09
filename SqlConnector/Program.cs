using System;

namespace SqlConnector
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            PlatDataAccess platDao = new PlatDataAccess();
            CommandeDataAccess commandeDao= new CommandeDataAccess();

            // 🔹 Ajouter un plat
            Plat plat = new Plat
            {
                Nom = "Pizza Margherita",
                Description = "Pizza classique avec mozzarella et basilic",
                Categorie = "plat",
                Personnes = 2,
                Prix = 12.99m,
                Nationalite = "italienne",
                RegimeAlimentaire = "végétarien",
                Ingredients = "farine, tomate, mozzarella, basilic",
                CuisinierId = 1
            };
            platDao.Ajouter(plat);
            Console.WriteLine("Plat ajouté !");

            // 🔹 Ajouter une commande
            Commande commande = new Commande
            {
                ClientId = 1,
                DateCommande = DateTime.Now,
                PrixTotal = 25.00m,
                Statut = "en attente"
            };
            commandeDao.Ajouter(commande);
            Console.WriteLine("Commande ajoutée !");

            // 🔹 Lire toutes les commandes
            var commandes = commandeDao.ObtenirToutes();
            foreach (var c in commandes)
            {
                Console.WriteLine($"Commande {c.Id} - Client {c.ClientId} - {c.PrixTotal}€ - {c.Statut}");
            }
        }
    }
}