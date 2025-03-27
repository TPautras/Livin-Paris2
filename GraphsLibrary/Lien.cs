namespace Graphs
{
    public class Lien<T>
    {
        /// <summary>
        /// Donne l'accès en lecture et en écriture au lienDepart
        /// </summary>
        public Noeud<T> LienDepart { get; set; }
        /// <summary>
        /// Donne l'accès en lecture et en ecriture au lienArrive
        /// </summary>
        public Noeud<T> LienArrivee { get; set; }
        /// <summary>
        /// Donne l'accès en lecture et en ecriture au lienPoids
        /// </summary>
        public double LienPoids { get; set; }
        /// <summary>
        /// Constructeur de la classe Lien
        /// </summary>
        /// <param name="depart">variable du lien de départ</param>
        /// <param name="arrivee">variable du lien d'arrivé</param>
        /// <param name="poids">variable du lien du poids</param>
        public Lien(Noeud<T> depart, Noeud<T> arrivee, double poids)
        {
            LienDepart = depart;
            LienArrivee = arrivee;
            LienPoids = poids;
        }
        /// <summary>
        /// Affichage d'un lien
        /// </summary>
        /// <returns>renvoie l'affichage d'un lien avec son point de départ, d'arrivé et son poids</returns>
        public override string ToString()
        {
            return $"{LienDepart.Noeud_id} -> {LienArrivee.Noeud_id} (Poids: {LienPoids})";
        }

        
    }
}
