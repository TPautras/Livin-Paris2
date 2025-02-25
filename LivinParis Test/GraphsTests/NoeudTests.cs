using System;
using System.Collections.Generic;
using NUnit.Framework;
using Graphs;

namespace LivinParis_Test
{
    [TestFixture]
    public class NoeudTests
    {
        [Test]
        public void Noeud_ShouldInitializeCorrectly()
        {
            var noeud = new Noeud<int>(1, 100);
            Assert.AreEqual(1, noeud.Noeud_id);
            Assert.AreEqual(100, noeud.Noeud_Valeur);
        }

        [Test]
        public void Noeud_ShouldContainEmptyLienListInitially()
        {
            var noeud = new Noeud<int>(1);
            Assert.IsNotNull(noeud.Liens);
            Assert.AreEqual(0, noeud.Liens.Count);
        }
    }
}