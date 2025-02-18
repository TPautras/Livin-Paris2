using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class PlatService : IDataService<Plat>
    {
        private readonly IDataAccess<Plat> _dataAccess;

        public PlatService(IDataAccess<Plat> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Plat> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Plat GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Plat entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.PlatId, "Plat_Id");
            DateValidationHelper.ValidateDateNotInPast(entity.PlatDateDeFabrication, "Plat_date_de_fabrication");
            DateValidationHelper.ValidateDateNotInPast(entity.PlatDateDePeremption, "Plat_Date_de_peremption");
            ValidationHelper.ValidateStringField(entity.PlatPrix, "Plat_Prix", 50, allowNull: false);
            NumericValidationHelper.ValidatePositiveInt(entity.PlatNombrePortion, "Plat_Nombre_Portion");
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.RecetteId, "Recette_id");

            _dataAccess.Insert(entity);
        }

        public void Update(Plat entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.PlatId, "Plat_Id");
            DateValidationHelper.ValidateDateNotInPast(entity.PlatDateDeFabrication, "Plat_date_de_fabrication");
            DateValidationHelper.ValidateDateNotInPast(entity.PlatDateDePeremption, "Plat_Date_de_peremption");
            ValidationHelper.ValidateStringField(entity.PlatPrix, "Plat_Prix", 50, allowNull: false);
            NumericValidationHelper.ValidatePositiveInt(entity.PlatNombrePortion, "Plat_Nombre_Portion");
            NumericValidationHelper.ValidatePositiveInt(entity.CuisinierId, "Cuisinier_Id");
            NumericValidationHelper.ValidatePositiveInt(entity.RecetteId, "Recette_id");

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}
