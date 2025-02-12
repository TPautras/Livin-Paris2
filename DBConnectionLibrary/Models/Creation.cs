namespace SqlConnector.Models
{
    public class Creation
    {
        public int CuisinierId { get; set; }
        public int PlatId { get; set; }
        public Cuisinier Cuisinier { get; set; }
        public Plat Plat { get; set; }
    }
}