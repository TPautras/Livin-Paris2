using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class CommandeService
    {
        private readonly CommandeDataAccess _dataAccess = new CommandeDataAccess();

        public List<Commande> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Commande GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Commande cmd)
        {
            _dataAccess.Insert(cmd);
        }

        public void Update(Commande cmd)
        {
            _dataAccess.Update(cmd);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}