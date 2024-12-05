namespace ChallengeOne;

class Program {
    public static void Main() {
        var rules = new List<(int, int)>();
        var inputs = new List<List<int>>();
        
        GetInput(rules, inputs);
        
        var middles = new List<int>();
        
        inputs.ForEach(input => {
            (bool, int) res = Validate(rules, input);
            if (res.Item1) {
                middles.Add(res.Item2);
            }
        });
        
        Console.WriteLine(middles.Sum());
    }

    private static void GetInput(List<(int, int)> rules, List<List<int>> inputs) {
        string text = File.ReadAllText("Inputs/inputs.txt");
        
        var input = text.Split('\n').ToList();

        foreach (var item in input) {
            if (item.Contains('|')) {
                var rule = item.Split('|');
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            } else if (item.Contains(',')) {
                var items = item.Split(',');
                inputs.Add(items.Select(int.Parse).ToList());
            }
        }
    }

    private static (bool, int) Validate(List<(int, int)> rules, List<int> input) {
        foreach (var rule in rules) {
            if (!input.Contains(rule.Item1) || !input.Contains(rule.Item2)) continue;
            if (input.IndexOf(rule.Item1) > input.IndexOf(rule.Item2)) return (false, 0);
        }

        return (true, input[input.Count / 2]);
    }
}