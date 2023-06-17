using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class VisitOpen
    {
        public Valve Visited { get; }
        public bool Opened { get; }

        public VisitOpen(Valve valveVisited, bool opened)
        {
            this.Visited = valveVisited;
            this.Opened = opened;
        }
    }
}
