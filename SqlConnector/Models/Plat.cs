using System;

namespace SqlConnector
{
    public class Plat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
        public int Personnes { get; set; }
        public DateTime DateFabrication { get; set; } = DateTime.Today;
        public DateTime DatePeremption { get; set; } = DateTime.Today;
        public decimal Prix { get; set; }
        public string Nationalite { get; set; }
        public string RegimeAlimentaire { get; set; }
        public string Ingredients { get; set; }
        public int CuisinierId { get; set; }
    }
}