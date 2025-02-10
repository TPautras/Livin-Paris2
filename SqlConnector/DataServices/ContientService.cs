using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class ContientService
    {
        private readonly ContientDataAccess _dataAccess = new ContientDataAccess();

        public List<Contient> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public void Insert(Contient c)
        {
            _dataAccess.Insert(c);
        }
    }
}