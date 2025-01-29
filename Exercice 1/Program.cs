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
            Graphe mygraph = new Graphe("../../soc-karate.mtx",' ',34);
            Console.WriteLine(mygraph.ToString());
            GrapheImage visualiseur = new GrapheImage(mygraph);
            visualiseur.DessinerGraphe("graphe.png");
            Console.ReadKey();
        }
    }
}
