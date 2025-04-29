using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Entreprise: ILpModels<Entreprise>
    {
        public int EntrepriseId { get; set; }
        public string EntrepriseNom { get; set; }
        public IDataAccess<Entreprise> DataAccess { get; } = new EntrepriseDataAccess();
        public IDataService<Entreprise> DataService { get; } = new EntrepriseService(new EntrepriseDataAccess());
    }
}