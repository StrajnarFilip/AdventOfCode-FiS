using System.Text.RegularExpressions;
using Fprog.Algorithms.Common;
using Fprog.Algorithms.Common.Structures;

namespace Day16
{
    internal class Program
    {
        private static (string Valve, int Rate, string[] OutNeighbours) ExtractData(string line)
        {
            // Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
            Regex rx = new Regex(
                @"Valve ([A-Z]+) has flow rate=(0-9)+; tunnels lead to valves (.+)"
            );
            // string[] matched = rx.Matches(line);
            // Console.WriteLine(matched.FormattedString());

            return ("", 1, new string[] { });
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("Assets/data.txt");
            var lineDetails = lines.Select(ExtractData).ToArray();
            Graph<Valve> graph = new Graph<Valve>();
        }
    }
}
