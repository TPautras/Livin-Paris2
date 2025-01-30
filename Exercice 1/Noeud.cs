using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    public class Noeud<T> where T : class
    {
        T Noeud_valeur { get; set; }
        public int Noeud_id { get; set; }
        public List<Lien<T>> Liens { get; set; }

        public Noeud(int id) 
        { 
            this.Noeud_id = id;
            this.Liens = new List<Lien<T>>();
        }
        public Noeud(int id, T valeur)
        {
            this.Noeud_id = id;
            this.Noeud_valeur = valeur;
            this.Liens = new List<Lien<T>>();
        }

        public override string ToString()
        {
            string res = $"L'id de ce noeud est : {Noeud_id}\n";
            if(Noeud_valeur != null)
            {
                res += $"Sa valeur est {Noeud_valeur}\n";
            }
            foreach (Lien<T> l in this.Liens) { res += l.ToString() + "\n";}
            return res;
        }
    }
}
