namespace SqlConnector.Models
{
    public class Contient
    {
        public int CommandeId { get; set; }
        public int PlatId { get; set; }
        public Commande Commande { get; set; }
        public Plat Plat { get; set; }
    }
}