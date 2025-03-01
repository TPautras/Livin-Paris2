using System;
using System.Collections.Generic;
using System.IO;

namespace Graphs
{
    public class Graphe<T>
    {
        /// <summary>
        /// Fonction qui donne l'accès en lecture et en écriture aux paramètres de la classe
        /// </summary>
        public Dictionary<int, Noeud<T>> Noeuds { get; set; } = new Dictionary<int, Noeud<T>>();
        /// <summary>
        /// Constructeur de la classe Graphe
        /// </summary>
        /// <param name="path">variable chemin</param>
        /// <param name="divider">variable diviseur</param>
        /// <param name="maxCount">variable nombreMax</param>
        
        private bool IsOriented { get; set; }= false;

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
        /// Fonction qui permet d'ajouter un Noeuds
        /// </summary>
        /// <param name="id">identifiant du noeud</param>
        /// <param name="valeur">valeur du noeud</param>
        public void AjouterNoeud(int id, T valeur = default)
        {
            if (!Noeuds.ContainsKey(id))
            {
                Noeuds[id] = new Noeud<T>(id, valeur);
            }
        }
        /// <summary>
        /// Fonction qui permet d'ajouter un lien
        /// </summary>
        /// <param name="idSource">Source du lien</param>
        /// <param name="idDestination">destination du lien</param>
        /// <param name="poids">poids du lien</param>
        public void AjouterLien(int idSource, int idDestination, double poids = 1)
        {
            if (Noeuds.ContainsKey(idSource) && Noeuds.ContainsKey(idDestination))
            {
                Noeuds[idSource].Liens.Add(new Lien<T>(Noeuds[idSource], Noeuds[idDestination], poids));
            }
        }
        #region Infos
        /// <summary>
        /// Fonction qui permet de donner le nom des propriétés du graphe
        /// </summary>
        /// <returns>renvoie le nom des propriétés du graphe</returns>
        public override string ToString()
        {
            string res = "";
            foreach (var noeud in Noeuds.Values)
            {
                res += noeud.ToString() + "\n";
            }
            res += "\n\n\n";
            res += "Les propriétés du graphe sont :\n";
            if (this.Connexe())
            {
                res += "Le graphe est connexe\n";
            }
            else
            {
                res += "Le graphe n'est pas connexe\n";
            }
            res += $"L'ordre du graphe est {Convert.ToString(this.Ordre())}\n";
            res += $"La taille du graphe est {Convert.ToString(this.Taille())}\n";
            if (this.Pondere())
            {
                res += "Le graphe est pondéré\n";
            }
            else
            {
                res += "Le graphe n'est pas pondéré\n";
            }
            return res;
        }
        /// <summary>
        /// Fonction qui définit si un graphe est connexe
        /// </summary>
        /// <returns>renvoie si oui ou non le graphe est connexe</returns>
        public bool Connexe()
        {
            bool res = true;
            foreach(var Noeud in  Noeuds.Values)
            {
                if(Noeud.Liens.Count == 0)
                    res = false;
            }
            return res;
        }
        /// <summary>
        /// Fonction qui permet de compter le nombre de neuds du graphe
        /// </summary>
        /// <returns>renvoie le nombre de neuds du graphe</returns>
        public int Ordre()
        {
            return Noeuds.Count;
        }
        /// <summary>
        /// Fonction qui permet de compter le nombre de lien du graphe
        /// </summary>
        /// <returns>renvoie le nombre de lien du graphe</returns>
        public int Taille()
        {
            int res = 0;
            foreach (var Noeud in Noeuds.Values)
            {
                    res += Noeud.Liens.Count;
            }
            return res;
        }
        /// <summary>
        /// Fonction qui définit si un graphe est pondéré
        /// </summary>
        /// <returns>renvoie si oui ou non le graphe est pondéré</returns>
        public bool Pondere()
        {
            bool res = false;
            foreach (var Noeud in Noeuds.Values)
            {
                foreach(var Lien in Noeud.Liens)
                {
                    if(Lien.LienPoids != 1)
                        res = true;
                }
            }
            return res;
        }
        #endregion

