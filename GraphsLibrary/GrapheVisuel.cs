using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Graphs
{
    public class GrapheVisuel<T>
    {
        private Graphe<T> _graphe;
        private Dictionary<int, Point> _positionsNoeuds;
        private Dictionary<int, string> _couleursParNoeud;

        public GrapheVisuel(Graphe<T> graphe)
        {
            _graphe = graphe;
            _positionsNoeuds = new Dictionary<int, Point>();
            _couleursParNoeud = new Dictionary<int, string>();

            Random rnd = new Random();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                _positionsNoeuds[noeud.Noeud_id] = new Point(rnd.Next(50, 450), rnd.Next(50, 450));
            }
        }

        public void ColorierNoeud(int id, string couleur)
        {
            _couleursParNoeud[id] = couleur;
        }

        public void DessinerGraphe(string filename = "graphe.png")
        {
            Bitmap bmp = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 10);
            Brush brushTexte = new SolidBrush(Color.White);

            // Dessin des arêtes
            foreach (var noeud in _graphe.Noeuds.Values)
            {
                foreach (var lien in noeud.Liens)
                {
                    Point p1 = _positionsNoeuds[lien.LienDepart.Noeud_id];
                    Point p2 = _positionsNoeuds[lien.LienArrivee.Noeud_id];
                    g.DrawLine(pen, p1, p2);
                }
            }

            // Dessin des nœuds
            foreach (var noeud in _graphe.Noeuds.Values)
            {
                Point position = _positionsNoeuds[noeud.Noeud_id];

                string couleurStr = _couleursParNoeud.TryGetValue(noeud.Noeud_id, out string couleur) ? couleur : "Blue";
                Color color = Color.FromName(couleurStr);
                Brush brushNoeud = new SolidBrush(color);

                Rectangle rect = new Rectangle(position.X - 10, position.Y - 10, 20, 20);
                g.FillEllipse(brushNoeud, rect);
                g.DrawEllipse(pen, rect);

                string texte = noeud.Noeud_Valeur?.ToString() ?? noeud.Noeud_id.ToString();
                g.DrawString(texte, font, brushTexte, position.X - 5, position.Y - 5);
            }

            try
            {
                bmp.Save(filename);
                Console.WriteLine($"🖼️ Graphe sauvegardé sous {filename}");
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                throw new Exception($"Erreur lors de la sauvegarde de l'image : {filename}");
            }

            ImageViewer(Path.Combine(Directory.GetCurrentDirectory(), filename));
        }

        private void ImageViewer(string path)
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
