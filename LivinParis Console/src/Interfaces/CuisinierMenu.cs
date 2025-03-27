using System;
using Graphs;
using LivinParis_Console.Assets;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console
{
    public class CuisinierMenu
    {
        public Cuisinier Cuisinier { get; set; }= null;

        public CuisinierMenu(Cuisinier cuisinier)
        {
            this.Cuisinier = cuisinier;
        }

        public void CuisinierMenuMain()
        {
            PlatDataAccess platDataAccess = new PlatDataAccess();
            RecetteDataAccess recetteDataAccess = new RecetteDataAccess();
            RecetteService recetteService = new RecetteService(recetteDataAccess);
            bool quit = false;
            string[] options = { "Vos plats" ,"Ajouter un plat", "Déconnexion"};
            while (!quit)
            {
                int mainMenuChoice = Affichages.MenuSelect(ASCII.Cuisinier, options);
                switch (mainMenuChoice)
                {
                    case 0:
                        foreach (var plat in new PlatService(platDataAccess).GetAll())
                        {
                            if (plat.CuisinierUsername == Cuisinier.CuisinierUsername)
                            {
                                Recette recette = recetteService.GetById(plat.RecetteId);
                                Console.WriteLine(recette.RecetteNom);
                                Console.WriteLine(plat.PlatPrix);
                            }
                            Console.ReadKey();
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