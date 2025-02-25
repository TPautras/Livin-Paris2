using System;
using System.Collections.Generic;
using NUnit.Framework;
using Graphs;

namespace LivinParis_Test
{

    [TestFixture]
    public class GrapheTests
    {
        private Graphe<int> graphe;

        [SetUp]
        public void Setup()
        {
            graphe = new Graphe<int>("", ' ', 0);
        }

        [Test]
        public void AjouterNoeud_ShouldAddUniqueNoeud()
        {
            graphe.AjouterNoeud(1);
            Assert.IsTrue(graphe.Noeuds.ContainsKey(1));
        }

        [Test]
        public void AjouterNoeud_ShouldNotAddDuplicateNoeud()
        {
            graphe.AjouterNoeud(1);
            graphe.AjouterNoeud(1);
            Assert.AreEqual(1, graphe.Noeuds.Count);
        }

        [Test]
        public void AjouterLien_ShouldCreateLienBetweenNodes()
        {
            graphe.AjouterNoeud(1);
            graphe.AjouterNoeud(2);
            graphe.AjouterLien(1, 2, 5);

            Assert.AreEqual(1, graphe.Noeuds[1].Liens.Count);
            Assert.AreEqual(2, graphe.Noeuds[1].Liens[0].LienArrivee.Noeud_id);
            Assert.AreEqual(5, graphe.Noeuds[1].Liens[0].LienPoids);
        }
    }
}