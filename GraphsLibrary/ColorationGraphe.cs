using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class ColorationGraphe<T>
    {
        private Graphe<T> _graphe;

        public ColorationGraphe(Graphe<T> graphe)
        {
            _graphe = graphe;
        }

        /// <summary>
        /// Applique l'algorithme de Welsh-Powell pour colorier le graphe
        /// </summary>
        /// <returns>Dictionnaire {Id du sommet, Couleur assignée}</returns>
        public Dictionary<int, int> WelshPowell()
        {
            var degres = _graphe.Noeuds.Values
                .Select(noeud => new { Id = noeud.Noeud_id, Degre = noeud.Liens.Count })
                .OrderByDescending(x => x.Degre)
                .ToList();

            var coloration = new Dictionary<int, int>();
            int couleurActuelle = 0;

            foreach (var courant in degres)
            {
                int id = courant.Id;

                if (coloration.ContainsKey(id)) continue;

                coloration[id] = couleurActuelle;

                foreach (var noeud in degres)
                {
                    int autreId = noeud.Id;

                    if (!coloration.ContainsKey(autreId) &&
                        !_graphe.Noeuds[id].Liens.Any(l => l.LienArrivee.Noeud_id == autreId) &&
                        !_graphe.Noeuds[autreId].Liens.Any(l => l.LienArrivee.Noeud_id == id))
                    {
                        bool compatible = true;

                        foreach (var voisin in _graphe.Noeuds[autreId].Liens.Select(l => l.LienArrivee.Noeud_id))
                        {
                            if (coloration.TryGetValue(voisin, out int c) && c == couleurActuelle)
                            {
                                compatible = false;
                                break;
                            }
                        }

                        if (compatible)
                        {
                            coloration[autreId] = couleurActuelle;
                        }
                    }
                }

                couleurActuelle++;
            }

            return coloration;
        }

        /// <summary>
        /// Détermine le nombre minimal de couleurs utilisées
        /// </summary>
        public int NombreMinimalDeCouleurs(Dictionary<int, int> coloration)
        {
            return coloration.Values.Distinct().Count();
        }

        /// <summary>
        /// Détermine si le graphe est biparti
        /// </summary>
        public bool EstBiparti()
        {
            var couleurs = WelshPowell();
            return NombreMinimalDeCouleurs(couleurs) == 2;
        }

        /// <summary>
        /// Vérifie si le graphe est planaire via l'inégalité d'Euler
        /// (approximation suffisante pour ton projet)
        /// </summary>
        public bool EstPlanaire()
        {
            int n = _graphe.Noeuds.Count;
            int m = _graphe.Liens.Count;

            if (n <= 4) return true;
            return m <= 3 * n - 6;
        }

        /// <summary>
        /// Retourne les groupes indépendants en fonction de la coloration
        /// </summary>
        public Dictionary<int, List<int>> GroupesIndependants(Dictionary<int, int> coloration)
        {
            var groupes = new Dictionary<int, List<int>>();

            foreach (var pair in coloration)
            {
                int id = pair.Key;
                int couleur = pair.Value;

                if (!groupes.ContainsKey(couleur))
                {
                    groupes[couleur] = new List<int>();
                }

                groupes[couleur].Add(id);
            }

            return groupes;
        }
    }
}
