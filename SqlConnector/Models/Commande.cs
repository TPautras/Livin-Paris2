using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime? CommandeDate { get; set; }
        public int? ClientId { get; set; }
        public Clients Client { get; set; }
        public ICollection<Contient> Contients { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
        public Livraison Livraison { get; set; }
    }
}