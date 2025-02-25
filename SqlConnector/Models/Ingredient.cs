using System;
using System.Collections.Generic;

namespace SqlConnector.Models
{
    public class Ingredient
    {
        public int Ingredient_Id { get; set; }
        public string Ingredient_Nom { get; set; }
        public string Ingredient_Volume { get; set; }
        public ICollection<CompositionDuPlat> CompositionDuPlat { get; set; }
    }
}