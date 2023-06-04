using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Graph<V> where V : IEquatable<V>
    {
        public HashSet<V> Vertices { get; } = new();
        public List<Edge<V>> Edges { get; } = new();
        public Dictionary<V, HashSet<V>> OutNeighbours { get; } = new();

        public Graph()
        {
        }

        public Graph(IEnumerable<V> vertices)
        {
            this.Vertices = vertices.ToHashSet();
            foreach (var vertex in vertices)
            {
                OutNeighbours[vertex] = new();
            }
        }

        public void AddVertex(V vertex)
        {
            this.Vertices.Add(vertex);
            OutNeighbours[vertex] = new();
        }

        public Graph(IEnumerable<V> vertices, IEnumerable<Edge<V>> edges) : this(vertices)
        {
            AddEdges(edges);
        }

        public void AddEdges(IEnumerable<Edge<V>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddUndirectedEdge(Edge<V> edge)
        {
            AddEdge(edge);
            AddEdge(new Edge<V>(edge.To, edge.From, edge.Weight));
        }

        /// <summary>
        /// Adds directed edge.
        /// </summary>
        /// <param name="edge"></param>
        public void AddEdge(Edge<V> edge)
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
