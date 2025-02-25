using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class CreationService
    {
        private readonly CreationDataAccess _dataAccess = new CreationDataAccess();

        public List<Creation> GetAll()
        {
            return _dataAccess.GetAll();
        }
        
        public void Insert(Creation c)
        {
            _dataAccess.Insert(c);
        }
    }
}