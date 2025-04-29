using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Cuisinier: ILpModels<Cuisinier>
    {
        public string CuisinierUsername { get; set; }
        public string CuisinierPassword { get; set; }
        public string PersonneEmail { get; set; }
        public Personne Personne { get; set; }
        public IDataAccess<Cuisinier> DataAccess { get; } = new CuisinierDataAccess();
        public IDataService<Cuisinier> DataService { get; } = new CuisinierService(new CuisinierDataAccess());
    }
}