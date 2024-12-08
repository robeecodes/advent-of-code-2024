namespace ChallengeOne {
    class Program {
        public static void Main(string[] args) {
            string[] input = File.ReadAllText("Inputs/inputs.txt").Split('\n');

            for (int i = 0; i < input.Length; i++) {
                input[i] = input[i].TrimEnd('\r', '\n');
            }

            var maxPos = (input[0].Length, input.Length);
            var positions = GetPositions(input);

            Console.WriteLine(FindAntinodes(positions, maxPos));
        }

        private static Dictionary<char, List<(int, int)>> GetPositions(string[] input) {
            var positions = new Dictionary<char, List<(int, int)>>();

            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input[y].Length; x++) {
                    if (input[y][x] == '.') continue;

                    if (!positions.ContainsKey(input[y][x])) {
                        positions.Add(input[y][x], new List<(int, int)>());
                    }
                    positions[input[y][x]].Add((x, y));
                }
            }

            return positions;
        }

        private static int FindAntinodes(Dictionary<char, List<(int, int)>> positions, (int, int) maxPos) {
            var antinodes = new HashSet<(int, int)>();

            foreach (var freq in positions.Keys) {
                var points = positions[freq];

                for (int i = 0; i < points.Count - 1; i++) {
                    var current = points[i];
                    for (int j = i + 1; j < points.Count; j++) {
                        var next = points[j];

                        var dx = next.Item1 - current.Item1;
                        var dy = next.Item2 - current.Item2;

                        var a = (current.Item1 - dx, current.Item2 - dy);
                        var b = (next.Item1 + dx, next.Item2 + dy);

                        if (0 <= a.Item1 && a.Item1 < maxPos.Item1 && 0 <= a.Item2 && a.Item2 < maxPos.Item2) {
                            antinodes.Add(a);
                        }

                        if (0 <= b.Item1 && b.Item1 < maxPos.Item1 && 0 <= b.Item2 && b.Item2 < maxPos.Item2) {
                            antinodes.Add(b);
                        }
                    }
                }
            }

            return antinodes.Count;
        }
    }
}
