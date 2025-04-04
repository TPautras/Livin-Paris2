using System;
using System.Collections.Generic;
using Graphs;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Console.Modules
{
    public class ModuleAutre
    {
        public void ModuleAutreMain()
        {
            string[] options =
            {
                "Recommendation du chef",
                "Commande Surprise",
                "Remixer une commande",
                "Annuler une commande avec style",
                "Générer un rapport des ventes",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Autre";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleAutre1();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleAutre2();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleAutre3();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleAutre4();
                        Console.ReadKey();
                        break;
                    case 4:
                        ModuleAutre5();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        private void ModuleAutre1()
        {
            string[] regimeOptions = { "Omnivore", "Végétarien", "Pescétarien" };
            int regimeChoice = Affichages.MenuSelect("Quel régime alimentaire préférez-vous ?", regimeOptions);
            string regime = regimeOptions[regimeChoice];
            string[] origineOptions = { "Française", "Italienne", "Mexicaine", "Japonaise", "Chinoise", "Grecque", "Moyen-Orient", "Espagnole" };
            int origineChoice = Affichages.MenuSelect("Quelle origine de cuisine préférez-vous ?", origineOptions);
            string origine = origineOptions[origineChoice];
            List<Recette> recettes = new RecetteDataAccess().GetAll();
            List<Recette> suggestions = recettes.FindAll(r => r.RecetteRegimeAlimentaire.Equals(regime, StringComparison.OrdinalIgnoreCase)
                                                            && r.RecetteOrigine.Equals(origine, StringComparison.OrdinalIgnoreCase));
            if (suggestions.Count > 0)
            {
                Console.WriteLine("Les plats suggérés pour vous sont :");
                foreach (Recette rec in suggestions)
                {
                    Console.WriteLine($"{rec.RecetteNom} - {rec.RecetteTypeDePlat}");
                }
            }
            else
            {
                Console.WriteLine("Aucun plat ne correspond à vos critères.");
            }
        }

        private void ModuleAutre2()
        {
            var random = new Random();
            var clients = new ClientDataAccess().GetAll();
            var randomClient = clients[random.Next(clients.Count)];
            var plats = new PlatDataAccess().GetAll();
            var randomPlat = plats[random.Next(plats.Count)];
            int newId = new CommandeDataAccess().GetAll().Count + 1;
            Commande cmd = new Commande { CommandeId = newId, ClientUsername = randomClient.ClientUsername, CuisinierUsername = randomPlat.CuisinierUsername };
            new CommandeDataAccess().Insert(cmd);
            Creation creation = new Creation { CommandeId = cmd.CommandeId, PlatId = randomPlat.PlatId };
            new CreationDataAccess().Insert(creation);
            Console.WriteLine($"Commande surprise créée pour {randomClient.ClientUsername} avec le plat {new RecetteDataAccess().GetById(randomPlat.RecetteId).RecetteNom}");
        }

        private void ModuleAutre3()
        {
            Console.Write("Entrez l'ID de la commande à remixer : ");
            int id = int.Parse(Console.ReadLine());
            Commande cmd = new CommandeDataAccess().GetById(id);
            if (cmd != null)
            {
                var plats = new PlatDataAccess().GetAll();
                var random = new Random();
                Plat bonusPlat = plats[random.Next(plats.Count)];
                Creation creation = new Creation { CommandeId = cmd.CommandeId, PlatId = bonusPlat.PlatId };
                new CreationDataAccess().Insert(creation);
                Console.WriteLine($"Bonus ajouté : {new RecetteDataAccess().GetById(bonusPlat.RecetteId).RecetteNom} offert !");
            }
            else
            {
                Console.WriteLine("Commande introuvable.");
            }
        }

        private void ModuleAutre4()
        {
            Console.Write("Entrez l'ID de la commande à annuler : ");
            int id = int.Parse(Console.ReadLine());
            new CommandeDataAccess().Delete(id);
            Console.WriteLine("Commande annulée avec succès. Nous espérons vous revoir bientôt pour une nouvelle aventure culinaire !");
        }

        private void ModuleAutre5()
        {
            List<Commande> commandes = new CommandeDataAccess().GetAll();
            double total = 0;
            int count = 0;
            List<Creation> creations = new CreationDataAccess().GetAll();
            foreach (Creation creation in creations)
            {
                Plat plat = new PlatDataAccess().GetById(creation.PlatId);
                total += double.Parse(plat.PlatPrix, System.Globalization.CultureInfo.InvariantCulture);
                count++;
            }
            double moyenne = count > 0 ? total / count : 0;
            Console.WriteLine("Total des ventes : " + total + " - Nombre de plats : " + count + " - Vente moyenne : " + moyenne);
        }
    }
}
