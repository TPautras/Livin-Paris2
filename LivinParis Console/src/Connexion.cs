using System;
using Graphs;
using Sprache;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console
{
    public class Connexion
    {
        public static (string[],Personne)? ConnexionMenu()
        {
            PersonneDataAccess personneDataAccess = new PersonneDataAccess();
            PersonneService personneService = new PersonneService(personneDataAccess);
            string[] options = { "Compte client", "Compte Cuisinier", "Créer un compte", "Quitter" };
            int connexionChoice = Affichages.MenuSelect(Assets.ASCII.Bienvenue, options);
            Console.WriteLine("Quel est votre username ?");
            string userName = Console.ReadLine();
            Console.WriteLine("Quel est votre password ?");
            string password = Console.ReadLine();
            switch (connexionChoice)
            {
                case 0:
                    ClientDataAccess client = new ClientDataAccess();
                    ClientService clientService = new ClientService(client);
                    bool testConnection = clientService.TestConnection(userName, password);
                    while (!testConnection)
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
                        testConnection = clientService.TestConnection(userName, password);
                    }
                    string[] toReturn = {userName, password,"Client"};
                    string clientMail = clientService.GetByUsername(userName).PersonneEmail;
                    return (toReturn, personneService.GetByEmail(clientMail));
                case 1:
                    CuisinierDataAccess Cuisinier = new CuisinierDataAccess();
                    CuisinierService cuisinierService = new CuisinierService(Cuisinier);
                    testConnection = cuisinierService.TestConnection(userName, password);
                    while (!testConnection)
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
                        testConnection = cuisinierService.TestConnection(userName, password);
                    }
                    string[] toReturn2 = {userName, password,"Cuisinier"};
                    string cuisinierMail = cuisinierService.GetByUsername(userName).PersonneEmail;
                    return (toReturn2, personneService.GetByEmail(cuisinierMail));
                case 2:
                    string[] optionsCompte = {"Compte client", "Compte Cuisinier"};
                    int choixCompte = Affichages.MenuSelect(Assets.ASCII.Bienvenue, optionsCompte);
                    switch (choixCompte)
                    {
                        case 0:
                            ClientDataAccess client1 = new ClientDataAccess();
                            ClientService clientService1 = new ClientService(client1);
                           
                            Console.WriteLine("Quel est votre nom ?");
                            string nom = Console.ReadLine();
                            Console.WriteLine("Quel est votre prénom ?");
                            string prenom = Console.ReadLine();
                            Console.WriteLine("Quel est votre email ?");
                            string email = Console.ReadLine();
                            Console.WriteLine("Quel est votre code postal ?");
                            int codePostal = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Quelle est votre ville");
                            string ville = Console.ReadLine();
                            Console.WriteLine("Quel est votre Nom de rue ?");
                            string nomDeRue = Console.ReadLine();
                            Console.WriteLine("Quel est votre numéro de rue");
                            int numeroDeRue = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Quel est votre numéro de téléphone");
                            string numeroDeTel = Console.ReadLine();
                            Console.WriteLine("Quel est votre station de métro la plus proche ?");
                            string station = Console.ReadLine();
                            Console.WriteLine("Quel est votre username ?");
                            string username = Console.ReadLine();
                            Console.WriteLine("Quel est votre mot de passe ?");
                            string motdePasse = Console.ReadLine();
                            
                            Personne userPersonne = new Personne();
                            userPersonne.PersonneNom = nom;
                            userPersonne.PersonnePrenom = prenom;
                            userPersonne.PersonneEmail = email;
                            userPersonne.PersonneTelephone = numeroDeTel;
                            userPersonne.PersonneCodePostale = codePostal;
                            userPersonne.PersonneVille = ville;
                            userPersonne.PersonneNumeroDeLaRue = numeroDeRue;
                            userPersonne.PersonneNomDeLaRue = nomDeRue;
                            userPersonne.PersonneStationDeMetroLaPlusProche = station;
                            
                            Client userClient = new Client();
                            userClient.ClientPassword = motdePasse;
                            userClient.ClientUsername = username;
                            userClient.PersonneEmail = email;
                            userClient.Personne = userPersonne;
                            
                            clientService1.Insert(userClient);
                            personneService.Insert(userPersonne);
                    }
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