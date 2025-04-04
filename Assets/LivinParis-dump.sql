-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: livin_paris
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clients` (
  `Client_Username` varchar(50) NOT NULL,
  `Client_Password` varchar(50) NOT NULL,
  `Personne_Email` varchar(50) NOT NULL,
  PRIMARY KEY (`Client_Username`),
  UNIQUE KEY `Personne_Email` (`Personne_Email`),
  CONSTRAINT `clients_ibfk_1` FOREIGN KEY (`Personne_Email`) REFERENCES `personne` (`Personne_Email`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES ('admin','admin','admin'),('client1','pwdclient1','person16@example.com'),('client10','pwdclient10','person25@example.com'),('client11','pwdclient11','person26@example.com'),('client12','pwdclient12','person27@example.com'),('client13','pwdclient13','person28@example.com'),('client14','pwdclient14','person29@example.com'),('client15','pwdclient15','person30@example.com'),('client16','pwdclient16','person31@example.com'),('client17','pwdclient17','person32@example.com'),('client18','pwdclient18','person33@example.com'),('client19','pwdclient19','person34@example.com'),('client2','pwdclient2','person17@example.com'),('client20','pwdclient20','person35@example.com'),('client3','pwdclient3','person18@example.com'),('client4','pwdclient4','person19@example.com'),('client5','pwdclient5','person20@example.com'),('client6','pwdclient6','person21@example.com'),('client7','pwdclient7','person22@example.com'),('client8','pwdclient8','person23@example.com'),('client9','pwdclient9','person24@example.com');
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commande`
--

