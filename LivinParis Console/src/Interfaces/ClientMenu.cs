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

        public static void ClientMenuMain()
        {
            PlatDataAccess platDataAccess = new PlatDataAccess();
            RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
            RecetteService recetteService = new RecetteService(recetteDataAccess);
            bool quit = false;
            string[] options = { "Voir la liste des plats" ,"Acheter un plat", "Déconnexion"};
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
                    default:
                        quit = true;
                        break;
                }
            }
        }
    }
}