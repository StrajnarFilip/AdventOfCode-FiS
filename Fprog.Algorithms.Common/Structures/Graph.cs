using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Graph<T> where T : IEquatable<T>
    {
        public HashSet<T> Vertices { get; } = new();
        List<Edge<T>> Edges { get; } = new();
        public Dictionary<T, HashSet<Edge<T>>> OutNeighbours { get; } = new();

        public Graph()
        {
        }

        public Graph(IEnumerable<T> vertices)
        {
            if (vertices is null)
                throw new ArgumentNullException(nameof(vertices), "Vertices are null.");

            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        public Graph(IEnumerable<T> vertices, IEnumerable<Edge<T>> edges) : this(vertices)
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
            nodes[initial].BestKnownPath = new();

            return DijkstrasAlgorithmRecursive(nodes, new List<Edge<T>>(), nodes[initial]);
        }

        private Dictionary<T, DijkstraNode<T>> DijkstrasAlgorithmRecursive(
            Dictionary<T, DijkstraNode<T>> nodes,
            List<Edge<T>> currentPath,
            DijkstraNode<T> currentNode
            )
        {
            if (currentNode.Visited)
            {
                return nodes;
            }

            var allNeighbours = this.OutNeighbours[currentNode.Vertex]
                .OrderBy(neighbour => neighbour.Weight)
                .ToArray();
            var currentDistance = currentPath.Sum(edge => edge.Weight);

            foreach (var neighbour in allNeighbours)
            {
                var newDistance = currentDistance + neighbour.Weight;
                var neighbourBestPath = nodes[neighbour.To].BestKnownPath;
                if (neighbourBestPath is not null)
                {
                    var oldDistance = neighbourBestPath.Sum(edge => edge.Weight);
                    if (oldDistance > newDistance)
                    {
                        nodes[neighbour.To].BestKnownPath = currentPath.Append(neighbour).ToList();
                    }
                }
                else
                {
                    nodes[neighbour.To].BestKnownPath = currentPath.Append(neighbour).ToList();
                }
            }

            currentNode.Visited = true;

            foreach (var neighbour in allNeighbours)
            {
                DijkstrasAlgorithmRecursive(
                    nodes,
                    currentPath.Append(neighbour).ToList(),
                    nodes[neighbour.To]
                    );
            }

            return nodes;
        }
    }
}
