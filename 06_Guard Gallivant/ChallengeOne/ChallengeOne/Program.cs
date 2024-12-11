namespace ChallengeOne;

class Program() {
    public static void Main() {
        List<string> input = GetInput();

        (int, int) start = FindStart(input);

        HashSet<(int, int)> visited = new HashSet<(int, int)>();

        Console.WriteLine(Simulation(input, start, visited));
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

    private static int Simulation(List<string> input, (int, int) start, HashSet<(int, int)> visited)
    {
        var directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int directionIndex = 0;
        int count = 0;

        var current = start;

        while (true) {
            if (!IsWithinBounds(current, input)) break;

            if (visited.Add(current)) count++;

            var next = (current.Item1 + directions[directionIndex].Item1, 
                current.Item2 + directions[directionIndex].Item2);

            if (!IsWithinBounds(next, input)) break;

            if (input[next.Item1][next.Item2] != '#') current = next;
            else directionIndex = (directionIndex + 1) % 4;
        }

        return count;
    }

    private static bool IsWithinBounds((int, int) cell, List<string> input)
    {
        return cell.Item1 >= 0 && cell.Item1 < input.Count &&
               cell.Item2 >= 0 && cell.Item2 < input[0].Length;
    }
}