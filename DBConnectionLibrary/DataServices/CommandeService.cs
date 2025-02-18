using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class CommandeService : IDataService<Commande>
    {
        private readonly IDataAccess<Commande> _dataAccess;

        public CommandeService(IDataAccess<Commande> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Commande> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Commande GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Commande entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.CommandeId, "Commande_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.EntrepriseId, "Entreprise_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.ClientId, "Client_Id");

            _dataAccess.Insert(entity);
        }

        public void Update(Commande entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.CommandeId, "Commande_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.EntrepriseId, "Entreprise_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.ClientId, "Client_Id");

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}