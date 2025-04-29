using System.Collections.Generic;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class CompositionDuPlatService : IDataService<CompositionDuPlat>
    {
        private readonly CompositionDuPlatDataAccess _dataAccess = new CompositionDuPlatDataAccess();

        public List<CompositionDuPlat> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public CompositionDuPlat GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(CompositionDuPlat c)
        {
            _dataAccess.Insert(c);
        }

        public void Update(CompositionDuPlat entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}