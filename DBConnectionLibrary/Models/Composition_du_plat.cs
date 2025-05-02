using SqlConnector.DataService;
using SqlConnector.DataServices;

namespace SqlConnector.Models
{
    public class CompositionDuPlat: ILpModels<CompositionDuPlat>
    {
        public int PlatId { get; set; }
        public int IngredientId { get; set; }
        public Plat Plat { get; set; }
        public Ingredient Ingredient { get; set; }
        public IDataAccess<CompositionDuPlat> DataAccess { get; } = new CompositionDuPlatDataAccess();
        public IDataService<CompositionDuPlat> DataService { get; } = new CompositionDuPlatService();
    }
}