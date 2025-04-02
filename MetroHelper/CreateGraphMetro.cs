using Graphs;
using System.Globalization;
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
            Graphe<Station_de_metro> graphe = new Graphe<Station_de_metro>("", ',', 0);

            AjouterStationsEtLiensDesLignes(dossierData, graphe);
            AjouterCorrespondances(dossierData, graphe);

            return graphe;
        }

        private void AjouterStationsEtLiensDesLignes(string dossierData, Graphe<Station_de_metro> graphe)
        {
            var fichiers = Directory.GetFiles(dossierData, "Ligne_*.csv");

            foreach (var fichier in fichiers)
            {
                string numeroLigne = Path.GetFileNameWithoutExtension(fichier).Split('_')[1];

                var lignes = File.ReadAllLines(fichier).Skip(1).ToList();
                Station_de_metro stationPrecedente = null;

                foreach (var ligne in lignes)
                {
                    var parties = ligne.Split(',');
                    if (parties.Length < 2) continue;

                    string nom = parties[0].Trim();
                    int temps = int.Parse(parties[1].Trim());

                    string cle = $"{nom} (Ligne {numeroLigne})";

                    if (!stations.ContainsKey(cle))
                    {
                        Station_de_metro station = new Station_de_metro(idCounter++, cle);
                        stations[cle] = station;
                        graphe.AjouterNoeud(station.Id, station);
                    }

                    var stationActuelle = stations[cle];

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
            string chemin = Path.Combine(dossierData, "Correspondance.csv");

            if (!File.Exists(chemin)) return;

            var lignes = File.ReadAllLines(chemin).Skip(1);

            foreach (var ligne in lignes)
            {
                var parties = ligne.Split(',');
                if (parties.Length < 6) continue;

                string nom1 = $"{parties[1].Trim()} (Ligne {parties[2].Trim()})";
                string nom2 = $"{parties[3].Trim()} (Ligne {parties[4].Trim()})";
                int temps = int.Parse(parties[5].Trim());

                if (!stations.ContainsKey(nom1))
                {
                    Station_de_metro s1 = new Station_de_metro(idCounter++, nom1);
                    stations[nom1] = s1;
                    graphe.AjouterNoeud(s1.Id, s1);
                }

                if (!stations.ContainsKey(nom2))
                {
                    Station_de_metro s2 = new Station_de_metro(idCounter++, nom2);
                    stations[nom2] = s2;
                    graphe.AjouterNoeud(s2.Id, s2);
                }

                Station_de_metro stationA = stations[nom1];
                Station_de_metro stationB = stations[nom2];

                Correspondance correspondanceAB = new Correspondance(stationA, stationB, temps, -1);
                Correspondance correspondanceBA = new Correspondance(stationB, stationA, temps, -1);

                graphe.AjouterLien(stationA.Id, stationB.Id, correspondanceAB.Temps);
                graphe.AjouterLien(stationB.Id, stationA.Id, correspondanceBA.Temps);
            }
        }
    }
}
