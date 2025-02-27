using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientNom { get; set; }
        public string IngredientVolume { get; set; }
        public string IngredientUnite { get; set; }
    }
}