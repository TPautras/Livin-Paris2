using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Plat
    {
        public int PlatId { get; set; }
        public DateTime PlatDateDeFabrication { get; set; }
        public DateTime PlatDateDePeremption { get; set; }
        public string PlatPrix { get; set; }
        public int PlatNombrePortion { get; set; }
        public int CuisinierId { get; set; }
        public int RecetteId { get; set; }
        public Cuisinier Cuisinier { get; set; }
        public Recette Recette { get; set; }
    }
}