using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Exercice_1
{
    public class GrapheImage<T>
    {
        public Dictionary<int, Point> PositionsNoeuds;
        private Graphe<T> graphe;

        public GrapheImage(Graphe<T> g)
        {
            this.graphe = g;
            this.PositionsNoeuds = new Dictionary<int, Point>();

            Random rnd = new Random();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                PositionsNoeuds[noeud.Noeud_id] = new Point(rnd.Next(50, 450), rnd.Next(50, 450));
            }
        }

        public void DessinerGraphe(string filename = "graphe.png")
        {
            Bitmap bmp = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 2);
            Brush brushNoeud = new SolidBrush(Color.Blue);
            Brush brushTexte = new SolidBrush(Color.White);
            Font font = new Font("Arial", 10);

            foreach (var noeud in graphe.Noeuds.Values)
            {
                foreach (var lien in noeud.Liens)
                {
                    Point p1 = PositionsNoeuds[lien.LienDepart.Noeud_id];
                    Point p2 = PositionsNoeuds[lien.LienArrivee.Noeud_id];

                    g.DrawLine(pen, p1, p2);
                }
            }

            foreach (var noeud in graphe.Noeuds.Values)
            {
                Point position = PositionsNoeuds[noeud.Noeud_id];
                Rectangle rect = new Rectangle(position.X - 10, position.Y - 10, 20, 20);
                g.FillEllipse(brushNoeud, rect);
                g.DrawEllipse(pen, rect);

                g.DrawString(noeud.Noeud_Valeur?.ToString() ?? noeud.Noeud_id.ToString(), font, brushTexte, position.X - 5, position.Y - 5);
            }

            try
            {
                bmp.Save(filename);
                Console.WriteLine($"Graphe sauvegardé sous {filename}");
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                throw new Exception($"Erreur lors de la sauvegarde de l'image : {filename}");
            }
            string path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            ImageViewer(path);
        }

        public void ImageViewer(string path)
        {
            var psi = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
