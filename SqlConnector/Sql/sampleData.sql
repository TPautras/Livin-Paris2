
INSERT INTO Personne (Personne_Id, Personne_Nom, Personne_Prenom, Personne_Ville) VALUES
    (1, 'Dupont', 'Jean', 'Paris'),
    (2, 'Martin', 'Sophie', 'Lyon'),
    (3, 'Durand', 'Alice', 'Marseille');

INSERT INTO Clients (Client_Id) VALUES (1);
INSERT INTO Cuisinier (Cuisinier_Id) VALUES (2);
INSERT INTO Clients (Client_Id) VALUES (3);

INSERT INTO Commande (Commande_Id, Commande_Date, Client_Id) VALUES
    (101, '2025-02-10', 1),
    (102, '2025-02-10', 3);

INSERT INTO Plat (Plat_Id, Plat_Nom, Plat_Type_de_plat) VALUES
    (201, 'Pizza Margherita', 'Plat principal'),
    (202, 'Salade César', 'Entrée');

INSERT INTO Creation (Cuisinier_Id, Plat_Id) VALUES
    (2, 201),
    (2, 202);

INSERT INTO Contient (Commande_Id, Plat_Id) VALUES
    (101, 201),
    (102, 202);

INSERT INTO Ingredient (Ingredient_Id, Ingredient_Nom, Ingredient_Volume) VALUES
    (301, 'Tomate', '100g'),
    (302, 'Mozzarella', '80g'),
    (303, 'Salade', '50g');

INSERT INTO Composition_du_plat (Plat_Id, Ingredient_Id) VALUES
    (201, 301),
    (201, 302),
    (202, 303);

INSERT INTO Evaluation (Evaluation_Id, Evaluation_Client, Evaluation_Cuisinier, Commande_Id) VALUES
    (401, 1, 2, 101);

INSERT INTO Livraison (Livraison_Id, Livraison_Adresse, Livraison_Date, Commande_Id) VALUES
    (501, '12, Rue de la Paix, 75002 Paris', '2025-02-11', 101);

SELECT * FROM personne;
