using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    public class Lien
    {
        public Noeud Lien_Depart {  get; set; }
        public Noeud Lien_Arrivee { get; set; }
        public double Lien_Poids {  get; set; }

        public Lien(Noeud depart, Noeud arrivee, double poids)
        {
            Lien_Depart = depart;
            Lien_Arrivee = arrivee;
            Lien_Poids = poids;
        }

        public override string ToString()
        {
            return $"{Lien_Depart.Noeud_id} -> {Lien_Arrivee.Noeud_id} (Poids: {Lien_Poids})";
        }
    }
}
