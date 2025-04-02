// File: Program.cs
using System;
using System.Collections.Generic;
using DBConnectionLibrary.Sql;
using Graphs;
using LivinParis_Console.Assets;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace LivinParis_Console
{
    public class BddConnection
    {
        public static void ConnectorTest()
        {
            string[] options = { "Initialiser la base de donnees", "Quitter" };
            int bddChoice = Affichages.MenuSelect(ASCII.Bdd+"\n Quelle option voulez vous choisir ?", options);
            switch (bddChoice)
            {
                case 0:
                    InitializeSql.Initialize();
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}
