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
                    case 2:
                        toExport.Add("Composition du plat");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 3:
                        toExport.Add("Composition de la recette");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 4:
                        toExport.Add("Contient");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 5:
                        toExport.Add("Creation");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 6:
                        toExport.Add("Cuisinier");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 7:
                        toExport.Add("Entreprise");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 8:
                        toExport.Add("Evaluation");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 9:
                        toExport.Add("Fait partie de");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 10:
                        toExport.Add("Ingredients");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 11:
                        toExport.Add("Livraison");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 12:
                        toExport.Add("Livre");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 13:
                        toExport.Add("Personne");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 14:
                        toExport.Add("Plat");
                        Debug.Assert(models != null, nameof(models) + " != null");
                        models.Add(typeof(Commande));
                        if (Affichages.MenuSelect("Voulez vous choisir d'autres donnees ?", new string[] { "oui", "non" }) == 1)
                        {
                            export = true;
                        }
                        break;
                    case 15:
                        toExport.Add("Recette");
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

            options = new[] { "JSON", "XML (Toutes les donnees seront exportees)", "CSV" };
            prompt = "Sous quel format voulez vous exporter vos donnees ?";
            switch (Affichages.MenuSelect(prompt, options))
            {
                case 0:
                    Console.WriteLine(Exports.ExportToJson(models));
                    break;
                case 1:
                    Console.WriteLine(Exports.ExportAllToXml());
                    break;
                case 2:
                    Console.WriteLine(Exports.ExportToCsv(models));
                    break;
                default:
                    Console.WriteLine("Erreur dans le choix du type");
                    break;
            }
            Console.WriteLine("Vos donnes ont ete exportees dans le fichier Exports.<votre format> Dans le dossier Livin Paris Console/bin/Debug !");
            Console.WriteLine("Appuyez sur une touche pour continuer");
            Console.ReadKey();
        }
    }
}
