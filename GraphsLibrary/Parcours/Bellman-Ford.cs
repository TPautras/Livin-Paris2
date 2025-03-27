using System;
using System.Collections.Generic;

namespace Graphs.Parcours
{
    /// <summary>
    /// Classe implémentant l'algorithme de Bellman-Ford pour trouver le plus court chemin dans un graphe
    /// </summary>
    public class BellmanFord<T>
    {
        /// <summary>
        /// Méthode qui trouve le plus court chemin entre deux sommets dans un graphe en utilisant l'algorithme de Bellman-Ford
        /// </summary>
        /// <param name="graphe">Le graphe dans lequel chercher le chemin</param>
        /// <param name="depart">Le sommet de départ</param>
        /// <param name="arrivee">Le sommet d'arrivée</param>
        /// <returns>Un objet représentant le résultat du plus court chemin</returns>
        public static (List<int>, double) CheminPlusCourt(Graphe<T> graphe, int depart, int arrivee)
        {
            // Vérifier que les sommets existent dans le graphe
            if (!graphe.Noeuds.ContainsKey(depart) || !graphe.Noeuds.ContainsKey(arrivee))
            {
                throw new ArgumentException("Les sommets de départ ou d'arrivée n'existent pas dans le graphe.");
            }

            // Initialisation des distances
            var distances = new Dictionary<int, double>();
            var predecesseurs = new Dictionary<int, int?>();

            // Initialiser toutes les distances à l'infini sauf le noeud de départ
            foreach (var noeud in graphe.Noeuds.Keys)
            {
                distances[noeud] = double.PositiveInfinity;
                predecesseurs[noeud] = null;
            }
            distances[depart] = 0;

            // Nombre de nœuds dans le graphe
            int nombreNoeuds = graphe.Noeuds.Count;

            // Liste de tous les liens du graphe
            var tousLesLiens = new List<Lien<T>>();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                tousLesLiens.AddRange(noeud.Liens);
            }

            // Relaxation des arêtes
            for (int i = 1; i < nombreNoeuds; i++)
            {
                bool modificationEffectuee = false;

                foreach (var lien in tousLesLiens)
                {
                    int source = lien.LienDepart.Noeud_id;
                    int destination = lien.LienArrivee.Noeud_id;
                    double poids = lien.LienPoids;

                    // Relaxation de l'arête
                    if (distances[source] != double.PositiveInfinity && 
                        distances[source] + poids < distances[destination])
                    {
                        distances[destination] = distances[source] + poids;
                        predecesseurs[destination] = source;
                        modificationEffectuee = true;
                    }
                }

                // Si aucune modification n'a été effectuée, on peut s'arrêter
                if (!modificationEffectuee)
                    break;
            }

            // Vérification des cycles négatifs
            foreach (var lien in tousLesLiens)
            {
                int source = lien.LienDepart.Noeud_id;
                int destination = lien.LienArrivee.Noeud_id;
                double poids = lien.LienPoids;

                if (distances[source] != double.PositiveInfinity && 
                    distances[source] + poids < distances[destination])
                {
                    throw new InvalidOperationException("Le graphe contient un cycle négatif.");
                }
            }

            // Vérifier si un chemin existe
            if (distances[arrivee] == double.PositiveInfinity)
            {
                throw new InvalidOperationException("Aucun chemin n'existe entre les sommets spécifiés.");
            }

            // Reconstituer le chemin
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
