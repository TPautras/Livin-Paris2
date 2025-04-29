using SqlConnector.DataService;
using SqlConnector.DataServices;

namespace SqlConnector.Models
{
    public class Contient: ILpModels<Contient>
    {
        public int CommandeId { get; set; }
        public int PlatId { get; set; }
        public Commande Commande { get; set; }
        public Plat Plat { get; set; }
        public IDataAccess<Contient> DataAccess { get; } = new ContientDataAccess();
        public IDataService<Contient> DataService { get; } = new ContientService();
    }
}