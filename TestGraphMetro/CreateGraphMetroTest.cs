using System;
using System.IO;
using NUnit.Framework;
using MetroHelper;
using Graphs;
using System.Linq;

namespace TestGraphMetro
{
    [TestFixture]
    public class CreateGraphMetroTests
    {
        private string dossierTest;

        [SetUp]
        public void Setup()
        {
            dossierTest = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData");

            if (!Directory.Exists(dossierTest))
                Directory.CreateDirectory(dossierTest);

            // Fichier Ligne_1.csv
            File.WriteAllText(Path.Combine(dossierTest, "Ligne_1.csv"),
@"Nom station de métro,Temps vers station suivante (min)
Châtelet,2
Hôtel de Ville,2
Pont Marie,0");

            // Fichier Correspondance.csv
            File.WriteAllText(Path.Combine(dossierTest, "Correspondance.csv"),
@"IdCorrespondance,Station A,Ligne A,Station B,Ligne B,Temps de correspondance (min)
17,Châtelet,1,Châtelet,4,5");
        }

        [Test]
        public void ChargerReseauDepuisFichiers_ShouldCreateGraphWithStationsAndCorrespondances()
        {
            var creator = new CreateGraphMetro();
            Graphe<Station_de_metro> graphe = creator.ChargerReseauDepuisFichiers(dossierTest);

            // Vérifie que les noeuds ont bien été ajoutés
            Assert.IsTrue(graphe.Noeuds.Count >= 3, "Le graphe devrait contenir au moins 3 stations.");
            
            // Vérifie qu'une correspondance a été ajoutée entre Châtelet (Ligne 1) et Hôtel de Ville (Ligne 11)
            var chateletCle = "Châtelet (Ligne 1)";
            var hotelVilleCle = "Hôtel de Ville (Ligne 11)";

            var chatelet = creator.Stations[chateletCle];
            var hotel = creator.Stations[hotelVilleCle];

            var lien = graphe.Liens.FirstOrDefault(l => l.LienDepart.Equals(chatelet.Id) && l.LienArrivee.Equals(hotel.Id));
            Assert.IsNotNull(lien, "Un lien entre Châtelet (L1) et Hôtel de Ville (L11) devrait exister.");
            Assert.AreEqual(2, lien.LienPoids, "Le poids du lien de correspondance devrait être 2.");
        }
    }
}