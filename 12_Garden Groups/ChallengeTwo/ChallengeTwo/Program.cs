namespace ChallengeTwo {
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
            var plantTypes = new HashSet<char>();
            foreach (var c in input.SelectMany(line => line)) {
                plantTypes.Add(c);
            }

            return plantTypes;
        }
        
        private static (int, int) GetRegionParameters(List<string> input, char plantType, (int, int) regionStart, HashSet<(int, int)> visited) {
            int area = 0;
            int corners = 0;
            
            var directions = new[] {
                (0, -1), // up
                (0, 1),  // down
                (-1, 0), // left
                (1, 0)   // right
            };

            var stack = new Stack<(int, int)>();
            stack.Push(regionStart);
            visited.Add(regionStart);

            while (stack.Count > 0) {
                var (row, col) = stack.Pop();
                area++;
                
                CheckCorners(input, plantType, (row, col), ref corners);
                
                foreach (var (dx, dy) in directions) {
                    int newRow = row + dx;
                    int newCol = col + dy;

                    if (newRow >= 0 && newRow < input.Count && newCol >= 0 && newCol < input[0].Length) {
                        if (input[newRow][newCol] == plantType && !visited.Contains((newRow, newCol))) {
                            stack.Push((newRow, newCol));
                            visited.Add((newRow, newCol));
                        }
                    }
                }
            }

            return (area, corners);
        }

        private static void CheckCorners(List<string> input, char plantType, (int, int) idx, ref int corners) {

            // for outer corners
            // up
            bool up = false;
            if (idx.Item1 - 1 < 0) up = false;
            else up = input[idx.Item1 - 1][idx.Item2] == plantType;
            
            // down
            bool down = false;
            if (idx.Item1 + 1 >= input.Count) down = false;
            else down = input[idx.Item1 + 1][idx.Item2] == plantType;
            
            // left
            bool left = false;
            if (idx.Item2 - 1 < 0) left = false;
            else left = input[idx.Item1][idx.Item2 - 1] == plantType;
            
            // right
            bool right = false;
            if (idx.Item2 + 1 >= input[0].Length) right = false;
            else right = input[idx.Item1][idx.Item2 + 1] == plantType;
            
            if (!left && !up) corners++;
            if (!left && !down) corners++;
            if (!right && !up) corners++;
            if (!right && !down) corners++;
            
            // for inner corners
            // leftUp
            bool leftUp = true;
            if (idx.Item1 - 1 < 0) leftUp = false;
            if (idx.Item2 - 1 < 0) leftUp = false;
            if (leftUp) leftUp = input[idx.Item1 - 1][idx.Item2 - 1] == plantType;
            
            // leftDown
            bool leftDown = true;
            if (idx.Item1 + 1 >= input.Count) leftDown = false;
            if (idx.Item2 - 1 < 0) leftDown = false;
            if (leftDown) leftDown = input[idx.Item1 + 1][idx.Item2 - 1] == plantType;
            
            // rightUp
            bool rightUp = true;
            if (idx.Item1 - 1 < 0) rightUp = false;
            if (idx.Item2 + 1 >= input[0].Length) rightUp = false;
            if (rightUp) rightUp = input[idx.Item1 - 1][idx.Item2 + 1] == plantType;
            
            // rightDown
            bool rightDown = true;
            if (idx.Item1 + 1 >= input.Count) rightDown = false;
            if (idx.Item2 + 1 >= input[0].Length) rightDown = false;
            if (rightDown) rightDown = input[idx.Item1 + 1][idx.Item2 + 1] == plantType;
            
            if (left && up && !leftUp) corners++;
            if (left && down && !leftDown) corners++;
            if (right && up && !rightUp) corners++;
            if (right && down && !rightDown) corners++;
        }
        
        private static List<int> GetRegions(List<string> input, HashSet<char> plantTypes) {
            var results = new List<int>();
            var visited = new HashSet<(int, int)>();

            foreach (char plant in plantTypes) {
                for (int i = 0; i < input.Count; i++) {
                    for (int j = 0; j < input[0].Length; j++) {
                        if (input[i][j] == plant && !visited.Contains((i, j))) {
                            var (area, sides) = GetRegionParameters(input, plant, (i, j), visited);
                            results.Add(area * sides);
                        }
                    }
                }
            }

            return results;
        }
    }
}