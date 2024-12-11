namespace Challenge_One;

class Program {
    static void Main() {
        List<int> column1 = new List<int>();
        List<int> column2 = new List<int>();
        
        BuildLists(column1, column2);
        
        column1.Sort();
        column2.Sort();
        
        int total = FindDistance(column1, column2);
        
        Console.WriteLine(total);
    }

    private static void BuildLists(List<int> column1, List<int> column2) {
        string input = File.ReadAllText("Inputs/inputs.txt");
        
        var lines = input.Trim().Split('\n');

        foreach (var num in lines) {
            var parts = num.Split("   ");
            column1.Add(int.Parse(parts[0]));
            column2.Add(int.Parse(parts[1]));
        }
    }

    private static int FindDistance(List<int> column1, List<int> column2) {
        if (column1.Count == 0) return 0;
        
        int minColumn1 = column1.Last();
        int minColumn2 = column2.Last();
        
        column1.RemoveAt(column1.Count - 1);
        column2.RemoveAt(column2.Count - 1);
        
        return Math.Abs(minColumn1 - minColumn2) + FindDistance(column1, column2);
    }
}

