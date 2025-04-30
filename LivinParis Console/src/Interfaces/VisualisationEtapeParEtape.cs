using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Graphs;
using MetroHelper;

namespace LivinParis_Console
{
    public class VisualisationEtapeParEtape
    {
        
        private Graphe<Station_de_metro> _graphe;
        private List<Station_de_metro> _chemin;
        private Dictionary<string, GrandeStation> _grandesStations;
        private Dictionary<int, string> _couleursParLigne;
        private string _dossierTemp;

        
        public VisualisationEtapeParEtape(Graphe<Station_de_metro> graphe, List<Station_de_metro> chemin, Dictionary<string, GrandeStation> grandesStations, Dictionary<int, string> couleursParLigne, string dossierTemp = "..\\..\\..\\temp_images")
        {
            this._graphe = graphe;
            this._chemin = chemin;
            this._grandesStations = grandesStations;
            this._couleursParLigne = couleursParLigne;
            this._dossierTemp = dossierTemp;
        }
        
        public Graphe<Station_de_metro> Graphe { get => _graphe; set => _graphe = value; }
        public List<Station_de_metro> Chemin { get => _chemin; set => _chemin = value; }
        public Dictionary<string, GrandeStation> GrandesStations { get => _grandesStations; set => _grandesStations = value; }
        public Dictionary<int, string> CouleursParLigne { get => _couleursParLigne; set => _couleursParLigne = value; }
        public string DossierTemp { get => _dossierTemp; set => _dossierTemp = value; }

        public void Afficher()
{
    if (!Directory.Exists(_dossierTemp))
        Directory.CreateDirectory(_dossierTemp);

    var coordonneesGeo = new Dictionary<int, (int lat, int lon)>();
    foreach (var noeud in _graphe.Noeuds.Values)
    {
        if (_chemin.Any(c => c.Id == noeud.Noeud_id))
        {
            string nom = (noeud.Noeud_Valeur as Station_de_metro)?.Nom.Split('(')[0].Trim();
            if (_grandesStations.TryGetValue(nom, out var grande))
            {
                coordonneesGeo[noeud.Noeud_id] = (grande.Latitude, grande.Longitude);
            }
        }
    }

    Process proc = null;

    for (int i = 1; i <= _chemin.Count; i++)
    {
        // Sous-graphe dynamique pour l'étape
        var sousGraphe = new Graphe<Station_de_metro>(_chemin.Count);
        for (int j = 0; j < i; j++)
        {
            sousGraphe.AjouterNoeud(_chemin[j].Id, _chemin[j]);
        }

        for (int j = 0; j < i - 1; j++)
        {
            int fromId = _chemin[j].Id;
            int toId = _chemin[j + 1].Id;

            double poids = _graphe.Liens
                .FirstOrDefault(l => l.LienDepart.Noeud_id == fromId && l.LienArrivee.Noeud_id == toId)?.LienPoids ?? 1;

            sousGraphe.AjouterLien(fromId, toId, poids);
        }

        // Préparer le visualiseur
        var visualiseur = new GrapheImageGeo<Station_de_metro>(sousGraphe, coordonneesGeo)
        {
            CouleurParDefault = "LightGray",
            RayonNoeud = 7,
            CouleurRemplissageNoeud = "White",
            CouleurContourNoeud = "Black",
            CouleurDesLiens = (from, to) =>
            {
                int num = AffichageMetro.ExtraireNumeroLigne(from.Nom);
                return _couleursParLigne.TryGetValue(num, out string color) ? color : "DarkGray";
            },
            AffichageChemin = sousGraphe.Noeuds.Keys.ToList(),
            NafficherQueChemin = false
        };

        string imagePath = Path.Combine(_dossierTemp, $"step_{i}.png");
        visualiseur.Dessiner(imagePath);

        // Fermer l'ancienne image
        if (proc != null && !proc.HasExited)
        {
            try { proc.Kill(); } catch { /* Ignorer */ }
        }

        // Ouvrir la nouvelle image
        proc = Process.Start(new ProcessStartInfo
        {
            FileName = imagePath,
            UseShellExecute = true
        });

        Thread.Sleep(4000); // 4 secondes d'affichage

        if (i != _chemin.Count)
        {
            try
            {
                proc?.Kill();
                proc?.WaitForExit();
                File.Delete(imagePath);
            }
            catch { /* Ignorer */ }
        }
    }

    Console.WriteLine("✅ Visualisation terminée : dernière image affichée.");
}

        
    }
}

