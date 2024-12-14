using System.Text.RegularExpressions;

namespace ChallengeTwo {
    class Program {
        public static void Main() {
            var input = GetInput();

            int width = 101;
            int height = 103;

            Console.WriteLine(FindTree(ref input, ref width, ref height, 0));
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

        private static List<(int, int)[]> MoveBots(ref List<(int, int)[]> input, ref int width, ref int height, int iterations) {
            var res = new List<(int, int)[]>(input);
            for (int i = 0; i < input.Count; i++) {
                var bot = input[i];
                
                var (x, y) = bot[0];
                var (vx, vy) = bot[1];
                
                x += (vx * iterations);
                x = ((x % width) + width) % width;

                y += (vy * iterations);
                y = ((y % height) + height) % height;
                
                res[i] = [(x, y), (vx, vy)];
            }
            return res;
        }

        private static int FindTree(ref List<(int, int)[]> input, ref int width, ref int height, int iteration) {
            while (true) {
                var positions = new HashSet<(int, int)>();
                var bots = MoveBots(ref input, ref width, ref height, iteration);

                foreach (var bot in bots) {
                    var botPos = bot[0];
                    if (!positions.Add((botPos.Item1, botPos.Item2))) break;
                }
                
                if (positions.Count == input.Count) break;
                iteration++;
            }
            
            return iteration;
        }
        
    }
}

