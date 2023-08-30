using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public sealed class Cube : IEquatable<Cube>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Cube(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Cube(string line)
        {
            var coordinates = line.Split(',')
                .Select(coordinate => Convert.ToInt32(coordinate))
                .ToArray();
            this.X = coordinates[0];
            this.Y = coordinates[1];
            this.Z = coordinates[2];
        }

        public List<Cube> TheoreticalNeighbours()
        {
            List<(int x, int y, int z)> offsets =
                new() { (-1, 0, 0), (1, 0, 0), (0, 0, -1), (0, 0, 1), (0, -1, 0), (0, 1, 0), };

            return offsets
                .Select(offset => new Cube(X + offset.x, Y + offset.y, Z + offset.z))
                .ToList();
        }

        public List<Cube> WaterFillNeighbours(IEnumerable<Cube> lavaCubes)
        {
            return TheoreticalNeighbours().Where(neighbour => !lavaCubes.Any(lavaCube => neighbour.Equals(lavaCube))).ToList();
        }

        public int CoveredSides(IEnumerable<Cube> lavaCubes)
        {
            return TheoreticalNeighbours()
                .Count(
                    droplet => lavaCubes.Any(lavaCube => lavaCube.Equals(droplet))
                );
        }

        public int UncoveredSurface(IEnumerable<Cube> allDroplets)
        {
            return 6 - CoveredSides(allDroplets);
        }

        public bool Equals(Cube? other)
        {
            if (other is not null)
            {
                return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
            }

            return this == null;
        }
    }
}
