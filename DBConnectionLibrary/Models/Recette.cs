using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Recette: ILpModels<Recette>
    {
        public int RecetteId { get; set; }
        public string RecetteNom { get; set; }
        public string RecetteOrigine { get; set; }
        public string RecetteTypeDePlat { get; set; }
        public string RecetteApportNutritifs { get; set; }
        public string RecetteRegimeAlimentaire { get; set; }
        public IDataAccess<Recette> DataAccess { get; } = new RecetteDataAccess();
        public IDataService<Recette> DataService { get; } = new RecetteService(new RecetteDataAccess());
    }
}