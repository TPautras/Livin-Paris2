using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    public class Graphe<T> where T : class
    {
        public Dictionary<int,Noeud<T>> Noeuds { get; set; } = new Dictionary<int, Noeud<T>>();

        public Graphe(string path, char divider, int maxCount) 
        {
            int counter = 0;
            IEnumerable<string> lines = null;
            try
            {
                lines = File.ReadLines(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            for(int i = 0; i < maxCount; i++) 
            {
                AjouterNoeud(i);
            }
            foreach (var line in lines)
            {
                var relation = line.Split(divider);
                if(relation.Length == 2)
                {
                    AjouterLien(Convert.ToInt32(relation[0]), Convert.ToInt32(relation[1]));
                }
                if (relation.Length == 3)
                {
                    AjouterLien(Convert.ToInt32(relation[0]), Convert.ToInt32(relation[1]), Convert.ToInt32(relation[2]));
                }
                counter++;
            }

        }
        public void AjouterNoeud(int id)
        {
            if (!Noeuds.ContainsKey(id))
            {
                Noeuds[id] = new Noeud<T>(id);
            }
        }

        public void AjouterLien(int idSource, int idDestination, double poids = 1)
        {
            Noeud<T> source = Noeuds[idSource];
            Noeud<T> destination = Noeuds[idDestination];
            if (Noeuds.ContainsKey(idSource) && Noeuds.ContainsKey(idDestination))
            {
                source.Liens.Add(new Lien<T>(source, destination, poids));
            }
        }

        public override string ToString()
        {
            string res = "";
            foreach(var Noeud in this.Noeuds)
            {
                res += Noeud.ToString() + "\n";
            }
            return res;
        }
    }
}
