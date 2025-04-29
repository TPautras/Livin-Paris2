using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Livre: ILpModels<Livre>
    {
        public int PlatId { get; set; }
        public int LivraisonId { get; set; }
        public IDataAccess<Livre> DataAccess { get; } = new LivreDataAccess();
        public IDataService<Livre> DataService { get; } = new LivreService(new LivreDataAccess());
    }
}