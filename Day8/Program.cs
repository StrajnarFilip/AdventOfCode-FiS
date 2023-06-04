namespace Day8;
using Fprog.Algorithms.Common;
using Fprog.Algorithms.Common.Sorting;
using Common.Parsing;
using Fprog.Algorithms.Common.Structures;

public static class Program
{
    public static void Main()
    {
        var matrix = MatrixParse.ParseSingleDigitMatrix("Assets/data.txt");

        // Initialize a boolean matrix with all false values, except on edges.
        var visibility = new bool[matrix.RowCount][];
        for (int i = 0; i < visibility.Length; i++)
        {
            if (i == 0 || i == visibility.Length - 1)
            {
                // All true
                visibility[i] = new bool[matrix.ColumnsCount].Select(el => true).ToArray();
                continue;
            }


            var row = new bool[matrix.ColumnsCount].Select(el => false).ToArray();
            row[0] = true;
            row[visibility.Length - 1] = true;
            visibility[i] = row;
        }

        // Iterate from the top
        for (int columnIndex = 1; columnIndex < matrix.ColumnsCount - 1; columnIndex++)
        {
            int localMax = matrix[0, columnIndex];
            for (int rowIndex = 1; rowIndex < matrix.RowCount - 1; rowIndex++)
            {
                if (matrix[rowIndex, columnIndex] > localMax)
                {
                    visibility[rowIndex][columnIndex] = true;
                    localMax = matrix[rowIndex, columnIndex];
                }
            }
        }

        // Iterate from the bottom
        for (int columnIndex = 1; columnIndex < matrix.ColumnsCount - 1; columnIndex++)
        {
            int localMax = matrix[matrix.RowCount - 1, columnIndex];
            for (int rowIndex = matrix.RowCount - 2; rowIndex > 0; rowIndex--)
            {
                if (matrix[rowIndex, columnIndex] > localMax)
                {
                    visibility[rowIndex][columnIndex] = true;
                    localMax = matrix[rowIndex, columnIndex];
                }
            }
        }

        // Iterate from the left
        for (int rowIndex = 1; rowIndex < matrix.RowCount - 1; rowIndex++)
        {
            int localMax = matrix[rowIndex, 0];
            for (int columnIndex = 1; columnIndex < matrix.ColumnsCount - 1; columnIndex++)
            {
                if (matrix[rowIndex, columnIndex] > localMax)
                {
                    visibility[rowIndex][columnIndex] = true;
                    localMax = matrix[rowIndex, columnIndex];
                }
            }
        }

        // Iterate from the right
        for (int rowIndex = 1; rowIndex < matrix.RowCount - 1; rowIndex++)
        {
            int localMax = matrix[rowIndex, matrix.ColumnsCount - 1];
            for (int columnIndex = matrix.ColumnsCount - 2; columnIndex > 0; columnIndex--)
            {
                if (matrix[rowIndex, columnIndex] > localMax)
                {
                    visibility[rowIndex][columnIndex] = true;
                    localMax = matrix[rowIndex, columnIndex];
                }
            }
        }

        // At this point the visibility matrix is done.
        // The following line converts booleans to 1s and 0s, and sums them up.
        Console.WriteLine(new Matrix<int>(visibility.Select(r => r.Select(c => c ? 1 : 0))).AllValues().Sum());
    }
}