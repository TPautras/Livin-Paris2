using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Parcours
{

    /// <summary>
    /// Classe représentant l'algorithme de Dijkstra pour trouver le plus court chemin entre deux sommets dans un graphe
    /// </summary>
    public class Dijkstra<T>
    {
        /// <summary>
        /// Méthode qui trouve le plus court chemin entre deux sommets dans un graphe
        /// </summary>
        /// <param name="graphe">Le graphe dans lequel chercher le chemin</param>
        /// <param name="depart">Le sommet de départ</param>
        /// <param name="arrivee">Le sommet d'arrivée</param>
        /// <returns>Un objet représentant le résultat du plus court chemin</returns>
        public static (List<int>, double) CheminPlusCourt(Graphe<T> graphe, int depart, int arrivee)
        {
            if (!graphe.Noeuds.ContainsKey(depart) || !graphe.Noeuds.ContainsKey(arrivee))
            {
                throw new ArgumentException("Les sommets de départ ou d'arrivée n'existent pas dans le graphe.");
            }

            var distances = new Dictionary<int, double>();
            var predecesseurs = new Dictionary<int, int?>();
            var noeudsNonVisites = new HashSet<int>();

            foreach (var noeud in graphe.Noeuds.Keys)
            {
                distances[noeud] = double.PositiveInfinity;
                predecesseurs[noeud] = null;
                noeudsNonVisites.Add(noeud);
            }

            distances[depart] = 0;

            while (noeudsNonVisites.Count > 0)
            {
                int noeudCourantId = noeudsNonVisites
                    .OrderBy(n => distances[n])
                    .First();

                if (noeudCourantId == arrivee)
                    break;

                noeudsNonVisites.Remove(noeudCourantId);

                var noeudCourant = graphe.Noeuds[noeudCourantId];
                foreach (var lien in noeudCourant.Liens)
                {
                    int voisin = lien.LienArrivee.Noeud_id;

                    if (noeudsNonVisites.Contains(voisin))
                    {
                        double distanceAlternative = distances[noeudCourantId] + lien.LienPoids;

                        if (distanceAlternative < distances[voisin])
                        {
                            distances[voisin] = distanceAlternative;
                            predecesseurs[voisin] = noeudCourantId;
                        }
                    }
                }
            }

            var chemin = new List<int>();
            int? etapeCourante = arrivee;
            while (etapeCourante != null)
            {
                chemin.Insert(0, etapeCourante.Value);
                etapeCourante = predecesseurs[etapeCourante.Value];
            }

            return (chemin, distances[arrivee]);
        }
    }
}