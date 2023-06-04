using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Vertex<T> : IEquatable<Vertex<T>> where T : IEquatable<T>
    {
        public T Value { get; }

        public Vertex(T value)
        {
            this.Value = value;
        }

        public bool Equals(Vertex<T>? other)
        {
            return this.Value.Equals(other.Value);
        }
    }
}
