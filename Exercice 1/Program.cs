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
            Console.WriteLine("test");
            Graphe<string> mygraph = new Graphe<string>("../../soc-karate.mtx",' ',34);
            Console.WriteLine(mygraph.ToString());
            GrapheImage<string> visualiseur = new GrapheImage<string>(mygraph);
            visualiseur.DessinerGraphe("../../graphe.png");
            Console.ReadKey();
        }
    }
}
