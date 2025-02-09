CREATE DATABASE IF NOT EXISTS livin_paris;
USE livin_paris;

-- Table Utilisateurs (Cuisinier ou Client)
CREATE TABLE Utilisateurs (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    telephone VARCHAR(20),
    adresse TEXT NOT NULL,
    type ENUM('client', 'cuisinier', 'les deux') NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- Table Plats
CREATE TABLE Plats (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nom VARCHAR(255) NOT NULL,
    description TEXT,
    categorie ENUM('entrée', 'plat', 'dessert') NOT NULL,
    personnes INT NOT NULL,
    date_fabrication DATE NOT NULL,
    date_peremption DATE NOT NULL,
    prix DECIMAL(10,2) NOT NULL,
    nationalite VARCHAR(100),
    regime_alimentaire VARCHAR(255),
    ingredients TEXT,
    photo VARCHAR(255),
    cuisinier_id INT,
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id)
);

-- Table Commandes
CREATE TABLE Commandes (
    id INT PRIMARY KEY AUTO_INCREMENT,
    client_id INT,
    date_commande DATETIME DEFAULT CURRENT_TIMESTAMP,
    prix_total DECIMAL(10,2) NOT NULL,
    statut ENUM('en attente', 'validée', 'livrée', 'annulée') DEFAULT 'en attente',
    FOREIGN KEY (client_id) REFERENCES Utilisateurs(id)
);

-- Table LigneCommande (Un client peut commander plusieurs plats)
CREATE TABLE LigneCommande (
    id INT PRIMARY KEY AUTO_INCREMENT,
    commande_id INT,
    plat_id INT,
    quantite INT NOT NULL,
    prix_unitaire DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (commande_id) REFERENCES Commandes(id),
    FOREIGN KEY (plat_id) REFERENCES Plats(id)
);

-- Table Livraisons
CREATE TABLE Livraisons (
    id INT PRIMARY KEY AUTO_INCREMENT,
    commande_id INT,
    adresse_livraison TEXT NOT NULL,
    date_livraison DATE NOT NULL,
    cuisinier_id INT,
    FOREIGN KEY (commande_id) REFERENCES Commandes(id),
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id)
);


