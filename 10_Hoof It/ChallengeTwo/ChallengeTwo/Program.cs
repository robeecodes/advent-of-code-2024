namespace ChallengeTwo {
    class Program {
        public static void Main() {
            var graph = BuildGraph();

            Console.WriteLine(FindTrails(graph));
        }

        private static List<List<int>> BuildGraph() {
            string[] lines = File.ReadAllText("Inputs/inputs.txt").Split('\n');
            for (int i = 0; i < lines.Length; i++) {
                lines[i] = lines[i].TrimEnd('\r', '\n');
            }

            List<List<int>> graph = new List<List<int>>();

            foreach (string line in lines) {
                var row = new List<int>();
                foreach (char c in line) {
                    row.Add(int.Parse(c.ToString()));
                }

                graph.Add(row);
            }

            return graph;
        }

        private static int FindTrails(List<List<int>> graph) {
            int totalRating = 0;

            for (int row = 0; row < graph.Count; row++) {
                for (int col = 0; col < graph[row].Count; col++) {
                    if (graph[row][col] == 0) {
                        var uniquePaths = new List<List<(int, int)>>();
                        ExplorePaths(row, col, graph, -1, new HashSet<(int, int)>(), new List<(int, int)>(), uniquePaths);
                        totalRating += uniquePaths.Count;
                    }
                }
            }

            return totalRating;
        }

        private static void ExplorePaths(int row, int col, List<List<int>> graph, int prev, HashSet<(int, int)> visited, List<(int, int)> path, List<List<(int, int)>> uniquePaths) {
            var directions = new[] {
                (-1, 0), // up
                (1, 0),  // down
                (0, -1), // left
                (0, 1)   // right
            };

            if (row < 0 || row >= graph.Count || col < 0 || col >= graph[0].Count || visited.Contains((row, col)))
                return;

            int currentValue = graph[row][col];
            if (currentValue - prev != 1) return;

            path.Add((row, col));
            visited.Add((row, col));

            if (currentValue == 9) {
                if (!uniquePaths.Any(existingPath => existingPath.SequenceEqual(path))) {
                    uniquePaths.Add([..path]);
                }
            } else {
                // Continue exploring all directions
                foreach (var (dr, dc) in directions) {
                    ExplorePaths(row + dr, col + dc, graph, currentValue, visited, path, uniquePaths);
                }
            }

            path.RemoveAt(path.Count - 1);
            visited.Remove((row, col));
        }
    }
}