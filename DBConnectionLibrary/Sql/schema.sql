DROP DATABASE IF EXISTS livin_paris;
CREATE DATABASE IF NOT EXISTS livin_paris;
USE livin_paris;

DROP TABLE IF EXISTS Composition_du_plat CASCADE;
DROP TABLE IF EXISTS composition_de_la_recette CASCADE;
DROP TABLE IF EXISTS Ingredient CASCADE;
DROP TABLE IF EXISTS creation CASCADE;
DROP TABLE IF EXISTS Contient CASCADE;
DROP TABLE IF EXISTS fait_partie_de CASCADE;
DROP TABLE IF EXISTS livré CASCADE;
DROP TABLE IF EXISTS Plat CASCADE;
DROP TABLE IF EXISTS livraison CASCADE;
DROP TABLE IF EXISTS Evaluation CASCADE;
DROP TABLE IF EXISTS Commande CASCADE;
DROP TABLE IF EXISTS cuisinier CASCADE;
DROP TABLE IF EXISTS Clients CASCADE;
DROP TABLE IF EXISTS personne CASCADE;
DROP TABLE IF EXISTS entreprise CASCADE;
DROP TABLE IF EXISTS recette CASCADE;

CREATE TABLE Personne(
    Personne_Email VARCHAR(50),
    Personne_Nom VARCHAR(50),
    Personne_Prenom VARCHAR(50),
    Personne_Ville VARCHAR(50),
    Personne_Code_postale INT,
    Personne_Nom_de_la_rue VARCHAR(50),
    Personne_Numero_de_la_rue INT,
    Personne_Telephone VARCHAR(50),
    Personne_Station_de_metro_la_plus_proche VARCHAR(50),
    Personne_Is_Admin BOOL,
    PRIMARY KEY(Personne_Email)
);

CREATE TABLE Ingredient(
                           Ingredient_Id INT,
                           Ingredient_Nom VARCHAR(50),
                           Ingredient_volume VARCHAR(50),
                           Ingrédient_Unité VARCHAR(50),
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

CREATE TABLE Entreprise(
                           Entreprise_Id INT,
                           Entreprise_Nom VARCHAR(50) NOT NULL,
                           PRIMARY KEY(Entreprise_Id),
                           UNIQUE(Entreprise_Nom)
);

CREATE TABLE Cuisinier(
                          Cuisinier_Username VARCHAR(50),
                          Cuisinier_Password VARCHAR(50) NOT NULL,
                          Personne_Email VARCHAR(50) NOT NULL,
                          PRIMARY KEY(Cuisinier_Username),
                          UNIQUE(Personne_Email),
                          FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email) ON UPDATE CASCADE
);

CREATE TABLE Clients(
                        Client_Username VARCHAR(50),
                        Client_Password VARCHAR(50) NOT NULL,
                        Personne_Email VARCHAR(50) NOT NULL,
                        PRIMARY KEY(Client_Username) ,
                        UNIQUE(Personne_Email),
                        FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email) ON UPDATE CASCADE
);

CREATE TABLE Commande(
                         Commande_Id INT,
                         Entreprise_Id INT,
                         Cuisinier_Username VARCHAR(50) NOT NULL,
                         Client_Username VARCHAR(50) NOT NULL,
                         Commande_Date DATETIME,
                         PRIMARY KEY(Commande_Id),
                         FOREIGN KEY(Entreprise_Id) REFERENCES Entreprise(Entreprise_Id) ON DELETE CASCADE ON UPDATE CASCADE,
                         FOREIGN KEY(Cuisinier_Username) REFERENCES Cuisinier(Cuisinier_Username) ON DELETE CASCADE ON UPDATE CASCADE,
                         FOREIGN KEY(Client_Username) REFERENCES Clients(Client_Username) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Plat(
                     Plat_Id INT,
                     Plat_date_de_fabrication DATE,
                     Plat_Date_de_peremption DATE,
                     Plat_Prix VARCHAR(50),
                     Plat_Nombre_Portion INT NOT NULL,
                     Cuisinier_Username VARCHAR(50) NOT NULL,
                     Recette_id INT NOT NULL,
                     Plat_Du_Jour BOOL,
                     PRIMARY KEY(Plat_Id),
                     FOREIGN KEY(Cuisinier_Username) REFERENCES Cuisinier(Cuisinier_Username) ON DELETE CASCADE ON UPDATE CASCADE,
                     FOREIGN KEY(Recette_id) REFERENCES Recette(Recette_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE evaluation(
                           Evaluation_Id INT,
                           Evaluation_Client DECIMAL(15,2),
                           Evaluation_Cuisinier DECIMAL(15,2),
                           Evaluation_Description_Client TEXT,
                           Evaluation_Description_Cuisinier TEXT,
                           Commande_Id INT NOT NULL,
                           PRIMARY KEY(Evaluation_Id),
                           FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Creation(
                         Commande_Id INT,
                         Plat_Id INT,
                         Creation_Quantity INT,
                         PRIMARY KEY(Commande_Id, Plat_Id),
                         FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id) ON DELETE CASCADE ON UPDATE CASCADE,
                         FOREIGN KEY(Plat_Id) REFERENCES Plat(Plat_Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Composition_de_la_recette(
                                          Ingredient_Id INT,
                                          Recette_id INT,
                                          PRIMARY KEY(Ingredient_Id, Recette_id),
                                          FOREIGN KEY(Ingredient_Id) REFERENCES Ingredient(Ingredient_Id) ON DELETE CASCADE ON UPDATE CASCADE,
                                          FOREIGN KEY(Recette_id) REFERENCES Recette(Recette_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE livré(
                      Plat_Id INT,
                      Livraison_Id INT,
                      PRIMARY KEY(Plat_Id, Livraison_Id),
                      FOREIGN KEY(Plat_Id) REFERENCES Plat(Plat_Id) ON DELETE CASCADE ON UPDATE CASCADE,
                      FOREIGN KEY(Livraison_Id) REFERENCES Livraison(Livraison_Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Fait_Partie_De(
                               Personne_Email VARCHAR(50),
                               Entreprise_Id INT,
                               PRIMARY KEY(Personne_Email, Entreprise_Id),
                               FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email) ON DELETE CASCADE ON UPDATE CASCADE,
                               FOREIGN KEY(Entreprise_Id) REFERENCES Entreprise(Entreprise_Id) ON DELETE CASCADE ON UPDATE CASCADE
);
