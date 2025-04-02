namespace MetroHelper
{
    public class Station_de_metro
    {
        private int id;
        private string nom;

        public Station_de_metro(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public int Id
        {
            get { return this.id;}
            set{this.id=value;}
        }

        public string Nom
        {
            get { return this.nom;}
            set{this.nom=value;}
        }
    
    }
}