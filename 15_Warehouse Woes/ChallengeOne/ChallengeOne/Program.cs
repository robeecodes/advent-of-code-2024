using System.Text;

namespace ChallengeOne {
    class Program {
        public static void Main() {
            var (map, instructions, startPoint) = GetInput();
            // foreach (var row in map) {
            //     foreach (char c in row) {
            //         Console.Write(c);
            //     }
            //     Console.WriteLine();
            // }
            //
            MoveRobot(ref map, ref instructions, ref startPoint);
            
            Console.WriteLine(BoxGPS(ref map));
        }

        private static (List<List<char>>, string, (int, int)) GetInput() {
            var input = File.ReadAllText("Inputs/inputs.txt");
            var map = new List<List<char>>();
            var instructions = new StringBuilder();
            var startPoint = (0, 0);

            foreach (string line in input.Split('\n')) {
                if (line[0] == '#') map.Add(line.ToCharArray().ToList());
                else if (!string.IsNullOrEmpty(line)) instructions.Append(line.TrimEnd());
                if (line.Contains('@')) startPoint = (map.Count - 1, line.IndexOf('@'));
            }
            
            return (map, instructions.ToString(), startPoint);
        }
        
        private static readonly Dictionary<char, (int, int)> Directions = new Dictionary<char, (int, int)>() {
            { '>', (0, 1) },
            { '<', (0, -1) },
            { '^', (-1, 0) },
            { 'v', (1, 0) },
        };

        private static void MoveRobot(ref List<List<char>> map, ref string instructions, ref (int, int) botPosition) {
            foreach (char movement in instructions) {
                var (currRow, currColumn) = botPosition;
                var newPosition = (
                    currRow + Directions[movement].Item1,
                    currColumn  + Directions[movement].Item2
                    );
                
                var (newRow, newColumn) = newPosition;
                
                if (map[newRow][newColumn] == '.') {
                    map[newRow][newColumn] = '@';
                    map[currRow][currColumn] = '.';
                    botPosition = newPosition;
                }
                else if (map[newRow][newColumn] == 'O') {
                    if (MoveBox(ref map, movement, newPosition)) {
                        map[newRow][newColumn] = '@';
                        map[currRow][currColumn] = '.';
                        botPosition = newPosition;
                    }
                }
                // Console.WriteLine($"\nMove {movement}:");
                // foreach (var row in map) {
                //     foreach (char c in row) {
                //         Console.Write(c);
                //     }
                //     Console.WriteLine();
                // }
            }
        }

        private static bool MoveBox(ref List<List<char>> map, char movement, (int, int) boxPosition) {
            var (currRow, currColumn) = boxPosition;
            var newPosition = (
                currRow + Directions[movement].Item1,
                currColumn + Directions[movement].Item2
            );

            var (newRow, newColumn) = newPosition;

            if (map[newRow][newColumn] == '#') return false;

            if (map[newRow][newColumn] == 'O') {
                if (!MoveBox(ref map, movement, newPosition)) return false;
            }

            if (map[newRow][newColumn] == '.') {
                map[newRow][newColumn] = 'O';
                map[currRow][currColumn] = '.';
                return true;
            }

            return false;
        }

        private static int BoxGPS(ref List<List<char>> map) {
            int res = 0;
            
            for (int row = 0; row < map.Count; row++) {
                for (int col = 0; col < map[row].Count; col++) {
                    if (map[row][col] == 'O') res += (100 * row) + col;
                }
            }
            
            return res;
        }
    }
}