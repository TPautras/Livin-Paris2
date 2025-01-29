using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Exercice_1
{
    internal class GrapheVisuel
    {

        private Dictionary<int, Point> positionsNoeuds;
        private Graphe graphe;

        public GrapheVisuel(Graphe g)
        {
            this.graphe = g;
            this.Text = "Visualisation du Graphe";
            this.Size = new Size(600, 600);
            this.DoubleBuffered = true;
            this.positionsNoeuds = new Dictionary<int, Point>();

            // Génération aléatoire des positions des nœuds
            Random rnd = new Random();
            foreach (var noeud in graphe.Noeuds.Values)
            {
                positionsNoeuds[noeud.Id] = new Point(rnd.Next(50, 550), rnd.Next(50, 550));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            Brush brushNoeud = new SolidBrush(Color.Blue);
            Brush brushTexte = new SolidBrush(Color.White);
            Font font = new Font("Arial", 10);

            // Dessiner les liens (arêtes)
            foreach (var noeud in graphe.Noeuds.Values)
            {
                foreach (var lien in noeud.Liens)
                {
                    Point p1 = positionsNoeuds[lien.Source.Id];
                    Point p2 = positionsNoeuds[lien.Destination.Id];

                    g.DrawLine(pen, p1, p2);
                }
            }

            // Dessiner les nœuds (sommets)
            foreach (var noeud in graphe.Noeuds.Values)
            {
                Point position = positionsNoeuds[noeud.Noeud_id];
                Rectangle rect = new Rectangle(position.X - 15, position.Y - 15, 30, 30);
                g.FillEllipse(brushNoeud, rect);
                g.DrawEllipse(pen, rect);

                // Afficher l'ID du nœud au centre
                g.DrawString(noeud.Noeud_id.ToString(), font, brushTexte, position.X - 7, position.Y - 7);
            }
        }
    }
}