DROP TABLE IF EXISTS `commande`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commande` (
  `Commande_Id` int NOT NULL,
  `Entreprise_Id` int NOT NULL,
  `Cuisinier_Username` varchar(50) NOT NULL,
  `Client_Username` varchar(50) NOT NULL,
  `Commande_Date` datetime DEFAULT NULL,
  PRIMARY KEY (`Commande_Id`),
  KEY `Entreprise_Id` (`Entreprise_Id`),
  KEY `Cuisinier_Username` (`Cuisinier_Username`),
  KEY `Client_Username` (`Client_Username`),
  CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`Entreprise_Id`) REFERENCES `entreprise` (`Entreprise_Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `commande_ibfk_2` FOREIGN KEY (`Cuisinier_Username`) REFERENCES `cuisinier` (`Cuisinier_Username`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `commande_ibfk_3` FOREIGN KEY (`Client_Username`) REFERENCES `clients` (`Client_Username`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commande`
--

LOCK TABLES `commande` WRITE;
/*!40000 ALTER TABLE `commande` DISABLE KEYS */;
INSERT INTO `commande` VALUES (1,1,'cuisinier1','client1','2025-03-05 14:30:00'),(2,2,'cuisinier2','client2','2025-03-06 12:15:00'),(3,3,'cuisinier3','client3','2025-03-07 18:20:00'),(4,4,'cuisinier4','client4','2025-03-08 11:00:00'),(5,5,'cuisinier5','client5','2025-03-09 16:45:00'),(6,6,'cuisinier6','client6','2025-03-10 13:30:00'),(7,7,'cuisinier7','client7','2025-03-11 15:00:00'),(8,8,'cuisinier8','client8','2025-03-12 17:10:00'),(9,9,'cuisinier9','client9','2025-03-13 10:05:00'),(10,10,'cuisinier10','client10','2025-03-14 19:00:00'),(11,1,'cuisinier11','client11','2025-03-15 12:50:00'),(12,2,'cuisinier12','client12','2025-03-16 14:20:00'),(13,3,'cuisinier13','client13','2025-03-17 16:00:00'),(14,4,'cuisinier14','client14','2025-03-18 11:45:00'),(15,5,'cuisinier15','client15','2025-03-19 18:30:00'),(16,6,'cuisinier1','client16','2025-03-20 13:15:00'),(17,7,'cuisinier2','client17','2025-03-21 15:30:00'),(18,8,'cuisinier3','client18','2025-03-22 17:45:00'),(19,9,'cuisinier4','client19','2025-03-23 12:00:00'),(20,10,'cuisinier5','client20','2025-03-24 14:10:00'),(21,1,'cuisinier6','client1','2025-03-25 18:20:00'),(22,2,'cuisinier7','client2','2025-03-26 11:30:00'),(23,3,'cuisinier8','client3','2025-03-27 13:40:00'),(24,4,'cuisinier9','client4','2025-03-28 16:55:00'),(25,5,'cuisinier10','client5','2025-03-29 12:35:00');
/*!40000 ALTER TABLE `commande` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `composition_de_la_recette`
--

DROP TABLE IF EXISTS `composition_de_la_recette`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `composition_de_la_recette` (
  `Ingredient_Id` int NOT NULL,
  `Recette_id` int NOT NULL,
  PRIMARY KEY (`Ingredient_Id`,`Recette_id`),
  KEY `Recette_id` (`Recette_id`),
  CONSTRAINT `composition_de_la_recette_ibfk_1` FOREIGN KEY (`Ingredient_Id`) REFERENCES `ingredient` (`Ingredient_Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `composition_de_la_recette_ibfk_2` FOREIGN KEY (`Recette_id`) REFERENCES `recette` (`Recette_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `composition_de_la_recette`
--

LOCK TABLES `composition_de_la_recette` WRITE;
/*!40000 ALTER TABLE `composition_de_la_recette` DISABLE KEYS */;
INSERT INTO `composition_de_la_recette` VALUES (1,1),(16,1),(2,2),(17,2),(3,3),(18,3),(4,4),(19,4),(5,5),(20,5),(6,6),(21,6),(7,7),(22,7),(8,8),(23,8),(9,9),(24,9),(10,10),(25,10),(11,11),(12,11),(13,12),(14,12),(1,13),(15,13),(2,14),(3,14),(4,15),(5,15),(6,16),(7,16),(8,17),(9,17),(10,18),(11,18),(12,19),(13,19),(14,20),(15,20);
/*!40000 ALTER TABLE `composition_de_la_recette` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `creation`
--

DROP TABLE IF EXISTS `creation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `creation` (
  `Commande_Id` int NOT NULL,
  `Plat_Id` int NOT NULL,
  PRIMARY KEY (`Commande_Id`,`Plat_Id`),
  KEY `Plat_Id` (`Plat_Id`),
  CONSTRAINT `creation_ibfk_1` FOREIGN KEY (`Commande_Id`) REFERENCES `commande` (`Commande_Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `creation_ibfk_2` FOREIGN KEY (`Plat_Id`) REFERENCES `plat` (`Plat_Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `creation`
--

LOCK TABLES `creation` WRITE;
/*!40000 ALTER TABLE `creation` DISABLE KEYS */;
INSERT INTO `creation` VALUES (1,1),(2,2),(3,3),(4,4),(5,5),(6,6),(7,7),(8,8),(9,9),(10,10),(11,11),(12,12),(13,13),(14,14),(15,15),(16,16),(17,17),(18,18),(19,19),(20,20),(21,21),(22,22),(23,23),(24,24),(25,25);
/*!40000 ALTER TABLE `creation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cuisinier`
--

DROP TABLE IF EXISTS `cuisinier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cuisinier` (
  `Cuisinier_Username` varchar(50) NOT NULL,
  `Cuisinier_Password` varchar(50) NOT NULL,
  `Personne_Email` varchar(50) NOT NULL,
  PRIMARY KEY (`Cuisinier_Username`),
  UNIQUE KEY `Personne_Email` (`Personne_Email`),
  CONSTRAINT `cuisinier_ibfk_1` FOREIGN KEY (`Personne_Email`) REFERENCES `personne` (`Personne_Email`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cuisinier`
--

LOCK TABLES `cuisinier` WRITE;
/*!40000 ALTER TABLE `cuisinier` DISABLE KEYS */;
INSERT INTO `cuisinier` VALUES ('admin','admin','admin'),('cuisinier1','pwdchef1','person1@example.com'),('cuisinier10','pwdchef10','person10@example.com'),('cuisinier11','pwdchef11','person11@example.com'),('cuisinier12','pwdchef12','person12@example.com'),('cuisinier13','pwdchef13','person13@example.com'),('cuisinier14','pwdchef14','person14@example.com'),('cuisinier15','pwdchef15','person15@example.com'),('cuisinier2','pwdchef2','person2@example.com'),('cuisinier3','pwdchef3','person3@example.com'),('cuisinier4','pwdchef4','person4@example.com'),('cuisinier5','pwdchef5','person5@example.com'),('cuisinier6','pwdchef6','person6@example.com'),('cuisinier7','pwdchef7','person7@example.com'),('cuisinier8','pwdchef8','person8@example.com'),('cuisinier9','pwdchef9','person9@example.com');
/*!40000 ALTER TABLE `cuisinier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entreprise`
--

DROP TABLE IF EXISTS `entreprise`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entreprise` (
  `Entreprise_Id` int NOT NULL,
  `Entreprise_Nom` varchar(50) NOT NULL,
  PRIMARY KEY (`Entreprise_Id`),
  UNIQUE KEY `Entreprise_Nom` (`Entreprise_Nom`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entreprise`
--

LOCK TABLES `entreprise` WRITE;
/*!40000 ALTER TABLE `entreprise` DISABLE KEYS */;
INSERT INTO `entreprise` VALUES (8,'Bon Appétit SARL'),(10,'Chef à Domicile'),(6,'Cuisine Authentique'),(3,'Délices Express'),(7,'Epicurien Services'),(4,'Gastronomie 75'),(2,'Le Bon Goût'),(9,'Livraison Gourmande'),(1,'Paris Gourmand'),(5,'Saveurs d\'Exception');
/*!40000 ALTER TABLE `entreprise` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluation`
--

DROP TABLE IF EXISTS `evaluation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `evaluation` (
  `Evaluation_Id` int NOT NULL,
  `Evaluation_Client` decimal(15,2) DEFAULT NULL,
  `Evaluation_Cuisinier` decimal(15,2) DEFAULT NULL,
  `Evaluation_Description_Client` text,
  `Evaluation_Description_Cuisinier` text,
  `Commande_Id` int NOT NULL,
  PRIMARY KEY (`Evaluation_Id`),
  KEY `Commande_Id` (`Commande_Id`),
  CONSTRAINT `evaluation_ibfk_1` FOREIGN KEY (`Commande_Id`) REFERENCES `commande` (`Commande_Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluation`
--

LOCK TABLES `evaluation` WRITE;
/*!40000 ALTER TABLE `evaluation` DISABLE KEYS */;
INSERT INTO `evaluation` VALUES (1,4.50,4.00,'Plat savoureux et bien présenté','Client poli et précis',1),(2,3.50,3.00,'Correct, peut mieux faire','Manque de ponctualité',2),(3,5.00,4.50,'Une expérience culinaire excellente','Service très professionnel',3),(4,4.00,4.00,'Plat bien équilibré','Communication claire',4),(5,3.00,3.50,'Goût moyen','Peut améliorer la présentation',5),(6,4.50,4.00,'Très bon plat, recommandé','Client sympathique',6),(7,3.50,3.00,'Rien de spécial','Livraison dans les temps',7),(8,5.00,4.50,'Parfait, je recommande vivement','Excellent travail',8),(9,4.00,4.00,'Bonne expérience','Service amical',9),(10,3.00,3.50,'Peut mieux faire','Client à l’écoute',10),(11,4.50,4.00,'Plat de grande qualité','Très professionnel',11),(12,3.50,3.00,'Correct, sans plus','Bon suivi',12),(13,5.00,4.50,'Exceptionnel sur tous les plans','Communication parfaite',13),(14,4.00,4.00,'Satisfaisant','Service régulier',14),(15,3.00,3.50,'Moyen, manque d\'originalité','Client ponctuel',15),(16,4.00,3.50,'Très bon plat','Réactif et professionnel',16),(17,4.50,4.00,'Excellente présentation','Service de qualité',17),(18,3.50,3.00,'Plat correct','Peut s\'améliorer',18),(19,5.00,4.50,'Une réussite totale','Client très agréable',19),(20,4.00,4.00,'Plat savoureux','Livraison rapide',20),(21,3.50,3.00,'Goût authentique','Bien exécuté',21),(22,4.50,4.00,'Très bon rapport qualité/prix','Client courtois',22),(23,3.50,3.00,'Peut être amélioré','Service correct',23),(24,5.00,4.50,'Expérience inoubliable','Livraison impeccable',24),(25,4.00,4.00,'Plat délicieux et équilibré','Client satisfait',25);
/*!40000 ALTER TABLE `evaluation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fait_partie_de`
--

DROP TABLE IF EXISTS `fait_partie_de`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fait_partie_de` (
  `Personne_Email` varchar(50) NOT NULL,
  `Entreprise_Id` int NOT NULL,
  PRIMARY KEY (`Personne_Email`,`Entreprise_Id`),
  KEY `Entreprise_Id` (`Entreprise_Id`),
  CONSTRAINT `fait_partie_de_ibfk_1` FOREIGN KEY (`Personne_Email`) REFERENCES `personne` (`Personne_Email`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fait_partie_de_ibfk_2` FOREIGN KEY (`Entreprise_Id`) REFERENCES `entreprise` (`Entreprise_Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fait_partie_de`
--

LOCK TABLES `fait_partie_de` WRITE;
/*!40000 ALTER TABLE `fait_partie_de` DISABLE KEYS */;
INSERT INTO `fait_partie_de` VALUES ('person1@example.com',1),('person11@example.com',1),('person12@example.com',2),('person2@example.com',2),('person13@example.com',3),('person3@example.com',3),('person14@example.com',4),('person4@example.com',4),('person15@example.com',5),('person5@example.com',5),('person36@example.com',6),('person6@example.com',6),('person37@example.com',7),('person7@example.com',7),('person38@example.com',8),('person8@example.com',8),('person39@example.com',9),('person9@example.com',9),('person10@example.com',10),('person40@example.com',10);
/*!40000 ALTER TABLE `fait_partie_de` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingredient`
--

DROP TABLE IF EXISTS `ingredient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredient` (
  `Ingredient_Id` int NOT NULL,
  `Ingredient_Nom` varchar(50) DEFAULT NULL,
  `Ingredient_volume` varchar(50) DEFAULT NULL,
  `Ingrédient_Unité` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Ingredient_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingredient`
--

LOCK TABLES `ingredient` WRITE;
/*!40000 ALTER TABLE `ingredient` DISABLE KEYS */;
INSERT INTO `ingredient` VALUES (1,'Tomate','100ml','ml'),(2,'Fromage','200g','g'),(3,'Poulet','150g','g'),(4,'Boeuf','250g','g'),(5,'Carotte','80g','g'),(6,'Pommes de terre','300g','g'),(7,'Oignon','50g','g'),(8,'Ail','10g','g'),(9,'Basilic','5g','g'),(10,'Thym','5g','g'),(11,'Laurier','3g','g'),(12,'Sel','2g','g'),(13,'Poivre','2g','g'),(14,'Huile d\'olive','50ml','ml'),(15,'Champignon','100g','g'),(16,'Pâtes','200g','g'),(17,'Riz','250g','g'),(18,'Crème','100ml','ml'),(19,'Oeuf','60g','g'),(20,'Beurre','50g','g'),(21,'Farine','100g','g'),(22,'Sucre','30g','g'),(23,'Citron','50ml','ml'),(24,'Poivron','80g','g'),(25,'Courgette','90g','g');
/*!40000 ALTER TABLE `ingredient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livraison`
--

DROP TABLE IF EXISTS `livraison`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livraison` (
  `Livraison_Id` int NOT NULL,
  `Livraison_Adresse` varchar(50) DEFAULT NULL,
  `Livraison_Date` datetime DEFAULT NULL,
  PRIMARY KEY (`Livraison_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livraison`
--

LOCK TABLES `livraison` WRITE;
/*!40000 ALTER TABLE `livraison` DISABLE KEYS */;
INSERT INTO `livraison` VALUES (1,'10 Rue de Rivoli, Paris','2025-03-01 10:00:00'),(2,'20 Avenue des Champs-Élysées, Paris','2025-03-02 12:00:00'),(3,'15 Boulevard Saint-Germain, Paris','2025-03-03 11:30:00'),(4,'5 Rue de la Paix, Paris','2025-03-04 09:45:00'),(5,'30 Avenue Montaigne, Paris','2025-03-05 14:15:00'),(6,'8 Rue de la Pompe, Paris','2025-03-06 16:00:00'),(7,'12 Rue de Sèvres, Paris','2025-03-07 10:30:00'),(8,'25 Boulevard Haussmann, Paris','2025-03-08 13:00:00'),(9,'3 Rue du Faubourg Saint-Honoré, Paris','2025-03-09 15:00:00'),(10,'50 Avenue Kléber, Paris','2025-03-10 17:30:00'),(11,'18 Rue Oberkampf, Paris','2025-03-11 12:20:00'),(12,'22 Rue de Charonne, Paris','2025-03-12 14:10:00'),(13,'7 Rue des Rosiers, Paris','2025-03-13 11:00:00'),(14,'33 Rue du Temple, Paris','2025-03-14 10:50:00'),(15,'9 Rue Oberkampf, Paris','2025-03-15 09:00:00'),(16,'40 Boulevard Voltaire, Paris','2025-03-16 18:00:00'),(17,'28 Rue de Belleville, Paris','2025-03-17 16:30:00'),(18,'6 Rue de Ménilmontant, Paris','2025-03-18 13:45:00'),(19,'14 Rue du Faubourg, Paris','2025-03-19 11:15:00'),(20,'32 Avenue de Clichy, Paris','2025-03-20 15:30:00');
/*!40000 ALTER TABLE `livraison` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livré`
--

DROP TABLE IF EXISTS `livré`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livré` (
  `Plat_Id` int NOT NULL,
  `Livraison_Id` int NOT NULL,
  PRIMARY KEY (`Plat_Id`,`Livraison_Id`),
  KEY `Livraison_Id` (`Livraison_Id`),
  CONSTRAINT `livré_ibfk_1` FOREIGN KEY (`Plat_Id`) REFERENCES `plat` (`Plat_Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `livré_ibfk_2` FOREIGN KEY (`Livraison_Id`) REFERENCES `livraison` (`Livraison_Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livré`
--

LOCK TABLES `livré` WRITE;
/*!40000 ALTER TABLE `livré` DISABLE KEYS */;
INSERT INTO `livré` VALUES (1,1),(21,1),(2,2),(22,2),(3,3),(23,3),(4,4),(24,4),(5,5),(25,5),(6,6),(7,7),(8,8),(9,9),(10,10),(11,11),(12,12),(13,13),(14,14),(15,15),(16,16),(17,17),(18,18),(19,19),(20,20);
/*!40000 ALTER TABLE `livré` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `personne`
--

DROP TABLE IF EXISTS `personne`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `personne` (
  `Personne_Email` varchar(50) NOT NULL,
  `Personne_Nom` varchar(50) DEFAULT NULL,
  `Personne_Prenom` varchar(50) DEFAULT NULL,
  `Personne_Ville` varchar(50) DEFAULT NULL,
  `Personne_Code_postale` int DEFAULT NULL,
  `Personne_Nom_de_la_rue` varchar(50) DEFAULT NULL,
  `Personne_Numero_de_la_rue` int DEFAULT NULL,
  `Personne_Telephone` varchar(50) DEFAULT NULL,
  `Personne_Station_de_metro_la_plus_proche` varchar(50) DEFAULT NULL,
  `Personne_Is_Admin` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Personne_Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personne`
--

LOCK TABLES `personne` WRITE;
/*!40000 ALTER TABLE `personne` DISABLE KEYS */;
INSERT INTO `personne` VALUES ('admin','admin','admin','Lyon',69000,'Rue de Lyon 2',30,'0123456818','Station AD',1),('person1@example.com','Dupont','Jean','Paris',75001,'Rue de la Paix',1,'0123456789','Châtelet',1),('person10@example.com','Garcia','Louis','Rennes',35000,'Rue de la Monnaie',7,'0123456798','Place des Lices',0),('person11@example.com','Martinez','Bernard','Montpellier',34000,'Rue de l\'Université',11,'0123456799','Antigone',0),('person12@example.com','Lopez','Daniel','Grenoble',38000,'Avenue Jean Jaurès',3,'0123456800','Centre',0),('person13@example.com','Gonzalez','David','Dijon',21000,'Rue de la Liberté',16,'0123456801','Dijon Ville',0),('person14@example.com','Hernandez','Alfred','Reims',51100,'Boulevard de Champagne',9,'0123456802','Champagne',0),('person15@example.com','Roux','Henri','Le Havre',76600,'Rue de l\'Ambassade',22,'0123456803','Le Havre Centre',0),('person16@example.com','Blanc','Emmanuel','Saint-Étienne',42000,'Avenue de Verdun',18,'0123456804','Saint-Étienne',0),('person17@example.com','Guerin','Pauline','Toulon',83000,'Rue de la République',6,'0123456805','Toulon',0),('person18@example.com','Fournier','Sophie','Angers',49000,'Rue des Lices',4,'0123456806','Angers Centre',0),('person19@example.com','Morel','Camille','Nîmes',30000,'Avenue de la Romanité',29,'0123456807','Nîmes',0),('person2@example.com','Martin','Paul','Lyon',69001,'Rue de Lyon',12,'0123456790','Part-Dieu',0),('person20@example.com','Girard','Julien','Clermont-Ferrand',63000,'Rue de l\'Univers',13,'0123456808','Clermont',0),('person21@example.com','Andre','Laura','Orléans',45000,'Rue Royale',2,'0123456809','Place du Martroi',0),('person22@example.com','Lambert','Marine','Metz',57000,'Rue Serpenoise',17,'0123456810','Centre',0),('person23@example.com','Fontaine','Nicolas','Brest',29200,'Rue de Siam',30,'0123456811','Recouvrance',0),('person24@example.com','Rousseau','Claire','Limoges',87000,'Rue de la Boucherie',21,'0123456812','Limoges Centre',0),('person25@example.com','Vincent','Mathieu','Tours',37000,'Rue Nationale',15,'0123456813','Tours',0),('person26@example.com','Muller','Hugo','Amiens',80000,'Boulevard de la Libération',27,'0123456814','Amiens',0),('person27@example.com','Leroy','Celine','Dijon',21000,'Rue Mercière',19,'0123456815','Dijon',0),('person28@example.com','Bonnet','Adrien','Nice',60000,'Rue de la Buffa',8,'0123456816','Jean Médecin',0),('person29@example.com','Martineau','Alexandre','Paris',75000,'Rue de Rivoli',35,'0123456817','Châtelet',0),('person3@example.com','Bernard','Pierre','Marseille',13001,'Rue de Marseille',8,'0123456791','La Canebière',0),('person30@example.com','Lemaire','Antoine','Lyon',69000,'Rue Garibaldi',10,'0123456818','Part-Dieu',0),('person31@example.com','Brun','Isabelle','Paris',75012,'Rue de Bercy',5,'0123456819','Bercy',0),('person32@example.com','Marchand','Sébastien','Lyon',69002,'Rue Victor Hugo',22,'0123456820','Bellecour',0),('person33@example.com','Renaud','Amélie','Marseille',13002,'Rue Paradis',17,'0123456821','Noailles',0),('person34@example.com','Gauthier','Juliette','Toulouse',31002,'Rue Alsace-Lorraine',11,'0123456822','Capitole',0),('person35@example.com','Perrin','Mathilde','Nice',60002,'Avenue Jean Médecin',3,'0123456823','Jean Médecin',0),('person36@example.com','Barbier','Antoinette','Nantes',44001,'Rue de Bretagne',14,'0123456824','Nantes',0),('person37@example.com','Garnier','Maxime','Strasbourg',67001,'Rue des Orfèvres',6,'0123456825','Centre',0),('person38@example.com','Dubois','Clara','Bordeaux',33001,'Cours Pey-Berland',9,'0123456826','Bordeaux',0),('person39@example.com','Vidal','Thomas','Rennes',35001,'Rue d\'Antrain',12,'0123456827','Centre',0),('person4@example.com','Petit','Jacques','Lille',59000,'Rue de Lille',25,'0123456792','Moulins',0),('person40@example.com','Noir','Elodie','Montpellier',34001,'Rue de la Loge',7,'0123456828','Antigone',0),('person5@example.com','Robert','Alain','Toulouse',31000,'Rue de Toulouse',14,'0123456793','Capitole',0),('person6@example.com','Richard','Luc','Nice',60000,'Avenue Jean Médecin',5,'0123456794','Jean Médecin',0),('person7@example.com','Durand','Claude','Nantes',44000,'Boulevard de la République',33,'0123456795','Capitole',0),('person8@example.com','Moreau','André','Strasbourg',67000,'Rue des Grandes Arcades',10,'0123456796','Gare',0),('person9@example.com','Lefevre','Michel','Bordeaux',33000,'Cours de l\'Intendance',20,'0123456797','Capucins',0);
/*!40000 ALTER TABLE `personne` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plat`
--

DROP TABLE IF EXISTS `plat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `plat` (
  `Plat_Id` int NOT NULL,
  `Plat_date_de_fabrication` date DEFAULT NULL,
  `Plat_Date_de_peremption` date DEFAULT NULL,
  `Plat_Prix` varchar(50) DEFAULT NULL,
  `Plat_Nombre_Portion` int NOT NULL,
  `Cuisinier_Username` varchar(50) NOT NULL,
  `Recette_id` int NOT NULL,
  `Plat_Du_Jour` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Plat_Id`),
  KEY `Cuisinier_Username` (`Cuisinier_Username`),
  KEY `Recette_id` (`Recette_id`),
  CONSTRAINT `plat_ibfk_1` FOREIGN KEY (`Cuisinier_Username`) REFERENCES `cuisinier` (`Cuisinier_Username`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `plat_ibfk_2` FOREIGN KEY (`Recette_id`) REFERENCES `recette` (`Recette_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plat`
--

LOCK TABLES `plat` WRITE;
/*!40000 ALTER TABLE `plat` DISABLE KEYS */;
INSERT INTO `plat` VALUES (1,'2025-03-04','2025-03-14','12.50',4,'cuisinier1',1,0),(2,'2025-03-05','2025-03-15','14.00',3,'cuisinier2',2,0),(3,'2025-03-06','2025-03-16','15.50',5,'cuisinier3',3,1),(4,'2025-03-07','2025-03-17','16.00',4,'cuisinier4',4,0),(5,'2025-03-08','2025-03-18','18.00',6,'cuisinier5',5,0),(6,'2025-03-09','2025-03-19','19.50',4,'cuisinier6',6,0),(7,'2025-03-10','2025-03-20','20.00',3,'cuisinier7',7,0),(8,'2025-03-11','2025-03-21','21.00',5,'cuisinier8',8,0),(9,'2025-03-12','2025-03-22','22.50',4,'cuisinier9',9,1),(10,'2025-03-13','2025-03-23','23.00',6,'cuisinier10',10,0),(11,'2025-03-14','2025-03-24','24.50',4,'cuisinier11',11,0),(12,'2025-03-15','2025-03-25','25.00',3,'cuisinier12',12,0),(13,'2025-03-16','2025-03-26','26.50',5,'cuisinier13',13,0),(14,'2025-03-17','2025-03-27','27.00',4,'cuisinier14',14,0),(15,'2025-03-18','2025-03-28','28.50',6,'cuisinier15',15,1),(16,'2025-03-19','2025-03-29','30.00',4,'cuisinier1',16,0),(17,'2025-03-20','2025-03-30','31.50',3,'cuisinier2',17,0),(18,'2025-03-21','2025-03-31','32.00',5,'cuisinier3',18,0),(19,'2025-03-22','2025-04-01','33.50',4,'cuisinier4',19,0),(20,'2025-03-23','2025-04-02','34.00',6,'cuisinier5',20,0),(21,'2025-03-24','2025-04-03','35.50',4,'cuisinier6',1,0),(22,'2025-03-25','2025-04-04','36.00',3,'cuisinier7',2,0),(23,'2025-03-26','2025-04-05','37.50',5,'cuisinier8',3,0),(24,'2025-03-27','2025-04-06','38.00',4,'cuisinier9',4,0),(25,'2025-03-28','2025-04-07','39.50',6,'cuisinier10',5,1);
/*!40000 ALTER TABLE `plat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recette`
--

DROP TABLE IF EXISTS `recette`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recette` (
  `Recette_id` int NOT NULL,
  `Recette_Nom` varchar(50) DEFAULT NULL,
  `Recette_Origine` varchar(50) DEFAULT NULL,
  `Recette_Type_de_plat` varchar(50) DEFAULT NULL,
  `Recette_Apport_nutritifs` varchar(50) DEFAULT NULL,
  `Recette_Regime_alimentaire` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Recette_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recette`
--

LOCK TABLES `recette` WRITE;
/*!40000 ALTER TABLE `recette` DISABLE KEYS */;
INSERT INTO `recette` VALUES (1,'Quiche Lorraine','Française','Plat principal','Riche en protéines','Omnivore'),(2,'Ratatouille','Française','Plat principal','Vitamines et fibres','Végétarien'),(3,'Bouillabaisse','Française','Plat principal','Oméga 3','Pescétarien'),(4,'Coq au Vin','Française','Plat principal','Riche en fer','Omnivore'),(5,'Salade Niçoise','Française','Entrée','Légère','Pescétarien'),(6,'Tarte Tatin','Française','Dessert','Sucré','Végétarien'),(7,'Crème Brûlée','Française','Dessert','Riche en calcium','Végétarien'),(8,'Spaghetti Carbonara','Italienne','Plat principal','Énergétique','Omnivore'),(9,'Pizza Margherita','Italienne','Plat principal','Équilibré','Végétarien'),(10,'Tiramisu','Italienne','Dessert','Sucré','Végétarien'),(11,'Enchiladas','Mexicaine','Plat principal','Épicé','Omnivore'),(12,'Guacamole','Mexicaine','Entrée','Riche en vitamines','Végétarien'),(13,'Sushi','Japonaise','Plat principal','Faible en gras','Pescétarien'),(14,'Ramen','Japonaise','Plat principal','Réconfortant','Omnivore'),(15,'Dim Sum','Chinoise','Entrée','Léger','Omnivore'),(16,'Canard laqué','Chinoise','Plat principal','Riche en saveurs','Omnivore'),(17,'Moussaka','Grecque','Plat principal','Riche en légumes','Végétarien'),(18,'Falafel','Moyen-Orient','Entrée','Protéiné','Végétarien'),(19,'Paella','Espagnole','Plat principal','Complet','Omnivore'),(20,'Crema Catalana','Espagnole','Dessert','Léger','Végétarien');
/*!40000 ALTER TABLE `recette` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-04  2:45:35
