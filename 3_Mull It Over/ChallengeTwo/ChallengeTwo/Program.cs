using System.Text.RegularExpressions;

namespace ChallengeTwo;

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

    public static List<int> ParseInput(string input) {
        // Match "do()", "don't()", and "mul(x,y)"
        var pattern = @"do\(\)|don't\(\)|mul\(\d+,\d+\)";
        MatchCollection matches = Regex.Matches(input, pattern);

        var multiples = new List<int>();
        bool isDoActive = true;

        foreach (Match match in matches) {
            string value = match.Value;

            if (value == "do()") {
                isDoActive = true;
            } else if (value == "don't()") {
                isDoActive = false;
            } else if (value.StartsWith("mul(")) {
                if (!isDoActive) continue;
                var numberPattern = @"\d+";
                MatchCollection numbers = Regex.Matches(value, numberPattern);
                int num1 = Convert.ToInt32(numbers[0].Value);
                int num2 = Convert.ToInt32(numbers[1].Value);
                    
                multiples.Add(num1 * num2);
            }
        }
        return multiples;
    }
}