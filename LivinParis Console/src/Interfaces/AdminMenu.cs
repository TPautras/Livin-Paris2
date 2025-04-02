using System;
using System.Collections.Generic;
using DBConnectionLibrary.DataAccess;
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
            Console.WriteLine("Suppression effectuee, appuyez sur n'importe quelle touche pour continuer");
            ClientData.DeleteByUsername(clients[clientChoice].ClientUsername);
        }
        #endregion

        #region Module Cuisinier
        public void ModuleCuisinierMain()
        {
            Console.WriteLine("Module Cuisinier");
        }
        #endregion

        #region Module Commande
        public void ModuleCommandMain()
        {
            Console.WriteLine("Module Commandes");
        }
        #endregion
        
        #region Module Stats
        public void ModuleStatistiquesMain()
        {
            string[] options =
            {
                "Avoir le nombre de livraisons effectuees par cuisinier", 
                "Afficher les commandes selon une periode de temps", 
                "Afficher la moyenne des prix des commandes", 
                "Afficher la moyenne des comptes clients",
                "Afficher la list des commandes pour un client selon la nationalite des plats, la periode",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Statistiques";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleStatistiquesLivraisonsParCuisinier();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleStatistiquesCommandesOverTime();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleStatistiquesMoyennePrixCommandes();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleStatistiquesMoyenneCompte();
                        Console.ReadKey();
                        break;
                    case 4:
                        ModuleStatistiquesCommandesBizarre();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        private void ModuleStatistiquesCommandesBizarre()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("Entrez la date de depart (jj/mm/aaaa)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la date d'arrivee (jj/mm/aaaa)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez l'Username du client que vous voulez analyser");
            string username = Console.ReadLine();
            Console.WriteLine("Entrez l'origine des plats");
            string origine = Console.ReadLine();
            
            List<int> commandes = dataAccess.GetCommandesByClientAndFilters(username, origine, startDate, endDate);
            foreach (int commande in commandes)
            {
                Console.WriteLine("La commande "+ commande +" a ete passee entre la date de depart et celle d'arrivee par "+username+" et est bien d'origine "+origine+".");
            }
        }

        private void ModuleStatistiquesMoyenneCompte()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("La moyenne de depense par compte est de : " + dataAccess.GetAverageClientSpending());
        }

        private void ModuleStatistiquesMoyennePrixCommandes()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("La moyenne de prix des commandes est de : " + dataAccess.GetAverageCommandePrice());
        }

        private void ModuleStatistiquesCommandesOverTime()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Console.WriteLine("Entrez la date de depart (jj/mm/aaaa)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Entrez la date d'arrivee (jj/mm/aaaa)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            List<int> res = dataAccess.GetCommandesBetweenDates(startDate, endDate);
            foreach (int i in res)
            {
                Console.WriteLine("La commande "+ i +" a ete passee entre la date de depart et celle d'arrivee");
            }
        }
        private void ModuleStatistiquesLivraisonsParCuisinier()
        {
            StatsDataAccess dataAccess = new StatsDataAccess();
            Dictionary<string, int> stats = dataAccess.GetLivraisonCountByCuisinier();
            foreach (var kvp in stats)
            {
                Console.WriteLine("Le cuisinier : "+kvp.Key+" a effectue : "+kvp.Value + " livraisons");
            }
        }

        #endregion

        #region ModuleAutre
        public void ModuleAutreMain()
        {
            Console.WriteLine("Module Autre");
        }
        #endregion
    }
}
