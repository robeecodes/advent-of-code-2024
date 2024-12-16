using System.Text;

namespace ChallengeOne {
    class Program {
        public static void Main() {
            var (map, instructions, startPoint) = GetInput();
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
            int i = 0;
            int fin = instructions.Length;
            foreach (char movement in instructions) {
                i++;
                var (currRow, currColumn) = botPosition;
                var newPosition = (
                    currRow + Directions[movement].Item1,
                    currColumn + Directions[movement].Item2
                );

                var (newRow, newColumn) = newPosition;

                if (map[newRow][newColumn] == '.') {
                    map[newRow][newColumn] = '@';
                    map[currRow][currColumn] = '.';
                    botPosition = newPosition;
                }
                else if (map[newRow][newColumn] == '[' || map[newRow][newColumn] == ']') {
                    if (MoveBox(ref map, movement, newPosition)) {
                        map[newRow][newColumn] = '@';
                        map[currRow][currColumn] = '.';
                        botPosition = newPosition;
                    }
                }

                // Console.WriteLine($"\nMove {movement}:");
                // Console.WriteLine($"Iteration: {i}/{fin}");
                // foreach (var row in map) {
                //     foreach (char c in row) {
                //         Console.Write(c);
                //     }
                //
                //     Console.WriteLine();
                // }

                // Console.ReadLine();
            }
        }

        private static bool MoveBox(ref List<List<char>> map, char movement, (int, int) boxPosition) {
            var (currRow, currColumn) = boxPosition;

            var tempMap = map.Select(innerList => innerList.ToList()).ToList();

            int colOffset = Directions[movement].Item2;

            if (movement == '^' || movement == 'v') {
                if (tempMap[currRow][currColumn] == ']') {
                    currColumn--;
                }
            }

            var newPosition = (
                currRow + Directions[movement].Item1,
                currColumn + Directions[movement].Item2 + colOffset
            );

            var (newRow, newColumn) = newPosition;

            if (tempMap[newRow][newColumn] == '#') return false;
            
            if (movement == '^' || movement == 'v') {
                if (tempMap[newRow][newColumn] == '.' && tempMap[newRow][newColumn + 1] == '[') {
                    if (!MoveBox(ref tempMap, movement, (newRow, newColumn + 1))) return false;
                }
            }

            if (tempMap[newRow][newColumn] == '[' || tempMap[newRow][newColumn] == ']') {
                if (movement == '^' || movement == 'v') {
                    if (tempMap[newRow][newColumn + 1] == '[') {
                        if (!MoveBox(ref tempMap, movement, (newRow, newColumn + 1)) ||
                            !MoveBox(ref tempMap, movement, newPosition)) {
                            return false;
                        }
                    }
                    else if (!MoveBox(ref tempMap, movement, newPosition)) {
                        return false;
                    }
                }
                else if (!MoveBox(ref tempMap, movement, newPosition)) {
                    return false;
                }
            }

            if (movement == '<' || movement == '>') {
                if (tempMap[newRow][newColumn] == '.') {
                    if (movement == '<') {
                        tempMap[newRow][newColumn] = '[';
                        tempMap[newRow][newColumn - colOffset] = ']';
                    }

                    if (movement == '>') {
                        tempMap[newRow][newColumn] = ']';
                        tempMap[newRow][newColumn - colOffset] = '[';
                    }

                    tempMap[currRow][currColumn] = '.';
                    map = tempMap;
                    return true;
                }
            }

            if (movement == '^' || movement == 'v') {
                if (tempMap[newRow][newColumn] == '.' && tempMap[newRow][newColumn + 1] == '.') {
                    tempMap[newRow][newColumn] = '[';
                    tempMap[newRow][newColumn + 1] = ']';
                    tempMap[currRow][currColumn] = '.';
                    tempMap[currRow][currColumn + 1] = '.';

                    map = tempMap;
                    return true;
                }
            }

            return false;
        }


        private static int BoxGPS(ref List<List<char>> map) {
            int res = 0;
            int middleRow = map.Count / 2;
            int middleColumn = map[0].Count / 2;

            for (int row = 0; row < map.Count; row++) {
                for (int col = 0; col < map[row].Count; col++) {
                    if (map[row][col] == '[') {
                        int horizontal = col + 1;
                        int vertical = row + 1;

                        if (col < middleColumn) horizontal = col;
                        if (row < middleRow) vertical = row;

                        res += (100 * row) + col;
                    }
                }
            }
            return res;
        }
    }
}