namespace SqlConnector.Models
{
    public class Cuisinier
    {
        public int CuisinierId { get; set; }
        public string CuisinierPassword { get; set; }
        public string PersonneId { get; set; }
        public Personne Personne { get; set; }
    }
}