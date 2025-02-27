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
            ValidationHelper.ValidateStringField(entity.PlatPrix, "Plat_Prix", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PlatNombrePortion, "Plat_Nombre_Portion");
            _dataAccess.Insert(entity);
        }
        public void Update(Plat entity)
        {
            ValidationHelper.ValidateStringField(entity.PlatPrix, "Plat_Prix", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PlatNombrePortion, "Plat_Nombre_Portion");
            _dataAccess.Update(entity);
        }
        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}