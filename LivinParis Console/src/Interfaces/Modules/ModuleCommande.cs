using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DBConnectionLibrary.DataAccess;
using Graphs;
using LivinParis_Console.Assets;
using LivinParis_Console.Modules;
using MetroHelper;
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
        public void ModuleCommandeAdding(Client userClient = null)
        {
            if (userClient == null)
            {
                List<Client> clients = ClientData.GetAll();
                string[] options = new string[clients.Count + 1];
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ClientUsername);
                    options[clients.IndexOf(client)] = client.ClientUsername;
                }
                options[clients.Count] = @"Créer un client";
                int clientChoice = Affichages.MenuSelect("A PARTIR DE QUEL CLIENT COMMANDER ?", options);
                if (clientChoice == clients.Count)
                {
                    Personne userPersonne = Connexion.CreationPersonne();
                    Client userClient2 = Connexion.CreationClient(userPersonne);
                }
                clients = ClientData.GetAll();
                userClient = new ClientDataAccess().GetByUsername(options[clientChoice]);
            }
            List<Plat> plats = new PlatDataAccess().GetAll();
            string[] optionsPlat = new string[plats.Count];
            Console.WriteLine(optionsPlat.Length);
            foreach (Plat plat in plats)
            {
                optionsPlat[plat.PlatId-1] = new RecetteDataAccess().GetById(plat.RecetteId).RecetteNom + "    Id du plat : "+plat.PlatId;
            }
            List<int> platChoice = new List<int>();
            platChoice.Add(Affichages.MenuSelect("Quel plat voulez vous commander ?", optionsPlat));
            string[] options2 = { "oui", "non" };
            while (Affichages.MenuSelect("Voulez vous commander un autre plat ?", options2) == 0)
            {
                platChoice.Add(Affichages.MenuSelect("Quel plat voulez vous commander ?", optionsPlat));
            }
            Commande myCommande = new Commande();
            myCommande.ClientUsername = userClient.ClientUsername;
            myCommande.CuisinierUsername = new PlatDataAccess().GetById(platChoice[0]).CuisinierUsername;
            myCommande.CommandeId = new CommandeDataAccess().GetAll().Count+1;
            myCommande.DateCreation = DateTime.Now;
            new CommandeDataAccess().Insert(myCommande);
            foreach (int i in platChoice)
            {
                Creation myCreation = new Creation();
                myCreation.CommandeId = myCommande.CommandeId;
                myCreation.PlatId = i + 1;
                new CreationDataAccess().Insert(myCreation);
            }
            string nomDepart = new PersonneDataAccess().GetByEmail(new CuisinierDataAccess().GetByUsername(myCommande.CuisinierUsername).PersonneEmail).PersonneStationDeMetroLaPlusProche;
            string nomArrivee = new PersonneDataAccess().GetByEmail(new ClientDataAccess().GetByUsername(myCommande.ClientUsername).PersonneEmail).PersonneStationDeMetroLaPlusProche;

            string solutionRoot = TrouverRacineProjet("MetroHelper");
            string dataPath = Path.Combine(solutionRoot, "MetroHelper", "Data");
            Console.WriteLine($"\n📂 Chemin des données utilisé : {dataPath}");

            try
            {
                var peuplement = new PeuplementGrandeStation();
                var grandes = peuplement.CreerGrandesStations(dataPath);
                Console.WriteLine(grandes.First().Key);
                if (!grandes.ContainsKey(nomDepart) || !grandes.ContainsKey(nomArrivee))
                {
                    Console.WriteLine("❌ Une des grandes stations sélectionnées n'existe pas dans les données.");
                    return;
                }

                GrandeStation grandeDepart = grandes[nomDepart];
                GrandeStation grandeArrivee = grandes[nomArrivee];

                List<Station_de_metro> chemin;
                int temps;
                int nbCorrespondances;

                (chemin, temps, nbCorrespondances) =OutilsMetroHelper.ParcoursAStar(grandeDepart, grandeArrivee, dataPath);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Une erreur est survenue : {ex.Message}");
                Console.WriteLine("📌 Détail : " + ex.StackTrace);
            }
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
        public string TrouverRacineProjet(string dossierCible)
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (!Directory.Exists(Path.Combine(dir.FullName, dossierCible)))
            {
                if (dir.Parent == null)
                    throw new DirectoryNotFoundException($"Impossible de trouver le dossier '{dossierCible}' dans la hiérarchie.");
                dir = dir.Parent;
            }

            return dir.FullName;
        }
    }
}