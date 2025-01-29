using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    internal class Graphe
    {
        public Dictionary<int,Noeud> Noeuds { get; set; } = new Dictionary<int, Noeud>();

        public Graphe(string path, char divider) 
        {
            
        }
        public void AjouterNoeud(int id)
        {
            if (!Noeuds.ContainsKey(id))
            {
                Noeuds[id] = new Noeud(id);
            }
        }

        public void AjouterLien(int idSource, int idDestination, double poids = 1)
        {
            if (Noeuds.ContainsKey(idSource) && Noeuds.ContainsKey(idDestination))
            {
                Lien lien = new Lien(Noeuds[idSource], Noeuds[idDestination], poids);
                Noeuds[idSource].Liens.Add(lien);
            }
        }
    }
}
