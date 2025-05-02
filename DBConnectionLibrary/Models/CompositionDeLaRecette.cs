using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class CompositionDeLaRecette: ILpModels<CompositionDeLaRecette>
    {
        public int IngredientId { get; set; }
        public int RecetteId { get; set; }
        public IDataAccess<CompositionDeLaRecette> DataAccess { get; } = new CompositionDeLaRecetteDataAccess();

        public IDataService<CompositionDeLaRecette> DataService { get; } =
            new CompositionDeLaRecetteService(new CompositionDeLaRecetteDataAccess());
    }
}