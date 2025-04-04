namespace LivinParis_Test.Parcours_Tests
{
    public class AStar
    {
        [TestFixture]
        public class AStarTests 
        {
        [Test]
        public void AStarTestChemin()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            
            // Créer le graphe à partir du fichier
            Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);
            
            // Créer l'instance de AStar
            AStar<int> aStar = new AStar<int>(Graph);
            
            // Trouver le chemin le plus court entre les noeuds 1 et 6
            (double distance, List<int> chemin) = aStar.TrouverChemin(1, 6);
            
            // Vérifier que le nœud d'arrivée est bien présent dans le chemin
            Assert.That(chemin.Contains(6), Is.True);
            
            // Vérifier que le nœud de départ est bien présent dans le chemin
            Assert.That(chemin.Contains(1), Is.True);
            
            // On peut aussi vérifier que le chemin est correctement ordonné
            Assert.That(chemin[0], Is.EqualTo(1)); // Premier élément est le départ
            Assert.That(chemin[chemin.Count - 1], Is.EqualTo(6)); // Dernier élément est l'arrivée
        }

        [Test]
        public void AStarTestDistance()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            
            // Créer le graphe à partir du fichier
            Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);
            
            // Créer l'instance de AStar
            AStar<int> aStar = new AStar<int>(Graph);
            
            // Trouver le chemin le plus court entre les noeuds 1 et 6
            (double distance, List<int> chemin) = aStar.TrouverChemin(1, 6);
            
            // Vérifier que la distance est celle attendue (comme pour Dijkstra et Floyd-Warshall)
            Assert.That(distance, Is.EqualTo(1));
        }
        
        [Test]
        public void AStarTestCheminImpossible()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            
            // Créer le graphe à partir du fichier
            Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);
            
            // Créer l'instance de AStar
            AStar<int> aStar = new AStar<int>(Graph);
            
            // Nœud qui n'existe pas dans le graphe
            int noeudInexistant = maxCount + 1;
            
            // Tester avec try/catch pour les noeuds inexistants
            try
            {
                (double distance, List<int> chemin) = aStar.TrouverChemin(1, noeudInexistant);
                Assert.Fail("Une exception aurait dû être levée pour un noeud inexistant");
            }
            catch (ArgumentException)
            {
                // C'est le comportement attendu
                Assert.Pass();
            }
        }
        
        [Test]
        public void AStarTestCheminComplet()
        {
            char divider = ' ';
            int maxCount = 35;
            string path = "../../soc-karate.mtx";
            
            // Créer le graphe à partir du fichier
            Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);
            
            // Créer l'instance de AStar
            AStar<int> aStar = new AStar<int>(Graph);
            
            // Trouver le chemin le plus court entre les noeuds 1 et 6
            (double distance, List<int> chemin) = aStar.TrouverChemin(1, 6);
            
            // Vérifier que le chemin est cohérent - chaque nœud doit être connecté au suivant
            for (int i = 0; i < chemin.Count - 1; i++)
            {
                int noeudActuel = chemin[i];
                int noeudSuivant = chemin[i + 1];
                
                // Vérifier que noeudActuel est bien connecté à noeudSuivant
                bool estConnecte = false;
                foreach (var lien in Graph.Noeuds[noeudActuel].Liens)
                {
                    if (lien.LienArrivee.Noeud_id == noeudSuivant)
                    {
                        estConnecte = true;
                        break;
                    }
                }
                
                Assert.That(estConnecte, Is.True, $"Le noeud {noeudActuel} n'est pas connecté au noeud {noeudSuivant}");
            }
        }
    }
    }
}