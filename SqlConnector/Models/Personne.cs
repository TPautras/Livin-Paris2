namespace SqlConnector.Models
{
    public class Personne
    {
        public int PersonneId { get; set; }
        public string PersonneNom { get; set; }
        public string PersonnePrenom { get; set; }
        public string PersonneNumeroDeLicence { get; set; }
        public string PersonneVille { get; set; }
        public string PersonneCodepostale { get; set; }
        public string PersonneNomDeLaRue { get; set; }
        public string PersonneEmail { get; set; }
        public string PersonneTelephone { get; set; }
        public string PersonneStationDeMetroLaPlusProche { get; set; }
    }
}