using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class LivraisonService : IDataService<Livraison>
    {
        private readonly IDataAccess<Livraison> _dataAccess;

        public LivraisonService(IDataAccess<Livraison> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Livraison> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Livraison GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Livraison entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.LivraisonId, "Livraison_Id");
            ValidationHelper.ValidateStringField(entity.LivraisonAdresse, "Livraison_Adresse", 50, allowNull: false);
            DateValidationHelper.ValidateDateNotInPast(entity.LivraisonDate, "Livraison_Date");

            _dataAccess.Insert(entity);
        }

        public void Update(Livraison entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.LivraisonId, "Livraison_Id");
            ValidationHelper.ValidateStringField(entity.LivraisonAdresse, "Livraison_Adresse", 50, allowNull: false);
            DateValidationHelper.ValidateDateNotInPast(entity.LivraisonDate, "Livraison_Date");

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}