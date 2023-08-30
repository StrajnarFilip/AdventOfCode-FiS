namespace Day18
{
    internal class Program
    {
        static int Part1(Cube[] dropletCubes)
        {
            return dropletCubes.Sum(cube => cube.UncoveredSurface(dropletCubes));
        }

        static void RecursiveWaterFill(
            Cube[] lavaCubes,
            List<Cube> visited,
            Cube currentDroplet,
            ref int sidesVisible
        )
        {
            sidesVisible += currentDroplet.CoveredSides(lavaCubes);
            visited.Add(currentDroplet);
            Console.WriteLine(visited.Count);
            var visitedCubes = visited;
            foreach (
                var neighbour in currentDroplet
                    .WaterFillNeighbours(lavaCubes)
                    .Where(neighbour => !visitedCubes.Any(cube => cube.Equals(neighbour)))
            )
            {
                visitedCubes.Add(neighbour);
                RecursiveWaterFill(
                    lavaCubes,
                    visited,
                    neighbour,
                    ref sidesVisible
                );
            }
        }

        static int Part2(Cube[] dropletCubes)
        {
            int minX = dropletCubes.Min(droplet => droplet.X) - 1;
            int maxX = dropletCubes.Max(droplet => droplet.X) + 1;
            int minY = dropletCubes.Min(droplet => droplet.Y) - 1;
            int maxY = dropletCubes.Max(droplet => droplet.Y) + 1;
            int minZ = dropletCubes.Min(droplet => droplet.Z) - 1;
            int maxZ = dropletCubes.Max(droplet => droplet.Z) + 1;

            Console.WriteLine($"X range: {maxX - minX}, Y range: {maxY - minY}, Z range: {maxZ - minZ}");

            List<Cube> allPossibleDroplets = new List<Cube>();

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    for (int z = minZ; z <= maxZ; z++)
                        allPossibleDroplets.Add(new Cube(x, y, z));

            Console.WriteLine(allPossibleDroplets.Count);

            Cube waterSource = allPossibleDroplets.First();
            int sidesVisible = 0;

            RecursiveWaterFill(
                dropletCubes,
                new List<Cube>(),
                waterSource,
                ref sidesVisible
            );

            return sidesVisible;
        }

        static void Runner()
        {
            Cube[] dropletCubes = File.ReadAllLines("Assets/test.txt")
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
