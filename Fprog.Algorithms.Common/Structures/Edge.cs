using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Edge<V> where V : IEquatable<V>
    {
        public V From { get; }
        public V To { get; }
        public decimal Weight { get; }

        public Edge(V from, V to, decimal weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public override string ToString()
        {
            return $"[ {this.From} , {this.To} ]";
        }
    }
}
