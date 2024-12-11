namespace ChallengeOne {
    class Program {
        public static void Main(string[] args) {
            var input = ReadInput();

            for (int i = 0; i < 25; i++) {
                Console.WriteLine(i);
                Blink(input);
            }
            
            Console.WriteLine(input.Count);
        }

        private static List<long> ReadInput() {
            string[] text = File.ReadAllText("Inputs/inputs.txt").Split(' ');

            return text.Select(item => long.Parse(item)).ToList();
        }

        private static void Blink(List<long> input) {
            int i = 0;
            while (i < input.Count) {
                if (input[i] == 0) input[i] = 1;
                else if (input[i].ToString().Length % 2 == 0) {
                    string tmp = input[i].ToString();
                    string tmpL = tmp.Substring(0, tmp.Length / 2);
                    string tmpR = tmp.Substring(tmp.Length / 2, tmp.Length / 2);
                    input[i] = long.Parse(tmpL);
                    input.Insert(i + 1, long.Parse(tmpR));
                    i++;
                }
                else input[i] *= 2024;

                i++;
            }
            
            // foreach (long num in input) {
            //     Console.Write(num + ", ");
            // }
            // Console.WriteLine();
        }
    }
}