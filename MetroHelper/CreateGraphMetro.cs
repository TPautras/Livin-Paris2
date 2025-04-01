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

                foreach (string ligne in lignes)
                {
                    string nom = ligne.Trim();
                    if (nom == "") continue;

                    if (!stations.ContainsKey(nom))
                    {
                        Station_de_metro nouvelleStation = new Station_de_metro(idCounter, nom);
                        stations[nom] = nouvelleStation;
                        graphe.AjouterNoeud(nouvelleStation.Id, nouvelleStation);
                        idCounter++;
                    }

                    Station_de_metro stationActuelle = stations[nom];

                    if (stationPrecedente != null)
                    {
                        graphe.AjouterLien(stationPrecedente.Id, stationActuelle.Id, 1);
                        graphe.AjouterLien(stationActuelle.Id, stationPrecedente.Id, 1);
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

            foreach (string ligne in lignes)
            {
                string[] parties = ligne.Split(';');
                if (parties.Length < 3) continue;

                string nom1 = parties[0].Trim();
                string nom2 = parties[1].Trim();
                int temps = int.TryParse(parties[2], out int t) ? t : 1;

                if (!stations.ContainsKey(nom1))
                {
                    Station_de_metro station1 = new Station_de_metro(idCounter, nom1);
                    stations[nom1] = station1;
                    graphe.AjouterNoeud(station1.Id, station1);
                    idCounter++;
                }

                if (!stations.ContainsKey(nom2))
                {
                    Station_de_metro station2 = new Station_de_metro(idCounter, nom2);
                    stations[nom2] = station2;
                    graphe.AjouterNoeud(station2.Id, station2);
                    idCounter++;
                }

                Station_de_metro s1 = stations[nom1];
                Station_de_metro s2 = stations[nom2];

                graphe.AjouterLien(s1.Id, s2.Id, temps);
                graphe.AjouterLien(s2.Id, s1.Id, temps);
            }
        }
    }
}
