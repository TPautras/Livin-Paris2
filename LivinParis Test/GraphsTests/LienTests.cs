using System;
using System.Collections.Generic;
using NUnit.Framework;
using Graphs;

namespace LivinParis_Test
{
    [TestFixture]
    public class LienTests
    {
        private Noeud<int> noeud1;
        private Noeud<int> noeud2;
        private Lien<int> lien;

        [SetUp]
        public void Setup()
        {
            noeud1 = new Noeud<int>(1);
            noeud2 = new Noeud<int>(2);
            lien = new Lien<int>(noeud1, noeud2, 2.5);
        }

        [Test]
        public void Lien_ShouldCorrectlyInitialize()
        {
            Assert.AreEqual(1, lien.LienDepart.Noeud_id);
            Assert.AreEqual(2, lien.LienArrivee.Noeud_id);
            Assert.AreEqual(2.5, lien.LienPoids);
        }

        [Test]
        public void ToString_ShouldReturnCorrectFormat()
        {
            string expected = "1 -> 2 (Poids: 2.5)";
            Assert.AreEqual(expected, lien.ToString());
        }
    }
}