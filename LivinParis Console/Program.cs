﻿using System;
using Graphs;
using SqlConnector;
using static LivinParis_Console.BddConnection;

namespace LivinParis_Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] options = { "Rendu 1", "Partie Metro/ BDD Initialisation", "Livin'Paris", "Afficher Carte du Métro","Quitter" };
            bool Quit = false;
            while (!Quit)
            {
                int res = Affichages.MenuSelect(
                    Assets.ASCII.Psi2025 + "\n Quel partie du code voulez vous explorer ?", options);
                switch (res)
                {
                    case 0:
                        Exercice1.Exo1();
                        break;
                    case 1:
                        BddConnection.ConnectorTest();
                        break;
                    case 2:
                        LivinParis.ConnectorTest();
                        break;
                    case 3 :
                        MenuAffichageMetro();
                        break;
                    default:
                        Quit = true;
                        break;
                }
            }
        }
        
        public static void MenuAffichageMetro()
        {
            string[] sousOptions = {
                "Afficher la carte des grandes stations (plan simplifié)",
                "Afficher le graphe complet avec correspondances",
                "Retour"
            };

            int choix = Affichages.MenuSelect("\nQuel type de carte souhaitez-vous afficher ?", sousOptions);

            switch (choix)
            {
                case 0:
                    AffichageMetro.AfficherCarte(); 
                    break;
                case 1:
                    AffichageMetro.AfficherCarteAvecCorrespondances(); 
                    break;
                default:
                    break;
            }
        }
    }
}