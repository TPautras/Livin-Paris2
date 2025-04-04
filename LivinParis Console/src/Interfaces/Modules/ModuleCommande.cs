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
            Console.WriteLine("Module Commandes");
        }
        
    }
}