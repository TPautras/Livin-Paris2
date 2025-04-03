using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetroHelper;
using Graphs;

namespace TestGraphMetro
{
    [TestClass]
    public class TestGraphMetroV2
    {
        public TestContext TestContext { get; set; }
        private string dossierTest;

        [TestInitialize]
        public void Setup()
        {
            dossierTest = Path.Combine(TestContext.TestDir, "TestDataChatelet");
            if (!Directory.Exists(dossierTest))
                Directory.CreateDirectory(dossierTest);
            File.WriteAllText(Path.Combine(dossierTest, "Ligne_1_Château_de_Vincennes_vers_La_Defense.csv"),
@"Nom station de métro,Temps vers station suivante (min)
Châtelet,2
Hôtel de Ville,0");
            File.WriteAllText(Path.Combine(dossierTest, "Ligne_11_Châtelet_vers_Mairie_des_Lilas.csv"),
@"Nom station de métro,Temps vers station suivante (min)
Châtelet,2
Hôtel de Ville,2
Rambuteau,2
Arts et Métiers,2");
            File.WriteAllText(Path.Combine(dossierTest, "Correspondance.csv"),
@"IdCorrespondance,Station A,Ligne A,Station B,Ligne B,Temps de correspondance (min)
21,Châtelet,1,Châtelet,11,5");
        }

        [TestMethod]
        public void Graphe_ChateletVersArtsEtMetiersViaCorrespondance_DoiventEtreCorrects()
        {
            var createur = new CreateGraphMetro();
            var graphe = createur.ChargerReseauDepuisFichiers(dossierTest);
            var cleChateletL1 = "Châtelet (Ligne 1)";
            var cleChateletL11 = "Châtelet (Ligne 11)";
            var cleHotel = "Hôtel de Ville (Ligne 11)";
            var cleRambuteau = "Rambuteau (Ligne 11)";
            var cleArts = "Arts et Métiers (Ligne 11)";
            var sChateletL1 = createur.Stations[cleChateletL1];
            var sChateletL11 = createur.Stations[cleChateletL11];
            var sHotel = createur.Stations[cleHotel];
            var sRambuteau = createur.Stations[cleRambuteau];
            var sArts = createur.Stations[cleArts];
            var lienChatelet = graphe.Noeuds[sChateletL1.Id].Liens.FirstOrDefault(l => l.LienArrivee.Noeud_id == sChateletL11.Id);
            var lienHotel = graphe.Noeuds[sChateletL11.Id].Liens.FirstOrDefault(l => l.LienArrivee.Noeud_id == sHotel.Id);
            var lienRambuteau = graphe.Noeuds[sHotel.Id].Liens.FirstOrDefault(l => l.LienArrivee.Noeud_id == sRambuteau.Id);
            var lienArts = graphe.Noeuds[sRambuteau.Id].Liens.FirstOrDefault(l => l.LienArrivee.Noeud_id == sArts.Id);
            Assert.IsNotNull(lienChatelet, "La correspondance Châtelet (L1) → Châtelet (L11) doit exister.");
            Assert.AreEqual(5, lienChatelet.LienPoids, "Le temps de correspondance Châtelet (L1 → L11) doit être de 5 minutes (id 21).");
            Assert.IsNotNull(lienHotel, "Lien Châtelet (L11) → Hôtel de Ville (L11) manquant.");
            Assert.AreEqual(2, lienHotel.LienPoids, "Le poids du lien Châtelet → Hôtel de Ville doit être 2.");
            Assert.IsNotNull(lienRambuteau, "Lien Hôtel de Ville → Rambuteau (L11) manquant.");
            Assert.AreEqual(2, lienRambuteau.LienPoids, "Le poids du lien Hôtel de Ville → Rambuteau doit être 2.");
            Assert.IsNotNull(lienArts, "Lien Rambuteau → Arts et Métiers (L11) manquant.");
            Assert.AreEqual(2, lienArts.LienPoids, "Le poids du lien Rambuteau → Arts et Métiers doit être 2.");
        }
    }
}
