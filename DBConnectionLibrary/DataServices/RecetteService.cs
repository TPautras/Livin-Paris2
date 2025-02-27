using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class RecetteService : IDataService<Recette>
    {
        private readonly IDataAccess<Recette> _dataAccess;

        public RecetteService(IDataAccess<Recette> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Recette> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Recette GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Recette entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.RecetteId, "Recette_id");
            ValidationHelper.ValidateStringField(entity.RecetteNom, "Recette_Nom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteOrigine, "Recette_Origine", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteTypeDePlat, "Recette_Type_de_plat", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteApportNutritifs, "Recette_Apport_nutritifs", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteRegimeAlimentaire, "Recette_Regime_alimentaire", 50, allowNull: false);

            _dataAccess.Insert(entity);
        }

        public void Update(Recette entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.RecetteId, "Recette_id");
            ValidationHelper.ValidateStringField(entity.RecetteNom, "Recette_Nom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteOrigine, "Recette_Origine", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteTypeDePlat, "Recette_Type_de_plat", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteApportNutritifs, "Recette_Apport_nutritifs", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.RecetteRegimeAlimentaire, "Recette_Regime_alimentaire", 50, allowNull: false);

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}
