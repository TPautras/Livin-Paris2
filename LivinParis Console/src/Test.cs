// File: Program.cs
using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace LivinParis_Console
{
    public class Test
    {
        public static void ConnectorTest()
        {
            PersonneDataAccess personneDataAccess = new PersonneDataAccess();
            foreach (var personne in personneDataAccess.GetAll())
            {
                Console.WriteLine(personne.PersonneNom);
            }
        }
    }
}
