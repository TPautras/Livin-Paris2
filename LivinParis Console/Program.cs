using System;
using System.Collections.Generic;
using System.IO;
using Graphs;
using MetroHelper;
using SqlConnector;
using static LivinParis_Console.BddConnection;

namespace LivinParis_Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] options = {
                "Rendu 1",
                "Partie Metro/ BDD Initialisation",
                "Livin'Paris",
                "Afficher Carte du Métro",
                "Quitter"
            };

            bool Quit = false;
            while (!Quit)
            {
                int res = Affichages.MenuSelect(
                    Assets.ASCII.Psi2025 + "\nQuel partie du code voulez-vous explorer ?", options);
                switch (res)
                {
                    case 0:
                        Exercice1.Exo1();
                        break;
                    case 1:
                        BddConnection.ConnectorTest();
                        break;
                    case 2:
                        LivinParis.ConnectorTest();
                        break;
                    case 3:
                        MenuAffichageMetro();
                        break;
                    default:
                        Quit = true;
                        break;
                }
            }
        }

        public static void MenuAffichageMetro()
        {
            string[] sousOptions = {
                "Afficher la carte des grandes stations (plan simplifié)",
                "Afficher le graphe complet avec correspondances",
                "Afficher un itinéraire avec un algorithme de parcours",
                "Retour"
            };

            int choix = Affichages.MenuSelect("\nQuel type de carte souhaitez-vous afficher ?", sousOptions);

            switch (choix)
            {
                case 0:
                    AffichageMetro.AfficherCarte();
                    break;
                case 1:
                    AffichageMetro.AfficherCarteAvecCorrespondances();
                    break;
                case 2:
                    MenuChoixParcours();
                    break;
                default:
                    break;
            }
        }

        public static void MenuChoixParcours()
        {
            string[] algos = { "Dijkstra", "Bellman-Ford", "Floyd-Warshall", "A*", "Retour" };
            int choixAlgo = Affichages.MenuSelect("Quel algorithme souhaitez-vous utiliser ?", algos);
            if (choixAlgo == 4) return;

            string[] grandesStations = {
                "Châtelet", "Gare de Lyon", "République", "Nation", "Bastille",
                "Montparnasse - Bienvenüe", "Saint-Lazare", "Charles de Gaulle - Étoile", "Opéra", "Concorde",
                "Place d'Italie", "Belleville", "Strasbourg - Saint-Denis", "Gare du Nord", "La Motte-Picquet - Grenelle",
                "Franklin D. Roosevelt", "Trocadéro", "Invalides", "Hôtel de Ville", "Arts et Métiers"
            };

            int departIndex = Affichages.MenuSelect("Choisissez une station de départ :", grandesStations);
            int arriveeIndex = Affichages.MenuSelect("Choisissez une station d'arrivée :", grandesStations);
            if (departIndex == arriveeIndex)
            {
                Console.WriteLine("⚠️ Les stations de départ et d’arrivée doivent être différentes.");
                return;
            }

            string nomDepart = grandesStations[departIndex];
            string nomArrivee = grandesStations[arriveeIndex];

            string solutionRoot = TrouverRacineProjet("MetroHelper");
            string dataPath = Path.Combine(solutionRoot, "MetroHelper", "Data");
            Console.WriteLine($"\n📂 Chemin des données utilisé : {dataPath}");

            try
            {
                var peuplement = new PeuplementGrandeStation();
                var grandes = peuplement.CreerGrandesStations(dataPath);

                if (!grandes.ContainsKey(nomDepart) || !grandes.ContainsKey(nomArrivee))
                {
                    Console.WriteLine("❌ Une des grandes stations sélectionnées n'existe pas dans les données.");
                    return;
                }

                GrandeStation grandeDepart = grandes[nomDepart];
                GrandeStation grandeArrivee = grandes[nomArrivee];

                List<Station_de_metro> chemin;
                int temps;
                int nbCorrespondances;

                switch (choixAlgo)
                {
                    case 0:
                        (chemin, temps, nbCorrespondances) = OutilsMetroHelper.ParcoursDijkstra(grandeDepart, grandeArrivee, dataPath);
                        break;
                    case 1:
                        (chemin, temps, nbCorrespondances) = OutilsMetroHelper.ParcoursBellmanFord(grandeDepart, grandeArrivee, dataPath);
                        break;
                    case 2:
                        (chemin, temps, nbCorrespondances) = OutilsMetroHelper.ParcoursFloydWarshall(grandeDepart, grandeArrivee, dataPath);
                        break;
                    case 3:
                        (chemin, temps, nbCorrespondances) = OutilsMetroHelper.ParcoursAStar(grandeDepart, grandeArrivee, dataPath);
                        break;
                    default:
                        return;
                }

                Console.WriteLine("\n--- 🧭 Itinéraire trouvé ---\n");
                foreach (var station in chemin)
                {
                    Console.WriteLine($" - {station.Nom}");
                }
                Console.WriteLine($"\n⏱ Temps total : {temps} minutes");
                Console.WriteLine($"🔁 Nombre de correspondances : {nbCorrespondances}\n");

                // ✅ Générer et afficher le graphe du chemin
                var createur = new CreateGraphMetro();
                var graphe = createur.ChargerReseauDepuisFichiers(dataPath);
                AffichageMetro.AfficherChemin(graphe, chemin, grandes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Une erreur est survenue : {ex.Message}");
                Console.WriteLine("📌 Détail : " + ex.StackTrace);
            }

            Console.WriteLine("\nAppuyez sur une touche pour revenir au menu principal...");
            Console.ReadKey();
        }

        public static string TrouverRacineProjet(string dossierCible)
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (!Directory.Exists(Path.Combine(dir.FullName, dossierCible)))
            {
                if (dir.Parent == null)
                    throw new DirectoryNotFoundException($"Impossible de trouver le dossier '{dossierCible}' dans la hiérarchie.");
                dir = dir.Parent;
            }

            return dir.FullName;
        }
    }
}
