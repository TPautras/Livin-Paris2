using System;
using System.Collections.Generic;
using Graphs;
using NUnit.Framework;

namespace LivinParis_Test.Parcours_Tests
{
    [TestFixture]
    public class Dijkstra
    {
        [Test]
        public void DisjktraTestList()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            Graphe<int> Graph = new Graphe<int>(path,divider,maxCount);
            (List<int>,double) result = Graphs.Parcours.Dijkstra<int>.CheminPlusCourtDiskstra(Graph,1,6);
            List<int> expected = new List<int>();
            expected.Add(6);
            Assert.That(result.Item1, Is.EqualTo(expected));
        }
        [Test]
        public void DisjktraTestPoids()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            Graphe<int> Graph = new Graphe<int>(path,divider,maxCount);
            (List<int>,double) result = Graphs.Parcours.Dijkstra<int>.CheminPlusCourtDiskstra(Graph,1,6);
            Assert.That(result.Item2, Is.EqualTo(1));
        }
    }
}