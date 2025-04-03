using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Graphs;

namespace MetroHelper
{
    public class CreateGraphMetro
    {
        private Dictionary<string, Station_de_metro> stations;
        private int idCounter;

        public CreateGraphMetro()
        {
            this.stations = new Dictionary<string, Station_de_metro>();
            this.idCounter = 1;
        }

        public Dictionary<string, Station_de_metro> Stations
        {
            get { return stations; }
            set { stations = value; }
        }

        public int IdCounter
        {
            get { return idCounter; }
            set { idCounter = value; }
        }

        public Graphe<Station_de_metro> ChargerReseauDepuisFichiers(string dossierData)
        {
            var graphe = new Graphe<Station_de_metro>("", ',', 0);

            AjouterStationsEtLiensDesLignes(dossierData, graphe);
            AjouterCorrespondances(dossierData, graphe);

            return graphe;
        }
        /// <summary>
        /// Methode qui permet d'ajouter des stations de métro à un graphe issus d'une meme ligne et d'avoir le lien entre les stations d'une meme ligne
        /// </summary>
        /// <param name="dossierData"></param> Dossier de la base de données de fichiers csv pour constituer les lignes
        /// <param name="graphe"></param> Est l'élément sur lequel les différentes stations de metro
        private void AjouterStationsEtLiensDesLignes(string dossierData, Graphe<Station_de_metro> graphe)
        {
            var fichiers = Directory.GetFiles(dossierData, "Ligne_*.csv");

            foreach (var fichier in fichiers)
            {
                string nomFichier = Path.GetFileNameWithoutExtension(fichier);
                string numeroLigne = nomFichier.Split('_')[1];

                var lignes = File.ReadAllLines(fichier).Skip(1).ToList();
                for (int i = 0; i < lignes.Count - 1; i++)
                {
                    var parts = lignes[i].Split(',');
                    var partsSuivante = lignes[i + 1].Split(',');

                    if (parts.Length < 2 || partsSuivante.Length < 1) continue;

                    string nomActuelle = parts[0].Trim().Trim('"');
                    int tempsVersSuivante = int.Parse(parts[1].Trim());
                    string nomSuivante = partsSuivante[0].Trim().Trim('"');

                    string cleActuelle = $"{nomActuelle} (Ligne {numeroLigne})";
                    string cleSuivante = $"{nomSuivante} (Ligne {numeroLigne})";

                    if (!stations.ContainsKey(cleActuelle))
                    {
                        var station = new Station_de_metro(idCounter++, cleActuelle);
                        stations[cleActuelle] = station;
                        graphe.AjouterNoeud(station.Id, station);
                    }

                    if (!stations.ContainsKey(cleSuivante))
                    {
                        var station = new Station_de_metro(idCounter++, cleSuivante);
                        stations[cleSuivante] = station;
                        graphe.AjouterNoeud(station.Id, station);
                    }

                    graphe.AjouterLien(stations[cleActuelle].Id, stations[cleSuivante].Id, tempsVersSuivante);
                }
                
                var lastParts = lignes.Last().Split(',');
                if (lastParts.Length >= 1)
                {
                    string nomDerniere = lastParts[0].Trim().Trim('"');
                    string cleDerniere = $"{nomDerniere} (Ligne {numeroLigne})";

                    if (!stations.ContainsKey(cleDerniere))
                    {
                        var station = new Station_de_metro(idCounter++, cleDerniere);
                        stations[cleDerniere] = station;
                        graphe.AjouterNoeud(station.Id, station);
                    }
                }
            }
        }
/// <summary>
/// Permet d'ajouter les différentes correspondances entre les stations de metro. 
/// </summary>
/// <param name="dossierData"></param> la base de donnée qui a le fichier Correspondance.csv qui liste les correspondances
/// <param name="graphe"></param> le graphe sur lequel greffer ces correspondances
        private void AjouterCorrespondances(string dossierData, Graphe<Station_de_metro> graphe)
        {
            string chemin = Path.Combine(dossierData, "Correspondance.csv");

            if (!File.Exists(chemin)) return;

            var lignes = File.ReadAllLines(chemin).Skip(1);

            foreach (var ligne in lignes)
            {
                var parts = ligne.Split(',');
                if (parts.Length < 6) continue;

                int id = int.Parse(parts[0].Trim());
                string nomA = parts[1].Trim().Trim('"');
                string ligneA = parts[2].Trim();
                string nomB = parts[3].Trim().Trim('"');
                string ligneB = parts[4].Trim();
                int temps = int.Parse(parts[5].Trim());

                string cleA = $"{nomA} (Ligne {ligneA})";
                string cleB = $"{nomB} (Ligne {ligneB})";

                if (!stations.ContainsKey(cleA))
                {
                    var sA = new Station_de_metro(idCounter++, cleA);
                    stations[cleA] = sA;
                    graphe.AjouterNoeud(sA.Id, sA);
                }

                if (!stations.ContainsKey(cleB))
                {
                    var sB = new Station_de_metro(idCounter++, cleB);
                    stations[cleB] = sB;
                    graphe.AjouterNoeud(sB.Id, sB);
                }

                var stationA = stations[cleA];
                var stationB = stations[cleB];

                var correspondance = new Correspondance(stationA, stationB, temps, id);

                graphe.AjouterLien(correspondance.S1.Id, correspondance.S2.Id, correspondance.Temps);
                graphe.AjouterLien(correspondance.S2.Id, correspondance.S1.Id, correspondance.Temps);
            }
        }
        
    }
}
