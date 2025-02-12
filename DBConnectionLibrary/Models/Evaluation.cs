namespace SqlConnector.Models
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public int? EvaluationClient { get; set; }
        public int? EvaluationCuisinier { get; set; }
        public int? CommandeId { get; set; }
        public Clients Client { get; set; }
        public Cuisinier Cuisinier { get; set; }
        public Commande Commande { get; set; }
    }
}