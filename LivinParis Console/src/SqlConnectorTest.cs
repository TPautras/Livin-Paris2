// File: Program.cs
using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataService;

namespace LivinParis_Console
{
    public class SqlConnectorTest
    {
        public static void ConnectorTest()
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connString))
            {
                connString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";
            }
            Console.WriteLine("=== Interface Console de Connexion ===");
            Console.WriteLine("Chaîne de connexion : " + connString);
            Console.WriteLine();

            // Pour le développement : affichage des identifiants de test
            Console.WriteLine("=== Identifiants de test ===");
            Console.WriteLine("Clients :");
            Console.WriteLine("  Username: client1  /  Mot de passe: clientpass1");
            Console.WriteLine("  Username: client2  /  Mot de passe: clientpass2");
            Console.WriteLine("Cuisiniers :");
            Console.WriteLine("  Username: chef1    /  Mot de passe: chefpass1");
            Console.WriteLine("  Username: chef2    /  Mot de passe: chefpass2");
            Console.WriteLine();

            var clientService = new ClientService(new ClientDataAccess());
            var cuisinierService = new CuisinierService(new CuisinierDataAccess());

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Sélectionnez une option :");
                Console.WriteLine("1. Se connecter en tant que Client");
                Console.WriteLine("2. Se connecter en tant que Cuisinier");
                Console.WriteLine("3. Lister tous les identifiants (Clients et Cuisiniers)");
                Console.WriteLine("4. Quitter");
                Console.Write("Votre choix : ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        LoginAndShowClientMenu(clientService);
                        break;
                    case "2":
                        LoginAndShowCuisinierMenu(cuisinierService);
                        break;
                    case "3":
                        ListAllCredentials(clientService, cuisinierService);
                        break;
                    case "4":
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

        static void ListAllCredentials(ClientService clientService, CuisinierService cuisinierService)
        {
            Console.WriteLine("=== Liste des Identifiants ===");
            Console.WriteLine("Clients :");
            List<Client> clients = clientService.GetAll();
            foreach (var c in clients)
            {
                Console.WriteLine($"Username: {c.ClientUsername} - Mot de passe: {c.ClientPassword} - Email: {c.PersonneEmail}");
            }
            Console.WriteLine();
            Console.WriteLine("Cuisiniers :");
            List<Cuisinier> cuisiniers = cuisinierService.GetAll();
            foreach (var cu in cuisiniers)
            {
                Console.WriteLine($"Username: {cu.CuisinierUsername} - Mot de passe: {cu.CuisinierPassword} - Email: {cu.PersonneEmail}");
            }
        }
    }
}
