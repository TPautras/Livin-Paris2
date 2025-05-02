using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class FaitPartieDe: ILpModels<FaitPartieDe>
    {
        public string PersonneId { get; set; }
        public int EntrepriseId { get; set; }
        public IDataAccess<FaitPartieDe> DataAccess { get; } = new FaitPartieDeDataAccess();
        public IDataService<FaitPartieDe> DataService { get; } = new FaitPartieDeService(new FaitPartieDeDataAccess());
    }
}