using System;
using System.Collections.Generic;
using System.Linq;
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
                        Graphe<string> mygraph = new Graphe<string>("../../soc-karate.mtx",' ',34);
                        Console.WriteLine(mygraph.ToString());
                        GrapheImage<string> visualiseur = new GrapheImage<string>(mygraph);
                        visualiseur.DessinerGraphe("../../graphe.png");
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                    case 2:
                        Quit = true;
                        break;
                }
            }
        }
    }
}
