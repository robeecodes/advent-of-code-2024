namespace ChallengeTwo {
    class Program {
        public static void Main() {
            var nums = BuildList();

            var free = FindFree(nums);

            CompactFiles(nums, free);

            Console.WriteLine(CheckSum(nums));
        }

        private static List<long> BuildList() {
            string input = File.ReadAllText("Inputs/inputs.txt");

            var nums = new List<long>();
            long id = 0;
            bool isFree = false;

            foreach (char c in input) {
                long num = int.Parse(c.ToString());
                while (num > 0) {
                    if (isFree) nums.Add(-1);
                    else nums.Add(id);
                    num--;
                }

                if (isFree) isFree = false;
                else {
                    isFree = true;
                    id++;
                }
            }

            return nums;
        }

        private static void CompactFiles(List<long> nums, SortedDictionary<int, int> free) {
            int n = nums.Count - 1;

            while (n >= 0) {
                long currId = nums[n];

                if (currId == -1) {
                    n--;
                    continue;
                }

                // Identify the file (chunk of the same ID)
                int idCount = 1;
                int idStart = n;
                n--;
                while (n >= 0 && nums[n] == currId) {
                    idCount++;
                    n--;
                }

                // Find the leftmost suitable free space block
                int freeSpaceStart = -1;
                foreach (var kvp in free) {
                    if (kvp.Key > idStart) break;
                    if (kvp.Value >= idCount) {
                        freeSpaceStart = kvp.Key;
                        break;
                    }
                }

                if (freeSpaceStart < 0) continue;

                // Move the file
                for (int i = 0; i < idCount; i++) {
                    nums[freeSpaceStart + i] = nums[idStart - i];
                    nums[idStart - i] = -1;
                }

                // Update free spaces in the SortedDictionary
                int remainingFree = free[freeSpaceStart] - idCount;
                free.Remove(freeSpaceStart);
                if (remainingFree > 0) {
                    free[freeSpaceStart + idCount] = remainingFree;
                }

                // Mark the original space as free
                free[idStart - idCount + 1] = idCount;
            }
        }

        private static SortedDictionary<int, int> FindFree(List<long> nums) {
            var free = new SortedDictionary<int, int>();

            bool isFree = false;
            int currentStart = -1;
            for (int i = 0; i < nums.Count; i++) {
                if (nums[i] == -1) {
                    if (!isFree) {
                        currentStart = i;
                        free[currentStart] = 1;
                        isFree = true;
                    } else free[currentStart]++;
                } else isFree = false;
            }

            return free;
        }

        private static long CheckSum(List<long> nums) {
            long checksum = 0;
            for (int i = 0; i < nums.Count; i++) {
                if (nums[i] != -1)  checksum += nums[i] * i;
            }
            return checksum;
        }
    }
}