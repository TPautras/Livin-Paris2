using System;
using Graphs;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console
{
    public class Connexion
    {
        public static (string[],Personne)? ConnexionMenu()
        {
            string[] options = { "Compte client", "Compte Cuisinier", "Créer un compte", "Quitter" };
            int connexionChoice = Affichages.MenuSelect(Assets.ASCII.Bienvenue, options);
            Console.WriteLine("Quel est votre username ?");
            string userName = Console.ReadLine();
            Console.WriteLine("Quel est votre password ?");
            string password = Console.ReadLine();
            switch (connexionChoice)
            {
                case 1:
                    ClientDataAccess client = new ClientDataAccess();
                    ClientService clientService = new ClientService(client);
                    while (!(clientService.TestConnection(userName, password)))
                    {
                        Console.WriteLine("Votre username ou mot de passe est incorrect. Voulez vous quitter ?(Tapez 1 pour quitter)");
                        string quit = Console.ReadLine();
                        if (quit == "1")
                        {
                            return null;
                        }
                        Console.WriteLine("Quel est votre username ?");
                        userName = Console.ReadLine();
                        Console.WriteLine("Quel est votre password ?");
                        password = Console.ReadLine();
                    }
                    string[] toReturn = {userName, password,"Client"};
                    Console.WriteLine("CA MARCHE ICI");
                    return (toReturn, clientService.GetByUsername(userName).Personne);
                case 2:
                    CuisinierDataAccess Cuisinier = new CuisinierDataAccess();
                    CuisinierService cuisinierService = new CuisinierService(Cuisinier);
                    while (!(cuisinierService.TestConnection(userName, password)))
                    {
                        Console.WriteLine("Votre username ou mot de passe est incorrect. Voulez vous quitter ?(Tapez 1 pour quitter)");
                        string quit = Console.ReadLine();
                        if (quit == "1")
                        {
                            return null;
                        }
                        Console.WriteLine("Quel est votre username ?");
                        userName = Console.ReadLine();
                        Console.WriteLine("Quel est votre password ?");
                        password = Console.ReadLine();
                    }
                    string[] toReturn2 = {userName, password,"Cuisinier"};
                    return (toReturn2, cuisinierService.GetByUsername(userName).Personne);
                default:
                    return null;
            }
        }
    }
}
/*
Console.WriteLine("Quel est votre username ?");
            string userName = Console.ReadLine();
            
            Console.WriteLine("Quel est votre password ?");
            string password = Console.ReadLine();
            if (Connexion.TestConnection(userName, password))
            {
                Console.WriteLine(userName);
            }
            else
            {
                Console.WriteLine("VOTRE TENTATIVE DE CONNEXION A ECHOUE");
            }
            Console.ReadKey();
*/