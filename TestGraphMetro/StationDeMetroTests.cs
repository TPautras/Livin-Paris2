using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetroHelper;

namespace TestGraphMetro
{
    [TestClass]
    public class StationDeMetroTests
    {
        [TestMethod]
        public void Constructeur_InitialiseIdEtNomCorrectement()
        {
            int id = 42;
            string nom = "République";
            var station = new Station_de_metro(id, nom);
            Assert.AreEqual(id, station.Id);
            Assert.AreEqual(nom, station.Nom);
        }

        [TestMethod]
        public void Propriete_Id_PeutEtreModifiee()
        {
            var station = new Station_de_metro(1, "Châtelet");
            station.Id = 99;
            Assert.AreEqual(99, station.Id);
        }

        [TestMethod]
        public void Propriete_Nom_PeutEtreModifiee()
        {
            var station = new Station_de_metro(1, "Châtelet");
            station.Nom = "Nation";
            Assert.AreEqual("Nation", station.Nom);
        }
    }
}