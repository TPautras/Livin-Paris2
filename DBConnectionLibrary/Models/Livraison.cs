using System;
namespace SqlConnector.Models
{
    public class Livraison
    {
        public int LivraisonId { get; set; }
        public string LivraisonAdresse { get; set; }
        public DateTime? LivraisonDate { get; set; }
    }
}