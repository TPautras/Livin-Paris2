using System;
using System.Collections.Generic;
using Graphs;

namespace LivinParis_Console
{
    public class Exercice1
    {
        
       /// <summary>
       /// Permet à l'utilisateur de charger et de visualiser un graphe à partir d'un fichier, avec la possibilité de personnaliser le type des noeudds et le séparateur.
       /// </summary>
        public static void Exo1()
        {
            string[] options = { "Oui", "Non", "Quitter" };
            bool Quit = false;
            while (!Quit)
            {
                int res = Affichages.MenuSelect(Affichages.Banner() + "\n Voulez vous utiliser l'exercice 1 par défaut ?", options);
                switch (res)
                {
                    case 0:
                        DemoGraph<string>();
                        break;
                    case 1:
                        string[] optionsDivider = { "Period", "Comma", "Default"};
                        int DividerChoice = Affichages.MenuSelect(Affichages.Banner() + "\n De quel type sont vos noeuds ?", optionsDivider);
                        char divider = ' ';
                        switch (DividerChoice)
                        {
                            case 0:
                                divider = '.';
                                break;
                            case 1:
                                divider = ',';
                                break;
                            case 2:
                                break;
                            default:
                                break;
                        }
                        string[] optionsType = { "string", "int", "bool", "Quitter" };
                        int TypeChoice = Affichages.MenuSelect(Affichages.Banner() + "\n De quel type sont vos noeuds ?", optionsType);
                        switch (TypeChoice)
                        {
                            case 0:
                                DemoGraph<string>(divider);
                                break;
                            case 1:
                                DemoGraph<int>(divider);
                                break;
                            case 2:
                                DemoGraph<bool>(divider);
                                break;
                            default:
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case 2:
                        Quit = true;
                        break;
                }
            }
        }
        /// <summary>
        /// Charge un graphe à partir d'un fichier, l'affiche en texte, effectue des parcours Bfs et Dfs, puis génère une image du graphe.
        /// </summary>
        /// <param name="divider"></param> Caractère utilisé comme séparateur dans le fichier du graphe (' ' par défaut)
        /// <param name="maxCount"></param> Nombre maximal de noeuds à lire dans le fichier 
        /// <param name="path"></param> Chemin du fichier contenant le graphe
        /// <typeparam name="T"></typeparam> Type des noeuds du graphe (string, int, bool, etc...)
        static void DemoGraph<T>(char divider = ' ', int maxCount = 35, string path = "../../soc-karate.mtx")
        {
            Graphe<T> myGraph = new Graphe<T>(path, divider, maxCount);
            Console.WriteLine(myGraph.ToString());

            Console.WriteLine(myGraph.ListeAdjacence());
            
            Console.WriteLine(myGraph.MatriceAdjacence());

            Console.WriteLine(myGraph.Boucle());
            int startIndex;
            Console.WriteLine("Veuillez entrer un index de départ pour le parcours du graphe : ");
            if (int.TryParse(Console.ReadLine(), out startIndex))
            {
                myGraph.Bfs(startIndex);
                myGraph.Dfs(startIndex);
            }
            else
            {
                Console.WriteLine("Entrée invalide.");
            }
            GrapheVisuel<T> visualiseur = new GrapheVisuel<T>(myGraph);
            visualiseur.DessinerGraphe("../../graphe.png");
            Console.ReadKey();
        }
    }
}