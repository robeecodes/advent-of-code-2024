using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace _1_Historian_Hysteria;

class Program {
    static void Main() {
        List<int> column1 = new List<int>();
        List<int> column2 = new List<int>();
        BuildLists(column1, column2);

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
        if (column1.Count < 1) return 0;
        
        int n = column1.Min();
        int m = column2.Min();
        
        column1.Remove(n);
        column2.Remove(m);
        
        return Math.Abs(n - m) + FindDistance(column1, column2);
    }
}

