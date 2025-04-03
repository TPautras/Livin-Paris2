using System;
using System.Collections.Generic;
using System.Reflection;
using DBConnectionLibrary.DataAccess;
using Graphs;
using LivinParis_Console.Assets;
using LivinParis_Console.Modules;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console.Modules
{
    public class ModuleStatistiques : AdminMenu
    {
        public void ModuleStatistiquesMain()
        {
            string[] options =
            {
                "Avoir le nombre de livraisons effectuees par cuisinier", 
                "Afficher les commandes selon une periode de temps", 
                "Afficher la moyenne des prix des commandes", 
                "Afficher la moyenne des comptes clients",
                "Afficher la list des commandes pour un client selon la nationalite des plats, la periode",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Statistiques";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleStatistiquesLivraisonsParCuisinier();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleStatistiquesCommandesOverTime();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleStatistiquesMoyennePrixCommandes();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleStatistiquesMoyenneCompte();
                        Console.ReadKey();
                        break;
                    case 4:
                        ModuleStatistiquesCommandesBizarre();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        private void ModuleStatistiquesCommandesBizarre()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("Entrez la date de depart (jj/mm/aaaa)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la date d'arrivee (jj/mm/aaaa)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez l'Username du client que vous voulez analyser");
            string username = Console.ReadLine();
            Console.WriteLine("Entrez l'origine des plats");
            string origine = Console.ReadLine();
            
            List<int> commandes = dataAccess.GetCommandesByClientAndFilters(username, origine, startDate, endDate);
            foreach (int commande in commandes)
            {
                Console.WriteLine("La commande "+ commande +" a ete passee entre la date de depart et celle d'arrivee par "+username+" et est bien d'origine "+origine+".");
            }
        }

        private void ModuleStatistiquesMoyenneCompte()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("La moyenne de depense par compte est de : " + dataAccess.GetAverageClientSpending());
        }

        private void ModuleStatistiquesMoyennePrixCommandes()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("La moyenne de prix des commandes est de : " + dataAccess.GetAverageCommandePrice());
        }

        private void ModuleStatistiquesCommandesOverTime()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("Entrez la date de depart (jj/mm/aaaa)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la date d'arrivee (jj/mm/aaaa)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            List<int> res = dataAccess.GetCommandesBetweenDates(startDate, endDate);
            foreach (int i in res)
            {
                Console.WriteLine("La commande "+ i +" a ete passee entre la date de depart et celle d'arrivee");
            }
        }
        private void ModuleStatistiquesLivraisonsParCuisinier()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Dictionary<string, int> stats = dataAccess.GetLivraisonCountByCuisinier();
            foreach (var kvp in stats)
            {
                Console.WriteLine("Le cuisinier : "+kvp.Key+" a effectue : "+kvp.Value + " livraisons");
            }
        }
    }
}