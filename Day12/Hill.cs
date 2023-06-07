using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Hill : IEquatable<Hill>, IComparer<Hill>
    {
        public char Height { get; }
        public int Id { get; }

        public Hill(char height, int id)
        {
            Height = height;
            Id = id;
        }

        public bool Equals(Hill? other)
        {
            if (other is null)
                return false;
            return other.Id == Id;
        }

        public int Compare(Hill? x, Hill? y)
        {
            if (x is null)
                throw new ArgumentNullException(nameof(x));
            if (y is null)
                throw new ArgumentNullException(nameof(x));

            return x.Id.CompareTo(y.Id);
        }

        public override int GetHashCode()
        {
            return $"{this.Height} {this.Id}".GetHashCode();
        }

        public override string ToString()
        {
            return $"{Height} ({Id})";
        }
    }
}
