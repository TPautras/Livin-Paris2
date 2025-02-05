using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_1
{
    internal class Program
    {
        static void Main(string[] args)
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

        static void DemoGraph<T>(char divider = ' ', int maxCount = 35, string path = "../../soc-karate.mtx")
        {
            Graphe<T> myGraph = new Graphe<T>(path, divider, maxCount);
            Console.WriteLine(myGraph.ToString());

            Console.WriteLine("Veuillez entrer un index de départ pour le parcours du graphe : ");
            int startIndex;
            if (int.TryParse(Console.ReadLine(), out startIndex))
            {
                myGraph.BFS(startIndex);
                myGraph.DFS(startIndex);
            }
            else
            {
                Console.WriteLine("Entrée invalide.");
            }

            GrapheImage<T> visualiseur = new GrapheImage<T>(myGraph);
            visualiseur.DessinerGraphe("../../graphe.png");
            Console.ReadKey();
        }

    }
}
