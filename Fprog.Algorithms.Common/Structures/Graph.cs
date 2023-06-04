using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Graph<T> where T : IEquatable<T>
    {
        public HashSet<Vertex<T>> Vertices { get; } = new();
        public List<Edge<T>> Edges { get; } = new();
        public Dictionary<Vertex<T>, HashSet<Vertex<T>>> Neighbours { get; } = new();

        public Graph()
        {
        }

        public Graph(IEnumerable<T> values)
        {
            this.Vertices = values.Select(value => new Vertex<T>(value)).ToHashSet();
        }

        public Graph(IEnumerable<Vertex<T>> vertices)
        {
            this.Vertices = vertices.ToHashSet();
        }

        public Graph(IEnumerable<T> values, IEnumerable<Edge<T>> edges) : this(values)
        {
            
        }

        public void AddEdges(IEnumerable<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddEdge(Edge<T> edge)
        {
            if (edge is null)
                return;
            this.Edges.Add(edge);

            // Checking if HashSets have not been initialized yet.
            if (!this.Neighbours.ContainsKey(edge.From))
                this.Neighbours[edge.From] = new();
            if (!this.Neighbours.ContainsKey(edge.To))
                this.Neighbours[edge.To] = new();

            this.Neighbours[edge.From].Add(edge.To);
            this.Neighbours[edge.To].Add(edge.From);
        }
    }
}
