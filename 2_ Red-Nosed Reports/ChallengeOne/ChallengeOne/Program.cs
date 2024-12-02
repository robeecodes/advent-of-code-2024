namespace ChallengeOne;

class Program { 
    public static void Main() {
        var levels = new List<List<int>>();
        BuildLevels(levels);

        int safe = FindSafeLevels(levels);
        
        Console.WriteLine($"Levels safe: {safe}");
    }

    private static void BuildLevels(List<List<int>> levels) {
        var input = File.ReadAllText("Inputs/inputs.txt");
        var lines = input.Split('\n');

        foreach (var line in lines) {
            var nums = line.Split(' ');
            levels.Add(nums.Select(int.Parse).ToList());
        }
    }

    private static int FindSafeLevels(List<List<int>> levels) {
        int safeLevels = 0;
        
        foreach (var line in levels) {
            int dir = line[1] - line.First();
            
            if (Math.Abs(dir) > 3 || dir == 0) continue;
            
            // Set dir to 1 or -1
            dir /= Math.Abs(dir);
            
            int prev = line[1];
            
            bool isSafe = true;
            
            for (int i = 2; i < line.Count; i++) {
                int diff = line[i] - prev;
                
                if (diff == 0) isSafe = false;
                if ((diff > 0 && dir < 0) || (diff < 0 && dir > 0)) isSafe = false;
                if (Math.Abs(diff) > 3) isSafe = false;
                
                if (!isSafe) break;
                
                prev = line[i];
            }
            
            if (isSafe) safeLevels++;
        }

        return safeLevels;
    }
}