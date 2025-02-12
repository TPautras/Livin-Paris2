using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class CuisinierService
    {
        private readonly CuisinierDataAccess _dataAccess = new CuisinierDataAccess();
        private readonly PersonneService _personneService = new PersonneService();

        public List<Cuisinier> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Cuisinier GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Cuisinier c)
        {
            _personneService.Insert(c);
            _dataAccess.Insert(c);
        }

        public void Update(Cuisinier c)
        {
            _personneService.Update(c);
            _dataAccess.Update(c);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}