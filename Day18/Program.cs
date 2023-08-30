namespace Day18
{
    internal class Program
    {
        static int Part1()
        {
            DropletCube[] cubes = File.ReadAllLines("Assets/data.txt")
                .Select(line => new DropletCube(line))
                .ToArray();

            return cubes.Sum(cube => cube.UncoveredSurface(cubes));
        }

        static int Part2()
        {
            return 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Part 1: {Part1()}, Part 2: {Part2()}");
        }
    }
}
