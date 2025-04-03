using Graphs;
using Graphs.Parcours;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetroHelper
{
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

        public static (List<Station_de_metro>, int, int) ParcoursDijkstra(
            GrandeStation depart, GrandeStation arrivee, string dataPath)
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

        public static (List<Station_de_metro>, int, int) ParcoursBellmanFord(
            GrandeStation depart, GrandeStation arrivee, string dataPath)
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

        private static void AfficherTrajet(List<Station_de_metro> stations, double temps, int correspondances, string algo)
        {
            Console.WriteLine($"\n Résultat avec {algo} :");
            Console.WriteLine($"Temps total : {temps} minutes");
            Console.WriteLine($"Nombre de correspondances : {correspondances}");
            Console.WriteLine("Stations parcourues :");

            foreach (var station in stations)
            {
                Console.WriteLine($"  - {station.Nom}");
            }
            Console.WriteLine();
        }
    }
}
