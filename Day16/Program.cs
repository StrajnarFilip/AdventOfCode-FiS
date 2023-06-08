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
            string pattern = @"Valve (.+) has flow rate=(.+); tunnels* leads* to valves* (.+)";
            Match match = Regex.Match(line, pattern);

            string valveName = match.Groups[1].Value;
            int rate = int.Parse(match.Groups[2].Value);
            string[] outNeighbours = match.Groups[3].Value.Replace(" ", "").Split(",");

            Console.WriteLine(
                $"name: {valveName} rate: {rate} neighbours: {outNeighbours.FormattedString()}"
            );

            return (valveName, rate, outNeighbours);
        }

        private static int Part1()
        {
            throw new NotImplementedException();
        }

        private static int Part2()
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("Assets/test.txt");
            var lineDetails = lines.Select(ExtractData).ToArray();
            Graph<Valve> graph = new Graph<Valve>(
                lineDetails.Select(valve => new Valve(valve.Rate, valve.Valve))
            );
            foreach (var valve in lineDetails)
            {
                Valve currentValve = graph.Vertices.Single(el => el.Name == valve.Valve);
                foreach (var outNeighbour in valve.OutNeighbours)
                {
                    Valve currentNeighbour = graph.Vertices.Single(el => el.Name == outNeighbour);
                    graph.AddEdge(new Edge<Valve>(currentValve, currentNeighbour, 1));
                }
            }

            Console.WriteLine($"Part 1: {Part1()}, Part 2: {Part2()}");
        }
    }
}
