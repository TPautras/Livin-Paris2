CREATE DATABASE IF NOT EXISTS livin_paris;
USE livin_paris;

DROP TABLE IF EXISTS Composition_du_plat CASCADE;
DROP TABLE IF EXISTS Ingredient CASCADE;
DROP TABLE IF EXISTS Contient CASCADE;
DROP TABLE IF EXISTS Creation CASCADE;
DROP TABLE IF EXISTS Plat CASCADE;
DROP TABLE IF EXISTS Livraison CASCADE;
DROP TABLE IF EXISTS Evaluation CASCADE;
DROP TABLE IF EXISTS Commande CASCADE;
DROP TABLE IF EXISTS Cuisinier CASCADE;
DROP TABLE IF EXISTS Clients CASCADE;
DROP TABLE IF EXISTS Personne CASCADE;

CREATE TABLE Personne(
                         Personne_Id VARCHAR(50),
                         Personne_Nom VARCHAR(50),
                         Personne_Prenom VARCHAR(50),
                         Personne_Ville VARCHAR(50),
                         Personne_Code_postale INT,
                         Personne_Nom_de_la_rue VARCHAR(50),
                         Personne_Numero_de_la_rue INT,
                         Personne_Email VARCHAR(50),
                         Personne_Telephone VARCHAR(50),
                         Personne_Station_de_metro_la_plus_proche VARCHAR(50),
                         PRIMARY KEY(Personne_Id)
);

CREATE TABLE Ingredient(
                           Ingredient_Id INT,
                           Ingredient_Nom VARCHAR(50),
                           Ingredient_volume VARCHAR(50),
                           PRIMARY KEY(Ingredient_Id)
);

CREATE TABLE Livraison(
                          Livraison_Id INT,
                          Livraison_Adresse VARCHAR(50),
                          Livraison_Date DATETIME,
                          PRIMARY KEY(Livraison_Id)
);

CREATE TABLE Recette(
                        Recette_id INT,
                        Recette_Nom VARCHAR(50),
                        Recette_Origine VARCHAR(50),
                        Recette_Type_de_plat VARCHAR(50),
                        Recette_Apport_nutritifs VARCHAR(50),
                        Recette_Regime_alimentaire VARCHAR(50),
                        PRIMARY KEY(Recette_id)
);

CREATE TABLE Cuisinier(
                          Cuisinier_Id INT,
                          Cuisinier_Password VARCHAR(50) NOT NULL,
                          Personne_Id VARCHAR(50) NOT NULL,
                          PRIMARY KEY(Cuisinier_Id),
                          UNIQUE(Personne_Id),
                          FOREIGN KEY(Personne_Id) REFERENCES Personne(Personne_Id)
);

CREATE TABLE Clients(
                        Client_Id INT,
                        Client_Password VARCHAR(50) NOT NULL,
                        Personne_Id VARCHAR(50) NOT NULL,
                        PRIMARY KEY(Client_Id),
                        UNIQUE(Personne_Id),
                        FOREIGN KEY(Personne_Id) REFERENCES Personne(Personne_Id)
);

CREATE TABLE Commande(
                         Commande_Id INT,
                         Cuisinier_Id INT NOT NULL,
                         Client_Id INT NOT NULL,
                         PRIMARY KEY(Commande_Id),
                         FOREIGN KEY(Cuisinier_Id) REFERENCES Cuisinier(Cuisinier_Id),
                         FOREIGN KEY(Client_Id) REFERENCES Clients(Client_Id)
);

CREATE TABLE Plat(
                     Plat_Id INT,
                     Plat_date_de_fabrication DATE,
                     Plat_Date_de_peremption DATE,
                     Plat_Prix VARCHAR(50),
                     Plat_Nombre_Portion INT NOT NULL,
                     Recette_id INT NOT NULL,
                     Commande_Id INT NOT NULL,
                     PRIMARY KEY(Plat_Id),
                     FOREIGN KEY(Recette_id) REFERENCES Recette(Recette_id),
                     FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id)
);

CREATE TABLE evaluation(
                           Evaluation_Id INT,
                           Evaluation_Client DECIMAL(15,2),
                           Evaluation_Cuisinier DECIMAL(15,2),
                           Commande_Id INT NOT NULL,
                           PRIMARY KEY(Evaluation_Id),
                           FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id)
);

CREATE TABLE Composition_de_la_recette(
                                          Ingredient_Id INT,
                                          Recette_id INT,
                                          PRIMARY KEY(Ingredient_Id, Recette_id),
                                          FOREIGN KEY(Ingredient_Id) REFERENCES Ingredient(Ingredient_Id),
                                          FOREIGN KEY(Recette_id) REFERENCES Recette(Recette_id)
);

CREATE TABLE livré(
                      Plat_Id INT,
                      Livraison_Id INT,
                      PRIMARY KEY(Plat_Id, Livraison_Id),
                      FOREIGN KEY(Plat_Id) REFERENCES Plat(Plat_Id),
                      FOREIGN KEY(Livraison_Id) REFERENCES Livraison(Livraison_Id)
);
