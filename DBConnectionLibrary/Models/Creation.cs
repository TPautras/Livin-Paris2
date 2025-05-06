using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Creation: ILpModels<Creation>
    {
        public int CommandeId { get; set; }
        public int PlatId { get; set; }
        public int Quantity { get; set; }
        public IDataAccess<Creation> DataAccess { get; } = new CreationDataAccess();
        public IDataService<Creation> DataService { get; } = new CreationService(new CreationDataAccess());
    }
}