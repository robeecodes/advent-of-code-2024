namespace ChallengeOne {
    class Program {
        public static void Main() {
            var input = ReadInput();
            var plantTypes = GetPlantTypes(input);
            var regions = GetRegions(input, plantTypes);

            Console.WriteLine(regions.Sum());
        }

        private static List<string> ReadInput() {
            var text = File.ReadAllText("Inputs/inputs.txt").Split('\n');
            for (int i = 0; i < text.Length; i++) {
                text[i] = text[i].TrimEnd('\r', '\n');
            }

            return text.ToList();
        }

        private static HashSet<char> GetPlantTypes(List<string> input) {
            var regions = new HashSet<char>();
            foreach (var c in input.SelectMany(line => line)) {
                regions.Add(c);
            }

            return regions;
        }

        private static (int, int) GetRegionParameters(List<string> input, char plantType, (int, int) regionStart, HashSet<(int, int)> visited) {
            int area = 0;
            int perimeter = 0;
            var directions = new[] {
                (0, -1), // up
                (0, 1), // down
                (-1, 0), // left
                (1, 0) // right
            };

            var stack = new Stack<(int, int)>();
            stack.Push(regionStart);
            visited.Add(regionStart);

            while (stack.Count > 0) {
                var (row, col) = stack.Pop();
                area++;

                int localPerimeter = 4;

                foreach (var (dx, dy) in directions) {
                    int newRow = row + dx;
                    int newCol = col + dy;

                    if (newRow >= 0 && newRow < input.Count && newCol >= 0 && newCol < input[0].Length) {
                        if (input[newRow][newCol] == plantType) {
                            localPerimeter--; // Shared edge, reduce perimeter
                            if (!visited.Contains((newRow, newCol))) {
                                stack.Push((newRow, newCol));
                                visited.Add((newRow, newCol));
                            }
                        }
                    }
                }

                perimeter += localPerimeter;
            }

            return (area, perimeter);
        }

        private static List<int> GetRegions(List<string> input, HashSet<char> plantTypes) {
            var results = new List<int>();
            var visited = new HashSet<(int, int)>();

            foreach (char plant in plantTypes) {
                for (int i = 0; i < input.Count; i++) {
                    for (int j = 0; j < input[0].Length; j++) {
                        if (input[i][j] == plant && !visited.Contains((i, j))) {
                            var (area, perimeter) = GetRegionParameters(input, plant, (i, j), visited);
                            results.Add(area * perimeter);
                        }
                    }
                }
            }

            return results;
        }
    }
}