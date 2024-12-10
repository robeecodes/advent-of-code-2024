namespace ChallengeOne {
    class Program {
        public static void Main() {
            var graph = BuildGraph();

            // foreach (var row in graph) {
            //     foreach (int col in row) {
            //         Console.Write($"{col},");
            //     }
            //
            //     Console.WriteLine();
            // }

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
            int totalScore = 0;

            for (int row = 0; row < graph.Count; row++) {
                for (int col = 0; col < graph[row].Count; col++) {
                    if (graph[row][col] == 0) {
                        totalScore += BuildTrail(row, col, graph, -1, new HashSet<(int, int)>());
                    }
                }
            }

            return totalScore;
        }

        private static int BuildTrail(int row, int col, List<List<int>> graph, int prev, HashSet<(int, int)> visited) {
            var directions = new[] {
                (-1, 0), // up
                (1, 0),  // down
                (0, -1), // left
                (0, 1)   // right
            };

            if (row < 0 || row >= graph.Count || col < 0 || col >= graph[0].Count || visited.Contains((row, col)))
                return 0;

            int currentValue = graph[row][col];
            if (currentValue - prev != 1) return 0;

            visited.Add((row, col));

            int score = currentValue == 9 ? 1 : 0;

            // Explore all directions
            foreach (var (dr, dc) in directions) {
                score += BuildTrail(row + dr, col + dc, graph, currentValue, visited);
            }

            return score;
        }
    }
}