using System;
using System.Collections.Generic;
using DBConnectionLibrary.DataAccess;
using Graphs;
using LivinParis_Console.Assets;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console.Modules
{
    public class ModuleClient : AdminMenu
    {
        public void ModuleClientMain()
        {
            string[] options =
            {
                "Lister tous les clients", "Supprimer un client", "Modifier un client", "Ajouter un client",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Client";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleClientListing();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleClientSuppressing();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleClientModifying();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleClientAdding();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }
        private void ModuleClientAdding()
        {
            Console.WriteLine(@"ADMINISTRATOR | Module Client | Adding");
            Personne userPersonne = Connexion.CreationPersonne();
            Client userClient = Connexion.CreationClient(userPersonne);
            List<Client> clients = ClientService.GetAll();
            foreach (Client client in clients)
            {
                Console.WriteLine(client.ClientUsername);
            }
            Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
        }

        private void ModuleClientModifying()
        {
            List<Client> clients = ClientData.GetAll();
            string[] options = new string[clients.Count + 1];
            foreach (Client client in clients)
            {
                options[clients.IndexOf(client)] = client.ClientUsername;
            }
            options[clients.Count] = @"Créer un client";
            int clientChoice = Affichages.MenuSelect(ASCII.Client + "\n QUEL CLIENT VOULEZ VOUS MODIFIER ?", options);
            
            bool endMod = false;
            string[] optionsMod = {"Username", "Mot de passe", "Email","Quitter"};
            Client userClient = clients[clientChoice];
            while (endMod == false)
            {
                Client client = ClientData.GetByUsername(userClient.ClientUsername);
                string prompt = "Que voulez vous modifier ? \n";
                prompt += "Username actuel : " + client.ClientUsername + "\n";
                prompt += "Mot de passe actuel : " + client.ClientPassword + "\n";
                prompt += "Email actuel : " + client.PersonneEmail + "\n";
                int userChoice = Affichages.MenuSelect(prompt, optionsMod);
                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("ADMINISTRATOR | Module Client | Modifying \n Entrez le nouvel username");
                        string username = Console.ReadLine();
                        userClient.ClientUsername = username;
                        ClientData.UpdateUsername(userClient);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    case 1:
                        Console.WriteLine("ADMINISTRATOR | Module Client | Modifying \n Entrez le nouveau mot de passe");
                        string mdp = Console.ReadLine();
                        userClient.ClientPassword = mdp;
                        ClientData.Update(userClient);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    case 2:
                        Console.WriteLine("ADMINISTRATOR | Module Client | Modifying \n Entrez le nouvel email");
                        string email = Console.ReadLine();
                        userClient.PersonneEmail = email;
                        ClientData.Update(userClient);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    default:
                        endMod = true;
                        break;
                }
                Console.ReadKey();
            }
        }
        public void ModuleClientListing()
        {
            string[] options =
            {
                "Ordre alphabétique", "Ordre alphabétique de la rue", "Par montant des achats cumulés",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Client | LISTING";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            switch (adminChoice)
            {
                case 0:
                    Console.WriteLine("Ordre alphabétique");
                    List<Client> clients = ClientData.GetAllByNameAsc();
                    foreach (Client client in clients)
                    {
                        Console.WriteLine(client.ClientUsername);
                    }
                    break;
                case 1:
                    Console.WriteLine("Ordre alphabétique de la rue");
                     clients = ClientData.GetAllByStreetAsc();
                    foreach (Client client in clients)
                    {
                        Console.WriteLine(client.ClientUsername + " : " + new PersonneDataAccess().GetByEmail(client.PersonneEmail).PersonneNomDeLaRue);
                    }
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Par montant des achats");
                    clients = ClientData.GetAllByStreetAsc();
                    foreach (Client client in clients)
                    {
                        Console.WriteLine(client.ClientUsername + " : Rue " + new PersonneDataAccess().GetByEmail(client.PersonneEmail).PersonneNomDeLaRue);
                    }
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        public void ModuleClientSuppressing()
        {
            List<Client> clients = ClientData.GetAll();
            string[] options = new string[clients.Count];
            foreach (Client client in clients)
            {
                options[clients.IndexOf(client)] = client.ClientUsername;
            }
            int clientChoice = Affichages.MenuSelect(ASCII.Client + "\n QUEL CLIENT VOULEZ VOUS SUPPRIMER ?", options);
            Console.WriteLine("Vous avez choisi : " + clients[clientChoice].ClientUsername);
            Console.WriteLine("Suppression effectuee, appuyez sur n'importe quelle touche pour continuer");
            ClientData.DeleteByUsername(clients[clientChoice].ClientUsername);
        }
    }
}