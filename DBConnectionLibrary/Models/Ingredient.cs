using System;
using System.Collections.Generic;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public class Ingredient: ILpModels<Ingredient>
    {
        public int IngredientId { get; set; }
        public string IngredientNom { get; set; }
        public string IngredientVolume { get; set; }
        public string IngredientUnite { get; set; }
        public IDataAccess<Ingredient> DataAccess { get; } = new IngredientDataAccess();
        public IDataService<Ingredient> DataService { get; } = new IngredientService(new IngredientDataAccess());
    }
}