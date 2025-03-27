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
            CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
            foreach (var personne in clientDataAccess.GetAll())
            {
                Console.WriteLine(personne.ClientUsername);
                Console.WriteLine(personne.ClientPassword);
            }

            (string[] Test1, Personne test2)? bonk = Connexion.ConnexionMenu();
            if (bonk != null)
            {
                Console.WriteLine(bonk.Value.test2.PersonneNom);
                if (bonk.Value.Test1[2] == "Client")
                {
                    ClientMenu.ClientMenuMain();
                }
                else
                {
                    CuisinierMenu cuisinierMenu = new CuisinierMenu(cuisinierDataAccess.GetByUsername(bonk.Value.Test1[0]));
                    cuisinierMenu.CuisinierMenuMain();
                }
            }
            else
                Console.WriteLine("Erreur dans la connexion");
            Console.ReadKey();
        }
    }
}
