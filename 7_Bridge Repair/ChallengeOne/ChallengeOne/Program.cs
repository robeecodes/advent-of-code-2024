namespace ChallengeOne;

class Program {
    public static void Main() {
        var input = ReadInput();

        UInt64 sum = 0;
        
        foreach (var group in input) {
            sum = group.Where(values => CheckTarget(group.Key, values)).Aggregate(sum, (current, values) => current + group.Key);
        }

        Console.WriteLine(sum);
    }

    private static ILookup<UInt64, List<UInt64>> ReadInput()
    {
        string inputString = File.ReadAllText("Inputs/inputs.txt");

        var lookup = inputString
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var values = line.Split(": ");
                UInt64 target = UInt64.Parse(values[0]);
                var nums = values[1].Split(' ').Select(UInt64.Parse).ToList();
                return (Key: target, Value: nums);
            })
            .ToLookup(pair => pair.Key, pair => pair.Value);

        return lookup;
    }

    private static bool CheckTarget(UInt64 target, List<UInt64> nums)
    {
        return CheckTargetRec(target, nums, nums[0], 1);
    }

    private static bool CheckTargetRec(UInt64 target, List<UInt64> nums, UInt64 currentResult, int index)
    {
        if (index == nums.Count) return currentResult == target;

        UInt64 nextNum = nums[index];

        if (CheckTargetRec(target, nums, currentResult + nextNum, index + 1)) return true;
        if (CheckTargetRec(target, nums, currentResult * nextNum, index + 1)) return true;

        return false;
    }
}