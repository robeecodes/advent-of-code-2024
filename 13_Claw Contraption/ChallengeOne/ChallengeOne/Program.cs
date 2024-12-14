namespace ChallengeOne {
    class Program {
        public static void Main() {
            var input = GetInput();

            int aPresses = 0;
            int bPresses = 0;
            
            WinPrizes(ref input, ref aPresses, ref bPresses);
            
            Console.WriteLine(aPresses + bPresses);
            
        }

        private static List<Dictionary<string, int>> GetInput() {
            var input = new List<Dictionary<string, int>>();
            var text = File.ReadAllText("Inputs/inputs.txt").Split("\n");

            var formula = new Dictionary<string, int>();
            foreach (string line in text) {
                if (string.IsNullOrEmpty(line.Trim())) {
                    input.Add(formula);
                    formula = new Dictionary<string, int>();
                } else if (formula.Count == 0) {
                    string x0 = line.Split("X+")[1];
                    x0 = x0.Split(",")[0];
                    formula.Add("x0", int.Parse(x0));
                    
                    string y0 = line.Split("Y+")[1];
                    formula.Add("y0", int.Parse(y0));
                } else if (formula.Count == 2) {
                    string x1 = line.Split("X+")[1];
                    x1 = x1.Split(",")[0];
                    formula.Add("x1", int.Parse(x1));
                    
                    string y1 = line.Split("Y+")[1];
                    formula.Add("y1", int.Parse(y1));
                }
                else {
                    string xf = line.Split("X=")[1];
                    xf = xf.Split(",")[0];
                    formula.Add("xf", int.Parse(xf));
                    
                    string yf = line.Split("Y=")[1];
                    formula.Add("yf", int.Parse(yf));
                }
            }

            return input;
        }

        private static void WinPrizes(ref List<Dictionary<string, int>> input, ref int aPresses, ref int bPresses) {
            foreach (var f in input) {
                decimal denom = f["x0"] * f["y1"] - f["y0"] * f["x1"];
                if (denom == 0) continue;
                
                decimal a = (f["y1"] * f["xf"] - f["x1"] * f["yf"]) / denom;
                decimal b = (f["x0"] * f["yf"] - f["y0"] * f["xf"]) / denom;
                
                if (a % 1 != 0) continue;
                if (b % 1 != 0) continue;
                
                aPresses += 3 * (int) a;
                bPresses += (int) b;
            }
        }
    }
}