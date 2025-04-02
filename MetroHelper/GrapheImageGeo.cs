using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Graphs;
using MetroHelper;

public class GrapheImageGeo<T>
{
    private Graphe<T> graphe;
    private Dictionary<int, PointF> coordonneesNormalisées;

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
        Bitmap bmp = new Bitmap(width, height);
        Graphics g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.White);
        Pen edgePen = new Pen(Color.Black, 1);
        Pen arrowPen = new Pen(Color.Black, 1) { CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5) };
        Brush nodeBrush = Brushes.Blue;
        Brush textBrush = Brushes.White;
        Font font = new Font("Arial", 9);

        // Dessin des arêtes (flèches)
        foreach (var noeud in graphe.Noeuds.Values)
        {
            foreach (var lien in noeud.Liens)
            {
                var p1 = coordonneesNormalisées[noeud.Noeud_id];
                var p2 = coordonneesNormalisées[lien.LienArrivee.Noeud_id];
                g.DrawLine(arrowPen, p1, p2);
            }
        }

        // Dessin des nœuds
        foreach (var noeud in graphe.Noeuds.Values)
        {
            var pos = coordonneesNormalisées[noeud.Noeud_id];
            RectangleF circle = new RectangleF(pos.X - 10, pos.Y - 10, 20, 20);
            g.FillEllipse(nodeBrush, circle);
            g.DrawEllipse(Pens.Black, circle);

            string label = (noeud.Noeud_Valeur as Station_de_metro)?.Nom.Split('(')[0] ?? noeud.Noeud_id.ToString();
            g.DrawString(label, font, Brushes.Black, pos.X + 12, pos.Y - 8);
        }

        bmp.Save(filename);
        Console.WriteLine($"Graphe sauvegardé sous : {filename}");
    }
}
