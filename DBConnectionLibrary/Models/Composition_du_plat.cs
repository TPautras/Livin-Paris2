namespace SqlConnector.Models
{
    public class CompositionDuPlat
    {
        public int PlatId { get; set; }
        public int IngredientId { get; set; }
        public Plat Plat { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}