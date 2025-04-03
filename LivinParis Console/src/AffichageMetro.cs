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
            var grandes = peuplement.CreerGrandesStations(dataPath);

            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

            var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (noeud.Noeud_Valeur is Station_de_metro station)
                {
                    string nomGrande = station.Nom.Split('(')[0].Trim();
                    if (grandes.TryGetValue(nomGrande, out var grande))
                    {
                        coordonneesGeo[station.Id] = (grande.Latitude, grande.Longitude);
                    }
                }
            }

            var couleursParLigne = GetCouleursParLigne();

            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo)
            {
                CouleurParDefault = "DarkGray",
                RayonNoeud = 7,
                CouleurRemplissageNoeud = "White",
                CouleurContourNoeud = "Black",
                CouleurDesLiens = (from, to) =>
                {
                    int num = ExtraireNumeroLigne(from.Nom);
                    return couleursParLigne.TryGetValue(num, out string color) ? color : "DarkGray";
                }
            };

            string imagePath = Path.Combine("..", "..", "..", "graphe.png");
            visualiseur.Dessiner(imagePath);
            Console.WriteLine("✅ Plan simplifié du métro (liens colorés uniquement) enregistré.");
            OuvrirImage(imagePath);
        }

        public static void AfficherChemin(Graphe<Station_de_metro> graphe, List<Station_de_metro> chemin, Dictionary<string, GrandeStation> grandes)
        {
            var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (chemin.Any(c => c.Id == noeud.Noeud_id) && noeud.Noeud_Valeur is Station_de_metro s)
                {
                    string nom = s.Nom.Split('(')[0].Trim();
                    if (grandes.TryGetValue(nom, out var grandeStation))
                    {
                        coordonneesGeo[noeud.Noeud_id] = (grandeStation.Latitude, grandeStation.Longitude);
                    }
                }
            }

            var couleursParLigne = GetCouleursParLigne();
            string imagePath = Path.Combine("..", "..", "..", "chemin.png");

            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo)
            {
                CouleurParDefault = "LightGray",
                RayonNoeud = 7,
                CouleurRemplissageNoeud = "White",
                CouleurContourNoeud = "Black",
                CouleurDesLiens = (from, to) =>
                {
                    int num = ExtraireNumeroLigne(from.Nom);
                    return couleursParLigne.TryGetValue(num, out string color) ? color : "DarkGray";
                },
                AffichageChemin = chemin.Select(s => s.Id).ToList(),
                NafficherQueChemin = true
            };

            visualiseur.Dessiner(imagePath);
            Console.WriteLine("✅ Chemin affiché dans chemin.png.");
            OuvrirImage(imagePath);
        }

        public static void AfficherCarteAvecCorrespondances()
        {
            string dataPath = Path.Combine("..", "..", "..", "MetroHelper", "Data");
            var constructeur = new CreateGraphMetro();
            var graphe = constructeur.ChargerReseauDepuisFichiers(dataPath);

            var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            var positions = GenererCoordonneesAleatoires(graphe.Noeuds.Count);
            int i = 0;
            foreach (var noeud in graphe.Noeuds.Values)
                coordonneesGeo[noeud.Noeud_id] = positions[i++];

            string imagePath = Path.Combine("..", "..", "..", "graphe_complet.png");

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

        public static int ExtraireNumeroLigne(string nomComplet)
        {
            int debut = nomComplet.IndexOf("Ligne") + 6;
            string numero = nomComplet.Substring(debut).Trim(')', ' ');

            if (numero == "3bis") return 31;
            if (numero == "7bis") return 71;

            return int.TryParse(numero, out int n) ? n : -1;
        }

        public static Dictionary<int, string> GetCouleursParLigne()
        {
            return new Dictionary<int, string>
            {
                { 1, "Gold" }, { 2, "Blue" }, { 3, "DarkOrange" }, { 4, "Red" },
                { 5, "Brown" }, { 6, "OliveDrab" }, { 7, "Pink" }, { 8, "MediumPurple" },
                { 9, "Goldenrod" }, { 10, "DarkCyan" }, { 11, "DarkRed" }, { 12, "SeaGreen" },
                { 13, "Green" }, { 14, "SlateBlue" }, { 31, "DarkOrange" }, { 71, "Pink" }
            };
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
                bool tropProche = positions.Any(pos => Math.Abs(pos.Item1 - x) < marge && Math.Abs(pos.Item2 - y) < marge);
                if (!tropProche) positions.Add((x, y));
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
