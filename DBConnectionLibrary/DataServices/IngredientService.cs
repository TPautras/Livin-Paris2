using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class IngredientService : IDataService<Ingredient>
    {
        private readonly IDataAccess<Ingredient> _dataAccess;

        public IngredientService(IDataAccess<Ingredient> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Ingredient> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Ingredient GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Ingredient entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.IngredientId, "Ingredient_Id");
            ValidationHelper.ValidateStringField(entity.IngredientNom, "Ingredient_Nom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.IngredientVolume, "Ingredient_volume", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.IngredientUnite, "Ingrédient_Unité", 50, allowNull: false);

            _dataAccess.Insert(entity);
        }

        public void Update(Ingredient entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.IngredientId, "Ingredient_Id");
            ValidationHelper.ValidateStringField(entity.IngredientNom, "Ingredient_Nom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.IngredientVolume, "Ingredient_volume", 50, allowNull: false);
            ValidationHelper.ValidateStringField(entity.IngredientUnite, "Ingrédient_Unité", 50, allowNull: false);

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}