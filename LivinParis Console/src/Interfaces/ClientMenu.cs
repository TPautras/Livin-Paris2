using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using LivinParis_Console.Assets;
using LivinParis_Console.Modules;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console
{
    public class ClientMenu
    {
        public Client client { get; set; }= null;

        public ClientMenu(Client client)
        {
            this.client = client;
        }

        public void ClientMenuMain()
        {
            PlatDataAccess platDataAccess = new PlatDataAccess();
            RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
            RecetteService recetteService = new RecetteService(recetteDataAccess);
            bool quit = false;
            string[] options = { "Voir la liste des plats" ,"Acheter un plat","Voir la liste des recettes" , "Créer une recette", "Mes commandes", "Déconnexion"};
            while (!quit)
            {
                int mainMenuChoice = Affichages.MenuSelect(ASCII.Client, options);
                switch (mainMenuChoice)
                {
                    case 0:
                        foreach (var plat in new PlatService(platDataAccess).GetAll())
                        {
                            Recette recette = recetteService.GetById(plat.RecetteId);
                            Console.WriteLine(recette.RecetteNom);
                            Console.WriteLine(plat.PlatPrix);
                        }
                        Console.WriteLine("Pour sortir, appuyez sur n'importe quelle touche");
                        Console.ReadKey();
                        break;
                    case 1:
                        new ModuleCommande().ModuleCommandeAdding(client);
                        break;
                    case 2:
                        foreach (var recette in new RecetteService(recetteDataAccess).GetAll())
                        {
                            Console.WriteLine(recette.RecetteNom);
                            Console.WriteLine(recette.RecetteTypeDePlat);
                        }
                        Console.WriteLine("Pour sortir, appuyez sur n'importe quelle touche");
                        Console.ReadKey();
                        break;
                    case 3:
                        recetteDataAccess.Insert(RecetteCréation(recetteDataAccess.GetAll().Count));
                        break;
                    case 4:
                        CommandeDataAccess commandeDataAccess = new CommandeDataAccess();
                        CommandeService commandeService = new CommandeService(commandeDataAccess);
                        CreationDataAccess creationDataAccess = new CreationDataAccess();
                        LivreDataAccess livreDataAccess = new LivreDataAccess();
                        LivraisonDataAccess livraisonDataAccess = new LivraisonDataAccess();
                        List<Commande> commandes = commandeService.GetAll();
                        foreach (var commande in commandes.ToList())
                        {
                            if (commande.ClientUsername != client.ClientUsername)
                            {
                                commandes.Remove(commande);
                            }
                        }

                        foreach (var commande in commandes)
                        {
                            Console.WriteLine("Le nombre de commandes :" + commandes.Count);
                            Plat plat = platDataAccess.GetById(creationDataAccess.GetById(commande.CommandeId).PlatId);
                            Livre livre = livreDataAccess.GetById(plat.PlatId);
                            Livraison livraison = livraisonDataAccess.GetById(livre.LivraisonId);
                            Console.WriteLine(recetteDataAccess.GetById(plat.RecetteId).RecetteNom);
                            Console.WriteLine(commande.CuisinierUsername);
                            if (livraison.LivraisonDate == null)
                            {
                                Console.WriteLine("Votre commande est en chemin !");
                            }
                            else
                            {
                                Console.WriteLine("Votre commande est deja livree !");
                            }
                        }
                        Console.WriteLine("Pour sortir, appuyez sur n'importe quelle touche");
                        Console.ReadKey();
                        break;
                        
                    default:
                        quit = true;
                        break;
                }
            }
        }

        public Commande CommandeMain()
        {
            Commande command = new Commande();
            
            return command;
        }
        public Recette RecetteCréation(int recetteId)
        {
            Recette recette = new Recette();
            recette.RecetteId = recetteId+1;
            Console.WriteLine("Quel nom voulez vous donner à votre recette ?");
            recette.RecetteNom = Console.ReadLine();
            Console.WriteLine("Quelle est l'origine de votre recette ?");
            recette.RecetteOrigine = Console.ReadLine();
            string[] options = {"entrée", "plat", "dessert"};
            int typePlat = Affichages.MenuSelect("Quel est le type de votre plat  ?",options);
            recette.RecetteTypeDePlat = options[typePlat];
            Console.WriteLine("Quels sont les apports nutritifs de votre recette ?");
            recette.RecetteApportNutritifs = Console.ReadLine();
            Console.WriteLine("Quel est le régime alimentaire que suit votre recette ?");
            recette.RecetteRegimeAlimentaire = Console.ReadLine();
            return recette;
        }
    }
}