using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Graphs
{
    public class Graphe<T>
    {
        public Dictionary<int, Noeud<T>> Noeuds { get; set; } = new Dictionary<int, Noeud<T>>();

        private bool IsOriented { get; set; } = false;

        /// <summary>
        /// Constructeur classique : crée un graphe à partir d'un fichier CSV.
        /// </summary>
        public Graphe(string path, char divider, int maxCount)
        {
            try
            {
                var lines = File.ReadLines(path);
                for (int i = 1; i < maxCount; i++)
                {
                    AjouterNoeud(i);
                }
                foreach (var line in lines)
                {
                    var relation = line.Split(divider);
                    if (relation.Length >= 2)
                    {
                        int source = Convert.ToInt32(relation[0]);
                        int destination = Convert.ToInt32(relation[1]);
                        double poids = relation.Length == 3 ? Convert.ToDouble(relation[2]) : 1;
                        AjouterLien(source, destination, poids);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// NOUVEAU CONSTRUCTEUR : crée un graphe vide avec un nombre de noeuds donné (sans fichier).
        /// </summary>
        /// <param name="maxCount">Nombre de noeuds à créer</param>
        /// <param name="isOriented">Indique si le graphe est orienté</param>
        public Graphe(int maxCount, bool isOriented = false)
        {
            this.IsOriented = isOriented;
            for (int i = 1; i <= maxCount; i++)
            {
                AjouterNoeud(i);
            }
        }

        public List<Lien<T>> Liens
        {
            get
            {
                return Noeuds.Values.SelectMany(n => n.Liens).ToList();
            }
        }

        public void AjouterNoeud(int id, T valeur = default)
        {
            if (!Noeuds.ContainsKey(id))
            {
                Noeuds[id] = new Noeud<T>(id, valeur);
            }
        }

        public void AjouterLien(int idSource, int idDestination, double poids = 1)
        {
            if (Noeuds.ContainsKey(idSource) && Noeuds.ContainsKey(idDestination))
            {
                Noeuds[idSource].Liens.Add(new Lien<T>(Noeuds[idSource], Noeuds[idDestination], poids));
            }
        }

        #region Infos

        public override string ToString()
        {
            string res = "";
            foreach (var noeud in Noeuds.Values)
            {
                res += noeud.ToString() + "\n";
            }
            res += "\n\n\nLes propriétés du graphe sont :\n";
            res += this.Connexe() ? "Le graphe est connexe\n" : "Le graphe n'est pas connexe\n";
            res += $"L'ordre du graphe est {this.Ordre()}\n";
            res += $"La taille du graphe est {this.Taille()}\n";
            res += this.Pondere() ? "Le graphe est pondéré\n" : "Le graphe n'est pas pondéré\n";
            return res;
        }

        public string Boucle()
        {
            string res = "";
            bool boucle = false;
            List<string> parcours = new List<string>();
            foreach (var noeud in Noeuds.Values)
            {
                HashSet<int> visited = new HashSet<int>();
                Queue<Noeud<T>> queue = new Queue<Noeud<T>>();

                queue.Enqueue(Noeuds[noeud.Noeud_id]);
                visited.Add(noeud.Noeud_id);

                while (queue.Count > 0)
                {
                    Noeud<T> current = queue.Dequeue();

                    foreach (var lien in current.Liens)
                    {
                        if (!visited.Contains(lien.LienArrivee.Noeud_id))
                        {
                            queue.Enqueue(lien.LienArrivee);
                            visited.Add(lien.LienArrivee.Noeud_id);
                        }
                        else
                        {
                            boucle = true;
                            string ceParcours = "";
                            foreach (var n in visited)
                            {
                                ceParcours += $"{n} ";
                            }
                            ceParcours += lien.LienArrivee.Noeud_id;
                            parcours.Add(ceParcours);
                        }
                    }
                }
            }

            if (boucle)
            {
                res += "Le graphe a au moins une boucle\n";
                res += "Voici la liste contenant toutes les boucles trouvées \n";
                res += TrimmedBoucles(parcours);
            }
            return res;
        }

        public string TrimmedBoucles(List<string> boucles)
        {
            string res = "";
            foreach (var varParcour in boucles)
            {
                List<string> parcours = new List<string>();
                string[] indices = varParcour.Split(' ');
                Dictionary<string, int> seen = new Dictionary<string, int>();
                int debutCycle = -1, finCycle = -1;

                for (int i = 0; i < indices.Length; i++)
                {
                    if (seen.ContainsKey(indices[i]))
                    {
                        debutCycle = seen[indices[i]];
                        finCycle = i;
                        break;
                    }
                    else
                    {
                        seen.Add(indices[i], i);
                    }
                }

                if (debutCycle != -1 && finCycle != -1)
                {
                    string[] boucle = indices.Skip(debutCycle).Take(finCycle - debutCycle + 1).ToArray();
                    parcours.Add(string.Join(" ", boucle));
                }

                foreach (string id in parcours)
                {
                    res += id + " ";
                }
                res += "\n";
            }
            return res;
        }

        public bool Connexe()
        {
            bool res = true;
            foreach (var noeud in Noeuds.Values)
            {
                if (noeud.Liens.Count == 0)
                    res = false;
            }
            return res;
        }

        public int Ordre()
        {
            return Noeuds.Count;
        }

        public int Taille()
        {
            int res = 0;
            foreach (var noeud in Noeuds.Values)
            {
                res += noeud.Liens.Count;
            }
            return res;
        }

        public bool Pondere()
        {
            foreach (var noeud in Noeuds.Values)
            {
                if (noeud.Liens.Any(lien => lien.LienPoids != 1))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Parcours

        public void Bfs(int startIndex)
        {
            if (!Noeuds.ContainsKey(startIndex))
            {
                Console.WriteLine("Le nœud de départ n'existe pas dans le graphe.");
                return;
            }

            HashSet<int> visited = new HashSet<int>();
            Queue<Noeud<T>> queue = new Queue<Noeud<T>>();

            queue.Enqueue(Noeuds[startIndex]);
            visited.Add(startIndex);

            Console.WriteLine("Parcours en largeur (Bfs) :");

            while (queue.Count > 0)
            {
                Noeud<T> current = queue.Dequeue();
                Console.WriteLine($"Visite du nœud : {current.Noeud_id}");

                foreach (var lien in current.Liens)
                {
                    if (!visited.Contains(lien.LienArrivee.Noeud_id))
                    {
                        queue.Enqueue(lien.LienArrivee);
                        visited.Add(lien.LienArrivee.Noeud_id);
                    }
                }
            }
        }

        public void Dfs(int startIndex)
        {
            if (!Noeuds.ContainsKey(startIndex))
            {
                Console.WriteLine("Le nœud de départ n'existe pas dans le graphe.");
                return;
            }

            HashSet<int> visited = new HashSet<int>();
            Stack<Noeud<T>> stack = new Stack<Noeud<T>>();

            stack.Push(Noeuds[startIndex]);

            Console.WriteLine("Parcours en profondeur (Dfs) :");

            while (stack.Count > 0)
            {
                Noeud<T> current = stack.Pop();

                if (!visited.Contains(current.Noeud_id))
                {
                    visited.Add(current.Noeud_id);
                    Console.WriteLine($"Visite du nœud : {current.Noeud_id}");

                    foreach (var lien in current.Liens)
                    {
                        if (!visited.Contains(lien.LienArrivee.Noeud_id))
                        {
                            stack.Push(lien.LienArrivee);
                        }
                    }
                }
            }
        }

        #endregion

        #region Modes d'affichage

        public string MatriceAdjacence()
        {
            int taille = this.Noeuds.Count;
            double[,] result = new double[taille, taille];
            string a = "";

            for (int i = 0; i < taille; i++)
                for (int j = 0; j < taille; j++)
                    result[i, j] = 0;

            bool estPondere = this.Pondere();

            foreach (Noeud<T> noeud in Noeuds.Values)
            {
                foreach (Lien<T> lien in noeud.Liens)
                {
                    int idDepart = noeud.Noeud_id - 1;
                    int idArrivee = lien.LienArrivee.Noeud_id - 1;

                    double valeur = estPondere ? lien.LienPoids : 1;
                    result[idDepart, idArrivee] = valeur;

                    if (!IsOriented)
                    {
                        result[idArrivee, idDepart] = valeur;
                    }
                }
            }

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    a += result[i, j] + " ";
                }
                a += "\n";
            }
            return a;
        }

        public string ListeAdjacence()
        {
            int taille = this.Noeuds.Count;
            bool estPondere = this.Pondere();
            string resultat = "";

            if (estPondere)
            {
                resultat += IsOriented ? "Liste d'adjacence (Pondérée, Orientée) : \n" : "Liste d'adjacence (Pondérée, Non Orientée) : \n";
            }
            else
            {
                resultat += IsOriented ? "Liste d'adjacence (Non pondérée, Orientée) : \n" : "Liste d'adjacence (Non pondérée, Non Orientée) : \n";
            }

            foreach (Noeud<T> noeud in Noeuds.Values)
            {
                resultat += $"Noeud {noeud.Noeud_id} --->";
                foreach (var lien in noeud.Liens)
                {
                    resultat += estPondere ? $" {lien.LienArrivee.Noeud_id}, Poids: {lien.LienPoids}" : $" {lien.LienArrivee.Noeud_id}";
                }
                resultat += "\n";
            }

            return resultat;
        }

        public static bool LienExiste(Noeud<T> l1, Noeud<T> l2)
        {
            return l1.Liens.Any(l => l.LienArrivee == l2) || l2.Liens.Any(l => l.LienArrivee == l1);
        }

        #endregion
    }
}
