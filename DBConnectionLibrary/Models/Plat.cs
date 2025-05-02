using System;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Plat: ILpModels<Plat>
    {
        public int PlatId { get; set; }
        public DateTime PlatDateDeFabrication { get; set; }
        public DateTime PlatDateDePeremption { get; set; }
        public string PlatPrix { get; set; }
        public int PlatNombrePortion { get; set; }
        public string CuisinierUsername { get; set; }
        public int RecetteId { get; set; }
        public bool PlatDuJour  { get; set; }
        public IDataAccess<Plat> DataAccess { get; } = new PlatDataAccess();
        public IDataService<Plat> DataService { get; } = new PlatService(new PlatDataAccess());
    }
}