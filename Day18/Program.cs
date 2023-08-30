using System.ComponentModel;

namespace Day18
{
    public static class Program
    {
        static int Part1(Cube[] dropletCubes)
        {
            return dropletCubes.Sum(cube => cube.UncoveredSurface(dropletCubes));
        }

        static bool ContainsCube(this List<Cube> setOfCubes, Cube cubeToTest) =>
            setOfCubes.Any(visitedCube => visitedCube.Equals(cubeToTest));

        static void RecursiveWaterFill(
            IEnumerable<Cube> lavaCubes,
            IEnumerable<Cube> allPossibleCubes,
            List<Cube> visited,
            Cube currentCube,
            ref int sidesVisible
        )
        {
            if (
                // Terminate if current cube was already visited.
                visited.ContainsCube(currentCube)
                // Terminate if current cube is not meant to be checked
                // (neighbouring cube is out of bounds).
                || !allPossibleCubes.Any(possibleCube => possibleCube.Equals(currentCube))
                // Terminate if all possible cubes were already visited.
                || allPossibleCubes.All(visited.ContainsCube)
            )
                return;

            sidesVisible += currentCube.CoveredSides(lavaCubes);
            visited.Add(currentCube);

            foreach (
                // Recursively check neighbours that are accessible
                // and haven't been visited already.
                var emptyCube in currentCube
                    .WaterFillNeighbours(lavaCubes)
                    .Where(cube => !visited.ContainsCube(cube))
            )
            {
                RecursiveWaterFill(
                    lavaCubes,
                    allPossibleCubes,
                    visited,
                    emptyCube,
                    ref sidesVisible
                );
            }
        }

        static int Part2(Cube[] lavaCubes)
        {
            // Range that will cover all the cubes.
            int minX = lavaCubes.Min(droplet => droplet.X) - 1;
            int maxX = lavaCubes.Max(droplet => droplet.X) + 1;
            int minY = lavaCubes.Min(droplet => droplet.Y) - 1;
            int maxY = lavaCubes.Max(droplet => droplet.Y) + 1;
            int minZ = lavaCubes.Min(droplet => droplet.Z) - 1;
            int maxZ = lavaCubes.Max(droplet => droplet.Z) + 1;

            Console.WriteLine(
                $"X range: {maxX - minX}, Y range: {maxY - minY}, Z range: {maxZ - minZ}"
            );

            List<Cube> allPossibleCubes = new List<Cube>();

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    for (int z = minZ; z <= maxZ; z++)
                        allPossibleCubes.Add(new Cube(x, y, z));

            Console.WriteLine(allPossibleCubes.Count);

            Cube waterSource = allPossibleCubes.First();
            int sidesVisible = 0;

            RecursiveWaterFill(
                lavaCubes,
                allPossibleCubes,
                new List<Cube>(),
                waterSource,
                ref sidesVisible
            );

            return sidesVisible;
        }

        static void Runner()
        {
            Cube[] dropletCubes = File.ReadAllLines("Assets/data.txt")
                .Select(line => new Cube(line))
                .ToArray();
            Console.WriteLine($"Part 1: {Part1(dropletCubes)}, Part 2: {Part2(dropletCubes)}");
        }

        static void Main(string[] args)
        {
            Thread t1 = new Thread(Runner, 500_000_000);
            t1.Start();
        }
    }
}
