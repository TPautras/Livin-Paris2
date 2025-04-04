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
                        new ModuleClient().ModuleClientMain();
                        Console.ReadKey();
                        break;
                    case 1:
                        new ModuleCuisinier().ModuleCuisinierMain();
                        Console.ReadKey();
                        break;
                    case 2:
                        new ModuleCommande().ModuleCommandMain();
                        Console.ReadKey();
                        break;
                    case 3:
                        new ModuleStatistiques().ModuleStatistiquesMain();
                        Console.ReadKey();
                        break;
                    case 4:
                        new ModuleAutre().ModuleAutreMain();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        #region ModuleAutre
        public void ModuleAutreMain()
        {
            Console.WriteLine("Module Autre");
        }
        #endregion
    }
}
