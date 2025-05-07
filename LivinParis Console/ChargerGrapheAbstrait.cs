using Graphs;
using System;
using System.Collections.Generic;
using System.IO;

namespace LivinParis_Console
{
    public static class ChargeurGrapheAbstrait
    {
        /// <summary>
        /// Charge un graphe abstrait depuis un fichier CSV contenant des paires de sommets.
        /// </summary>
        /// <param name="cheminFichier">Chemin vers le fichier .csv</param>
        /// <returns>Graphe<int> construit à partir du fichier</returns>
        public static Graphe<int> ChargerDepuisCsv(string cheminFichier)
        {
            var graphe = new Graphe<int>(100, false);
            var lignes = File.ReadAllLines(cheminFichier);
            var sommetsVus = new HashSet<int>();

            foreach (var ligne in lignes)
            {
                var parts = ligne.Split(',');
                if (parts.Length != 2) continue;

                if (!int.TryParse(parts[0].Trim(), out int a)) continue;
                if (!int.TryParse(parts[1].Trim(), out int b)) continue;

                if (!sommetsVus.Contains(a))
                {
                    graphe.AjouterNoeud(a, a);
                    sommetsVus.Add(a);
                }

                if (!sommetsVus.Contains(b))
                {
                    graphe.AjouterNoeud(b, b);
                    sommetsVus.Add(b);
                }

                graphe.AjouterLien(a, b, 1); 
                graphe.AjouterLien(b, a, 1); 
            }

            return graphe;
        }
    }
}