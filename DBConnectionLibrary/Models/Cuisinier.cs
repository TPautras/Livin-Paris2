namespace SqlConnector.Models
{
    public class Cuisinier
    {
        public string CuisinierUsername { get; set; }
        public string CuisinierPassword { get; set; }
        public string PersonneEmail { get; set; }
        public Personne Personne { get; set; }
    }
}