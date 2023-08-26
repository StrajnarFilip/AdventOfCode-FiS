using System.Text.RegularExpressions;
using Fprog.Algorithms.Common;
using Fprog.Algorithms.Common.Structures;

namespace Day16
{
    internal class Program
    {
        private const int startingMinutes = 30;

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

        private static int ValveOpenings(Graph<Valve> graph)
        {
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths =
                graph.QuickestPaths();

            var startingValve = graph.Vertices.Single(valve => valve.Name == "AA");
            var valvesToVisit = graph.Vertices.Where(
                vertex => vertex.FlowRate > 0 && vertex != startingValve
            );
            var released = startingValve.FlowRate * (startingMinutes - 1);

            int outcomesLeftClosed = valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            graph,
                            new List<Valve>() { },
                            valve,
                            quickestPaths,
                            0,
                            startingMinutes - quickestPaths[(startingValve, valve)].Count
                        )
                )
                .Max();

            int outcomesOpened = valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            graph,
                            new List<Valve>() { startingValve },
                            valve,
                            quickestPaths,
                            released,
                            startingMinutes - quickestPaths[(startingValve, valve)].Count - 1
                        )
                )
                .Max();

            return Math.Max(outcomesLeftClosed, outcomesOpened);
        }

        private static int ValveOpeningRecursive(
            Graph<Valve> graph,
            List<Valve> visited,
            Valve current,
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths,
            int previousStepsReleased,
            int timeLeft
        )
        {
            // If there is either no time left, all valves with any flow are visited
            // or the current node was already visited, simply return the result.
            if (
                timeLeft < 1
                || graph.Vertices.Where(vertex => vertex.FlowRate > 0).All(visited.Contains)
                || visited.Contains(current)
            )
                return previousStepsReleased;

            List<Valve> newVisited = visited.Append(current).ToList();

            // Valves we have yet to visit must have positive flow rate.
            var valvesToVisit = graph.Vertices.Where(vertex => vertex.FlowRate > 0);

            // Pressure released, if we decide to open.
            // It takes one minute until it's open, hence - 1.
            var openingRelased = current.FlowRate * (timeLeft - 1);
            // Decide to open the valve
            int[] outcomesIfOpened = valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            graph,
                            newVisited,
                            valve,
                            quickestPaths,
                            previousStepsReleased + openingRelased,
                            timeLeft - quickestPaths[(current, valve)].Count - 1
                        )
                )
                .ToArray();

            int bestOutcomeIfOpened = outcomesIfOpened.Any()
                ? outcomesIfOpened.Max()
                : previousStepsReleased + openingRelased;

            return bestOutcomeIfOpened;
        }

        private static int Part1(Graph<Valve> graph)
        {
            return ValveOpenings(graph);
        }

        private static int Part2(Graph<Valve> graph)
        {
            return 0;
        }

        private static void Runner()
        {
            try
            {
                var lines = File.ReadAllLines("Assets/data.txt");
                var lineDetails = lines.Select(ExtractData).ToArray();
                Graph<Valve> graph = new Graph<Valve>(
                    lineDetails.Select(valve => new Valve(valve.Rate, valve.Valve))
                );
                foreach (var valve in lineDetails)
                {
                    Valve currentValve = graph.Vertices.Single(el => el.Name == valve.Valve);
                    foreach (var outNeighbour in valve.OutNeighbours)
                    {
                        Valve currentNeighbour = graph.Vertices.Single(
                            el => el.Name == outNeighbour
                        );
                        graph.AddEdge(new Edge<Valve>(currentValve, currentNeighbour, 1));
                    }
                }

                Console.WriteLine($"Part 1: {Part1(graph)}, Part 2: {Part2(graph)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(Runner, 400_000_000);
            thread.Start();

            //Runner();
        }
    }
}
