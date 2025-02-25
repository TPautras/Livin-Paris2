# Projet Livin'Paris

Par Candice ALEV, Samuel BENCHIMOL, Thomas PAUTRAS

## Contenu de la solution

### Projet Librairie 1 : GraphsLibrary

Ce projet de librairie définit les classes Noeud Lien et Graphe prinipalement. Il est utilisé pour le premier rendu afin de produire une analyse sur l'association de karaté.
Mais aussi pour le projet final pour la gestion des stations de métro.

### Projet Librairie 2 : DBConnectionLibrary

Ce projet sert d'interface entre le reste du code et la base de donnée.

Il comprend les sous-dossiers suivants :

  - Models : Modélisation des objets résultants des différentes tables, sert pour la normalisation des données recues de la BDD et pour l'interaction entre les différentes composantes du programme
  - DataAccess : Interface pour faire le CRUD vers la BDD, toutes les classes implémentent l'interface définie dans la classe DataAccess, et la connexion à la BDD est assurée par les classes BaseDataAccess et DataBase
  - DataService : Interface entre les interfaces utilisateurs et DataAccess, implémentant également une interface

### Projet Test : LivinParis Test

Ce projet sert à tester les autres projets avec des classes de test **NUnit**.

L'organisation de ce projet est de tester un projet par dossier, avec chaque classe du projet cible correspondant à une classe de test unitaire

### Projet Interface Console : LivinParis Console

Ce projet sert à faire l'interface avec l'utilisateur et pour l'instant à tester le bon fonctionnement des différents projets de la solution.

### Projet Interface Graphique : LivinParis Graphique

Ce projet sert également à faire l'interface avec l'utilisateur, il utilisera à terme les librairies définies plus haut, et sera le projet d'interface graphique.

