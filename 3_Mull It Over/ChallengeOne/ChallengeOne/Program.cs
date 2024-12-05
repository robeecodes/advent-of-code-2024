using System.Text.RegularExpressions;

namespace ChallengeOne;

class Program {
    public static void Main() {
        string input = GetInput();
        List<int> multiples = ParseInput(input);
        
        int sum = multiples.Sum();
        Console.WriteLine(sum);
    }

    private static string GetInput() {
        return File.ReadAllText("Inputs/inputs.txt");
    }

    private static List<int> ParseInput(string input) {
        var pattern = @"mul\(\d*,\d*\)";
        MatchCollection matches = Regex.Matches(input, pattern);
        
        var multiples = new List<int>();
        
        foreach (Match match in matches) {
            var numberPattern = @"(\d+)";
            MatchCollection numbers = Regex.Matches(match.Value, numberPattern);
            int num1 = Convert.ToInt32(numbers[0].Value);
            int num2 = Convert.ToInt32(numbers[1].Value);
            
            multiples.Add(num1 * num2);
        }
        return multiples;
    }
}