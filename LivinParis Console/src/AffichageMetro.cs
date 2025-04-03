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

            var imagePath = Path.Combine("..", "..", "..", "graphe.png");

            // Définir les couleurs des lignes (clé = numéro de ligne, valeur = nom de la couleur)
            var couleursParLigne = new Dictionary<int, string>
            {
                { 1, "Gold" }, { 2, "Blue" }, { 3, "DarkOrange" }, { 4, "Red" },
                { 5, "Brown" }, { 6, "OliveDrab" }, { 7, "Pink" }, { 8, "MediumPurple" },
                { 9, "Goldenrod" }, { 10, "DarkCyan" }, { 11, "DarkRed" }, { 12, "SeaGreen" },
                { 13, "Green" }, { 14, "SlateBlue" }, { 31, "DarkOrange" }, { 71, "Pink" }
            };

            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo)
            {
                CouleurParDefault = "DarkGray",
                RayonNoeud = 7,
                CouleurRemplissageNoeud = "White",
                CouleurContourNoeud = "Black",
                CouleurParLigne = (station) =>
                {
                    int num = ExtraireNumeroLigne(station.Nom);
                    return couleursParLigne.TryGetValue(num, out string color) ? color : "DarkGray";
                }
            };

            visualiseur.Dessiner(imagePath);

            Console.WriteLine("✅ Carte du métro simplifiée enregistrée.");
            OuvrirImage(imagePath);
        }

        public static void AfficherCarteAvecCorrespondances()
        {
            string dataPath = Path.Combine("..", "..", "..", "MetroHelper", "Data");

            var constructeur = new CreateGraphMetro();
            var graphe = constructeur.ChargerReseauDepuisFichiers(dataPath);

            var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            var random = new Random();
            var positions = GenererCoordonneesAleatoires(graphe.Noeuds.Count);
            int i = 0;
            foreach (var noeud in graphe.Noeuds.Values)
            {
                coordonneesGeo[noeud.Noeud_id] = positions[i++];
            }

            var imagePath = Path.Combine("..", "..", "..", "graphe_complet.png");

            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo)
            {
                CouleurParDefault = "DarkGray",
                RayonNoeud = 7,
                CouleurRemplissageNoeud = "White",
                CouleurContourNoeud = "Black"
            };

            visualiseur.Dessiner(imagePath);

            Console.WriteLine("✅ Graphe complet avec correspondances enregistré.");
            OuvrirImage(imagePath);
        }

        private static int ExtraireNumeroLigne(string nomComplet)
        {
            int debut = nomComplet.IndexOf("Ligne") + 6;
            string numero = nomComplet.Substring(debut).Trim(')', ' ');

            if (numero == "3bis") return 31;
            if (numero == "7bis") return 71;

            return int.TryParse(numero, out int n) ? n : -1;
        }

        private static List<(int, int)> GenererCoordonneesAleatoires(int count)
        {
            var positions = new List<(int, int)>();
            var rand = new Random();
            int marge = 30;

            while (positions.Count < count)
            {
                int x = rand.Next(100, 1000);
                int y = rand.Next(100, 1000);

                bool tropProche = positions.Any(pos =>
                    Math.Abs(pos.Item1 - x) < marge &&
                    Math.Abs(pos.Item2 - y) < marge);

                if (!tropProche)
                {
                    positions.Add((x, y));
                }
            }

            return positions;
        }

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
