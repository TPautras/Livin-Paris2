# Rapport du premier rendu

Groupe : Candice ALEV, Samuel BENCHMOL, Thomas PAUTRAS

---

## Partie BDD

### Schema entite-association

---

![img.png](Images/ImageEntiteAssociation.png)

Ce schéma entité-association represente la structure de base du backend de notre projet. Au centre, l’entité Personne permet de définir les caractéristiques 
communes à tous les individus du 
système (nom, coordonnées, etc.), tandis que des entités spécialisées comme Client, 
Cuisinier ou Livreur décrivent leurs rôles respectifs. L’entité Entreprise peut représenter 
la plateforme elle-même, supervisant l’ensemble des opérations. Les Commandes constituent 
un pivot important : elles relient un Client qui passe commande, un Cuisinier qui prépare 
les plats, et un Livreur qui les achemine. On retrouve également des entités comme Recettes 
pour la composition des plats, Stock (ou “Stocke” dans le schéma) pour la gestion des 
ingrédients, et des tables connexes pour gérer les aspects contractuels, légaux ou 
analytiques (diagnostique de marché, contrats, etc.).

Le système s’articule donc autour de la mise en relation entre des personnes qui proposent et préparent des plats, et 
celles qui souhaitent se faire livrer des repas. Chaque commande suit un cycle complet : de 
la sélection du plat (basé sur une ou plusieurs Recettes) jusqu’à la livraison, en passant 
par le suivi du Stock et la gestion d’éventuelles obligations légales (contrats, manifestes,
etc.).

### Script SQL de creation de la base

---

```mysql
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
                          FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email)
);

CREATE TABLE Clients(
                        Client_Username VARCHAR(50),
                        Client_Password VARCHAR(50) NOT NULL,
                        Personne_Email VARCHAR(50) NOT NULL,
                        PRIMARY KEY(Client_Username),
                        UNIQUE(Personne_Email),
                        FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email)
);

CREATE TABLE Commande(
                         Commande_Id INT,
                         Entreprise_Id INT NOT NULL,
                         Cuisinier_Username VARCHAR(50) NOT NULL,
                         Client_Username VARCHAR(50) NOT NULL,
                         PRIMARY KEY(Commande_Id),
                         FOREIGN KEY(Entreprise_Id) REFERENCES Entreprise(Entreprise_Id),
                         FOREIGN KEY(Cuisinier_Username) REFERENCES Cuisinier(Cuisinier_Username),
                         FOREIGN KEY(Client_Username) REFERENCES Clients(Client_Username)
);

CREATE TABLE Plat(
                     Plat_Id INT,
                     Plat_date_de_fabrication DATE,
                     Plat_Date_de_peremption DATE,
                     Plat_Prix VARCHAR(50),
                     Plat_Nombre_Portion INT NOT NULL,
                     Cuisinier_Username VARCHAR(50) NOT NULL,
                     Recette_id INT NOT NULL,
                     PRIMARY KEY(Plat_Id),
                     FOREIGN KEY(Cuisinier_Username) REFERENCES Cuisinier(Cuisinier_Username),
                     FOREIGN KEY(Recette_id) REFERENCES Recette(Recette_id)
);

CREATE TABLE evaluation(
                           Evaluation_Id INT,
                           Evaluation_Client DECIMAL(15,2),
                           Evaluation_Cuisinier DECIMAL(15,2),
                           Evaluation_Description_Client TEXT,
                           Evaluation_Description_Cuisinier TEXT,
                           Commande_Id INT NOT NULL,
                           PRIMARY KEY(Evaluation_Id),
                           FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id)
);

CREATE TABLE Creation(
                         Commande_Id INT,
                         Plat_Id INT,
                         PRIMARY KEY(Commande_Id, Plat_Id),
                         FOREIGN KEY(Commande_Id) REFERENCES Commande(Commande_Id),
                         FOREIGN KEY(Plat_Id) REFERENCES Plat(Plat_Id)
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

CREATE TABLE Fait_Partie_De(
                               Personne_Email VARCHAR(50),
                               Entreprise_Id INT,
                               PRIMARY KEY(Personne_Email, Entreprise_Id),
                               FOREIGN KEY(Personne_Email) REFERENCES Personne(Personne_Email),
                               FOREIGN KEY(Entreprise_Id) REFERENCES Entreprise(Entreprise_Id)
);
```

## IA et visualisation du graphe

### Prompts et reponses

