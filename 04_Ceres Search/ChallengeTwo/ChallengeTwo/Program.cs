using System.Text;

namespace ChallengeTwo;

class Program {
    public static void Main() {
        List<string> input = ReadInput();
        Console.WriteLine(CountX(input));
    }

    private static List<string> ReadInput() {
        string input = File.ReadAllText("Inputs/inputs.txt");
        return input.Split('\n').ToList();
    }

    private static int CountX(List<string> input) {
        int xCount = 0;

        for (int row = 1; row < input.Count - 1; row++) {
            for (int col = 1; col < input[row].Length - 2; col++) {
                CheckXTopDown(input, row, col);
                CheckXBottomUp(input, row, col);

                if (CheckXTopDown(input, row, col) && CheckXBottomUp(input, row, col)) xCount++;
            }
        }

        return xCount;
    }

    private static bool CheckXTopDown(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        int currCol = col - 1;
        for (int currRow = row - 1; currRow <= row + 1; currRow++) {
            sb.Append(input[currRow][currCol]);
            currCol++;
        }
        
        return sb.ToString() == "MAS" || sb.ToString() == "SAM";
    }
    
    private static bool CheckXBottomUp(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        int currCol = col - 1;
        for (int currRow = row + 1; currRow >= row - 1; currRow--) {
            sb.Append(input[currRow][currCol]);
            currCol++;
        }
        
        return sb.ToString() == "MAS" || sb.ToString() == "SAM";
    }
}