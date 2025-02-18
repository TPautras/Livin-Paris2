using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public int EntrepriseId { get; set; }
        public int CuisinierId { get; set; }
        public int ClientId { get; set; }
        public Entreprise Entreprise { get; set; }
        public Cuisinier Cuisinier { get; set; }
        public Client Client { get; set; }
    }
}