using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class ClientsService
    {
        private readonly ClientsDataAccess _dataAccess = new ClientsDataAccess();
        private readonly PersonneService _personneService = new PersonneService();
        
        public List<Clients> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Clients GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Clients c)
        {
            _personneService.Insert(c);
            _dataAccess.Insert(c);
        }

        public void Update(Clients c)
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