using MetroHelper;

namespace TestGraphMetro
{
    public class StationDeMetroTests
    {
        [Test]
        public void Constructeur_InitialiseIdEtNomCorrectement()
        {
            // Arrange
            int id = 42;
            string nom = "République";

            // Act
            var station = new Station_de_metro(id, nom);

            // Assert
            Assert.AreEqual(id, station.Id);
            Assert.AreEqual(nom, station.Nom);
        }

        [Test]
        public void Propriete_Id_PeutEtreModifiee()
        {
            // Arrange
            var station = new Station_de_metro(1, "Châtelet");

            // Act
            station.Id = 99;

            // Assert
            Assert.AreEqual(99, station.Id);
        }

        [Test]
        public void Propriete_Nom_PeutEtreModifiee()
        {
            // Arrange
            var station = new Station_de_metro(1, "Châtelet");

            // Act
            station.Nom = "Nation";

            // Assert
            Assert.AreEqual("Nation", station.Nom);
        }
    }
}