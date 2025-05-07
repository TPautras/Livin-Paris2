using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Parcours
{
    public class AStar<T>
    {
        private readonly Graphe<T> _graphe;
        
        /// <summary>
        /// Constructeur de la classe A*
        /// </summary>
        /// <param name="graphe">graphe sur lequel on applique l'algorithme</param>
        public AStar(Graphe<T> graphe)
        {
            _graphe = graphe;
        }

        /// <summary>
        /// Permet de trouver le plus court chemin entre deux sommets en utilisant l'algorithme A*
        /// </summary>
        /// <param name="sourceId">ID du sommet de départ</param>
        /// <param name="destinationId">ID du sommet d'arrivée</param>
        /// <returns>Tuple contenant la distance totale et la liste des noeuds formant le chemin</returns>
        public (double distance, List<int> chemin) TrouverChemin(int sourceId, int destinationId)
        {
            if (!_graphe.Noeuds.ContainsKey(sourceId) || !_graphe.Noeuds.ContainsKey(destinationId))
            {
                throw new ArgumentException("Les noeuds source ou destination n'existent pas dans le graphe.");
            }

            var openSet = new PriorityQueue<int, double>();
            openSet.Enqueue(sourceId, 0);

            var closedSet = new HashSet<int>();

            var gScore = new Dictionary<int, double>();
            foreach (var node in _graphe.Noeuds.Keys)
            {
                gScore[node] = double.MaxValue;
            }
            gScore[sourceId] = 0;

            var fScore = new Dictionary<int, double>();
            foreach (var node in _graphe.Noeuds.Keys)
            {
                fScore[node] = double.MaxValue;
            }
            fScore[sourceId] = Heuristique(sourceId, destinationId);

            var cameFrom = new Dictionary<int, int>();

            while (openSet.Count > 0)
            {
                int current = openSet.Dequeue();

                if (current == destinationId)
                {
                    return (gScore[current], ReconstituerChemin(cameFrom, current));
                }

                closedSet.Add(current);

                foreach (var lien in _graphe.Noeuds[current].Liens)
                {
                    int neighborId = lien.LienArrivee.Noeud_id;

                    if (closedSet.Contains(neighborId))
                        continue;

                    double tentativeGScore = gScore[current] + lien.LienPoids;

                    if (tentativeGScore < gScore[neighborId])
                    {
                        cameFrom[neighborId] = current;
                        gScore[neighborId] = tentativeGScore;
                        fScore[neighborId] = gScore[neighborId] + Heuristique(neighborId, destinationId);

                        if (openSet.UnorderedItems.Select(x => x.Element).Contains(neighborId))
                        {
                            openSet.Enqueue(neighborId, fScore[neighborId]);
                        }
                        else
                        {
                            openSet.Enqueue(neighborId, fScore[neighborId]);
                        }
                    }
                }
            }

            return (double.MaxValue, new List<int>());
        }

        /// <summary>
        /// Fonction heuristique pour estimer la distance entre deux noeuds
        /// Pour un graphe général, on peut utiliser une estimation simple
        /// </summary>
        /// <param name="nodeId">ID du noeud de départ</param>
        /// <param name="goalId">ID du noeud d'arrivée</param>
        /// <returns>Estimation de la distance</returns>
        private double Heuristique(int nodeId, int goalId)
        {
            return 1;
        }

        /// <summary>
        /// Reconstitue le chemin à partir du dictionnaire des prédécesseurs
        /// </summary>
        /// <param name="cameFrom">Dictionnaire des prédécesseurs</param>
        /// <param name="current">Noeud courant (destination)</param>
        /// <returns>Liste des noeuds du chemin</returns>
        private List<int> ReconstituerChemin(Dictionary<int, int> cameFrom, int current)
        {
            var chemin = new List<int> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                chemin.Insert(0, current);
            }
            return chemin;
        }
    }

    /// <summary>
    /// Classe de file de priorité personnalisée pour gérer les noeuds de l'algorithme A*
    /// </summary>
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly List<(TElement Element, TPriority Priority)> _elements = new List<(TElement, TPriority)>();

        public int Count => _elements.Count;

        public IEnumerable<(TElement Element, TPriority Priority)> UnorderedItems => _elements;

        public void Enqueue(TElement element, TPriority priority)
        {
            _elements.Add((element, priority));
        }

        public TElement Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i].Priority.CompareTo(_elements[bestIndex].Priority) < 0)
                {
                    bestIndex = i;
                }
            }

            TElement bestElement = _elements[bestIndex].Element;
            _elements.RemoveAt(bestIndex);
            return bestElement;
        }
    }
}