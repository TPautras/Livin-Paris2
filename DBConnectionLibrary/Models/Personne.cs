namespace SqlConnector.Models
{
    public class Personne
    {
        public string PersonneEmail { get; set; }
        public string PersonneNom { get; set; }
        public string PersonnePrenom { get; set; }
        public string PersonneVille { get; set; }
        public int PersonneCodePostale { get; set; }
        public string PersonneNomDeLaRue { get; set; }
        public int PersonneNumeroDeLaRue { get; set; }
        public string PersonneTelephone { get; set; }
        public string PersonneStationDeMetroLaPlusProche { get; set; }
    }
}