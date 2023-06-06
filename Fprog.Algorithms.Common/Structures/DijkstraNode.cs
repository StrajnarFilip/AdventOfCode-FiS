using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public sealed class DijkstraNode<T> where T : IEquatable<T>
    {
        private List<Edge<T>>? _bestKnownPath;
        public bool Visited { get; set; }
        public T Vertex { get; }

        public DijkstraNode(T vertex)
        {
            this.Vertex = vertex;
            this.Visited = false;
            this._bestKnownPath = null;
        }

        public List<Edge<T>>? BestKnownPath => _bestKnownPath;

        public void ChangeBestPath(List<Edge<T>> newBestPath)
        {
            this._bestKnownPath = newBestPath;
        }

        public override string ToString()
        {
            return $"Vertex: {this.Vertex}, best path so far: {BestKnownPath?.FormattedString()}. Visited: {Visited}";
        }
    }
}
