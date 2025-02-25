using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class CompositionDuPlatService
    {
        private readonly CompositionDuPlatDataAccess _dataAccess = new CompositionDuPlatDataAccess();

        public List<CompositionDuPlat> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public void Insert(CompositionDuPlat c)
        {
            _dataAccess.Insert(c);
        }
    }
}