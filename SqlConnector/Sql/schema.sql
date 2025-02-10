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

CREATE TABLE Personne (
    Personne_Id INT PRIMARY KEY,
    Personne_Nom VARCHAR(50) NOT NULL,
    Personne_Prenom VARCHAR(50) NOT NULL,
    Personne_Numero_de_licence VARCHAR(50),
    Personne_Ville VARCHAR(50),
    Personne_Code_postale VARCHAR(10),
    Personne_Nom_de_la_rue VARCHAR(100),
    Personne_Email VARCHAR(100),
    Personne_Telephone VARCHAR(20),
    Personne_Station_de_metro_la_plus_proche VARCHAR(50)
);

CREATE TABLE Clients (
    Client_Id INT PRIMARY KEY,
    FOREIGN KEY (Client_Id) REFERENCES Personne(Personne_Id)
);

CREATE TABLE Cuisinier (
    Cuisinier_Id INT PRIMARY KEY,
    FOREIGN KEY (Cuisinier_Id) REFERENCES Personne(Personne_Id)
);

CREATE TABLE Commande (
    Commande_Id INT PRIMARY KEY,
    Commande_Date DATE,
    Client_Id INT,
    FOREIGN KEY (Client_Id) REFERENCES Clients(Client_Id)
);

CREATE TABLE Evaluation (
    Evaluation_Id INT PRIMARY KEY,
    Evaluation_Client INT,
    Evaluation_Cuisinier INT,
    Commande_Id INT,
    FOREIGN KEY (Evaluation_Client) REFERENCES Clients(Client_Id),
    FOREIGN KEY (Evaluation_Cuisinier) REFERENCES Cuisinier(Cuisinier_Id),
    FOREIGN KEY (Commande_Id) REFERENCES Commande(Commande_Id)
);

CREATE TABLE Livraison (
    Livraison_Id INT PRIMARY KEY,
    Livraison_Adresse VARCHAR(255),
    Livraison_Date DATE,
    Commande_Id INT,
    FOREIGN KEY (Commande_Id) REFERENCES Commande(Commande_Id)
);

CREATE TABLE Plat (
    Plat_Id INT PRIMARY KEY,
    Plat_Nom VARCHAR(50),
    Plat_Origine VARCHAR(50),
    Plat_Aromes_naturels VARCHAR(100),
    Plat_Date_de_fabrication DATE,
    Plat_Date_de_peremption DATE,
    Plat_Type_de_plat VARCHAR(50),
    Plat_Regime_alimentaire VARCHAR(50)
);

CREATE TABLE Creation (
    Cuisinier_Id INT,
    Plat_Id INT,
    PRIMARY KEY (Cuisinier_Id, Plat_Id),
    FOREIGN KEY (Cuisinier_Id) REFERENCES Cuisinier(Cuisinier_Id),
    FOREIGN KEY (Plat_Id) REFERENCES Plat(Plat_Id)
);

CREATE TABLE Contient (
    Commande_Id INT,
    Plat_Id INT,
    PRIMARY KEY (Commande_Id, Plat_Id),
    FOREIGN KEY (Commande_Id) REFERENCES Commande(Commande_Id),
    FOREIGN KEY (Plat_Id) REFERENCES Plat(Plat_Id)
);

CREATE TABLE Ingredient (
    Ingredient_Id INT PRIMARY KEY,
    Ingredient_Nom VARCHAR(50),
    Ingredient_Volume VARCHAR(50)
);

CREATE TABLE Composition_du_plat (
    Plat_Id INT,
    Ingredient_Id INT,
    PRIMARY KEY (Plat_Id, Ingredient_Id),
    FOREIGN KEY (Plat_Id) REFERENCES Plat(Plat_Id),
    FOREIGN KEY (Ingredient_Id) REFERENCES Ingredient(Ingredient_Id)
);