---
**Prompt**
> Comment utiliser System.Drawings pour représenter un graphe ?

**Code genere**

```csharp
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class GrapheVisuel : Form
{
    private Dictionary<int, Point> positionsNoeuds;
    private Graphe graphe;

    public GrapheVisuel(Graphe g)
    {
        this.graphe = g;
        this.Text = "Visualisation du Graphe";
        this.Size = new Size(600, 600);
        this.DoubleBuffered = true;
        this.positionsNoeuds = new Dictionary<int, Point>();

        // Génération aléatoire des positions des nœuds
        Random rnd = new Random();
        foreach (var noeud in graphe.Noeuds.Values)
        {
            positionsNoeuds[noeud.Id] = new Point(rnd.Next(50, 550), rnd.Next(50, 550));
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        Pen pen = new Pen(Color.Black, 2);
        Brush brushNoeud = new SolidBrush(Color.Blue);
        Brush brushTexte = new SolidBrush(Color.White);
        Font font = new Font("Arial", 10);

        // Dessiner les liens (arêtes)
        foreach (var noeud in graphe.Noeuds.Values)
        {
            foreach (var lien in noeud.Liens)
            {
                Point p1 = positionsNoeuds[lien.Source.Id];
                Point p2 = positionsNoeuds[lien.Destination.Id];

                g.DrawLine(pen, p1, p2);
            }
        }

        // Dessiner les nœuds (sommets)
        foreach (var noeud in graphe.Noeuds.Values)
        {
            Point position = positionsNoeuds[noeud.Id];
            Rectangle rect = new Rectangle(position.X - 15, position.Y - 15, 30, 30);
            g.FillEllipse(brushNoeud, rect);
            g.DrawEllipse(pen, rect);

            // Afficher l'ID du nœud au centre
            g.DrawString(noeud.Id.ToString(), font, brushTexte, position.X - 7, position.Y - 7);
        }
    }
}
```

**Prompt**
> Je suis sur une application console

**Code genere**

```csharp
using System;
using System.Collections.Generic;
using System.Drawing;

public class GrapheImage
{
    private Dictionary<int, Point> positionsNoeuds;
    private Graphe graphe;

    public GrapheImage(Graphe g)
    {
        this.graphe = g;
        this.positionsNoeuds = new Dictionary<int, Point>();

        Random rnd = new Random();
        foreach (var noeud in graphe.Noeuds.Values)
        {
            positionsNoeuds[noeud.Id] = new Point(rnd.Next(50, 450), rnd.Next(50, 450));
        }
    }

    public void DessinerGraphe(string filename = "graphe.png")
    {
        Bitmap bmp = new Bitmap(500, 500);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        Pen pen = new Pen(Color.Black, 2);
        Brush brushNoeud = new SolidBrush(Color.Blue);
        Brush brushTexte = new SolidBrush(Color.White);
        Font font = new Font("Arial", 10);

        // Dessiner les liens
        foreach (var noeud in graphe.Noeuds.Values)
        {
            foreach (var lien in noeud.Liens)
            {
                Point p1 = positionsNoeuds[lien.Source.Id];
                Point p2 = positionsNoeuds[lien.Destination.Id];

                g.DrawLine(pen, p1, p2);
            }
        }

        // Dessiner les nœuds
        foreach (var noeud in graphe.Noeuds.Values)
        {
            Point position = positionsNoeuds[noeud.Id];
            Rectangle rect = new Rectangle(position.X - 10, position.Y - 10, 20, 20);
            g.FillEllipse(brushNoeud, rect);
            g.DrawEllipse(pen, rect);

            g.DrawString(noeud.Id.ToString(), font, brushTexte, position.X - 5, position.Y - 5);
        }

        bmp.Save(filename);
        Console.WriteLine($"Graphe sauvegardé sous {filename}");
    }
}
```

### Conclusion et critique de l'utilisation de l'IA

L'IA pensait par defaut que nous utilisions une application console ce qui reflete probablement ce 
sur quoi elle a ete le plus entrainee ou ce qui est plus utilise. Une fois cette erreur 
corrigee, le code reste de bonne qualite meme si les parties de namespace, nom de la classe etc
sont a revoir...
De plus, l'image ne s'ouvre pas automatiquement ce qui n'est pas tres pratique pour l'utilisateur,
d'ou les modifications dans la fonction finale.
On pourrait egalement considerer des affichages differents et une adaptation en fonction de 
l'orientation ou non du graphe et de la presence ou non de poids sur les liens

