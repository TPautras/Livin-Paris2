using System;

namespace SqlConnector
{
    public class Commande
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal PrixTotal { get; set; }
        public string Statut { get; set; }
    }
}