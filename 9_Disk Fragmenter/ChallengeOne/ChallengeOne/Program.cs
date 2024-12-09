namespace ChallengeOne {
    class Program {
        public static void Main() {
            var nums = BuildList();

            SortList(nums);
            
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

        private static void SortList(List<long> nums) {
            for (int i = nums.Count - 1; i >= 0; i--) {
                if (nums[i] >= 0) {
                    int freeSpace = nums.IndexOf(-1);
                    if (freeSpace >= 0 && freeSpace < i) {
                        nums[freeSpace] = nums[i];
                        nums[i] = -1;
                    }
                    else break;
                }
            }
        }

        private static long CheckSum(List<long> nums) {
            long checksum = 0;

            for (int i = 0; i < nums.Count; i++) {
                if (nums[i] != -1) checksum += nums[i] * i;
            }

            return checksum;
        }
    }
}