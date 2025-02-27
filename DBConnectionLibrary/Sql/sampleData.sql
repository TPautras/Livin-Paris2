INSERT INTO Personne (Personne_Email, Personne_Nom, Personne_Prenom, Personne_Ville, Personne_Code_postale, Personne_Nom_de_la_rue, Personne_Numero_de_la_rue, Personne_Telephone, Personne_Station_de_metro_la_plus_proche) VALUES
('person1@email.com', 'Nom1', 'Prenom1', 'Ville1', 75164, 'Rue1', 18, '0630774786', 'Station1'),
('person2@email.com', 'Nom2', 'Prenom2', 'Ville2', 75748, 'Rue2', 24, '0610446524', 'Station2'),
('person3@email.com', 'Nom3', 'Prenom3', 'Ville3', 75269, 'Rue3', 41, '0646131513', 'Station3'),
('person4@email.com', 'Nom4', 'Prenom4', 'Ville4', 75024, 'Rue4', 77, '0671976351', 'Station4'),
('person5@email.com', 'Nom5', 'Prenom5', 'Ville5', 75012, 'Rue5', 86, '0659591516', 'Station5'),
('person6@email.com', 'Nom6', 'Prenom6', 'Ville6', 75461, 'Rue6', 38, '0639819916', 'Station6'),
('person7@email.com', 'Nom7', 'Prenom7', 'Ville7', 75611, 'Rue7', 58, '0611722792', 'Station7'),
('person8@email.com', 'Nom8', 'Prenom8', 'Ville8', 75660, 'Rue8', 99, '0632491448', 'Station8'),
('person9@email.com', 'Nom9', 'Prenom9', 'Ville9', 75229, 'Rue9', 81, '0688774517', 'Station9'),
('person10@email.com', 'Nom10', 'Prenom10', 'Ville10', 75541, 'Rue10', 20, '0629837825', 'Station10'),
('person11@email.com', 'Nom11', 'Prenom11', 'Ville11', 75925, 'Rue11', 17, '0670333333', 'Station11'),
('person12@email.com', 'Nom12', 'Prenom12', 'Ville12', 75949, 'Rue12', 19, '0661899911', 'Station12'),
('person13@email.com', 'Nom13', 'Prenom13', 'Ville13', 75149, 'Rue13', 74, '0620223098', 'Station13'),
('person14@email.com', 'Nom14', 'Prenom14', 'Ville14', 75383, 'Rue14', 27, '0647057205', 'Station14'),
('person15@email.com', 'Nom15', 'Prenom15', 'Ville15', 75987, 'Rue15', 63, '0686375606', 'Station15'),
('person16@email.com', 'Nom16', 'Prenom16', 'Ville16', 75920, 'Rue16', 36, '0649307482', 'Station16'),
('person17@email.com', 'Nom17', 'Prenom17', 'Ville17', 75684, 'Rue17', 36, '0640701503', 'Station17'),
('person18@email.com', 'Nom18', 'Prenom18', 'Ville18', 75217, 'Rue18', 92, '0613659492', 'Station18'),
('person19@email.com', 'Nom19', 'Prenom19', 'Ville19', 75800, 'Rue19', 32, '0656291319', 'Station19'),
('person20@email.com', 'Nom20', 'Prenom20', 'Ville20', 75836, 'Rue20', 18, '0612870370', 'Station20');

INSERT INTO Ingredient (Ingredient_Id, Ingredient_Nom, Ingredient_volume, Ingrédient_Unité) VALUES
(1, 'Tomate', '200', 'grammes'),
(2, 'Poulet', '500', 'grammes'),
(3, 'Pâtes', '250', 'grammes'),
(4, 'Oignon', '100', 'grammes'),
(5, 'Lait', '1', 'litre'),
(6, 'Beurre', '200', 'grammes'),
(7, 'Farine', '500', 'grammes'),
(8, 'Riz', '500', 'grammes'),
(9, 'Poisson', '300', 'grammes'),
(10, 'Sel', '10', 'grammes');

INSERT INTO Livraison (Livraison_Id, Livraison_Adresse, Livraison_Date) VALUES
(1, '80 Rue Lafayette, Paris', '2024-02-21 18:12:00'),
(2, '22 Rue Saint-Michel, Paris', '2024-02-16 19:58:00'),
(3, '8 Rue Rivoli, Paris', '2024-02-13 13:53:00'),
(4, '85 Rue Rivoli, Paris', '2024-02-13 23:37:00'),
(5, '28 Rue Émile Zola, Paris', '2024-02-27 10:24:00'),
(6, '54 Rue Émile Zola, Paris', '2024-02-25 22:47:00'),
(7, '23 Rue Lafayette, Paris', '2024-02-14 11:30:00'),
(8, '95 Rue Rivoli, Paris', '2024-02-13 22:50:00'),
(9, '86 Rue Rivoli, Paris', '2024-02-26 23:10:00'),
(10, '82 Rue Émile Zola, Paris', '2024-02-13 14:50:00');

