using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    public class Noeud
    {
        public int Noeud_id { get; set; }
        public List<Lien> Liens { get; set; }

        public Noeud(int id) 
        { 
            this.Noeud_id = id;
            this.Liens = new List<Lien>();
        }

        public override string ToString()
        {
            string res = $"L'id de ce noeud est : {Noeud_id}\n";
            foreach (Lien l in this.Liens) { res += l.ToString() + "\n";}
            return res;
        }
    }
}
