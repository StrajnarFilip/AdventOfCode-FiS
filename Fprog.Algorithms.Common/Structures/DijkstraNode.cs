using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class DijkstraNode<T> where T : IEquatable<T>
    {
        public bool Visited { get; set; }
        public T Vertex { get; }
        public List<Edge<T>>? BestKnownPath { get; set; }

        public DijkstraNode(T vertex)
        {
            this.Vertex = vertex;
            this.Visited = false;
            this.BestKnownPath = null;
        }

        public override string ToString()
        {
            return $"Vertex: {this.Vertex}, best path so far: {BestKnownPath.FormattedString()}. Visited: {Visited}";
        }
    }
}
