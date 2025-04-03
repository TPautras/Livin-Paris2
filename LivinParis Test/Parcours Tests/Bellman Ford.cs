namespace LivinParis_Test.Parcours_Tests
{
        [TestFixture]
        public class Bellman_Ford
        {
            [Test]
            public void Bellman_FordTestList()
            {
                char divider = ' ';
                int maxCount = 35;
                string path = "../../soc-karate.mtx";
                Graphe<int> Graph = new Graphe<int>(path,divider,maxCount);
                (List<int>,double) result = Graphs.Parcours.Bellman_Ford<int>.CheminPlusCourtBellman_Ford(Graph,1,6);
                List<int> expected = new List<int>();
                expected.Add(6);
                Assert.That(result.Item1, Is.EqualTo(expected));
            }
            [Test]
            public void Bellman_FordTestPoids()
            {
                char divider = ' ';
                int maxCount = 35;
                string path = "../../soc-karate.mtx";
                Graphe<int> Graph = new Graphe<int>(path,divider,maxCount);
                (List<int>,double) result = Graphs.Parcours.Bellman_Ford<int>.CheminPlusCourtBellman_Ford(Graph,1,6);
                Assert.That(result.Item2, Is.EqualTo(1));
            }
        }
}