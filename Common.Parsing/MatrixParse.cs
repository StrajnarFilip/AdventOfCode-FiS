namespace Common.Parsing
{
    using Fprog.Algorithms.Common.Structures;

    public class MatrixParse
    {
        public static Matrix<int> ParseSingleDigitMatrix(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var numbers = lines
                .Where(line => line.Length > 0)
                .Select(line => line.Select(symbol => int.Parse("" + symbol)));
            return new Matrix<int>(numbers);
        }
    }
}