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
            ClientDataAccess clientDataAccess = new ClientDataAccess();
            foreach (var personne in clientDataAccess.GetAll())
            {
                Console.WriteLine(personne.ClientUsername);
                Console.WriteLine(personne.ClientPassword);
            }

            (string[] Test1, Personne test2)? bonk = Connexion.ConnexionMenu();
            if (bonk != null)
            {
                Console.WriteLine(bonk.Value.test2.PersonneNom);
            }
            else
                Console.WriteLine("NUUUUUUL");

            foreach (var personne in clientDataAccess.GetAll())
            {
                Console.WriteLine(personne.ClientUsername);
                Console.WriteLine(personne.ClientPassword);
            }
            Console.ReadKey();
            
        }
    }
}
