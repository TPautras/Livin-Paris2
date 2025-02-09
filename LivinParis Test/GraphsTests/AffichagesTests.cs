using System;
using System.Collections.Generic;
using NUnit.Framework;
using Graphs;

namespace LivinParis_Test
{
    [TestFixture]
    public class AffichagesTests
    {
        [Test]
        public void Banner_ShouldReturnNonEmptyString()
        {
            var result = Affichages.Banner();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }
    }
}