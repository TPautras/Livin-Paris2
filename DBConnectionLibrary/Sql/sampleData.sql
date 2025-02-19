USE livin_paris;

-- Ajout de données dans la table Personne
INSERT INTO Personne (Personne_Id, Personne_Nom, Personne_Prenom, Personne_Ville, Personne_Code_postale, Personne_Nom_de_la_rue, Personne_Numero_de_la_rue, Personne_Email, Personne_Telephone, Personne_Station_de_metro_la_plus_proche)
VALUES
    ('P001', 'Dupont', 'Jean', 'Paris', 75001, 'Rue de Rivoli', 10, 'jean.dupont@email.com', '0601020304', 'Châtelet'),
    ('P002', 'Martin', 'Claire', 'Paris', 75005, 'Boulevard Saint-Michel', 25, 'claire.martin@email.com', '0612345678', 'Odéon'),
    ('P003', 'Lemoine', 'Pierre', 'Paris', 75015, 'Avenue Émile Zola', 14, 'pierre.lemoine@email.com', '0623456789', 'Charles Michels');

-- Ajout d'ingrédients
INSERT INTO Ingredient (Ingredient_Id, Ingredient_Nom, Ingredient_volume, Ingrédient_Unité)
VALUES
    (1, 'Tomate', '200', 'grammes'),
    (2, 'Poulet', '500', 'grammes'),
    (3, 'Pâtes', '250', 'grammes');

-- Ajout de livraisons
INSERT INTO Livraison (Livraison_Id, Livraison_Adresse, Livraison_Date)
VALUES
    (1, '10 Rue de Rivoli, Paris', '2024-02-19 12:00:00'),
    (2, '25 Boulevard Saint-Michel, Paris', '2024-02-19 13:30:00');

-- Ajout de recettes
INSERT INTO Recette (Recette_id, Recette_Nom, Recette_Origine, Recette_Type_de_plat, Recette_Apport_nutritifs, Recette_Regime_alimentaire)
VALUES
    (1, 'Pâtes à la tomate', 'Italie', 'Plat principal', 'Riche en glucides', 'Végétarien'),
    (2, 'Poulet rôti', 'France', 'Plat principal', 'Riche en protéines', 'Sans gluten');

-- Ajout d'entreprises
INSERT INTO Entreprise (Entreprise_Id, Entreprise_Nom)
VALUES
    (1, 'Paris Food'),
    (2, 'Gourmet Express');

-- Ajout de cuisiniers
INSERT INTO Cuisinier (Cuisinier_Id, Cuisinier_Password, Personne_Id)
VALUES
    (1, 'password123', 'P001'),
    (2, 'chefsecret', 'P002');

-- Ajout de clients
INSERT INTO Clients (Client_Id, Client_Password, Personne_Id)
VALUES
    (1, 'clientpass', 'P003');

-- Ajout de commandes
INSERT INTO Commande (Commande_Id, Entreprise_Id, Cuisinier_Id, Client_Id)
VALUES
    (1, 1, 1, 1),
    (2, 2, 2, 1);

-- Ajout de plats
INSERT INTO Plat (Plat_Id, Plat_date_de_fabrication, Plat_Date_de_peremption, Plat_Prix, Plat_Nombre_Portion, Cuisinier_Id, Recette_id)
VALUES
    (1, '2024-02-18', '2024-02-20', '12.99', 2, 1, 1),
    (2, '2024-02-18', '2024-02-22', '15.50', 4, 2, 2);

-- Ajout d'évaluations
INSERT INTO Evaluation (Evaluation_Id, Evaluation_Client, Evaluation_Cuisinier, Evaluation_Description_Client, Evaluation_Description_Cuisinier, Commande_Id)
VALUES
    (1, 4.5, 4.8, 'Très bon plat, bien équilibré.', 'Merci pour votre commande !', 1),
    (2, 3.8, 4.0, 'Un peu trop salé mais bon.', 'Nous ajusterons cela !', 2);

-- Ajout de la relation entre Commande et Plat (Création)
INSERT INTO Creation (Commande_Id, Plat_Id)
VALUES
    (1, 1),
    (2, 2);

-- Ajout de la composition des recettes
INSERT INTO Composition_de_la_recette (Ingredient_Id, Recette_id)
VALUES
    (1, 1),
    (3, 1),
    (2, 2);

-- Ajout de la relation de livraison
INSERT INTO livré (Plat_Id, Livraison_Id)
VALUES
    (1, 1),
    (2, 2);

-- Ajout de la relation entre personne et entreprise
INSERT INTO Fait_Partie_De (Personne_Id, Entreprise_Id)
VALUES
    ('P001', 1),
    ('P002', 2);

