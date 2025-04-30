using System;
using System.Collections.Generic;
using System.IO;
using Graphs;

namespace LivinParis_Console
{
    public class ColorationConsole
    {
        private string _cheminDossier;
        private Dictionary<int, string> _couleurs;

        public ColorationConsole(string cheminDossier)
        {
            _cheminDossier = cheminDossier;
            InitialiserCouleurs();
        }

        public ColorationConsole()
        {
            string racineProjet = Program.TrouverRacineProjet("GraphsLibrary");
            _cheminDossier = Path.Combine(racineProjet, "graph_exemple");
            InitialiserCouleurs();
        }

        private void InitialiserCouleurs()
        {
            _couleurs = new Dictionary<int, string>
            {
                { 0, "Red" }, { 1, "Blue" }, { 2, "Green" }, { 3, "Orange" }, { 4, "Purple" },
                { 5, "Pink" }, { 6, "Brown" }, { 7, "Cyan" }, { 8, "Magenta" }, { 9, "Teal" }
            };
        }

        public void Lancer()
        {
            string[] options = {
                "Graphe Complet",
                "Cycle",
                "Arbre",
                "Planaire simple",
                "Sparsely connecté",
                "Retour"
            };

            int choix = Affichages.MenuSelect("Quel graphe souhaitez-vous colorier ?", options);
            if (choix == 5) return;

            string nomFichier;
            switch (choix)
            {
                case 0:
                    nomFichier = "complet.csv";
                    break;
                case 1:
                    nomFichier = "cycle.csv";
                    break;
                case 2:
                    nomFichier = "arbre.csv";
                    break;
                case 3:
                    nomFichier = "plannaire_simple.csv";
                    break;
                case 4:
                    nomFichier = "sparsely_connecté.csv";
                    break;
                default:
                    throw new Exception("Choix invalide.");
            }

            string chemin = Path.Combine(_cheminDossier, nomFichier);
            Console.WriteLine($"\n📂 Chemin final utilisé : {chemin}");

            if (!File.Exists(chemin))
            {
                Console.WriteLine($"❌ Fichier introuvable : {chemin}");
                return;
            }

            Graphe<int> graphe = ChargerDepuisCsv(chemin);

            var colorateur = new ColorationGraphe<int>(graphe);
            var resultat = colorateur.WelshPowell();

            Console.WriteLine("\n✅ Résultat de la coloration (Welsh-Powell) :\n");
            foreach (var kvp in resultat)
            {
                Console.WriteLine($"Sommet {kvp.Key} => Couleur {kvp.Value}");
            }

            var visualiseur = new GrapheVisuel<int>(graphe);
            foreach (var kvp in resultat)
            {
                if (_couleurs.TryGetValue(kvp.Value, out string couleur))
                {
                    visualiseur.ColorierNoeud(kvp.Key, couleur);
                }
            }

            string imagePath = Path.Combine(_cheminDossier, $"coloration_{Path.GetFileNameWithoutExtension(nomFichier)}.png");
            visualiseur.DessinerGraphe(imagePath);
            Console.WriteLine($"\n📷 Image enregistrée à : {imagePath}");
        }

        private Graphe<int> ChargerDepuisCsv(string cheminFichier)
        {
            var lignes = File.ReadAllLines(cheminFichier);
            var sommets = new HashSet<int>();

            // Collecte des identifiants uniques
            foreach (var ligne in lignes)
            {
                var parts = ligne.Split(',');
                if (parts.Length != 2) continue;

                if (int.TryParse(parts[0].Trim(), out int a))
                    sommets.Add(a);
                if (int.TryParse(parts[1].Trim(), out int b))
                    sommets.Add(b);
            }

            var graphe = new Graphe<int>(sommets.Count, false);
            foreach (var id in sommets)
                graphe.AjouterNoeud(id, id);

            foreach (var ligne in lignes)
            {
                var parts = ligne.Split(',');
                if (parts.Length != 2) continue;

                if (!int.TryParse(parts[0].Trim(), out int a)) continue;
                if (!int.TryParse(parts[1].Trim(), out int b)) continue;

                graphe.AjouterLien(a, b, 1);
                graphe.AjouterLien(b, a, 1); // graphe non orienté
            }

            return graphe;
        }
    }
}
