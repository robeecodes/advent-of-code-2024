namespace ChallengeTwo;

class Program { 
    public static void Main() {
        var levels = new List<List<int>>();
        BuildLevels(levels);

        int safe = FindSafeLevels(levels);
        
        Console.WriteLine($"Levels safe: {safe}");
    }

    private static void BuildLevels(List<List<int>> levels) {
        var input = File.ReadAllText("Inputs/inputs.txt");
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines) {
            var nums = line.Trim().Split(' ');
            levels.Add(nums.Select(int.Parse).ToList());
        }
    }

    private static int FindSafeLevels(List<List<int>> levels) {
        int safeLevels = 0;

        foreach (var level in levels) {
            // Check the full level and all possible "skip" variations
            for (int skip = -1; skip < level.Count; skip++) {
                if (IsSafeLevel(level, skip)) {
                    safeLevels++;
                    break;
                }
            }
        }

        return safeLevels;
    }

    private static bool IsSafeLevel(List<int> level, int skip) {
        int? dir = null;
        int? prev = null;

        for (int i = 0; i < level.Count; i++) {
            if (i == skip) continue;

            if (prev.HasValue) {
                int diff = level[i] - prev.Value;

                if (Math.Abs(diff) > 3 || diff == 0) return false;

                // Determine direction on the first valid difference
                if (!dir.HasValue) {
                    dir = Math.Sign(diff);
                } else if (Math.Sign(diff) != dir.Value) {
                    // Invalid direction change
                    return false;
                }
            }

            prev = level[i];
        }

        return true;
    }
}
