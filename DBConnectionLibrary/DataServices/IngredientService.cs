using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class IngredientService
    {
        private readonly IngredientDataAccess _dataAccess = new IngredientDataAccess();

        public List<Ingredient> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Ingredient GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Ingredient ing)
        {
            ValidateIngredient(ing);
            _dataAccess.Insert(ing);
        }

        public void Update(Ingredient ing)
        {
            ValidateIngredient(ing);
            _dataAccess.Update(ing);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }

        private void ValidateIngredient(Ingredient ing)
        {
            ValidationHelper.ValidateStringField(ing.Ingredient_Nom, "Ingredient_Nom", 50);
            ValidationHelper.ValidateStringField(ing.Ingredient_Volume, "Ingredient_Volume", 50);
        }
    }

}