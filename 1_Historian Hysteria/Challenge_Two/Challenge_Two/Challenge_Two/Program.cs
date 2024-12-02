namespace Challenge_Two;

class Program {
    static void Main() {
        var column1 = new List<int>();
        var column2 = new List<int>();
        
        BuildLists(column1, column2);
        
        var similarities = new Dictionary<int, int>();
        int total = CalculateSimilarity(column1, column2, similarities);
        
        Console.WriteLine(total);
    }

    private static void BuildLists(List<int> column1, List<int> column2) {
        var input = File.ReadAllText("Inputs/inputs.txt");
        var lines = input.Split('\n');

        foreach (var line in lines) {
            var values = line.Split("   ");
            column1.Add(int.Parse(values[0]));
            column2.Add(int.Parse(values[1]));
        }
    }

    private static int CalculateSimilarity(List<int> column1, List<int> column2, Dictionary<int, int> similarities) {
        int total = 0;
        
        foreach (var num in column1) {
            if (!similarities.TryGetValue(num, out var similarity)) {
                similarity = num * column2.Count(x => x == num);
                similarities[num] = similarity;
            }
            total += similarity;
        }

        return total;
    }
}