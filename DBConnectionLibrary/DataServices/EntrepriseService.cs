using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class EntrepriseService : IDataService<Entreprise>
    {
        private readonly IDataAccess<Entreprise> _dataAccess;

        public EntrepriseService(IDataAccess<Entreprise> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Entreprise> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Entreprise GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Entreprise entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.EntrepriseId, "Entreprise_Id");
            ValidationHelper.ValidateStringField(entity.EntrepriseNom, "Entreprise_Nom", 50, allowNull: false);

            _dataAccess.Insert(entity);
        }

        public void Update(Entreprise entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.EntrepriseId, "Entreprise_Id");
            ValidationHelper.ValidateStringField(entity.EntrepriseNom, "Entreprise_Nom", 50, allowNull: false);

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}