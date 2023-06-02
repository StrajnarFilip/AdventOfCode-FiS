using Fprog.Algorithms.Common.Structures;

Matrix<int> x = new(new int[][]{
    new int[]{ 1,2,3},
    new int[]{ 1,5,3}
});

foreach (var item in x.GetRow(1))
{
    Console.WriteLine(item);
}

Console.WriteLine($"{x.GetElement(0,1)}");