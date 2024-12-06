namespace ChallengeTwo;

class Program() {
    public static void Main() {
        List<string> input = GetInput();

        (int, int) start = FindStart(input);
        
        Console.WriteLine(CountLoopCausingPlacements(input, start));
    }
    private static List<string> GetInput() {
        string input = File.ReadAllText("Inputs/inputs.txt");
        return [..input.Split("\n")];
    }

    private static (int, int) FindStart(List<string> input) {
        for (int row = 0; row < input.Count; row++) {
            if (input[row].Contains('^')) return (row, input[row].IndexOf('^'));
        }

        return (-1, -1);
    }

    private static bool SimulationCreatesLoop(List<string> input, (int, int) start, (int, int) newObstacle)
    {
        var directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int directionIndex = 0;

        var grid = input.Select(row => row.ToCharArray()).ToArray();
        grid[newObstacle.Item1][newObstacle.Item2] = '#';

        var visited = new HashSet<(int, int, int)>();
        var current = start;

        while (true)
        {
            if (!IsWithinBounds(current, grid)) break;

            if (!visited.Add((current.Item1, current.Item2, directionIndex))) return true;

            var next = (current.Item1 + directions[directionIndex].Item1,
                current.Item2 + directions[directionIndex].Item2);

            if (!IsWithinBounds(next, grid)) break;
            if (grid[next.Item1][next.Item2] == '#') directionIndex = (directionIndex + 1) % 4;
            else current = next;
        }

        return false;
    }

    private static bool IsWithinBounds((int, int) pos, char[][] grid)
    {
        return pos.Item1 >= 0 && pos.Item1 < grid.Length &&
               pos.Item2 >= 0 && pos.Item2 < grid[0].Length;
    }

    private static int CountLoopCausingPlacements(List<string> input, (int, int) start)
    {
        int loopCount = 0;

        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Length; j++) {
                if (input[i][j] != '.') continue;
                if (SimulationCreatesLoop(input, start, (i, j))) loopCount++;
            }
        }

        return loopCount;
    }
}