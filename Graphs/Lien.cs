namespace Graphs
{
    public class Lien<T>
    {
        public Noeud<T> LienDepart { get; set; }
        public Noeud<T> LienArrivee { get; set; }
        public double LienPoids { get; set; }

        public Lien(Noeud<T> depart, Noeud<T> arrivee, double poids)
        {
            LienDepart = depart;
            LienArrivee = arrivee;
            LienPoids = poids;
        }

        public override string ToString()
        {
            return $"{LienDepart.Noeud_id} -> {LienArrivee.Noeud_id} (Poids: {LienPoids})";
        }
    }
}
