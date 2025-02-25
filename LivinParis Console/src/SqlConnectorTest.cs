using System;
using SqlConnector;
using SqlConnector.Models;
using SqlConnector.DataService;
using SqlConnector.DataAccess;


namespace SqlConnectorConsoleApp
{
    class SqlConnectorTest
    {
        public static void ConnectorTest()
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            Console.WriteLine("=== Interface Console de Connexion ===");
            Console.WriteLine("Chaîne de connexion : " + connString);
            Console.WriteLine();

            var clientService = new ClientService(new ClientDataAccess());
            var cuisinierService = new CuisinierService(new CuisinierDataAccess());

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Sélectionnez votre rôle pour vous connecter :");
                Console.WriteLine("1. Client");
                Console.WriteLine("2. Cuisinier");
                Console.WriteLine("3. Quitter");
                Console.Write("Votre choix : ");
                string roleChoice = Console.ReadLine();
                Console.WriteLine();

                switch (roleChoice)
                {
                    case "1":
                        LoginAndShowClientMenu(clientService);
                        break;
                    case "2":
                        LoginAndShowCuisinierMenu(cuisinierService);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            Console.WriteLine("Fin du programme.");
        }

        static void LoginAndShowClientMenu(ClientService clientService)
        {
            Console.WriteLine("=== Connexion en tant que Client ===");
            Console.Write("Entrez votre Username : ");
            string username = Console.ReadLine();
            Console.Write("Entrez votre mot de passe : ");
            string password = Console.ReadLine();

            Client client = clientService.GetByUsername(username);
            if (client == null)
            {
                Console.WriteLine("Client introuvable.");
                return;
            }
            if (client.ClientPassword != password)
            {
                Console.WriteLine("Mot de passe incorrect.");
                return;
            }
            Console.WriteLine("Connexion réussie ! Bienvenue Client " + client.ClientUsername);

            bool logout = false;
            while (!logout)
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Client ===");
                Console.WriteLine("1. Voir mes informations");
                Console.WriteLine("2. Mettre à jour mon mot de passe");
                Console.WriteLine("3. Déconnexion");
                Console.Write("Votre choix : ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Username: {client.ClientUsername}");
                        Console.WriteLine($"Personne Email: {client.PersonneEmail}");
                        Console.WriteLine($"Mot de passe: {client.ClientPassword}");
                        break;
                    case "2":
                        Console.Write("Nouveau mot de passe : ");
                        string newPwd = Console.ReadLine();
                        client.ClientPassword = newPwd;
                        clientService.Update(client);
                        Console.WriteLine("Mot de passe mis à jour !");
                        break;
                    case "3":
                        logout = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        static void LoginAndShowCuisinierMenu(CuisinierService cuisinierService)
        {
            Console.WriteLine("=== Connexion en tant que Cuisinier ===");
            Console.Write("Entrez votre Username : ");
            string username = Console.ReadLine();
            Console.Write("Entrez votre mot de passe : ");
            string password = Console.ReadLine();

            Cuisinier cuisinier = cuisinierService.GetByUsername(username);
            if (cuisinier == null)
            {
                Console.WriteLine("Cuisinier introuvable.");
                return;
            }

            if (cuisinier.CuisinierPassword != password)
            {
                Console.WriteLine("Mot de passe incorrect.");
                return;
            }

            Console.WriteLine("Connexion réussie ! Bienvenue Cuisinier " + cuisinier.CuisinierUsername);

            bool logout = false;
            while (!logout)
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Cuisinier ===");
                Console.WriteLine("1. Voir mes informations");
                Console.WriteLine("2. Mettre à jour mon mot de passe");
                Console.WriteLine("3. Déconnexion");
                Console.Write("Votre choix : ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Username: {cuisinier.CuisinierUsername}");
                        Console.WriteLine($"Personne Email: {cuisinier.PersonneEmail}");
                        Console.WriteLine($"Mot de passe: {cuisinier.CuisinierPassword}");
                        break;
                    case "2":
                        Console.Write("Nouveau mot de passe : ");
                        string newPwd = Console.ReadLine();
                        cuisinier.CuisinierPassword = newPwd;
                        cuisinierService.Update(cuisinier);
                        Console.WriteLine("Mot de passe mis à jour !");
                        break;
                    case "3":
                        logout = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }
    }
}
