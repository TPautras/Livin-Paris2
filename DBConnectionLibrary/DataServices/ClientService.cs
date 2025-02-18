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
            NumericValidationHelper.ValidatePositiveInt(entity.ClientId, "Client_Id");
            ValidationHelper.ValidateStringField(entity.ClientPassword, "Client_Password", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.PersonneId.ToString(), "Personne_Id", 50, allowNull: false);

            _dataAccess.Insert(entity);
        }

        public void Update(Client entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.ClientId, "Client_Id");
            ValidationHelper.ValidateStringField(entity.ClientPassword, "Client_Password", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.PersonneId.ToString(), "Personne_Id", 50, allowNull: false);

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}