using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Hill : IEquatable<Hill>
    {
        public char Height { get; }
        public int Id { get; }
        public bool StartNode { get; }
        public bool EndNode { get; }

        public Hill(char height, int id)
        {
            if (height == 'E')
            {
                Height = 'z';
                EndNode = true;
                Id = id;
                return;
            }

            if (height == 'S')
                StartNode = true;

            Height = height;
            Id = id;
        }

        public bool Equals(Hill? other)
        {
            if (other is null)
                return false;
            return other.Id == Id;
        }

        public override string ToString()
        {
            return $"{Height} ({Id})";
        }
    }
}
