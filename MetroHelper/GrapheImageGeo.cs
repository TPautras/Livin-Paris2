using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Graphs;
using MetroHelper;
/// <summary>
/// Classe pour générer une image de graph qui prend en compte des coordonnées
/// </summary>
/// <typeparam name="T"></typeparam>
public class GrapheImageGeo<T>
{
    private Graphe<T> graphe;
    private Dictionary<int, PointF> coordonneesNormalisées;

    public string CouleurParDefault { get; set; } = "DarkGray";
    public int RayonNoeud { get; set; } = 7;
    public string CouleurRemplissageNoeud { get; set; } = "White";
    public string CouleurContourNoeud { get; set; } = "Black";
    public Func<T, string> CouleurParLigne { get; set; } = null;
    public Func<T, T, string> CouleurDesLiens { get; set; } = null;

    public List<int> AffichageChemin { get; set; } = new List<int>();
    public bool NafficherQueChemin { get; set; } = false;

    public GrapheImageGeo(Graphe<T> graphe, Dictionary<int, (int lat, int lon)> coordonnees)
    {
        this.graphe = graphe;
        this.coordonneesNormalisées = NormaliserCoordonnees(coordonnees, 50, 950, 50, 950);
    }

    private Dictionary<int, PointF> NormaliserCoordonnees(Dictionary<int, (int lat, int lon)> coords, int minX, int maxX, int minY, int maxY)
    {
        int minLat = coords.Values.Min(c => c.lat);
        int maxLat = coords.Values.Max(c => c.lat);
        int minLon = coords.Values.Min(c => c.lon);
        int maxLon = coords.Values.Max(c => c.lon);

        return coords.ToDictionary(
            kvp => kvp.Key,
            kvp =>
            {
                float x = (float)(kvp.Value.lon - minLon) / (maxLon - minLon) * (maxX - minX) + minX;
                float y = (float)(maxLat - kvp.Value.lat) / (maxLat - minLat) * (maxY - minY) + minY;
                return new PointF(x, y);
            });
    }

    public void Dessiner(string filename = "graphe.png")
    {
        int width = 1000, height = 1000;

        using (Bitmap bmp = new Bitmap(width, height))
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            Font font = new Font("Arial", 8);

            bool filtrerChemin = NafficherQueChemin && AffichageChemin != null && AffichageChemin.Count > 0;

            // 🔁 Arêtes
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (!coordonneesNormalisées.ContainsKey(noeud.Noeud_id)) continue;
                if (filtrerChemin && !AffichageChemin.Contains(noeud.Noeud_id)) continue;

                PointF p1 = coordonneesNormalisées[noeud.Noeud_id];

                foreach (var lien in noeud.Liens)
                {
                    if (!coordonneesNormalisées.ContainsKey(lien.LienArrivee.Noeud_id))
                        continue;

                    bool estDansChemin = AffichageChemin.Contains(noeud.Noeud_id) &&
                                         AffichageChemin.Contains(lien.LienArrivee.Noeud_id);

                    if (filtrerChemin && !estDansChemin)
                        continue;

                    PointF p2 = coordonneesNormalisées[lien.LienArrivee.Noeud_id];

                    T from = noeud.Noeud_Valeur;
                    T to = lien.LienArrivee.Noeud_Valeur;

                    string couleur = CouleurDesLiens?.Invoke(from, to) ?? CouleurParDefault;

                    using (Pen pen = new Pen(Color.FromName(couleur), estDansChemin ? 3 : 1.5f))
                    {
                        pen.CustomEndCap = new AdjustableArrowCap(4, 4);
                        g.DrawLine(pen, p1, p2);
                    }

                    if (estDansChemin)
                    {
                        float midX = (p1.X + p2.X) / 2;
                        float midY = (p1.Y + p2.Y) / 2;
                        g.DrawString(lien.LienPoids.ToString(), font, Brushes.Black, midX, midY);
                    }
                }
            }

            // 🔁 Nœuds
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (!coordonneesNormalisées.ContainsKey(noeud.Noeud_id)) continue;
                if (filtrerChemin && !AffichageChemin.Contains(noeud.Noeud_id)) continue;

                PointF pos = coordonneesNormalisées[noeud.Noeud_id];
                RectangleF circle = new RectangleF(pos.X - RayonNoeud, pos.Y - RayonNoeud, RayonNoeud * 2, RayonNoeud * 2);

                using (Brush fillBrush = new SolidBrush(Color.FromName(CouleurRemplissageNoeud)))
                using (Pen outlinePen = new Pen(Color.FromName(CouleurContourNoeud), 1))
                {
                    g.FillEllipse(fillBrush, circle);
                    g.DrawEllipse(outlinePen, circle);
                }

                string label = (noeud.Noeud_Valeur as Station_de_metro)?.Nom.Split('(')[0] ?? noeud.Noeud_id.ToString();
                g.DrawString(label, font, Brushes.Black, pos.X + 10, pos.Y - 6);
            }

            bmp.Save(filename);
            Console.WriteLine($"✅ Graphe sauvegardé sous : {filename}");
        }
    }
}
