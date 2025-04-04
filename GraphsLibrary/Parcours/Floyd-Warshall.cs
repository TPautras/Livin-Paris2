
using System;
using System.Collections.Generic;

namespace Graphs.Parcours
{
    public class FloydWarshall<T>
    {
        private const double Infinity = double.MaxValue;
        private readonly Graphe<T> _graphe;
        private double[,] _distances;
        private int[,] _predecesseurs;
        private int _nbNoeuds;

        /// <summary>
        /// Constructeur de la classe Floyd-Warshall
        /// </summary>
        /// <param name="graphe">graphe sur lequel on applique l'algorithme</param>
        public FloydWarshall(Graphe<T> graphe)
        {
            _graphe = graphe;
            _nbNoeuds = graphe.Ordre();
            
            InitialiserMatrices();
            
            CalculerCheminsPlusCourts();
        }

        /// <summary>
        /// Initialise les matrices de distances et de prédécesseurs
        /// </summary>
        private void InitialiserMatrices()
        {
            _distances = new double[_nbNoeuds + 1, _nbNoeuds + 1];
            _predecesseurs = new int[_nbNoeuds + 1, _nbNoeuds + 1];

            for (int i = 1; i <= _nbNoeuds; i++)
            {
                for (int j = 1; j <= _nbNoeuds; j++)
                {
                    if (i == j)
                        _distances[i, j] = 0; 
                    else
                        _distances[i, j] = Infinity; 
                    
                    _predecesseurs[i, j] = -1; 
                }
            }

            foreach (var noeud in _graphe.Noeuds.Values)
            {
                foreach (var lien in noeud.Liens)
                {
                    int source = lien.LienDepart.Noeud_id;
                    int destination = lien.LienArrivee.Noeud_id;
                    double poids = lien.LienPoids;

                    _distances[source, destination] = poids;
                    _predecesseurs[source, destination] = source;
                }
            }
        }

        /// <summary>
        /// Calcule tous les chemins les plus courts entre toutes les paires de sommets
        /// </summary>
        private void CalculerCheminsPlusCourts()
        {
            // Algorithme de Floyd-Warshall
            for (int k = 1; k <= _nbNoeuds; k++)
            {
                for (int i = 1; i <= _nbNoeuds; i++)
                {
                    for (int j = 1; j <= _nbNoeuds; j++)
                    {
                        if (_distances[i, k] != Infinity && _distances[k, j] != Infinity &&
                            _distances[i, k] + _distances[k, j] < _distances[i, j])
                        {
                            _distances[i, j] = _distances[i, k] + _distances[k, j];
                            _predecesseurs[i, j] = _predecesseurs[k, j];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Trouve le plus court chemin entre deux sommets
        /// </summary>
        /// <param name="sourceId">ID du sommet de départ</param>
        /// <param name="destinationId">ID du sommet d'arrivée</param>
        /// <returns>Tuple contenant la distance et la liste des noeuds formant le chemin</returns>
        public (double distance, List<int> chemin) TrouverPlusCourtChemin(int sourceId, int destinationId)
        {
            if (!_graphe.Noeuds.ContainsKey(sourceId) || !_graphe.Noeuds.ContainsKey(destinationId))
            {
                throw new ArgumentException("Les noeuds source ou destination n'existent pas dans le graphe.");
            }

            if (_distances[sourceId, destinationId] == Infinity)
            {
                return (Infinity, new List<int>());
            }

            List<int> chemin = new List<int>();
            ReconstituerChemin(sourceId, destinationId, chemin);
            chemin.Add(destinationId);

            return (_distances[sourceId, destinationId], chemin);
        }

        /// <summary>
        /// Reconstitue le chemin à partir de la matrice des prédécesseurs
        /// </summary>
        /// <param name="source">ID du sommet de départ</param>
        /// <param name="destination">ID du sommet d'arrivée</param>
        /// <param name="chemin">Liste des noeuds du chemin</param>
        private void ReconstituerChemin(int source, int destination, List<int> chemin)
        {
            if (source == destination)
            {
                return;
            }

            int predecesseur = _predecesseurs[source, destination];
            if (predecesseur == -1)
            {
                return;
            }

            ReconstituerChemin(source, predecesseur, chemin);
            chemin.Add(predecesseur);
        }

        /// <summary>
        /// Affiche la matrice des distances entre tous les sommets
        /// </summary>
        /// <returns>Une chaîne représentant la matrice des distances</returns>
        public string AfficherMatriceDistances()
        {
            string result = "Matrice des distances (Floyd-Warshall):\n";
            for (int i = 1; i <= _nbNoeuds; i++)
            {
                for (int j = 1; j <= _nbNoeuds; j++)
                {
                    if (_distances[i, j] == Infinity)
                        result += "∞ ";
                    else
                        result += $"{_distances[i, j]} ";
                }
                result += "\n";
            }
            return result;
        }

        /// <summary>
        /// Affiche tous les plus courts chemins entre toutes les paires de sommets
        /// </summary>
        /// <returns>Une chaîne représentant tous les plus courts chemins</returns>
        public string AfficherTousLesChemins()
        {
            string result = "Tous les plus courts chemins:\n";
            for (int i = 1; i <= _nbNoeuds; i++)
            {
                for (int j = 1; j <= _nbNoeuds; j++)
                {
                    if (i != j)
                    {
                        (double distance, List<int> chemin) = TrouverPlusCourtChemin(i, j);
                        result += $"De {i} à {j}: ";
                        if (distance == Infinity)
                        {
                            result += "Pas de chemin\n";
                        }
                        else
                        {
                            result += $"Distance = {distance}, Chemin = {string.Join(" -> "+ chemin)}\n";
                        }
                    }
                }
            }
            return result;
        }
    }
    
}