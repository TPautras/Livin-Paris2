using MetroHelper;
using System;
using System.Collections.Generic;
using System.IO;
using Graphs;

namespace LivinParis_Console
{
    public class AffichageMetro
    {
        public static void AfficherCarte()
        {
            // === Étape 1 : Charger les grandes stations avec leurs coordonnées ===
            string dataPath = Path.Combine("..", "..", "..", "MetroHelper", "Data");
            var peuplement = new PeuplementGrandeStation();
            Dictionary<string, GrandeStation> grandesStations = peuplement.CreerGrandesStations(dataPath);

            // === Étape 2 : Créer un graphe avec les grandes stations comme nœuds ===
            var graphe = new Graphe<Station_de_metro>("", ',', 0);
            Dictionary<int, (int lat, int lon)> coordonneesGeo = new Dictionary<int, (int, int)>();

            foreach (var grandeStation in grandesStations.Values)
            {
                foreach (var station in grandeStation.Stations)
                {
                    graphe.AjouterNoeud(station.Id, station);
                    coordonneesGeo[station.Id] = (grandeStation.Latitude, grandeStation.Longitude);
                }
            }

            // === Étape 3 : Ajouter les liaisons entre les stations ===
            var constructeur = new CreateGraphMetro();
            var grapheComplet = constructeur.ChargerReseauDepuisFichiers(dataPath);

            foreach (var lien in grapheComplet.Liens)
            {
                var depart = grapheComplet.Noeuds[lien.LienDepart.Noeud_id];
                var arrivee = grapheComplet.Noeuds[lien.LienArrivee.Noeud_id];

                // Vérifier si les valeurs sont bien typées Station_de_metro
                if (depart.Noeud_Valeur is Station_de_metro stationDepart &&
                    arrivee.Noeud_Valeur is Station_de_metro stationArrivee)
                {
                    // Extraire les noms sans la partie " (Ligne X)"
                    string nom1 = stationDepart.Nom.Split('(')[0].Trim();
                    string nom2 = stationArrivee.Nom.Split('(')[0].Trim();

                    if (nom1 != nom2)
                    {
                        graphe.AjouterLien(depart.Noeud_id, arrivee.Noeud_id, lien.LienPoids);
                    }
                }
            }
            


            // === Étape 4 : Générer l’image PNG ===
            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo);
            visualiseur.Dessiner("../../graphe.png");

            Console.WriteLine("✅ Carte du métro enregistrée avec succès.");
            Console.ReadKey();
        }
    }
}
