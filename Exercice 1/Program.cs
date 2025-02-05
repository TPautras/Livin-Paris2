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
                        Myfunc<string>();
                        break;
                    case 1:
                        string[] optionsType = { "string", "int", "bool", "Quitter" };
                        int res2 = Affichages.MenuSelect(Affichages.Banner() + "\n De quel type sont vos noeuds ?", optionsType);
                        switch (res2)
                        {
                            case 0:
                                Myfunc<string>();
                                break;
                            case 1:
                                Myfunc<int>();
                                break;
                            case 2:
                                Myfunc<bool>();
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

        static void Myfunc<T>()
        {
            Graphe<T> myGraph = new Graphe<T>("../../soc-karate.mtx", ' ', 34);
            Console.WriteLine(myGraph.ToString());
            GrapheImage<T> VisualiseurBool = new GrapheImage<T>(myGraph);
            VisualiseurBool.DessinerGraphe("../../graphe.png");
            Console.ReadKey();
        }
    }
}