INSERT INTO Recette (Recette_id, Recette_Nom, Recette_Origine, Recette_Type_de_plat, Recette_Apport_nutritifs, Recette_Regime_alimentaire) VALUES
(1, 'Pâtes à la tomate', 'Italie', 'Plat principal', 'Riche en glucides', 'Végétarien'),
(2, 'Poulet rôti', 'France', 'Plat principal', 'Riche en protéines', 'Sans gluten'),
(3, 'Riz au curry', 'Inde', 'Plat principal', 'Épicé et savoureux', 'Végétarien'),
(4, 'Crêpes', 'France', 'Dessert', 'Riche en sucres', 'Végétarien'),
(5, 'Soupe miso', 'Japon', 'Entrée', 'Faible en calories', 'Vegan');

INSERT INTO Entreprise (Entreprise_Id, Entreprise_Nom) VALUES
(1, 'Paris Food'),
(2, 'Gourmet Express'),
(3, 'Delice Street'),
(4, 'Fine Cuisine'),
(5, 'Urban Taste');

INSERT INTO Cuisinier (Cuisinier_Username, Cuisinier_Password, Personne_Email) VALUES
('chef1', 'password1', 'person1@email.com'),
('chef2', 'password2', 'person2@email.com'),
('chef3', 'password3', 'person3@email.com'),
('chef4', 'password4', 'person4@email.com'),
('chef5', 'password5', 'person5@email.com');

INSERT INTO Clients (Client_Username, Client_Password, Personne_Email) VALUES
('client1', 'clientpass1', 'person11@email.com'),
('client2', 'clientpass2', 'person12@email.com'),
('client3', 'clientpass3', 'person13@email.com'),
('client4', 'clientpass4', 'person14@email.com'),
('client5', 'clientpass5', 'person15@email.com');

INSERT INTO Commande (Commande_Id, Entreprise_Id, Cuisinier_Username, Client_Username) VALUES
(1, 2, 'chef4', 'client3'),
(2, 2, 'chef1', 'client4'),
(3, 4, 'chef1', 'client2'),
(4, 3, 'chef3', 'client4'),
(5, 3, 'chef1', 'client5'),
(6, 1, 'chef3', 'client4'),
(7, 4, 'chef3', 'client4'),
(8, 5, 'chef4', 'client2'),
(9, 1, 'chef2', 'client4'),
(10, 2, 'chef3', 'client4');

INSERT INTO Plat (Plat_Id, Plat_date_de_fabrication, Plat_Date_de_peremption, Plat_Prix, Plat_Nombre_Portion, Cuisinier_Username, Recette_id) VALUES
(1, '2024-02-6', '2024-02-27', '30.99', 1, 'chef5', 4),
(2, '2024-02-13', '2024-02-24', '17.99', 2, 'chef2', 2),
(3, '2024-02-14', '2024-02-19', '29.99', 4, 'chef3', 2),
(4, '2024-02-13', '2024-02-25', '30.99', 1, 'chef4', 3),
(5, '2024-02-11', '2024-02-17', '30.99', 5, 'chef5', 1),
(6, '2024-02-9', '2024-02-16', '20.99', 5, 'chef2', 1),
(7, '2024-02-6', '2024-02-24', '15.99', 2, 'chef5', 3),
(8, '2024-02-2', '2024-02-26', '18.99', 5, 'chef3', 2),
(9, '2024-02-8', '2024-02-22', '24.99', 2, 'chef3', 3),
(10, '2024-02-3', '2024-02-18', '16.99', 2, 'chef1', 5);

INSERT INTO Evaluation (Evaluation_Id, Evaluation_Client, Evaluation_Cuisinier, Evaluation_Description_Client, Evaluation_Description_Cuisinier, Commande_Id) VALUES
(1, 4.4, 1.6, 'Très bon plat !', 'Merci pour votre retour !', 3),
(2, 2.5, 4.8, 'Très bon plat !', 'Merci pour votre retour !', 4),
(3, 1.6, 2.2, 'Très bon plat !', 'Merci pour votre retour !', 4),
(4, 4.9, 2.7, 'Très bon plat !', 'Merci pour votre retour !', 4),
(5, 1.3, 2.2, 'Manque un peu de sel.', 'Merci pour votre retour !', 9);

