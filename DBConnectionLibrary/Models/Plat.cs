using System;
namespace SqlConnector.Models
{
    public class Plat
    {
        public int PlatId { get; set; }
        public DateTime PlatDateDeFabrication { get; set; }
        public DateTime PlatDateDePeremption { get; set; }
        public string PlatPrix { get; set; }
        public int PlatNombrePortion { get; set; }
        public string CuisinierUsername { get; set; }
        public int RecetteId { get; set; }
    }
}