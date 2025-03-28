using System;
using System.Collections.Generic;
using System.Globalization;
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
            string[] options = { "Vos plats" ,"Ajouter un plat","Créer une recette", "Déconnexion"};
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
                        }
                        break;
                    case 1:
                        PlatMenuMain(recetteService, recetteDataAccess.GetAll().Count, platDataAccess);
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

        public void PlatMenuMain(RecetteService recetteService, int platId, PlatDataAccess platDataAccess)
        {
            List<Recette> recettes = recetteService.GetAll();
            string[] options = new string[recettes.Count+1];  
            foreach (var recette in recettes)
            {
                options[recette.RecetteId] = recette.RecetteNom;
            }
            options[recettes.Count] = "Créer votre recette";
            int platChoice = Affichages.MenuSelect(ASCII.Cuisinier+"Quelle est la recette de votre plat ?", options);
            if (platChoice == recettes.Count)
            {
                Recette newRecette = RecetteCréation(recettes.Count);
                recetteService.Insert(newRecette);
                platDataAccess.Insert(PlatCréation(newRecette, platId));
            }
            else
            {
                platDataAccess.Insert(PlatCréation(recettes[platChoice], platId));
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

        public Plat PlatCréation(Recette recette, int platId)
        {
            DateTime dateFabrication;
            DateTime datePeremption;
            Plat plat = new Plat();
            plat.RecetteId = recette.RecetteId;
            plat.PlatId = platId+1;
            Console.WriteLine("Quel est le prix de votre plat ?");
            plat.PlatPrix = Console.ReadLine();
            Console.WriteLine("Combien votre plat a il de portions ?");
            plat.PlatNombrePortion = Convert.ToInt32(Console.ReadLine());
            plat.CuisinierUsername = Cuisinier.CuisinierUsername;
            Console.WriteLine("=== Saisie des dates de fabrication et de péremption ===");
            dateFabrication = LireDate("Entrez la date de fabrication (jj/MM/yyyy) : ");
            while (true)
            {
                datePeremption = LireDate("Entrez la date de péremption (jj/MM/yyyy) : ");

                if (datePeremption > dateFabrication)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("⚠️ La date de péremption doit être postérieure à la date de fabrication.");
                }
            }
            return plat;
        }
        static DateTime LireDate(string message)
        {
            DateTime date;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("❌ Format invalide. Veuillez utiliser le format jj/MM/yyyy.");
                }
            }
        }
    }
}