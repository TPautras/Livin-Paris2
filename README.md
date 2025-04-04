# Projet Livin'Paris
[![C#](https://img.shields.io/badge/C%23-youlike?label=Language)](https://img.shields.io/badge/C%23-youlike?label=Language)
[![C#](https://img.shields.io/badge/.NET-youlike?logoColor=%23b024b3&label=Framework&color=%23b024b3
)](https://img.shields.io/badge/.NET-youlike?logoColor=%23b024b3&label=Framework&color=%23b024b3
)

Par Candice ALEV, Samuel BENCHIMOL, Thomas PAUTRAS

## Pour le rendu 1

**Partie BDD** : Les différents schémas se trouvent dans le dossier Assets,
le peuplement de la base de donnée et les différentes requêtes "simples" demandées se trouvent dans
SqlConnector>Sql

**Partie Graphe** : Les différentes classes demandées se trouvent dans le projet
GraphsLibrary, quand au test de ces classes, il se trouve dans LivinParis Console qu'il faut exécuter

**Rapport** : Le rapport se trouve egalement dans la partie asset, egalement sous forme de fichier markdown.

**Autre** : Les tests notamment se trouvent dans le projet de test associé

## Pour le rendu 2

**Initialisation du projet** : Le Sdk 8.0 est requis pour lancer le projet. Il est egalement possible qu'il faille changer la string
de connection qui se trouve dans le fichier BaseDataAccess.cs. De plus, il est necessaire pour lancer les projet Graphique
de desinstaller le projet TestGraphMetro puis de faire Ctrl + Z. 
Quand on lance le projet, il faut aller sur BDD initialisation pour instancier et peupler la base de donnees.

**Partie BDD** : Pour avoir acces aux differends modules, il faut se connecter en tant qu'admin. Pour cela, lancer le projet console,
puis aller dans livinParis puis se connecter en tant que client ou cuisinier en mettant admin et admin en mdp et username.
Ensuite tout est accessible depuis la comme dans le cahier des charges.
Pour une experience utilisateur plus "normale", on peut se connecter avec un client lambda de la base de donnee ou un cuisinier lambda\
Exemple :\
- Client :
  - Username : client1
  - mdp : pwdclient1
- Cuisinier
  - Username : cuisinier1
  - mdp : pwdchef1 \
 
\
La recherche du plus court chemin entre le cuisinier et le client est dans le menu admin quand on passe une nouvelle commande. \
**Le dump SQL se trouve dans le dossier Assets, a la racine du projet** \
**Partie Graphe** : La partie simulation du Metro se trouve dans le projet MetroHelper, les parcours de Graphe se trouvent dans le 
projet GraphsLibrary, dans le dossier parcours. 

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

### Assets

Dans ce dossier se trouvent tous les Assets qui se trouvent dans le projet.
On retrouve notamment :
- **Le fichier .loo comprenant notre diagramme entité association**
- Les différents fichiers utilisés pour injecter des données dans les bases de donnée
- L'énoncé du projet PSI