// File: Program.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DBConnectionLibrary.Sql;
using Graphs;
using LivinParis_Console.Assets;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace LivinParis_Console
{
    public class BddConnection
    {
        public static void ConnectorTest()
        {
            string[] options = { "Initialiser la base de donnees", "Exporter les donnees","Quitter" };
            int bddChoice = Affichages.MenuSelect(ASCII.Bdd+"\n Quelle option voulez vous choisir ?", options);
            switch (bddChoice)
            {
                case 0:
                    InitializeSql.Initialize();
                    break;
                case 1:
                    MainExport();
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }

        public static void MainExport()
        {
            bool export = false;
            List<string> toExport = new List<string>();
            List<Type> models = new List<Type>();
            string prompt = ASCII.Bdd + "\n Que voulez vous exporter ?";
            string[] options = { "Client", "Commande","Composition du plat","Composition de la recette", "Contient", "Creation", "Cuisinier", "Entreprise", "Evaluation", "Fait partie de", "Ingredients", "Livraison", "Livre", "Personne", "Plat", "Recette"};
            do
            {
                if(toExport.Count != 0)
                {
                    prompt += "\n Vous avez choisi : ";
                    foreach (string option in toExport)
                    {
                        prompt +="\n " + option;
                    }
                }
                int bddChoice = Affichages.MenuSelect(prompt, options);
                switch (bddChoice)
                {
                    case 0:
                        toExport.Add("Client");
                        Client c = null;
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Client));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 1:
                        toExport.Add("Commande");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    default:
                        Console.WriteLine("Erreur dans le choix du type");
                        break;
                }
                Console.WriteLine("Appuyez sur une touche pour continuer");
                Console.ReadKey();
            }while (!export);
            Console.WriteLine(Exports.ExportToJson(models));
            Console.ReadKey();
        }
    }
}
