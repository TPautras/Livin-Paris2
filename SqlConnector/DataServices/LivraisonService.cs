using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class LivraisonService
    {
        private readonly LivraisonDataAccess _dataAccess = new LivraisonDataAccess();

        public List<Livraison> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Livraison GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Livraison liv)
        {
            _dataAccess.Insert(liv);
        }

        public void Update(Livraison liv)
        {
            _dataAccess.Update(liv);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}