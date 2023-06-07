using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fprog.Algorithms.Common.Structures
{
    public class Graph<T>
        where T : IEquatable<T>
    {
        public HashSet<T> Vertices { get; } = new();
        List<Edge<T>> Edges { get; } = new();
        public Dictionary<T, HashSet<Edge<T>>> OutNeighbours { get; } = new();

        public Graph() { }

        public Graph(IEnumerable<T> vertices)
        {
            if (vertices is null)
                throw new ArgumentNullException(nameof(vertices), "Vertices are null.");

            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        public Graph(IEnumerable<T> vertices, IEnumerable<Edge<T>> edges)
            : this(vertices)
        {
            AddEdges(edges);
        }

        public Edge<T>[] AllEdges() => Edges.ToArray();

        public void AddVertex(T vertex)
        {
            this.Vertices.Add(vertex);
            OutNeighbours[vertex] = new();
        }

        public void AddEdges(IEnumerable<Edge<T>> edges)
        {
            if (edges is null)
                throw new ArgumentNullException(nameof(edges), "Edges are null.");
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddUndirectedEdge(Edge<T> edge)
        {
            if (edge is null)
                throw new ArgumentNullException(nameof(edge), "Edge is null.");
            AddEdge(edge);
            AddEdge(new Edge<T>(edge.To, edge.From, edge.Weight));
        }

        /// <summary>
        /// Adds directed edge.
        /// </summary>
        /// <param name="edge"></param>
        public void AddEdge(Edge<T> edge)
        {
            if (edge is null || edge.From is null || edge.To is null)
                return;

            this.Vertices.Add(edge.From);
            this.Vertices.Add(edge.To);

            this.Edges.Add(edge);

            // Checking if HashSet has not been initialized yet.
            if (!this.OutNeighbours.ContainsKey(edge.From))
                this.OutNeighbours[edge.From] = new();

            this.OutNeighbours[edge.From].Add(edge);
        }

        public Dictionary<T, DijkstraNode<T>> DijkstrasAlgorithm(T initial)
        {
            // Marks all nodes as unvisited, and tentative distances as infinity.
            Dictionary<T, DijkstraNode<T>> nodes = Vertices
                .Select(vertex => new DijkstraNode<T>(vertex))
                .ToDictionary(node => node.Vertex);
            nodes[initial].ChangeBestPath(new());

            DijkstrasAlgorithmRecursive(nodes, new List<T> { initial });
            return nodes;
        }

        private void DijkstrasAlgorithmRecursive(
            Dictionary<T, DijkstraNode<T>> nodes,
            List<T> verticesToVisit
        )
        {
            var nextToVisit = new List<T>();
            foreach (var vertex in verticesToVisit)
                VisitVertex(nodes, nextToVisit, vertex);

            if (nextToVisit.Any())
                DijkstrasAlgorithmRecursive(nodes, nextToVisit);
        }

        private void VisitVertex(
            Dictionary<T, DijkstraNode<T>> nodes,
            List<T> nextToVisit,
            T vertex
        )
        {
            var node = nodes[vertex];
            if (node.Visited)
                return;

            var allNeighbours = this.OutNeighbours[vertex]
                .OrderBy(neighbour => neighbour.Weight)
                .ToArray();

            foreach (var neighbour in allNeighbours)
                AddVisitNeighbours(nodes, nextToVisit, node, neighbour);

            node.Visited = true;
        }

        private static void AddVisitNeighbours(
            Dictionary<T, DijkstraNode<T>> nodes,
            List<T> nextToVisit,
            DijkstraNode<T> node,
            Edge<T> neighbour
        )
        {
            if (node.BestKnownPath is null)
                return;
            nextToVisit.Add(neighbour.To);
            var currentBestPath = node.BestKnownPath;
            var neighbourBestPath = nodes[neighbour.To].BestKnownPath;

            CalculateBestKnownPath(
                nodes[neighbour.To],
                neighbourBestPath,
                currentBestPath,
                neighbour
            );
        }

        private static void CalculateBestKnownPath(
            DijkstraNode<T> neighbourNode,
            List<Edge<T>>? neighbourBestPath,
            List<Edge<T>> currentBestPath,
            Edge<T> neighbour
        )
        {
            if (neighbourBestPath is null)
            {
                neighbourNode.ChangeBestPath(currentBestPath.Append(neighbour).ToList());
                return;
            }

            var oldDistance = neighbourBestPath.Sum(edge => edge.Weight);
            var currentBestDistance = currentBestPath.Sum(edge => edge.Weight);
            var newBestDistance = currentBestDistance + neighbour.Weight;

            if (oldDistance > newBestDistance)
                neighbourNode.ChangeBestPath(currentBestPath.Append(neighbour).ToList());
        }
    }
}
