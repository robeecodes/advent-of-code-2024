namespace ChallengeTwo {
    class Program {
        public static void Main() {
            var input = GetInput();

            decimal aPresses = 0;
            decimal bPresses = 0;
            
            WinPrizes(ref input, ref aPresses, ref bPresses);
            
            Console.WriteLine(aPresses + bPresses);
            
        }

        static (long x0, long y0, long x1, long y1, long xf, long yf)[] GetInput()
        {
            var lines = File.ReadAllLines("Inputs/inputs.txt");
            var list = new List<(long x0, long y0, long x1, long y1, long xf, long yf)>();

            for (var i = 0; i < lines.Length; i++)
            {
                var (x0, y0) = ParseLine("Button A: ");
                var (x1, y1) = ParseLine("Button B: ");
                var (xf, yf) = ParseLine("Prize: ");
                list.Add((x0, y0, x1, y1, xf, yf));

                (long x, long y) ParseLine(string prefix)
                {
                    var parts = lines[i++][prefix.Length..].Split(", ");
                    return (long.Parse(parts[0]["X_".Length..]), long.Parse(parts[1]["Y_".Length..]));
                }
            }

            return list.ToArray();
        }

        private static void WinPrizes(ref (long x0, long y0, long x1, long y1, long xf, long yf)[] input, ref decimal aPresses, ref decimal bPresses) {
            foreach (var machine in input) {
                var (x0, y0, x1, y1, xf, yf) = machine;
                
                xf += 10000000000000;
                yf += 10000000000000;
                
                decimal denom = x0 * y1 - y0 * x1;
                if (denom == 0) continue;
                
                decimal a = (y1 * xf - x1 * yf) / denom;
                decimal b = (x0 * yf - y0 * xf) / denom;
                
                if (a % 1 != 0) continue;
                if (b % 1 != 0) continue;

                aPresses += 3 * a;
                bPresses += b;
            }
        }
    }
}