using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Edge<T>
        where T : IEquatable<T>
    {
        public T From { get; }
        public T To { get; }
        public decimal Weight { get; }

        public Edge(T from, T to, decimal weight)
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
