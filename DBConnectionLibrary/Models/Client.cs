namespace SqlConnector.Models
{
    public class Client
    {
        public string ClientUsername { get; set; }
        public string ClientPassword { get; set; }
        public string PersonneEmail { get; set; }
        public Personne Personne { get; set; }
    }
}