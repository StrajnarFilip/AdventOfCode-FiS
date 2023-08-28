﻿using System.Text.RegularExpressions;
using Fprog.Algorithms.Common;
using Fprog.Algorithms.Common.Structures;

namespace Day16
{
    internal class Program
    {
        private const int startingMinutes = 30;
        private const int startingMinutesWithElephant = 26;

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

        private static int BestOutcomeClose(
            Graph<Valve> graph,
            TraversingEntity me,
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths,
            int previousStepsReleased
        )
        {
            IEnumerable<Valve> valvesToVisit = graph.Vertices.Where(
                vertex => vertex.FlowRate > 0 && !vertex.Equals(me.CurrentValve)
            );

            return valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            me.MakeMoves(quickestPaths[(me.CurrentValve, valve)]),
                            graph,
                            new List<Valve>(),
                            quickestPaths,
                            previousStepsReleased
                        )
                )
                .Max();
        }

        private static int BestOutcomeOpen(
            Graph<Valve> graph,
            TraversingEntity me,
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths,
            int previousStepsReleased
        )
        {
            IEnumerable<Valve> valvesToVisit = graph.Vertices.Where(
                vertex => vertex.FlowRate > 0 && !vertex.Equals(me.CurrentValve)
            );
            var released = me.CurrentValve.FlowRate * (startingMinutes - 1);

            return valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            me.OpenAndMakeMoves(quickestPaths[(me.CurrentValve, valve)]),
                            graph,
                            new List<Valve>() { me.CurrentValve },
                            quickestPaths,
                            previousStepsReleased + released
                        )
                )
                .Max();
        }

        private static int ValveOpenings(Graph<Valve> graph)
        {
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths =
                graph.QuickestPaths();

            var startingValve = graph.Vertices.Single(valve => valve.Name == "AA");
            var me = new TraversingEntity("Me", startingMinutes, startingValve);

            int outcomesLeftClosed = BestOutcomeClose(graph, me, quickestPaths, 0);
            int outcomesOpened = BestOutcomeOpen(graph, me, quickestPaths, 0);

            return Math.Max(outcomesLeftClosed, outcomesOpened);
        }

        private static int ValveOpeningRecursive(
            TraversingEntity me,
            Graph<Valve> graph,
            List<Valve> visited,
            Dictionary<(Valve starting, Valve ending), List<Edge<Valve>>> quickestPaths,
            int previousStepsReleased
        )
        {
            // If there is either no time left, all valves with any flow are visited
            // or the current node was already visited, simply return the result.
            if (
                me.OutOfTime()
                || graph.Vertices.Where(vertex => vertex.FlowRate > 0).All(visited.Contains)
                || visited.Contains(me.CurrentValve)
            )
                return previousStepsReleased;

            List<Valve> newVisited = visited.Append(me.CurrentValve).ToList();

            // Valves we have yet to visit must have positive flow rate.
            var valvesToVisit = graph.Vertices.Where(
                vertex => vertex.FlowRate > 0 && !vertex.Equals(me.CurrentValve)
            );

            // Pressure released, if we decide to open.
            // It takes one minute until it's open, hence - 1.
            var openingRelased = me.CurrentValve.FlowRate * (me.TimeLeft - 1);
            // Open the valve
            int[] outcomesIfOpened = valvesToVisit
                .Select(
                    valve =>
                        ValveOpeningRecursive(
                            me.OpenAndMakeMoves(quickestPaths[(me.CurrentValve, valve)]),
                            graph,
                            newVisited,
                            quickestPaths,
                            previousStepsReleased + openingRelased
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

            // Runner();
        }
    }
}
