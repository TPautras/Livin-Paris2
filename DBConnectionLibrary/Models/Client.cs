namespace SqlConnector.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientPassword { get; set; }
        public int PersonneId { get; set; }
        public Personne Personne { get; set; }
    }
}