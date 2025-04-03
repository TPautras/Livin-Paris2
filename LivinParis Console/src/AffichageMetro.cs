using MetroHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // === Étape 2 : Créer un graphe vide (on contourne le constructeur de lecture fichier) ===
            var graphe = new Graphe<Station_de_metro>("skip", ',', 1);
            graphe.Noeuds.Clear();  // Nettoyage pour repartir à zéro

            Dictionary<int, (int lat, int lon)> coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
            
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

                if (depart.Noeud_Valeur is Station_de_metro stationDepart &&
                    arrivee.Noeud_Valeur is Station_de_metro stationArrivee)
                {
                    string nom1 = stationDepart.Nom.Split('(')[0].Trim();
                    string nom2 = stationArrivee.Nom.Split('(')[0].Trim();

                    // On ajoute uniquement les liens entre grandes stations différentes
                    
                        graphe.AjouterLien(depart.Noeud_id, arrivee.Noeud_id, lien.LienPoids);
                        graphe.AjouterLien(arrivee.Noeud_id, depart.Noeud_id, lien.LienPoids);

                    
                }
            }

            // === Étape 4 : Générer l’image PNG ===
            string imagePath = Path.Combine("..", "..", "..", "graphe.png");
            var visualiseur = new GrapheImageGeo<Station_de_metro>(graphe, coordonneesGeo);
            visualiseur.Dessiner(imagePath);
            
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (noeud.Liens.Count == 0)
                {
                    var nomStation = (noeud.Noeud_Valeur as Station_de_metro)?.Nom ?? "(nom inconnu)";
                    Console.WriteLine($"⚠️ Station orpheline : {noeud.Noeud_id} - {nomStation}");
                }
            }

            Console.WriteLine("✅ Carte du métro enregistrée avec succès.");

            // === Étape 5 : Ouvrir l’image automatiquement ===
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = imagePath,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Erreur lors de l'ouverture de l'image : {e.Message}");
            }

            Console.ReadKey();
        }
    }
}
