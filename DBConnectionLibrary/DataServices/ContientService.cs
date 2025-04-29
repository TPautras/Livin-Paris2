using System.Collections.Generic;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class ContientService : IDataService<Contient>
    {
        private readonly ContientDataAccess _dataAccess = new ContientDataAccess();

        public List<Contient> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Contient GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Contient c)
        {
            _dataAccess.Insert(c);
        }

        public void Update(Contient entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}