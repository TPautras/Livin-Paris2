using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Plat
    {
        public int PlatId { get; set; }
        public string PlatNom { get; set; }
        public string PlatOrigine { get; set; }
        public string PlatAromesNaturels { get; set; }
        public DateTime? PlatDateDeFabrication { get; set; }
        public DateTime? PlatDateDePeremption { get; set; }
        public string PlatTypeDePlat { get; set; }
        public string PlatRegimeAlimentaire { get; set; }
        public ICollection<Creation> Creations { get; set; }
        public ICollection<Contient> Contients { get; set; }
        public ICollection<CompositionDuPlat> CompositionDuPlat { get; set; }
    }
}