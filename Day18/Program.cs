namespace Day18
{
    internal class Program
    {
        static int Part1(DropletCube[] dropletCubes)
        {
            return dropletCubes.Sum(cube => cube.UncoveredSurface(dropletCubes));
        }

        static int Part2()
        {
            return 0;
        }

        static void Main(string[] args)
        {
            DropletCube[] dropletCubes = File.ReadAllLines("Assets/data.txt")
                .Select(line => new DropletCube(line))
                .ToArray();
            Console.WriteLine($"Part 1: {Part1(dropletCubes)}, Part 2: {Part2()}");
        }
    }
}
