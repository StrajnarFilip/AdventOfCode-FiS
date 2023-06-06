namespace Day12;

using Common.Parsing;
using Fprog.Algorithms.Common.Structures;

public static class Program
{
    public static void Main()
    {
        Graph<int> graph = new(new[] { 1, 2, 3, 4, 5 });
        graph.AddEdge(new Edge<int>(1, 3, 10));
        graph.AddEdge(new Edge<int>(3, 4, 10));
        graph.AddEdge(new Edge<int>(1, 2, 11));
        graph.AddEdge(new Edge<int>(2, 4, 5));

        var result = graph.DijkstrasAlgorithm(1)[4];
        Console.WriteLine( result.BestKnownPath.Sum(edge => edge.Weight));
    }
}