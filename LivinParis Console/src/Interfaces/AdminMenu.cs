using System;
using System.Collections.Generic;
using Graphs;
using LivinParis_Console.Assets;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console
{
    public class AdminMenu
    {
        #region Initializing DataAccess & DataServices
        public static ClientDataAccess ClientData { get; set; } =  new ClientDataAccess();
        public static ClientService ClientService { get; set; } = new ClientService(ClientData);
        public static CuisinierDataAccess CuisinierDataAccess { get; set; } =  new CuisinierDataAccess();
        public static CuisinierService CuisinierService { get; set; } = new CuisinierService(CuisinierDataAccess);
        #endregion
        public void AdminMenuMain()
        {
            string[] options =
            {
                "Module Client", "Module Cuisinier", "Module Commandes", "Module Statistiques", "Module autre",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleClientMain();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleCuisinierMain();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleCommandMain();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleStatistiquesMain();
                        Console.ReadKey();
                        break;
                    case 4:
                        ModuleAutreMain();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        #region ModuleClient
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
                    case 4:
                        ModuleAutreMain();
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
            Console.WriteLine("Vous avez choisi : " + clients[clientChoice].ClientUsername);
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
                    List<Client> clients = ClientService.GetAll();
                    foreach (Client client in clients)
                    {
                        Console.WriteLine(client.ClientUsername);
                    }
                    break;
                case 1:
                    Console.WriteLine("Ordre alphabétique de la rue");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Par montant des achats");
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
            ClientData.DeleteByUsername(clients[clientChoice].ClientUsername);
        }
        #endregion
        public void ModuleCuisinierMain()
        {
            Console.WriteLine("Module Cuisinier");
        }

        public void ModuleCommandMain()
        {
            Console.WriteLine("Module Commandes");
        }

        public void ModuleStatistiquesMain()
        {
            Console.WriteLine("Module Statistiques");
        }

        public void ModuleAutreMain()
        {
            Console.WriteLine("Module Autre");
        }
    }
}
