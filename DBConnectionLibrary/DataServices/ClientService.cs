using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class ClientService : IDataService<Client>
    {
        private readonly IDataAccess<Client> _dataAccess;
        public ClientService(IDataAccess<Client> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<Client> GetAll()
        {
            return _dataAccess.GetAll();
        }
        public Client GetById(int id)
        {
            return _dataAccess.GetById(id);
        }
        public void Insert(Client entity)
        {
            ValidationHelper.ValidateStringField(entity.ClientUsername, "Client_Username", 50, false);
            ValidationHelper.ValidateStringField(entity.ClientPassword, "Client_Password", 50, false);
           
            _dataAccess.Insert(entity);
        }
        public void Update(Client entity)
        {
            ValidationHelper.ValidateStringField(entity.ClientUsername, "Client_Username", 50, false);
            ValidationHelper.ValidateStringField(entity.ClientPassword, "Client_Password", 50, false);
            _dataAccess.Update(entity);
        }
        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }

        public Client GetByUsername(string username)
        {
            ClientDataAccess dataAccess = new ClientDataAccess();
            return dataAccess.GetByUsername(username);
        }
        public bool TestConnection(string username, string password)
        {
            ClientDataAccess dataAccess = new ClientDataAccess();
            ClientService clientService = new ClientService(dataAccess);
            
            Client client = clientService.GetByUsername(username);
            try
            {
                client = clientService.GetByUsername(username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }

            if (client != null)
            {
                return true;
            }
            return false;
        }
    }
}