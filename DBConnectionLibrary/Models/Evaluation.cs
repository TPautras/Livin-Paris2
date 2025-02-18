namespace SqlConnector.Models
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public decimal EvaluationClient { get; set; }
        public decimal EvaluationCuisinier { get; set; }
        public string EvaluationDescriptionClient { get; set; }
        public string EvaluationDescriptionCuisinier { get; set; }
        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
    }
}