// 11 x 7

using System.Text.RegularExpressions;

namespace ChallengeOne {
    class Program {
        public static void Main() {
            var input = GetInput();

            int width = 101;
            int height = 103;
            
            MoveBots(ref input, ref width, ref height);

            var quadrants = FindQuadrants(ref width, ref height);
            var botCounts = CountBots(ref input, ref quadrants);

            int res = 0;
            
            foreach (var kvp in botCounts) {
                if (res == 0 && kvp.Value != 0) res = kvp.Value;
                else if (kvp.Value != 0) res *= kvp.Value;
            }
            
            Console.WriteLine(res);

        }
        
        private static List<(int, int)[]> GetInput() {
            var lines = File.ReadAllText("Inputs/inputs.txt").Split('\n');
            var input = new List<(int, int)[]>();

            foreach (string line in lines) {
                var (x, y) = ParseLine("p=");
                var (vx, vy) = ParseLine("v=");
                
                input.Add([(x, y), (vx, vy)]);
                
                (int, int) ParseLine(string prefix) {
                    Regex pattern = new Regex($"(?<={prefix})(-?[0-9]+,-?[0-9]+)");
                    MatchCollection matchedPart = pattern.Matches(line);
                    var part = matchedPart[0].ToString().Split(",");
                    
                    return (int.Parse(part[0]), int.Parse(part[1]));
                }
            }
            
            return input;
        }

        private static void MoveBots(ref List<(int, int)[]> input, ref int width, ref int height) {
            for (int i = 0; i < input.Count; i++) {
                var bot = input[i];
                
                var (x, y) = bot[0];
                var (vx, vy) = bot[1];
                
                x += (vx * 100);
                x = ((x % width) + width) % width;

                y += (vy * 100);
                y = ((y % height) + height) % height;
                
                input[i] = [(x, y), (vx, vy)];
            }
        }

        private static List<(int, int)[]> FindQuadrants(ref int width, ref int height) {
            int middleCol = width / 2;
            int middleRow = height / 2;

            var quadrants = new List<(int, int)[]>();

            // Top left
            quadrants.Add([
                (0, middleCol),
                (0, middleRow)
            ]);

            // Top right
            quadrants.Add([
                (middleCol + 1, width),
                (0, middleRow)
            ]);

            // Bottom left
            quadrants.Add([
                (0, middleCol),
                (middleRow + 1, height)
            ]);

            // Bottom right
            quadrants.Add([
                (middleCol + 1, width),
                (middleRow + 1, height)
            ]);

            return quadrants;
        }

        private static Dictionary<int, int> CountBots(ref List<(int, int)[]> input, ref List<(int, int)[]> quadrants) {
            var botCounts = new Dictionary<int, int>();

            foreach (var bot in input) {
                var (x, y) = bot[0];

                for (int i = 0; i < quadrants.Count; i++) {
                    var quadrant = quadrants[i];
                    var widthRange = quadrant[0];
                    var heightRange = quadrant[1];

                    // Check if bot is within the quadrant
                    if (x < widthRange.Item1 || x >= widthRange.Item2 ||
                        y < heightRange.Item1 || y >= heightRange.Item2) continue;
                    if (!botCounts.TryAdd(i, 1)) botCounts[i]++;
                    break;
                }
            }
            return botCounts;
        }
    }
}

