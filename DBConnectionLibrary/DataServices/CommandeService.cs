using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class CommandeService : IDataService<Commande>
    {
        private readonly IDataAccess<Commande> _dataAccess;
        public CommandeService(IDataAccess<Commande> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<Commande> GetAll()
        {
            return _dataAccess.GetAll();
        }
        public Commande GetById(int id)
        {
            return _dataAccess.GetById(id);
        }
        public void Insert(Commande entity)
        {
            _dataAccess.Insert(entity);
        }
        public void Update(Commande entity)
        {
            _dataAccess.Update(entity);
        }
        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}