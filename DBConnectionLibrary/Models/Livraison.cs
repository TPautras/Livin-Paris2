using System;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Livraison: ILpModels<Livraison>
    {
        public int LivraisonId { get; set; }
        public string LivraisonAdresse { get; set; }
        public DateTime? LivraisonDate { get; set; }
        public IDataAccess<Livraison> DataAccess { get; } = new LivraisonDataAccess();
        public IDataService<Livraison> DataService { get; } = new LivraisonService(new LivraisonDataAccess());
    }
}