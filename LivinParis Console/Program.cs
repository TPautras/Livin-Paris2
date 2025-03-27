using System;
using Graphs;
using SqlConnector;
using static LivinParis_Console.SqlConnectorTest;

namespace LivinParis_Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] options = { "Graphes", "Sql", "Test","Quitter" };
            bool Quit = false;
            while (!Quit)
            {
                int res = Affichages.MenuSelect(
                    Affichages.Banner() + "\n Quel partie du code voulez vous explorer ?", options);
                switch (res)
                {
                    case 0:
                        Exercice1.Exo1();
                        break;
                    case 1:
                        SqlConnectorTest.ConnectorTest();
                        break;
                    case 2:
                        Test.ConnectorTest();
                        break;
                    default:
                        Quit = true;
                        break;
                }
            }
        }
    }
}