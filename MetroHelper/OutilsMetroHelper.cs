using Graphs;
using Graphs.Parcours;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetroHelper
{
    /// <summary>
    /// Classe qui regroupe différents outils utiles pour les manipulations sur les metros
    /// </summary>
    public static class OutilsMetroHelper
    {
        public static (Station_de_metro, Station_de_metro) MeilleuresStations(
            GrandeStation depart, GrandeStation arrivee,
            Graphe<Station_de_metro> graphe)
        {
            int meilleurTemps = int.MaxValue;
            int minCorrespondances = int.MaxValue;
            Station_de_metro meilleurDepart = null;
            Station_de_metro meilleurArrivee = null;

            foreach (var sDep in depart.Stations)
            {
                foreach (var sArr in arrivee.Stations)
                {
                    var resultat = Dijkstra<Station_de_metro>.CheminPlusCourt(graphe, sDep.Id, sArr.Id);
                    var temps = (int)resultat.Item2;
                    var chemin = resultat.Item1;
                    var correspondances = CalculerNombreCorrespondances(chemin, graphe);

                    if (temps < meilleurTemps || (temps == meilleurTemps && correspondances < minCorrespondances))
                    {
                        meilleurTemps = temps;
                        minCorrespondances = correspondances;
                        meilleurDepart = sDep;
                        meilleurArrivee = sArr;
                    }
                }
            }

            return (meilleurDepart, meilleurArrivee);
        }
        /// <summary>
        /// Methode qui permet d'appliquer Dijkstra à un graph de station de metro.
        /// </summary>
        /// <param name="depart"></param>Correspond à la Grande station de départ de l'algorithme
        /// <param name="arrivee"></param> Correspond à la Grande station d'arrivée de l'algorithme
        /// <param name="dataPath"></param> Base de donnée pour charger le graph
        /// <returns>
        /// retourne une liste de station parcouru, un temps de trajet et un nombre de correspondance parcouru
        /// </returns>
        public static (List<Station_de_metro>, int, int) ParcoursDijkstra(GrandeStation depart, GrandeStation arrivee, string dataPath)
        {
            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

            var (start, end) = MeilleuresStations(depart, arrivee, graphe);
            var (cheminIds, temps) = Dijkstra<Station_de_metro>.CheminPlusCourt(graphe, start.Id, end.Id);

            var cheminStations = cheminIds.Select(id => graphe.Noeuds[id].Noeud_Valeur).ToList();
            var correspondances = CalculerNombreCorrespondances(cheminIds, graphe);

            AfficherTrajet(cheminStations, temps, correspondances, "Dijkstra");
            return (cheminStations, (int)temps, correspondances);
        }
        /// <summary>
        /// Methode qui permet d'appliquer Bellman-Ford à un graph de station de metro.
        /// </summary>
        /// <param name="depart"></param>Correspond à la Grande station de départ de l'algorithme
        /// <param name="arrivee"></param> Correspond à la Grande station d'arrivée de l'algorithme
        /// <param name="dataPath"></param> Base de donnée pour charger le graph
        /// <returns>
        /// Retourne une liste de station parcourus, un temps de trajet et un nombre de correspondance parcouru
        /// </returns>
        public static (List<Station_de_metro>, int, int) ParcoursBellmanFord(GrandeStation depart, GrandeStation arrivee, string dataPath)
        {
            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

            var (start, end) = MeilleuresStations(depart, arrivee, graphe);
            var (cheminIds, temps) = BellmanFord<Station_de_metro>.CheminPlusCourt(graphe, start.Id, end.Id);

            var cheminStations = cheminIds.Select(id => graphe.Noeuds[id].Noeud_Valeur).ToList();
            var correspondances = CalculerNombreCorrespondances(cheminIds, graphe);

            AfficherTrajet(cheminStations, temps, correspondances, "Bellman-Ford");
            return (cheminStations, (int)temps, correspondances);
        }
        /// <summary>
        /// Methode qui permet d'appliquer floyWarshall à un graph de station de metro.
        /// </summary>
        /// <param name="depart"></param>Correspond à la Grande station de départ de l'algorithme
        /// <param name="arrivee"></param> Correspond à la Grande station d'arrivée de l'algorithme
        /// <param name="dataPath"></param> Base de donnée pour charger le graph
        /// <returns>
        /// retourne une liste de station parcouru, un temps de trajet et un nombre de correspondance parcouru
        /// </returns>

        public static (List<Station_de_metro>, int, int) ParcoursFloydWarshall(
            GrandeStation depart, GrandeStation arrivee, string dataPath)
        {
            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

            var algo = new FloydWarshall<Station_de_metro>(graphe);
            var (start, end) = MeilleuresStations(depart, arrivee, graphe);
            var (temps, cheminIds) = algo.TrouverPlusCourtChemin(start.Id, end.Id);

            var cheminStations = cheminIds.Select(id => graphe.Noeuds[id].Noeud_Valeur).ToList();
            var correspondances = CalculerNombreCorrespondances(cheminIds, graphe);

            AfficherTrajet(cheminStations, temps, correspondances, "Floyd-Warshall");
            return (cheminStations, (int)temps, correspondances);
        }
        
        /// <summary>
        /// Methode qui permet d'appliquer Algo etoile à un graph de station de metro.
        /// </summary>
        /// <param name="depart"></param>Correspond à la Grande station de départ de l'algorithme
        /// <param name="arrivee"></param> Correspond à la Grande station d'arrivée de l'algorithme
        /// <param name="dataPath"></param> Base de donnée pour charger le graph
        /// <returns>
        /// Retourne une liste de station parcourue, un temps de trajet et un nombre de correspondance parcouru
        /// </returns>
        public static (List<Station_de_metro>, int, int) ParcoursAStar(
            GrandeStation depart, GrandeStation arrivee, string dataPath)
        {
            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

            var algo = new AStar<Station_de_metro>(graphe);
            var (start, end) = MeilleuresStations(depart, arrivee, graphe);
            var (temps, cheminIds) = algo.TrouverChemin(start.Id, end.Id);

            var cheminStations = cheminIds.Select(id => graphe.Noeuds[id].Noeud_Valeur).ToList();
            var correspondances = CalculerNombreCorrespondances(cheminIds, graphe);

            AfficherTrajet(cheminStations, temps, correspondances, "A*");
            return (cheminStations, (int)temps, correspondances);
        }

        /// <summary>
        /// Méthode qui permet de calculer le nombre de correspondances dans un trajet donnée
        /// Une correspondance est comptée lorsqu'il y a un changement de ligne au cours du trajet
        /// </summary>
        /// <param name="cheminIds"> Liste des identifiants des stations empruntés dans l'ordre du trajet</param>
        /// <param name="graphe"> Graphe sur lequel repose l'étude de l'algorithme </param>
        /// <returns>
        /// Un entier qui correspond au nombre de correspondances totales effectuées lors du trajet
        /// </returns>
        private static int CalculerNombreCorrespondances(List<int> cheminIds, Graphe<Station_de_metro> graphe)
        {
            int correspondances = 0;

            for (int i = 1; i < cheminIds.Count; i++)
            {
                var stationPrecedente = graphe.Noeuds[cheminIds[i - 1]].Noeud_Valeur;
                var stationActuelle = graphe.Noeuds[cheminIds[i]].Noeud_Valeur;

                if (stationPrecedente is Station_de_metro s1 && stationActuelle is Station_de_metro s2)
                {
                    string nom1 = s1.Nom.Split('(')[0].Trim();
                    string ligne1 = s1.Nom.Split('(')[1].Trim(')', ' ');

                    string nom2 = s2.Nom.Split('(')[0].Trim();
                    string ligne2 = s2.Nom.Split('(')[1].Trim(')', ' ');

                    if (nom1 == nom2 && ligne1 != ligne2)
                        correspondances++;
                }
            }

            return correspondances;
        }
        /// <summary>
        /// Affiche les information d'un trajet effectuée avec un des algorithmes de parcours
        /// </summary>
        /// <param name="stations"> Donne la liste des stations parcourues</param> 
        /// <param name="temps"> Donne le temps de trajet total</param>
        /// <param name="correspondances">Donne le nombre de correspondances qui ont été effectuées</param>
        /// <param name="algo"> Dit quel algorithme de parcours de graph a été utilisé</param>
        private static void AfficherTrajet(List<Station_de_metro> stations, double temps, int correspondances, string algo)
        {
            Console.WriteLine($"\n Résultat avec {algo} :");
            Console.WriteLine($"\n Temps total : {temps} minutes");
            Console.WriteLine($"Nombre de correspondances : {correspondances}\n");
            Console.WriteLine("\n--- Itinéraire trouvé ---\n");
            foreach (var station in stations)
            {
                Console.WriteLine($" - {station.Nom}");
            }
            Console.WriteLine("Résultat pour le trajet de la station : "+stations.First().Nom+" Jusqu'a " + stations.Last().Nom);
            Console.WriteLine();
        }
        /// <summary>
        /// Calcule le temps total de trajet en sommant les poids des arêtes empruntées dans le chemin.
        /// </summary>
        /// <param name="cheminIds">Liste ordonnée des identifiants des stations empruntées.</param>
        /// <param name="graphe">Graphe des stations de metro.</param>
        /// <returns>
        /// Un entier correspondant au temps total du trajet en minutes.
        /// </returns>
        public static int CalculerTempsTrajet(List<int> cheminIds, Graphe<Station_de_metro> graphe)
        {
            int total = 0;

            for (int i = 0; i < cheminIds.Count - 1; i++)
            {
                var depart = cheminIds[i];
                var arrivee = cheminIds[i + 1];

                var noeud = graphe.Noeuds[depart];
                var lien = noeud.Liens.FirstOrDefault(l => l.LienArrivee.Noeud_id == arrivee);

                if (lien != null)
                {
                    total += (int)lien.LienPoids;
                }
                else
                {
                    Console.WriteLine($" Aucun lien trouvé entre {depart} et {arrivee}. Trajet peut être incomplet.");
                }
            }

            return total;
        }

    }
}
