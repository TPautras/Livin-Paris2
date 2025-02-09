CREATE DATABASE IF NOT EXISTS livin_paris;
USE livin_paris;

-- Table Utilisateurs (Clients & Cuisiniers)
CREATE TABLE Utilisateurs (
    id INT AUTO_INCREMENT PRIMARY KEY,
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
    id INT AUTO_INCREMENT PRIMARY KEY,
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
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE
);

-- Table Commandes
CREATE TABLE Commandes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    client_id INT,
    date_commande DATETIME DEFAULT CURRENT_TIMESTAMP,
    prix_total DECIMAL(10,2) NOT NULL,
    statut ENUM('en attente', 'validée', 'livrée', 'annulée') DEFAULT 'en attente',
    FOREIGN KEY (client_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE
);

-- Table LigneCommande (Un client peut commander plusieurs plats)
CREATE TABLE LigneCommande (
    id INT AUTO_INCREMENT PRIMARY KEY,
    commande_id INT,
    plat_id INT,
    quantite INT NOT NULL,
    prix_unitaire DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (commande_id) REFERENCES Commandes(id) ON DELETE CASCADE,
    FOREIGN KEY (plat_id) REFERENCES Plats(id) ON DELETE CASCADE
);

-- Table Livraisons
CREATE TABLE Livraisons (
    id INT AUTO_INCREMENT PRIMARY KEY,
    commande_id INT,
    adresse_livraison TEXT NOT NULL,
    date_livraison DATE NOT NULL,
    cuisinier_id INT,
    FOREIGN KEY (commande_id) REFERENCES Commandes(id) ON DELETE CASCADE,
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE
);

-- Table Historique des Transactions
CREATE TABLE Transactions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    commande_id INT,
    date_transaction DATETIME DEFAULT CURRENT_TIMESTAMP,
    montant DECIMAL(10,2) NOT NULL,
    mode_paiement ENUM('carte', 'paypal', 'espèces') NOT NULL,
    statut ENUM('réussi', 'échec', 'remboursé') DEFAULT 'réussi',
    FOREIGN KEY (commande_id) REFERENCES Commandes(id) ON DELETE CASCADE
);

-- Table Avis Clients
CREATE TABLE Avis (
    id INT AUTO_INCREMENT PRIMARY KEY,
    client_id INT,
    cuisinier_id INT,
    note INT CHECK (note >= 1 AND note <= 5),
    commentaire TEXT,
    date_avis DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (client_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE,
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE
);

-- Table Relations Client-Cuisinier (Historique des commandes passées entre eux)
CREATE TABLE Relations_Client_Cuisinier (
    id INT AUTO_INCREMENT PRIMARY KEY,
    client_id INT,
    cuisinier_id INT,
    total_commandes INT DEFAULT 0,
    FOREIGN KEY (client_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE,
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE
);

-- Table Zones de Livraison
CREATE TABLE Zones_Livraison (
     id INT AUTO_INCREMENT PRIMARY KEY,
     nom_zone VARCHAR(255) NOT NULL,
     description TEXT
);

-- Table Assignation des Zones aux Cuisiniers
CREATE TABLE Assignation_Zones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cuisinier_id INT,
    zone_id INT,
    FOREIGN KEY (cuisinier_id) REFERENCES Utilisateurs(id) ON DELETE CASCADE,
    FOREIGN KEY (zone_id) REFERENCES Zones_Livraison(id) ON DELETE CASCADE
);

-- Table Métro pour le calcul des itinéraires (Graphes)
CREATE TABLE Metro_Stations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    latitude DECIMAL(10,6),
    longitude DECIMAL(10,6)
);

CREATE TABLE Metro_Liaisons (
    id INT AUTO_INCREMENT PRIMARY KEY,
    station_depart INT,
    station_arrivee INT,
    duree INT NOT NULL,
    FOREIGN KEY (station_depart) REFERENCES Metro_Stations(id) ON DELETE CASCADE,
    FOREIGN KEY (station_arrivee) REFERENCES Metro_Stations(id) ON DELETE CASCADE
);
