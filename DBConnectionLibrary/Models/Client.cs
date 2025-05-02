using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Client : ILpModels<Client>
    {
        public IDataAccess<Client> DataAccess { get; set; } = new ClientDataAccess();
        public IDataService<Client> DataService { get; set; } = new ClientService(new ClientDataAccess());
        public string ClientUsername { get; set; }
        public string ClientPassword { get; set; }
        public string PersonneEmail { get; set; }
        public Personne Personne { get; set; }
    }
}