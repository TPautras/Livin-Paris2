﻿using System;
using System.Collections.Generic;

namespace Exercice_1
{
    public class Noeud<T>
    {
        public T Noeud_Valeur { get; set; }
        public int Noeud_id { get; set; }
        public List<Lien<T>> Liens { get; set; }

        public Noeud(int id, T valeur = default)
        {
            Noeud_id = id;
            Noeud_Valeur = valeur;
            Liens = new List<Lien<T>>();
        }

        public override string ToString()
        {
            string res = $"L'id de ce noeud est : {Noeud_id}\n";
            if (Noeud_Valeur != null)
            {
                res += $"Sa valeur est {Noeud_Valeur}\n";
            }
            foreach (Lien<T> l in Liens)
            {
                res += l.ToString() + "\n";
            }
            return res;
        }
    }
}
