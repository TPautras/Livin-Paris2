using Graphs;
using System.Globalization;
namespace MetroHelper;

public class CreateGraphMetro
{
    private Dictionary<string, int> stationId;
    private int nextId;
    private Graphe<string> graph;

    public CreateGraphMetro(Dictionary<string, int> stationId, int nextId, Graphe<string>graph) 
    {
        
        this.stationId = stationId;
        this.nextId = nextId;
        this.graph = graph;
        
    }

    public void ConstruireDepuisDossier(string dossierCsv)
    {
        foreach (var fichier in Directory.GetFiles(dossierCsv,"*.csv"))
        {
            this.ChargerFichierCsv(fichier);
        }
    }

    private void ChargerFichierCsv(string chemin)
    {
        var lignes = File.ReadAllLines(chemin);
        
        for (int i = 1; i < lignes.Length; i++)
        {
            var ligne = lignes[i].Split(',');
            if (ligne.Length < 2) continue;

            string station = NettoyerNomStation(ligne[0]);
            if (!this.stationId.ContainsKey(station))
            {
                this.stationId[station] = this.nextId;
                this.graph.AjouterNoeud(this.nextId, station);
                this.nextId++;
            }
        }
        for (int i = 1; i < lignes.Length - 1; i++)
        {
            string depart = NettoyerNomStation(lignes[i].Split(',')[0]);
            string arrivee = NettoyerNomStation(lignes[i + 1].Split(',')[0]);
            string poidsStr = lignes[i].Split(',')[1].Trim();

            if (!double.TryParse(poidsStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double poids))
                poids = 2.0;

            if (this.stationId.ContainsKey(depart) && this.stationId.ContainsKey(arrivee))
            {
                int idDepart = this.stationId[depart];
                int idArrivee = this.stationId[arrivee];
                this.graph.AjouterLien(idDepart, idArrivee, poids);
            }
        }
    }
    private string NettoyerNomStation(string nom)
    {
        return nom.Trim().Replace("\u00a0", " ").Replace("’", "'").Replace("É", "E").Replace("é", "e")
            .Replace("è", "e").Replace("à", "a").Replace("î", "i").Replace("ô", "o").Replace("ç", "c");
    }


}