using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public sealed class DropletCube : IEquatable<DropletCube>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public DropletCube(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public DropletCube(string line)
        {
            var coordinates = line.Split(',')
                .Select(coordinate => Convert.ToInt32(coordinate))
                .ToArray();
            this.X = coordinates[0];
            this.Y = coordinates[1];
            this.Z = coordinates[2];
        }

        public List<DropletCube> TheoreticalNeighbours()
        {
            List<(int x, int y, int z)> offsets =
                new() { (-1, 0, 0), (1, 0, 0), (0, 0, -1), (0, 0, 1), (0, -1, 0), (0, 1, 0), };

            return offsets
                .Select(offset => new DropletCube(X + offset.x, Y + offset.y, Z + offset.z))
                .ToList();
        }

        public int CoveredSides(IEnumerable<DropletCube> allDroplets)
        {
            return TheoreticalNeighbours()
                .Count(
                    droplet => allDroplets.Any(dropletFromAll => dropletFromAll.Equals(droplet))
                );
        }

        public int UncoveredSurface(IEnumerable<DropletCube> allDroplets)
        {
            return 6 - CoveredSides(allDroplets);
        }

        public bool Equals(DropletCube? other)
        {
            if (other is not null)
            {
                return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
            }

            return this == null;
        }
    }
}
