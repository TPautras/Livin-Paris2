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
    public class ModuleCommande : AdminMenu
    {
        public void ModuleCommandMain()
        {
            string[] options =
            {
                "Lister toutes les commandes", "Ajouter une commande",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Commande";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleCommandeListing();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleCommandeAdding();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }

                adminChoice = Affichages.MenuSelect(prompt, options);

            }
        }
        private void ModuleCommandeAdding()
        {
            Console.WriteLine("Ajouter une commande");
        }

        private void ModuleCommandeListing()
        {
            Console.WriteLine("ADMINISTRATOR | Module Commande | LISTING");
            List<Commande> commandeListe = new CommandeDataAccess().GetAll();
            List<Plat> platListe = new PlatDataAccess().GetAll();
            List<Creation> creationListe = new CreationDataAccess().GetAll();
            foreach (Commande commande in commandeListe)
            {
                Console.WriteLine("Commande n :"+commande.CommandeId);
                Console.WriteLine("Client : " + commande.ClientUsername);
                Console.WriteLine("Cuisinier : " + commande.CuisinierUsername);
                Console.WriteLine("Plats commandes : ");
                foreach (Creation creation in creationListe)
                {
                    if (creation.CommandeId == commande.CommandeId)
                    {
                        Console.WriteLine(new RecetteDataAccess().GetById(new PlatDataAccess().GetById(creation.PlatId).RecetteId).RecetteNom);
                        Console.WriteLine("Prix : " + new PlatDataAccess().GetById(creation.PlatId).PlatPrix);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}