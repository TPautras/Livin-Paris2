using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class CuisinierService : IDataService<Cuisinier>
    {
        private readonly IDataAccess<Cuisinier> _dataAccess;

        public CuisinierService(IDataAccess<Cuisinier> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Cuisinier> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Cuisinier GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Cuisinier entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            ValidationHelper.ValidateStringField(entity.CuisinierPassword, "Cuisinier_Password", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.PersonneId, "Personne_Id", 50, allowNull: false);

            _dataAccess.Insert(entity);
        }

        public void Update(Cuisinier entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            ValidationHelper.ValidateStringField(entity.CuisinierPassword, "Cuisinier_Password", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.PersonneId, "Personne_Id", 50, allowNull: false);

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}