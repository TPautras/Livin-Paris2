namespace LivinParis_Test.Parcours_Tests
{
    public class Floyd_Warshall
    {
        [TestFixture]
        public class FloydWarshallTests
        {
            [Test]
            public void FloydWarshallTestChemin()
            {
                char divider = ' ';
                int maxCount = 35;
                string path = "../../soc-karate.mtx";

                // Créer le graphe à partir du fichier
                Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);

                // Créer l'instance de FloydWarshall
                FloydWarshall<int> floydWarshall = new FloydWarshall<int>(Graph);

                // Trouver le chemin le plus court entre les noeuds 1 et 6
                (double distance, List<int> chemin) = floydWarshall.TrouverPlusCourtChemin(1, 6);

                // Liste attendue pour le chemin
                List<int> expected = new List<int>();
                // Pour Floyd-Warshall, vous pourriez avoir un chemin différent de Dijkstra
                // Le chemin devrait contenir au moins l'arrivée, et peut-être des noeuds intermédiaires
                expected.Add(6);

                // Vérifier que le chemin est celui attendu
                Assert.That(chemin, Is.EqualTo(expected));
            }

            [Test]
            public void FloydWarshallTestDistance()
            {
                char divider = ' ';
                int maxCount = 35;
                string path = "../../soc-karate.mtx";

                // Créer le graphe à partir du fichier
                Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);

                // Créer l'instance de FloydWarshall
                FloydWarshall<int> floydWarshall = new FloydWarshall<int>(Graph);

                // Trouver le chemin le plus court entre les noeuds 1 et 6
                (double distance, List<int> chemin) = floydWarshall.TrouverPlusCourtChemin(1, 6);

                // Vérifier que la distance est celle attendue
                // Puisque Dijkstra a donné 1, on s'attend à la même distance pour Floyd-Warshall
                Assert.That(distance, Is.EqualTo(1));
            }

            [Test]
            public void FloydWarshallTestCheminImpossible()
            {
                char divider = ' ';
                int maxCount = 35;
                string path = "../../soc-karate.mtx";

                // Créer un graphe où certains chemins sont impossibles
                Graphe<int> Graph = new Graphe<int>(path, divider, maxCount);

                // On peut tester avec un noeud qui n'existe pas dans le graphe
                // ou deux noeuds qui ne sont pas connectés (selon la structure du graphe)
                int noeudInexistant = maxCount + 1; // Noeud qui n'existe pas

                // Créer l'instance de FloydWarshall
                FloydWarshall<int> floydWarshall = new FloydWarshall<int>(Graph);

                // Tester avec try/catch pour les noeuds inexistants
                try
                {
                    (double distance, List<int> chemin) = floydWarshall.TrouverPlusCourtChemin(1, noeudInexistant);
                    Assert.Fail("Une exception aurait dû être levée pour un noeud inexistant");
                }
                catch (ArgumentException)
                {
                    // C'est le comportement attendu
                    Assert.Pass();
                }
            }
        }
    }
}