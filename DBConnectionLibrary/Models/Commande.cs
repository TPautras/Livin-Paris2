using System;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Commande: ILpModels<Commande>
    {
        public int CommandeId { get; set; }
        public int? EntrepriseId { get; set; }
        public string CuisinierUsername { get; set; }
        public string ClientUsername { get; set; }
        public DateTime DateCreation { get; set; }
        public IDataAccess<Commande> DataAccess { get; } = new CommandeDataAccess();
        public IDataService<Commande> DataService { get; } = new CommandeService(new CommandeDataAccess());
    }
}