using System;
using System.Collections.Generic;
using System.IO;

namespace Exercice_1
{
    public class Graphe<T>
    {
        public Dictionary<int, Noeud<T>> Noeuds { get; set; } = new Dictionary<int, Noeud<T>>();

        public Graphe(string path, char divider, int maxCount)
        {
            try
            {
                var lines = File.ReadLines(path);
                for (int i = 0; i < maxCount; i++)
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

        public override string ToString()
        {
            string res = "";
            foreach (var noeud in Noeuds.Values)
            {
                res += noeud.ToString() + "\n";
            }
            return res;
        }
    }
}
