using MetroHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Graphs;
using System.Linq;

namespace LivinParis_Console
{
    public class AffichageMetro
    {
        /// <summary>
        /// Affiche la carte simplifiée du métro avec les grandes stations (sans correspondances).
        /// </summary>
        public static void AfficherCarte()
        {
            string dataPath = Path.Combine("..", "..", "..", "MetroHelper", "Data");
            var peuplement = new PeuplementGrandeStation();
            var grandesStations = peuplement.CreerGrandesStations(dataPath);

            var graphe = new Graphe<Station_de_metro>("skip", ',', 1);
            graphe.Noeuds.Clear();

            var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            foreach (var grandeStation in grandesStations.Values)
            {
                foreach (var station in grandeStation.Stations)
                {
                    graphe.AjouterNoeud(station.Id, station);
                    coordonneesGeo[station.Id] = (grandeStation.Latitude, grandeStation.Longitude);
                }
            }

            var constructeur = new CreateGraphMetro();
            var grapheComplet = constructeur.ChargerReseauDepuisFichiers(dataPath);

            foreach (var lien in grapheComplet.Liens)
            {
                var depart = grapheComplet.Noeuds[lien.LienDepart.Noeud_id];
                var arrivee = grapheComplet.Noeuds[lien.LienArrivee.Noeud_id];

                if (depart.Noeud_Valeur is Station_de_metro stationDepart &&
                    arrivee.Noeud_Valeur is Station_de_metro stationArrivee)
                {
                    string nom1 = stationDepart.Nom.Split('(')[0].Trim();
                    string nom2 = stationArrivee.Nom.Split('(')[0].Trim();

                    if (nom1 != nom2)
                    {
                        graphe.AjouterLien(depart.Noeud_id, arrivee.Noeud_id, lien.LienPoids);
                        graphe.AjouterLien(arrivee.Noeud_id, depart.Noeud_id, lien.LienPoids);
                    }
                }
            }

            string imagePath = Path.Combine("..", "..", "..", "graphe.png");
            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo);
            visualiseur.Dessiner(imagePath);

            Console.WriteLine("✅ Carte du métro simplifiée enregistrée.");

            OuvrirImage(imagePath);
        }

        /// <summary>
        /// Affiche le graphe complet du métro, avec toutes les stations et les correspondances.
        /// </summary>
        public static void AfficherCarteAvecCorrespondances()
        {
            string dataPath = Path.Combine("..", "..", "..", "MetroHelper", "Data");

            // === Charger le graphe complet (avec correspondances)
            var constructeur = new CreateGraphMetro();
            var graphe = constructeur.ChargerReseauDepuisFichiers(dataPath);

            // === Générer des coordonnées aléatoires sans superposition
            var coordonneesGeo = GenererCoordonneesAleatoires(graphe.Noeuds.Count);

            // === Associer les coordonnées aux IDs des stations
            var mapping = new Dictionary<int, (int lat, int lon)>();
            int i = 0;
            foreach (var noeud in graphe.Noeuds.Values)
            {
                var (x, y) = coordonneesGeo[i++];
                mapping[noeud.Noeud_id] = (x, y);
            }

            // === Dessiner le graphe
            string imagePath = Path.Combine("..", "..", "..", "graphe_complet.png");
            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, mapping);
            visualiseur.Dessiner(imagePath);

            Console.WriteLine("✅ Graphe complet avec correspondances enregistré.");
            OuvrirImage(imagePath);
        }
        
        private static List<(int, int)> GenererCoordonneesAleatoires(int count)
        {
            var positions = new List<(int, int)>();
            Random rand = new Random();
            int marge = 30;

            while (positions.Count < count)
            {
                int x = rand.Next(marge, 1000 - marge);
                int y = rand.Next(marge, 1000 - marge);

                bool tropProche = positions.Any(pos => Math.Abs(pos.Item1 - x) < marge && Math.Abs(pos.Item2 - y) < marge);
                if (!tropProche)
                {
                    positions.Add((x, y));
                }
            }

            return positions;
        }


        /// <summary>
        /// Ouvre une image à partir du chemin donné.
        /// </summary>
        private static void OuvrirImage(string imagePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = imagePath,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Erreur lors de l'ouverture de l'image : {e.Message}");
            }

            Console.ReadKey();
        }
    }
}
