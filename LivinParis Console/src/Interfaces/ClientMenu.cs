using System;
using Graphs;
using LivinParis_Console.Assets;
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
            string[] options = { "Voir la liste des plats" ,"Acheter un plat", "Créer une recette", "Déconnexion"};
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
                        break;
                    case 1:
                        break;
                    case 2:
                        recetteDataAccess.Insert(RecetteCréation(recetteDataAccess.GetAll().Count));
                        break;
                    default:
                        quit = true;
                        break;
                }
            }
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