        #region Parcours
        /// <summary>
        /// Fonction qui permet d'effectuer un parcours en largeur (BFS) 
        /// </summary>
        /// <param name="startIndex">noeud de départ pour commencer le parcours en largeur</param>
        public void BFS(int startIndex)
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

            Console.WriteLine("Parcours en largeur (BFS) :");

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
        /// <summary>
        /// méthode de parcours de graph DFS
        /// </summary>
        /// <param name="startIndex"></param>L'indice du noeud de départ pour commencer le parcours des différents noeuds
        public void DFS(int startIndex)
        {
            if (!Noeuds.ContainsKey(startIndex))
            {
                Console.WriteLine("Le nœud de départ n'existe pas dans le graphe.");
                return;
            }

            HashSet<int> visited = new HashSet<int>();
            Stack<Noeud<T>> stack = new Stack<Noeud<T>>();

            stack.Push(Noeuds[startIndex]);

            Console.WriteLine("Parcours en profondeur (DFS) :");

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

        
        /// <summary>
        /// methode qui construit la matrice Adjacente en prenant en considération si le graphe est pondéré ou non.
        /// </summary>
        /// <returns>
        /// la matrice adjacente double ainsi créee en string 
        /// </returns>
        public string MatriceAdjacence()
        {
            int taille = this.Noeuds.Count;
            double[,] result = new double[taille, taille];
            string a = "";

            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    result[i, j] = 0;
                }
            }
            bool estPondere = this.Pondere();

            foreach (Noeud<T> noeud in Noeuds.Values)
            {
                foreach (Lien<T> lien in noeud.Liens)
                {
                    int idDepart= noeud.Noeud_id-1;
                    int idArrivee = lien.LienArrivee.Noeud_id-1;

                    double valeur = 1;
                    if (estPondere == true)
                    {
                        valeur = lien.LienPoids;
                    }
                    result[idDepart, idArrivee] = valeur;

                    if (IsOriented == false)
                    {
                        result[idArrivee, idDepart] = valeur;
                    }
                }
            }
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    a= a+result[i, j] + " ";
                }

                a= a + "\n";
            }
            return a;
        }
        /// <summary>
        /// méthode qui construit la liste d'ajdacence d'un graph
        /// </summary>
        /// <returns>
        /// une string comportant la liste d'adjacence
        /// </returns>
        public string ListeAdjacence()
        {
            int taille = this.Noeuds.Count;
            bool estPondere = false;
            if (Pondere() == true)
            {
                estPondere = true;
            } 
            string resultat = "";

            if (estPondere == true)
            {
                if (IsOriented == true)
                {
                    resultat +="Liste d'adjacence (Pondérée, Orientée) : \n";
                }
                else
                {
                    resultat += "Liste d'adjacence (Pondérée,Non Orientée) : \n";
                }
            }
            else
            {
                if (IsOriented == true)
                {
                    resultat += "Liste d'adjacence (Non pondérée, Orientée): \n";
                }
                else
                {
                    resultat += "Liste d'adjacence (Non pondérée, Non Orientée): \n";
                }
            }

            foreach (Noeud<T> noeud in this.Noeuds.Values)
            {
                resultat += "Noeud" + noeud.Noeud_id + "--->";

                foreach (Lien<T> lien in noeud.Liens)
                {
                    int indexDepart = noeud.Noeud_id;
                    int indexVoisin = lien.LienArrivee.Noeud_id;
                    
                    double poids = 1;
                    if (estPondere == true)
                    {
                        poids = lien.LienPoids;
                    
                        resultat += " " + indexVoisin + ", Poids: " + poids + " ";
                    }
                    else
                    {
                        if(IsOriented == true)
                            resultat += " " + indexVoisin ;
                        else
                        {
                            resultat += " " + indexDepart;
                            resultat += " " + indexVoisin;
                        }
                    }

                   
                    
                    
                }

                resultat += "\n";
                
            }
            return resultat;
        }
        #endregion Modes d'affichage'
    }
}

