using System;
using System.Collections.Generic;
using Graphs;
using LivinParis.DataAccess;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Console.Modules
{
    public class ModuleEntreprise
    {
        public void ModuleEntrepriseMain()
        {
            string[] options =
            {
                "Liste des entreprises avec personnes",
                "Creer une entreprise",
                "Modifier une entreprise",
                "Supprimer une entreprise",
                "Ajouter une personne à une entreprise",
                "Quitter"
            };
            string prompt = "ADMINISTRATOR | Module Entreprise";
            int adminChoice = Affichages.MenuSelect(prompt, options);
            while (adminChoice < options.Length - 1)
            {
                switch (adminChoice)
                {
                    case 0:
                        ModuleEntrepriseListing();
                        Console.ReadKey();
                        break;
                    case 1:
                        ModuleEntrepriseAdding();
                        Console.ReadKey();
                        break;
                    case 2:
                        ModuleEntrepriseUpdating();
                        Console.ReadKey();
                        break;
                    case 3:
                        ModuleEntrepriseDeleting();
                        Console.ReadKey();
                        break;
                    case 4:
                        ModuleEntrepriseAddPersonne();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                adminChoice = Affichages.MenuSelect(prompt, options);
            }
        }

        private void ModuleEntrepriseListing()
        {
            List<Entreprise> entreprises = new EntrepriseDataAccess().GetAll();
            List<FaitPartieDe> associations = new FaitPartieDeDataAccess().GetAll();
            foreach (Entreprise ent in entreprises)
            {
                Console.WriteLine($"ID: {ent.EntrepriseId} - Nom: {ent.EntrepriseNom}");
                foreach (FaitPartieDe assoc in associations)
                {
                    if (assoc.EntrepriseId == ent.EntrepriseId)
                    {
                        Console.WriteLine($"\tPersonne: {assoc.PersonneId}");
                    }
                }
            }
        }

        private void ModuleEntrepriseAdding()
        {
            Console.Write("Entrez le nom de l'entreprise: ");
            string nom = Console.ReadLine();
            int newId = new EntrepriseDataAccess().GetAll().Count + 1;
            Entreprise ent = new Entreprise { EntrepriseId = newId, EntrepriseNom = nom };
            new EntrepriseDataAccess().Insert(ent);
            Console.WriteLine("Entreprise ajoutée.");
        }

        private void ModuleEntrepriseUpdating()
        {
            Console.Write("Entrez l'ID de l'entreprise à modifier: ");
            int id = int.Parse(Console.ReadLine());
            Entreprise ent = new EntrepriseDataAccess().GetById(id);
            if (ent != null)
            {
                Console.Write("Entrez le nouveau nom de l'entreprise: ");
                ent.EntrepriseNom = Console.ReadLine();
                new EntrepriseDataAccess().Update(ent);
                Console.WriteLine("Entreprise modifiée.");
            }
            else
            {
                Console.WriteLine("Entreprise introuvable.");
            }
        }

        private void ModuleEntrepriseDeleting()
        {
            Console.Write("Entrez l'ID de l'entreprise à supprimer: ");
            int id = int.Parse(Console.ReadLine());
            new EntrepriseDataAccess().Delete(id);
            Console.WriteLine("Entreprise supprimée.");
        }

        private void ModuleEntrepriseAddPersonne()
        {
            Console.Write("Entrez l'ID de l'entreprise: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Entrez l'email de la personne à ajouter: ");
            string personneId = Console.ReadLine();
            FaitPartieDe association = new FaitPartieDe { PersonneId = personneId, EntrepriseId = id };
            new FaitPartieDeDataAccess().Insert(association);
            Console.WriteLine("Personne ajoutée à l'entreprise.");
        }
    }
}
