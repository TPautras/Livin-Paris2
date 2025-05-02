namespace MetroHelper
{
    /// <summary>
    /// Classe qui permet de définir les correspondances. Elles sont définies entre 2 stations et sont reliée par un temps et identifiées par un id
    /// </summary>
    public class Correspondance
    {
        private Station_de_metro s1;
        private Station_de_metro s2;
        private int temps;
        private int id;
 

        public Correspondance(Station_de_metro s1, Station_de_metro s2, int temps, int id)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.temps = temps;
            this.id = id; 
        }

        public Station_de_metro S1
        {
            get { return s1; }
            set {this.s1 = value; }
        }
        public Station_de_metro S2
        {
            get { return s2; }
            set {this.s2 = value; }
        }
        public int Temps
        {
            get { return temps; }
            set {this.temps = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    
    }
}