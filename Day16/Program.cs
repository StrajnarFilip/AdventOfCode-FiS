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

        private static int ValveOpenings(Graph<Valve> graph)
        {
            return ValveOpeningRecursive(graph, new(), graph.Vertices.First(), 0, 20);
        }

        private static int ValveOpeningRecursive(
            Graph<Valve> graph,
            List<Valve> visited,
            Valve current,
            int previousStepsReleased,
            int timeLeft
        )
        {
            // If all vertices were already visited, or time is up, end search.
            // TODO: might have to be removed.
            if (timeLeft < 1 || graph.Vertices.All(visited.Contains))
                return previousStepsReleased;

            List<Valve> newVisited = visited.Append(current).ToList();
            // Pressure released, if we decide to open.
            // It takes one minute until it's open, hence - 1.
            int openingRelased = current.FlowRate * (timeLeft - 1);
            var neighboursToVisit = graph.OutNeighbours[current];

            int[] outcomesIfLeftClosed = neighboursToVisit
                .Select(
                    neighbour =>
                        ValveOpeningRecursive(
                            graph,
                            newVisited,
                            neighbour.To,
                            previousStepsReleased,
                            timeLeft - 1
                        )
                )
                .ToArray();
            int bestOutcomeIfClosed = outcomesIfLeftClosed.Any()
                ? outcomesIfLeftClosed.Max()
                : previousStepsReleased;

            if (timeLeft >= 2 && current.FlowRate > 0)
            {
                // Decide to open the valve
                int[] outcomesIfOpened = neighboursToVisit
                    .Select(
                        neighbour =>
                            ValveOpeningRecursive(
                                graph,
                                newVisited,
                                neighbour.To,
                                previousStepsReleased + openingRelased,
                                timeLeft - 2
                            )
                    )
                    .ToArray();

                int bestOutcomeIfOpened = outcomesIfOpened.Any()
                    ? outcomesIfOpened.Max()
                    : previousStepsReleased + openingRelased;
                return Math.Max(bestOutcomeIfOpened, bestOutcomeIfClosed);
            }
            else
                return bestOutcomeIfClosed;
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
        }
    }
}
