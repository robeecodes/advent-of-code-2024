using System.Text;

namespace ChallengeOne;

class Program {
    public static void Main() {
        List<string> input = ReadInput();
        Console.WriteLine(WordSearch(input));
    }

    private static List<string> ReadInput() {
        string input = File.ReadAllText("Inputs/inputs.txt");
        return input.Split('\n').ToList();
    }

    private static int WordSearch(List<string> input) {
        int xmasCount = 0;

        for (int row = 0; row < input.Count; row++) {
            for (int col = 0; col < input[row].Length; col++) {
                if (input[row][col] != 'X') continue;

                if (col < input[row].Length - 3)
                    xmasCount += CheckForwards(input[row], col) ? 1 : 0;

                if (col >= 3)
                    xmasCount += CheckBackwards(input[row], col) ? 1 : 0;

                if (row >= 3)
                    xmasCount += CheckUp(input, row, col) ? 1 : 0;
                
                if (row < input.Count - 3)
                    xmasCount += CheckDown(input, row, col) ? 1 : 0;

                if (row >= 3 && col >= 3)
                    xmasCount += CheckDiagonalUpBack(input, row, col) ? 1 : 0;
                
                if (row >= 3 && col < input[row].Length - 3)
                    xmasCount += CheckDiagonalUpForwards(input, row, col) ? 1 : 0;
                
                if (row < input.Count - 3 && col >= 3)
                    xmasCount += CheckDiagonalDownBack(input, row, col) ? 1 : 0;
                
                if (row < input.Count - 3 && col < input[row].Length - 3)
                    xmasCount += CheckDiagonalDownForwards(input, row, col) ? 1 : 0;
            }
        }

        return xmasCount;
    }

    private static bool CheckForwards(string input, int idx) {
        // Console.WriteLine(input.Substring(idx, 4));
        return input.Substring(idx, 4) == "XMAS";
    }

    private static bool CheckBackwards(string input, int idx) {
        // Console.WriteLine(input.Substring(idx - 3, 4));
        return input.Substring(idx - 3, 4) == "SAMX";
    }

    private static bool CheckUp(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        for (int currRow = row; currRow >= row - 3; currRow--) {
            sb.Append(input[currRow][col]);
        }
        
        // Console.WriteLine(sb.ToString());
        return sb.ToString() == "XMAS";
    }
    
    private static bool CheckDown(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        for (int currRow = row; currRow <= row + 3; currRow++) {
            sb.Append(input[currRow][col]);
        }
        return sb.ToString() == "XMAS";
    }

    private static bool CheckDiagonalUpBack(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        
        for (int column = col; column >= col - 3; column--) {
            sb.Append(input[row][column]);
            row--;
        }
        
        return sb.ToString() == "XMAS";
    }
    
    private static bool CheckDiagonalUpForwards(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        
        for (int column = col; column <= col + 3; column++) {
            sb.Append(input[row][column]);
            row--;
        }
        
        return sb.ToString() == "XMAS";
    }
    
    private static bool CheckDiagonalDownBack(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        
        for (int column = col; column >= col - 3; column--) {
            sb.Append(input[row][column]);
            row++;
        }
        
        // Console.WriteLine(sb.ToString());
        return sb.ToString() == "XMAS";
    }
    
    private static bool CheckDiagonalDownForwards(List<string> input, int row, int col) {
        StringBuilder sb = new StringBuilder();
        
        for (int column = col; column <= col + 3; column++) {
            sb.Append(input[row][column]);
            row++;
        }
        
        // Console.WriteLine(sb.ToString());
        return sb.ToString() == "XMAS";
    }
}