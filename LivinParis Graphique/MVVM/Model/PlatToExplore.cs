﻿using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.Model
{
    public class PlatToExplore
    {
        public string Cuisinier { get; set; }
        public string Recette { get; set; }
        public string Prix { get; set; }
        public Recette RecetteEntiere {get;set;} = new Recette();
    }
}