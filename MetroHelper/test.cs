using Graphs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MetroHelper
{
    public class test
    {
        private Dictionary<string, Station_de_metro> stations;
        private int idCounter;

        public test()
        {
            this.stations = new Dictionary<string, Station_de_metro>();
            this.idCounter = 1;
        }

        public Dictionary<string, Station_de_metro> Stations
        {
            get { return this.stations; }
            set { this.stations = value; }
        }

        public int IdCounter
        {
            get { return this.idCounter; }
            set { this.idCounter = value; }
        }

        public Graphe<Station_de_metro> ChargerReseauDepuisFichiers(string dossierData)
        {
            Graphe<Station_de_metro> graphe = new Graphe<Station_de_metro>("", ',', 0);

            AjouterStationsEtLiensDesLignes(dossierData, graphe);
            AjouterCorrespondances(dossierData, graphe);

            return graphe;
        }

        private void AjouterStationsEtLiensDesLignes(string dossierData, Graphe<Station_de_metro> graphe)
        {
            string[] fichiersLignes = Directory.GetFiles(dossierData, "Ligne_*.csv");

            foreach (string fichier in fichiersLignes)
            {
                string[] lignes = File.ReadAllLines(fichier);
                Station_de_metro stationPrecedente = null;

                foreach (string ligne in lignes.Skip(1))
                {
                    string[] champs = ligne.Split(',');
                    if (champs.Length < 2) continue;

                    string nom = champs[0].Trim();
                    double temps;
                    if (!double.TryParse(champs[1], NumberStyles.Any, CultureInfo.InvariantCulture, out temps))
                    {
                        temps = 1;
                    }

                    // Extraire le numéro de ligne à partir du nom de fichier
                    string nomFichier = Path.GetFileNameWithoutExtension(fichier);
                    string[] parts = nomFichier.Split('_');
                    string numeroLigne = parts.Length > 1 ? parts[1] : "?";

                    string cle = nom + " (Ligne " + numeroLigne + ")";

                    if (!stations.ContainsKey(cle))
                    {
                        Station_de_metro nouvelleStation = new Station_de_metro(idCounter, cle);
                        stations[cle] = nouvelleStation;
                        graphe.AjouterNoeud(nouvelleStation.Id, nouvelleStation);
                        idCounter++;
                    }

                    Station_de_metro stationActuelle = stations[cle];

                    if (stationPrecedente != null)
                    {
                        graphe.AjouterLien(stationPrecedente.Id, stationActuelle.Id, temps);
                    }

                    stationPrecedente = stationActuelle;
                }
            }
        }

        private void AjouterCorrespondances(string dossierData, Graphe<Station_de_metro> graphe)
        {
            string fichierCorrespondance = Path.Combine(dossierData, "Correspondance.csv");
            if (!File.Exists(fichierCorrespondance)) return;

            string[] lignes = File.ReadAllLines(fichierCorrespondance);

            foreach (string ligne in lignes.Skip(1))
            {
                string[] parties = ligne.Split(',');
                if (parties.Length < 6) continue;

                string nomA = parties[1].Trim();
                string ligneA = parties[2].Trim();
                string nomB = parties[3].Trim();
                string ligneB = parties[4].Trim();

                double temps;
                if (!double.TryParse(parties[5], NumberStyles.Any, CultureInfo.InvariantCulture, out temps))
                {
                    temps = 5;
                }

                string cleA = nomA + " (Ligne " + ligneA + ")";
                string cleB = nomB + " (Ligne " + ligneB + ")";

                if (!stations.ContainsKey(cleA))
                {
                    Station_de_metro sA = new Station_de_metro(idCounter++, cleA);
                    stations[cleA] = sA;
                    graphe.AjouterNoeud(sA.Id, sA);
                }

                if (!stations.ContainsKey(cleB))
                {
                    Station_de_metro sB = new Station_de_metro(idCounter++, cleB);
                    stations[cleB] = sB;
                    graphe.AjouterNoeud(sB.Id, sB);
                }

                Station_de_metro stA = stations[cleA];
                Station_de_metro stB = stations[cleB];

                graphe.AjouterLien(stA.Id, stB.Id, temps);
                graphe.AjouterLien(stB.Id, stA.Id, temps);
            }
        }
    }
}
