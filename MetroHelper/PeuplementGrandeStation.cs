using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MetroHelper
{
    public class PeuplementGrandeStation
    {
        // === ATTRIBUTS PRIVÉS ===
        private Dictionary<string, (int latitude, int longitude)> coordonnees;

        // === CONSTRUCTEUR ===
        public PeuplementGrandeStation()
        {
            this.coordonnees = new Dictionary<string, (int, int)>();
        }

        // === GETTER / SETTER POUR LES COORDONNÉES ===
        public Dictionary<string, (int latitude, int longitude)> Coordonnees
        {
            get { return coordonnees; }
            set { coordonnees = value; }
        }

        // === MÉTHODE PRINCIPALE POUR CRÉER LES GRANDES STATIONS ===
        public Dictionary<string, GrandeStation> CreerGrandesStations(string dossierData)
        {
            ChargerCoordonnees(Path.Combine(dossierData, "Coordonnees.csv"));

            Dictionary<string, List<Station_de_metro>> regroupementParNom = new Dictionary<string, List<Station_de_metro>>();
            int idCounter = 1;

            var fichiersLignes = Directory.GetFiles(dossierData, "Ligne_*.csv");

            foreach (var fichier in fichiersLignes)
            {
                string nomFichier = Path.GetFileNameWithoutExtension(fichier);
                string numeroLigne = nomFichier.Split('_')[1];

                var lignes = File.ReadAllLines(fichier).Skip(1);

                foreach (var ligne in lignes)
                {
                    var parties = ligne.Split(',');
                    if (parties.Length < 2) continue;

                    string nomStation = parties[0].Trim().Trim('"');

                    if (!regroupementParNom.ContainsKey(nomStation))
                        regroupementParNom[nomStation] = new List<Station_de_metro>();

                    string nomComplet = $"{nomStation} (Ligne {numeroLigne})";
                    var station = new Station_de_metro(idCounter++, nomComplet);
                    regroupementParNom[nomStation].Add(station);
                }
            }

            Dictionary<string, GrandeStation> grandesStations = new Dictionary<string, GrandeStation>();

            foreach (var kvp in regroupementParNom)
            {
                string nom = kvp.Key;
                List<Station_de_metro> listeStations = kvp.Value;

                if (!coordonnees.ContainsKey(nom))
                {
                    throw new Exception($"Coordonnées manquantes pour la station : {nom}");
                }

                var (lat, lon) = coordonnees[nom];
                var grandeStation = new GrandeStation(listeStations, lat, lon);
                grandesStations[nom] = grandeStation;
            }

            return grandesStations;
        }

        // === MÉTHODE DE CHARGEMENT DU FICHIER DE COORDONNÉES ===
        private void ChargerCoordonnees(string cheminFichier)
        {
            var lignes = File.ReadAllLines(cheminFichier).Skip(1); // ignorer l'en-tête

            foreach (var ligne in lignes)
            {
                var parties = ligne.Split(',');
                if (parties.Length < 3) continue;

                string nom = parties[0].Trim();
                double latitude = double.Parse(parties[1], CultureInfo.InvariantCulture);
                double longitude = double.Parse(parties[2], CultureInfo.InvariantCulture);

                coordonnees[nom] = ((int)(latitude * 1_000_000), (int)(longitude * 1_000_000));
            }
        }
    }
}
