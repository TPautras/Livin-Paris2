using System;
using System.Collections.Generic;
using SqlConnector.DataServices;
using SqlConnector.Models;
using DotNetEnv;

namespace SqlConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load(); 
            string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            Console.WriteLine("=== Application de test DataAccess & Services ===");

            var personneService = new PersonneService();
            var clientsService = new ClientsService();
            var cuisinierService = new CuisinierService();
            var commandeService = new CommandeService();
            var evaluationService = new EvaluationService();
            var livraisonService = new LivraisonService();
            var platService = new PlatService();
            var creationService = new CreationService();
            var contientService = new ContientService();
            var ingredientService = new IngredientService();
            var compositionDuPlatService = new CompositionDuPlatService();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Choisissez une option :");
                Console.WriteLine("1. Personne");
                Console.WriteLine("2. Clients");
                Console.WriteLine("3. Cuisinier");
                Console.WriteLine("4. Commande");
                Console.WriteLine("5. Evaluation");
                Console.WriteLine("6. Livraison");
                Console.WriteLine("7. Plat");
                Console.WriteLine("8. Ingredient");
                Console.WriteLine("9. Quitter");
                Console.Write("Votre choix : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": 
                        ManagePersonnes(personneService);
                        break;
                    case "2":
                        ManageClients(clientsService);
                        break;
                    case "3":
                        ManageCuisiniers(cuisinierService);
                        break;
                    case "4":
                        ManageCommandes(commandeService);
                        break;
                    case "5":
                        ManageEvaluations(evaluationService);
                        break;
                    case "6":
                        ManageLivraisons(livraisonService);
                        break;
                    case "7":
                        ManagePlats(platService);
                        break;
                    case "8":
                        ManageIngredients(ingredientService);
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }

            Console.WriteLine("Fin du programme.");
        }

        static void ManagePersonnes(PersonneService service)
        {
            Console.WriteLine("=== Gestion des Personnes ===");
            Console.WriteLine("1. Liste de toutes les Personnes");
            Console.WriteLine("2. Créer une Personne");
            Console.WriteLine("3. Voir une Personne (par ID)");
            Console.WriteLine("4. Mettre à jour une Personne");
            Console.WriteLine("5. Supprimer une Personne");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Personne> all = service.GetAll();
                        foreach (var pers in all)
                        {
                            Console.WriteLine($"{pers.PersonneId} : {pers.PersonneNom} {pers.PersonnePrenom}, {pers.PersonneVille}");
                        }
                        break;
                    case "2":
                        var p = new Personne();
                        Console.Write("ID : ");
                        p.PersonneId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nom : ");
                        p.PersonneNom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        p.PersonnePrenom = Console.ReadLine();
                        Console.Write("Ville : ");
                        p.PersonneVille = Console.ReadLine();
                        service.Insert(p);
                        Console.WriteLine("Personne insérée avec succès !");
                        break;
                    case "3":
                        Console.Write("ID de la Personne : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var persGet = service.GetById(idGet);
                        if (persGet != null)
                        {
                            Console.WriteLine(
                                $"ID={persGet.PersonneId}, {persGet.PersonneNom} {persGet.PersonnePrenom}, Ville={persGet.PersonneVille}");
                        }
                        else
                        {
                            Console.WriteLine("Aucune personne trouvée avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID de la Personne à mettre à jour : ");
                        int idUpdate = Convert.ToInt32(Console.ReadLine());
                        var persUpdate = service.GetById(idUpdate);
                        if (persUpdate != null)
                        {
                            Console.Write($"Nouveau nom (actuel: {persUpdate.PersonneNom}) : ");
                            string nomUpd = Console.ReadLine();
                            if (!string.IsNullOrEmpty(nomUpd))
                                persUpdate.PersonneNom = nomUpd;

                            Console.Write($"Nouveau prénom (actuel: {persUpdate.PersonnePrenom}) : ");
                            string prenomUpd = Console.ReadLine();
                            if (!string.IsNullOrEmpty(prenomUpd))
                                persUpdate.PersonnePrenom = prenomUpd;

                            Console.Write($"Nouvelle ville (actuelle: {persUpdate.PersonneVille}) : ");
                            string villeUpd = Console.ReadLine();
                            if (!string.IsNullOrEmpty(villeUpd))
                                persUpdate.PersonneVille = villeUpd;

                            service.Update(persUpdate);
                            Console.WriteLine("Mise à jour réussie.");
                        }
                        else
                        {
                            Console.WriteLine("Personne introuvable.");
                        }
                        break;
                    case "5":
                        Console.Write("ID de la Personne à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageClients(ClientsService service)
        {
            Console.WriteLine("=== Gestion des Clients ===");
            Console.WriteLine("1. Liste de tous les Clients");
            Console.WriteLine("2. Créer un Client");
            Console.WriteLine("3. Voir un Client (par ID)");
            Console.WriteLine("4. Supprimer un Client");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Clients> all = service.GetAll();
                        foreach (var c in all)
                        {
                            Console.WriteLine($"{c.PersonneId} : {c.PersonneNom} {c.PersonnePrenom}");
                        }
                        break;
                    case "2":
                        var cl = new Clients();
                        Console.Write("ID : ");
                        cl.PersonneId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nom : ");
                        cl.PersonneNom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        cl.PersonnePrenom = Console.ReadLine();
                        service.Insert(cl);
                        Console.WriteLine("Client inséré avec succès !");
                        break;
                    case "3":
                        Console.Write("ID du Client : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var clientGet = service.GetById(idGet);
                        if (clientGet != null)
                        {
                            Console.WriteLine($"ID={clientGet.PersonneId}, {clientGet.PersonneNom} {clientGet.PersonnePrenom}");
                        }
                        else
                        {
                            Console.WriteLine("Aucun client trouvé avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID du Client à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageCuisiniers(CuisinierService service)
        {
            Console.WriteLine("=== Gestion des Cuisiniers ===");
            Console.WriteLine("1. Liste de tous les Cuisiniers");
            Console.WriteLine("2. Créer un Cuisinier");
            Console.WriteLine("3. Voir un Cuisinier (par ID)");
            Console.WriteLine("4. Supprimer un Cuisinier");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Cuisinier> all = service.GetAll();
                        foreach (var cuisinier in all)
                        {
                            Console.WriteLine($"{cuisinier.PersonneId} : {cuisinier.PersonneNom} {cuisinier.PersonnePrenom}");
                        }
                        break;
                    case "2":
                        var cu = new Cuisinier();
                        Console.Write("ID : ");
                        cu.PersonneId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nom : ");
                        cu.PersonneNom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        cu.PersonnePrenom = Console.ReadLine();
                        service.Insert(cu);
                        Console.WriteLine("Cuisinier inséré avec succès !");
                        break;
                    case "3":
                        Console.Write("ID du Cuisinier : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var cuisGet = service.GetById(idGet);
                        if (cuisGet != null)
                        {
                            Console.WriteLine($"ID={cuisGet.PersonneId}, {cuisGet.PersonneNom} {cuisGet.PersonnePrenom}");
                        }
                        else
                        {
                            Console.WriteLine("Aucun cuisinier trouvé avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID du Cuisinier à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageCommandes(CommandeService service)
        {
            Console.WriteLine("=== Gestion des Commandes ===");
            Console.WriteLine("1. Liste de toutes les Commandes");
            Console.WriteLine("2. Créer une Commande");
            Console.WriteLine("3. Voir une Commande (par ID)");
            Console.WriteLine("4. Supprimer une Commande");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Commande> all = service.GetAll();
                        foreach (var cmd in all)
                        {
                            Console.WriteLine($"{cmd.CommandeId} : Date={cmd.CommandeDate}, ClientId={cmd.ClientId}");
                        }
                        break;
                    case "2":
                        var co = new Commande();
                        Console.Write("ID : ");
                        co.CommandeId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Date (yyyy-MM-dd) ou vide : ");
                        string dateInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(dateInput))
                            co.CommandeDate = DateTime.Parse(dateInput);
                        Console.Write("ID du Client : ");
                        string clId = Console.ReadLine();
                        if(!string.IsNullOrEmpty(clId))
                            co.ClientId = Convert.ToInt32(clId);
                        service.Insert(co);
                        Console.WriteLine("Commande insérée avec succès !");
                        break;
                    case "3":
                        Console.Write("ID de la Commande : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var cmdGet = service.GetById(idGet);
                        if (cmdGet != null)
                        {
                            Console.WriteLine($"ID={cmdGet.CommandeId}, Date={cmdGet.CommandeDate}, ClientId={cmdGet.ClientId}");
                        }
                        else
                        {
                            Console.WriteLine("Aucune commande trouvée avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID de la Commande à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageEvaluations(EvaluationService service)
        {
            Console.WriteLine("=== Gestion des Evaluations ===");
            Console.WriteLine("1. Liste de toutes les Evaluations");
            Console.WriteLine("2. Créer une Evaluation");
            Console.WriteLine("3. Voir une Evaluation (par ID)");
            Console.WriteLine("4. Supprimer une Evaluation");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Evaluation> all = service.GetAll();
                        foreach (var e in all)
                        {
                            Console.WriteLine($"{e.EvaluationId} : Client={e.EvaluationId}, Cuisinier={e.EvaluationCuisinier}, Commande={e.CommandeId}");
                        }
                        break;
                    case "2":
                        var eval = new Evaluation();
                        Console.Write("ID : ");
                        eval.EvaluationId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("ID du Client : ");
                        string clInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(clInput))
                            eval.EvaluationId = Convert.ToInt32(clInput);
                        Console.Write("ID du Cuisinier : ");
                        string cuisInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(cuisInput))
                            eval.EvaluationCuisinier = Convert.ToInt32(cuisInput);
                        Console.Write("ID de la Commande : ");
                        string cmdInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(cmdInput))
                            eval.CommandeId = Convert.ToInt32(cmdInput);
                        service.Insert(eval);
                        Console.WriteLine("Evaluation insérée avec succès !");
                        break;
                    case "3":
                        Console.Write("ID de l'Evaluation : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var eGet = service.GetById(idGet);
                        if (eGet != null)
                        {
                            Console.WriteLine($"ID={eGet.EvaluationId}, Client={eGet.EvaluationId}, Cuisinier={eGet.EvaluationCuisinier}, Commande={eGet.CommandeId}");
                        }
                        else
                        {
                            Console.WriteLine("Aucune evaluation trouvée avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID de l'Evaluation à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageLivraisons(LivraisonService service)
        {
            Console.WriteLine("=== Gestion des Livraisons ===");
            Console.WriteLine("1. Liste de toutes les Livraisons");
            Console.WriteLine("2. Créer une Livraison");
            Console.WriteLine("3. Voir une Livraison (par ID)");
            Console.WriteLine("4. Supprimer une Livraison");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Livraison> all = service.GetAll();
                        foreach (var l in all)
                        {
                            Console.WriteLine($"{l.LivraisonId} : Adresse={l.LivraisonAdresse}, Date={l.LivraisonDate}, CommandeId={l.CommandeId}");
                        }
                        break;
                    case "2":
                        var liv = new Livraison();
                        Console.Write("ID : ");
                        liv.LivraisonId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Adresse : ");
                        liv.LivraisonAdresse = Console.ReadLine();
                        Console.Write("Date (yyyy-MM-dd) ou vide : ");
                        string dateInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(dateInput))
                            liv.LivraisonDate = DateTime.Parse(dateInput);
                        Console.Write("ID de la Commande : ");
                        string cmdInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(cmdInput))
                            liv.CommandeId = Convert.ToInt32(cmdInput);
                        service.Insert(liv);
                        Console.WriteLine("Livraison insérée avec succès !");
                        break;
                    case "3":
                        Console.Write("ID de la Livraison : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var lGet = service.GetById(idGet);
                        if (lGet != null)
                        {
                            Console.WriteLine($"ID={lGet.LivraisonId}, Adresse={lGet.LivraisonAdresse}, Date={lGet.LivraisonDate}, CommandeId={lGet.CommandeId}");
                        }
                        else
                        {
                            Console.WriteLine("Aucune livraison trouvée avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID de la Livraison à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManagePlats(PlatService service)
        {
            Console.WriteLine("=== Gestion des Plats ===");
            Console.WriteLine("1. Liste de tous les Plats");
            Console.WriteLine("2. Créer un Plat");
            Console.WriteLine("3. Voir un Plat (par ID)");
            Console.WriteLine("4. Supprimer un Plat");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Plat> all = service.GetAll();
                        foreach (var p in all)
                        {
                            Console.WriteLine(
                                $"{p.PlatId} : {p.PlatNom}, " +
                                $"Origine={p.PlatOrigine}, " +
                                $"Type={p.PlatTypeDePlat}, " +
                                $"DateFab={p.PlatDateDeFabrication}, " +
                                $"DatePeremp={p.PlatDateDePeremption}");
                        }
                        break;
                    case "2":
                        var plat = new Plat();
                        Console.Write("ID : ");
                        plat.PlatId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nom : ");
                        plat.PlatNom = Console.ReadLine();
                        Console.Write("Origine : ");
                        plat.PlatOrigine = Console.ReadLine();
                        Console.Write("Aromes : ");
                        plat.PlatAromesNaturels = Console.ReadLine();
                        Console.Write("Date de fabrication (yyyy-MM-dd) ou vide : ");
                        string fabDateInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(fabDateInput))
                            plat.PlatDateDeFabrication = DateTime.Parse(fabDateInput);
                        Console.Write("Date de péremption (yyyy-MM-dd) ou vide : ");
                        string perempInput = Console.ReadLine();
                        if(!string.IsNullOrEmpty(perempInput))
                            plat.PlatDateDePeremption = DateTime.Parse(perempInput);
                        Console.Write("Type de plat : ");
                        plat.PlatTypeDePlat = Console.ReadLine();
                        Console.Write("Régime alimentaire : ");
                        plat.PlatRegimeAlimentaire = Console.ReadLine();
                        service.Insert(plat);
                        Console.WriteLine("Plat inséré avec succès !");
                        break;
                    case "3":
                        Console.Write("ID du Plat : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var pGet = service.GetById(idGet);
                        if (pGet != null)
                        {
                            Console.WriteLine(
                                $"ID={pGet.PlatId}, " +
                                $"{pGet.PlatNom}, " +
                                $"Origine={pGet.PlatOrigine}, " +
                                $"Type={pGet.PlatTypeDePlat}, " +
                                $"DateFab={pGet.PlatDateDeFabrication}, " +
                                $"DatePeremp={pGet.PlatDateDePeremption}");
                        }
                        else
                        {
                            Console.WriteLine("Aucun plat trouvé avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID du Plat à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static void ManageIngredients(IngredientService service)
        {
            Console.WriteLine("=== Gestion des Ingrédients ===");
            Console.WriteLine("1. Liste de tous les Ingrédients");
            Console.WriteLine("2. Créer un Ingrédient");
            Console.WriteLine("3. Voir un Ingrédient (par ID)");
            Console.WriteLine("4. Supprimer un Ingrédient");
            Console.Write("Votre choix : ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        List<Ingredient> all = service.GetAll();
                        foreach (var i in all)
                        {
                            Console.WriteLine(
                                $"{i.Ingredient_Id} : {i.Ingredient_Nom}, Volume={i.Ingredient_Volume}");
                        }
                        break;
                    case "2":
                        var ing = new Ingredient();
                        Console.Write("ID : ");
                        ing.Ingredient_Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nom : ");
                        ing.Ingredient_Nom = Console.ReadLine();
                        Console.Write("Volume : ");
                        ing.Ingredient_Volume = Console.ReadLine();
                        service.Insert(ing);
                        Console.WriteLine("Ingrédient inséré avec succès !");
                        break;
                    case "3":
                        Console.Write("ID de l'Ingrédient : ");
                        int idGet = Convert.ToInt32(Console.ReadLine());
                        var ingGet = service.GetById(idGet);
                        if (ingGet != null)
                        {
                            Console.WriteLine(
                                $"ID={ingGet.Ingredient_Id}, " +
                                $"Nom={ingGet.Ingredient_Nom}, " +
                                $"Volume={ingGet.Ingredient_Volume}");
                        }
                        else
                        {
                            Console.WriteLine("Aucun ingrédient trouvé avec cet ID.");
                        }
                        break;
                    case "4":
                        Console.Write("ID de l'Ingrédient à supprimer : ");
                        int idDel = Convert.ToInt32(Console.ReadLine());
                        service.Delete(idDel);
                        Console.WriteLine("Suppression réussie.");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
    }
}
