using System;

namespace SqlConnector.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public int? EntrepriseId { get; set; }
        public string CuisinierUsername { get; set; }
        public string ClientUsername { get; set; }
        public DateTime DateCreation { get; set; }
    }
}