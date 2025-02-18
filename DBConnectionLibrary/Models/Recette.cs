namespace SqlConnector.Models
{
    public class Recette
    {
        public int RecetteId { get; set; }
        public string RecetteNom { get; set; }
        public string RecetteOrigine { get; set; }
        public string RecetteTypeDePlat { get; set; }
        public string RecetteApportNutritifs { get; set; }
        public string RecetteRegimeAlimentaire { get; set; }
    }
}