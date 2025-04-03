using System;
using System.Collections.Generic;
using System.Reflection;
using DBConnectionLibrary.DataAccess;
using Graphs;
using LivinParis_Console.Assets;
using LivinParis_Console.Modules;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;

namespace LivinParis_Console.Modules
{
    public class ModuleCuisinier : AdminMenu
    {
        public void ModuleCuisinierMain()
        {
            string[] options =
            {
                "Lister tous les cuisiniers", "Supprimer un cuisinier", "Modifier un cuisinier", "Ajouter un cuisinier",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Cuisinier";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleCuisinierListing();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleCuisinierSuppressing();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleCuisinierModifying();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleCuisinierAdding();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }
        private void ModuleCuisinierAdding()
        {
            Console.WriteLine(@"ADMINISTRATOR | Module Cuisinier | Adding");
            Personne userPersonne = Connexion.CreationPersonne();
            Cuisinier userClient = Connexion.CreationCuisinier(userPersonne);
            List<Cuisinier> cuisiniers = CuisinierService.GetAll();
            foreach (Cuisinier cuisinier in cuisiniers)
            {
                Console.WriteLine(cuisinier.CuisinierUsername);
            }
            Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
        }

        private void ModuleCuisinierModifying()
        {
            List<Cuisinier> cuisiniers = CuisinierDataAccess.GetAll();
            string[] options = new string[cuisiniers.Count + 1];
            foreach (Cuisinier cuisinier in cuisiniers)
            {
                options[cuisiniers.IndexOf(cuisinier)] = cuisinier.CuisinierUsername;
            }
            options[cuisiniers.Count] = @"Créer un cuisinier";
            int clientChoice = Affichages.MenuSelect(ASCII.Cuisinier + "\n QUEL CUISINIAER VOULEZ VOUS MODIFIER ?", options);
            
            bool endMod = false;
            string[] optionsMod = {"Username", "Mot de passe", "Email","Quitter"};
            Cuisinier userCuisinier = cuisiniers[clientChoice];
            while (endMod == false)
            {
                Cuisinier cuisinier = CuisinierDataAccess.GetByUsername(userCuisinier.CuisinierUsername);
                string prompt = "Que voulez vous modifier ? \n";
                prompt += "Username actuel : " + cuisinier.CuisinierUsername + "\n";
                prompt += "Mot de passe actuel : " + cuisinier.CuisinierPassword + "\n";
                prompt += "Email actuel : " + cuisinier.PersonneEmail + "\n";
                int userChoice = Affichages.MenuSelect(prompt, optionsMod);
                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("ADMINISTRATOR | Module Cuisinier | Modifying \n Entrez le nouvel username");
                        string username = Console.ReadLine();
                        userCuisinier.CuisinierUsername = username;
                        CuisinierDataAccess.UpdateUsername(userCuisinier);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    case 1:
                        Console.WriteLine("ADMINISTRATOR | Module Cuisinier | Modifying \n Entrez le nouveau mot de passe");
                        string mdp = Console.ReadLine();
                        userCuisinier.CuisinierPassword = mdp;
                        CuisinierDataAccess.Update(userCuisinier);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    case 2:
                        Console.WriteLine("ADMINISTRATOR | Module Cuisinier | Modifying \n Entrez le nouvel email");
                        string email = Console.ReadLine();
                        userCuisinier.PersonneEmail = email;
                        CuisinierDataAccess.Update(userCuisinier);
                        Console.WriteLine("Modification enregistree, appuyez sur n'importe quelle touche pour continuer");
                        break;
                    default:
                        endMod = true;
                        break;
                }
                Console.ReadKey();
            }
        }
        public void ModuleCuisinierListing()
        {
            string prompt = "ADMINISTRATOR | Module Cuisinier | LISTING";
            Console.WriteLine(prompt);
            List<Cuisinier> cuisiniers = CuisinierDataAccess.GetAll();
            List<Commande> commandes = new CommandeDataAccess().GetAll();
            
            foreach (Cuisinier cuisinier in cuisiniers)
            {
                List<Commande> commandeListing = new List<Commande>();
                foreach (Commande commande in commandes)
                {
                    if (commande.CuisinierUsername == cuisinier.CuisinierUsername)
                    {
                        commandeListing.Add(commande);
                    }
                }

                Console.WriteLine(cuisinier.CuisinierUsername);
                Console.WriteLine("Clients servis :");
                foreach (Commande commande in commandeListing)
                {
                    Console.WriteLine(ClientData.GetByUsername(commande.ClientUsername).ClientUsername);
                }
                Console.WriteLine();
            }
        }

        public void ModuleCuisinierSuppressing()
        {
            List<Cuisinier> cuisiniers = CuisinierDataAccess.GetAll();
            string[] options = new string[cuisiniers.Count];
            foreach (Cuisinier cuisinier in cuisiniers)
            {
                options[cuisiniers.IndexOf(cuisinier)] = cuisinier.CuisinierUsername;
            }
            int cuisinierChoice = Affichages.MenuSelect(ASCII.Client + "\n QUEL CUISINIER VOULEZ VOUS SUPPRIMER ?", options);
            Console.WriteLine("Vous avez choisi : " + cuisiniers[cuisinierChoice].CuisinierUsername);
            Console.WriteLine("Suppression effectuee, appuyez sur n'importe quelle touche pour continuer");
            CuisinierDataAccess.DeleteByUsername(cuisiniers[cuisinierChoice].CuisinierUsername);
        }
    }
}