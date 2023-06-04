using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Graph<T> where T : IEquatable<T>
    {
        public HashSet<T> Vertices { get; } = new();
        List<Edge<T>> Edges { get; } = new();
        public Dictionary<T, HashSet<T>> OutNeighbours { get; } = new();

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

            this.OutNeighbours[edge.From].Add(edge.To);
        }
    }
}
