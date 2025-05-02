using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MetroHelper
{
    /// <summary>
    /// Classe qui permet de peupler  les grandes stations du métro parisien
    /// </summary>
    public class PeuplementGrandeStation
    {
        private Dictionary<string, (int latitude, int longitude)> coordonnees;

        public PeuplementGrandeStation()
        {
            this.coordonnees = new Dictionary<string, (int, int)>();
        }

        public Dictionary<string, (int latitude, int longitude)> Coordonnees
        {
            get => coordonnees;
            set => coordonnees = value;
        }
    /// <summary>
    /// Méthode qui permet de créer les grandes stations à partir de la base de donnée
    /// </summary>
    /// <param name="dossierData"> Base de donnée pour générer les grandes stations à partir du fichier Coordonnes (et ligne pour respecter les liens)</param>
    /// <returns> Renvoit un dictionnaire des différentes grandes stations qui sont uniques</returns>
    /// <exception cref="Exception">Elimine les doublons avec les stations déjà créées</exception>
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
                    string nomComplet = $"{nomStation} (Ligne {numeroLigne})";

                    if (!regroupementParNom.ContainsKey(nomStation))
                        regroupementParNom[nomStation] = new List<Station_de_metro>();
                    
                    Station_de_metro stationExistante = null;

                    foreach (var s in regroupementParNom[nomStation])
                    {
                        if (s.Nom == nomComplet)
                        {
                            stationExistante = s;
                            break;
                        }
                    }

                    if (stationExistante == null)
                    {
                        var nouvelleStation = new Station_de_metro(idCounter++, nomComplet);
                        regroupementParNom[nomStation].Add(nouvelleStation);
                    }
                    else
                    {
                        regroupementParNom[nomStation].Add(stationExistante);
                    }
                }
            }

           
            Dictionary<string, GrandeStation> grandesStations = new Dictionary<string, GrandeStation>();

            foreach (var kvp in regroupementParNom)
            {
                string nom = kvp.Key;
                List<Station_de_metro> listeStations = kvp.Value;

                if (!coordonnees.ContainsKey(nom))
                    throw new Exception($"Coordonnées manquantes pour la station : {nom}");

                var (lat, lon) = coordonnees[nom];
                grandesStations[nom] = new GrandeStation(listeStations, lat, lon);
            }

            return grandesStations;
        }
        /// <summary>
        /// Permet de charger les coordonnés des différentes stations de metro
        /// </summary>
        /// <param name="cheminFichier">Fichier contenant les coordonnées de mes stations de metro</param>
        private void ChargerCoordonnees(string cheminFichier)
        {
            var lignes = File.ReadAllLines(cheminFichier).Skip(1); 

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
        
        public Dictionary<string, GrandeStation> ChargerGrandesStationsDepuisCoordonnees(string cheminFichier)
        {
            Dictionary<string, GrandeStation> grandesStations = new Dictionary<string, GrandeStation>();

            var lignes = File.ReadAllLines(cheminFichier).Skip(1);
            foreach (var ligne in lignes)
            {
                var parties = ligne.Split(',');
                if (parties.Length < 3) continue;

                string nom = parties[0].Trim();
                double latitude = double.Parse(parties[1], CultureInfo.InvariantCulture);
                double longitude = double.Parse(parties[2], CultureInfo.InvariantCulture);
                
                int lat = (int)(latitude * 1_000_000);
                int lon = (int)(longitude * 1_000_000);

                grandesStations[nom] = new GrandeStation(new List<Station_de_metro>(), lat, lon);
            }

            return grandesStations;
        }

    }
}
