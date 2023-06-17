using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class Valve : IEquatable<Valve>
    {
        public int FlowRate { get; }
        public string Name { get; }

        public Valve(int flowRate, string name)
        {
            this.FlowRate = flowRate;
            this.Name = name;
        }

        public bool Equals(Valve? other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name;
        }
    }
}
