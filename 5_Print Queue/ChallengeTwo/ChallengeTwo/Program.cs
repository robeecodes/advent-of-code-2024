namespace ChallengeTwo;

class Program {
    public static void Main() {
        var rules = new List<(int, int)>();
        var inputs = new List<List<int>>();
        
        GetInput(rules, inputs);
        
        var middles = new List<int>();
        
        inputs.ForEach(input => {
            (bool, int) res = ValidateAndReorder(rules, input);
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

    private static (bool isValid, int middleValue) ValidateAndReorder(List<(int, int)> rules, List<int> input) {
        var ordering = new HashSet<(int, int)>(rules);
        var comparer = Comparer<int>.Create((x, y) => 
            ordering.Contains((x, y)) ? -1 : ordering.Contains((y, x)) ? 1 : 0);

        if (input.SequenceEqual(input.OrderBy(i => i, comparer))) {
            return (false, -1);
        }
        
        var reordered = input.OrderBy(i => i, comparer).ToList();

        return (true, reordered[reordered.Count / 2]);
    }
}