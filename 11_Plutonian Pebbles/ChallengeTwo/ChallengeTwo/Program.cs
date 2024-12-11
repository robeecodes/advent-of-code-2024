namespace ChallengeTwo {
    class Program {
        public static void Main(string[] args) {
            var input = ReadInput();

            for (int i = 0; i < 75; i++) {
                Blink(input);
            }
            
            Console.WriteLine(input.Values.Sum());
        }

        private static Dictionary<long, long> ReadInput() {
            string[] text = File.ReadAllText("Inputs/inputs.txt").Split(' ');
            var input = new Dictionary<long, long>();

            foreach (var item in text) {
                if (!input.TryAdd(long.Parse(item), 1)) input[long.Parse(item)]++;
            }

            return input;
        }

        private static void Blink(Dictionary<long, long> input) {
            var updates = new Dictionary<long, long>();

            foreach (var key in input.Keys.ToList()) {
                long count = input[key];
                if (count == 0) continue;

                if (key == 0) {
                    if (!updates.TryAdd(1, count)) updates[1] += count;
                }
                else if (key.ToString().Length % 2 == 0) {
                    string tmp = key.ToString();
                    int mid = tmp.Length / 2;
                    long tmpL = long.Parse(tmp.Substring(0, mid));
                    long tmpR = long.Parse(tmp.Substring(mid));

                    if (!updates.TryAdd(tmpL, count)) updates[tmpL] += count;
                    if (!updates.TryAdd(tmpR, count)) updates[tmpR] += count;
                }
                else {
                    long newKey = key * 2024;
                    if (!updates.TryAdd(newKey, count)) updates[newKey] += count;
                }

                input[key] = 0;
            }

            foreach (var kvp in updates) {
                if (!input.TryAdd(kvp.Key, kvp.Value)) input[kvp.Key] += kvp.Value;
            }
        }
    }
}