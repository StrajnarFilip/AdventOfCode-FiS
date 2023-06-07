namespace Day8;
using Common.Parsing;
using Fprog.Algorithms.Common.Structures;

public static class Program
{
    public static Matrix<bool> VisibilityFromOutsideMatrix(Matrix<int> matrix)
    {
        // Initialize a boolean matrix with all false values, except on edges.
        var visibility = new bool[matrix.RowCount][];
        for (int i = 0; i < visibility.Length; i++)
        {
            if (i == 0 || i == visibility.Length - 1)
            {
                // All true
                visibility[i] = new bool[matrix.ColumnsCount]
                    .Select(el => true)
                    .ToArray();
                continue;
            }

            var row = new bool[matrix.ColumnsCount]
                .Select(el => false)
                .ToArray();
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

        return new Matrix<bool>(visibility);
    }

    public static int VisibleTreesTop(Matrix<int> matrix, int rowIndex, int columnIndex)
    {
        int treeHeight = matrix[rowIndex, columnIndex];
        int visibleTrees = 0;

        for (int row = rowIndex; row > 0; row--)
        {
            visibleTrees++;
            if (matrix[row - 1, columnIndex] >= treeHeight)
                break;
        }

        return visibleTrees;
    }

    public static int VisibleTreesBottom(Matrix<int> matrix, int rowIndex, int columnIndex)
    {
        int treeHeight = matrix[rowIndex, columnIndex];
        int visibleTrees = 0;

        for (int row = rowIndex; row < matrix.RowCount - 1; row++)
        {
            visibleTrees++;
            if (matrix[row + 1, columnIndex] >= treeHeight)
                break;
        }

        return visibleTrees;
    }

    public static int VisibleTreesLeft(Matrix<int> matrix, int rowIndex, int columnIndex)
    {
        int treeHeight = matrix[rowIndex, columnIndex];
        int visibleTrees = 0;

        for (int column = columnIndex; column > 0; column--)
        {
            visibleTrees++;
            if (matrix[rowIndex, column - 1] >= treeHeight)
                break;
        }

        return visibleTrees;
    }

    public static int VisibleTreesRight(Matrix<int> matrix, int rowIndex, int columnIndex)
    {
        int treeHeight = matrix[rowIndex, columnIndex];
        int visibleTrees = 0;

        for (int column = columnIndex; column < matrix.ColumnsCount - 1; column++)
        {
            visibleTrees++;
            if (matrix[rowIndex, column + 1] >= treeHeight)
                break;
        }

        return visibleTrees;
    }

    /// <summary>
    /// Calculates scenic score of a single element (tree) in the matrix.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="rowIndex"></param>
    /// <param name="columnIndex"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int ScenicScoreElement(Matrix<int> matrix, int rowIndex, int columnIndex)
    {
        var visibleFromTop = VisibleTreesTop(matrix, rowIndex, columnIndex);
        var visibleFromBottom = VisibleTreesBottom(matrix, rowIndex, columnIndex);
        var visibleFromLeft = VisibleTreesLeft(matrix, rowIndex, columnIndex);
        var visibleFromRight = VisibleTreesRight(matrix, rowIndex, columnIndex);

        return visibleFromTop * visibleFromBottom * visibleFromLeft * visibleFromRight;
    }

    public static Matrix<int> ScenicScoreMatrix(Matrix<int> matrix)
    {
        var scenicScores = new int[matrix.RowCount][];
        for (int rowIndex = 0; rowIndex < matrix.ColumnsCount; rowIndex++)
        {
            var scenicScoreRow = new int[matrix.ColumnsCount];
            for (int columnIndex = 0; columnIndex < matrix.RowCount; columnIndex++)
            {
                scenicScoreRow[columnIndex] = ScenicScoreElement(matrix, rowIndex, columnIndex);
            }
            scenicScores[rowIndex] = scenicScoreRow;
        }

        return new Matrix<int>(scenicScores);
    }

    /// <summary>
    /// Calculates the amount of trees, visible from outside the grid.
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static int Part1(Matrix<int> matrix)
    {
        // Count true elements.
        return VisibilityFromOutsideMatrix(matrix).AllValues().Count(element => element);
    }

    /// <summary>
    /// Calculates the highest available scenic score.
    /// </summary>
    /// <returns></returns>
    public static int Part2(Matrix<int> matrix)
    {
        return ScenicScoreMatrix(matrix).AllValues().Max();
    }

    public static void Main()
    {
        var matrix = MatrixParse.ParseSingleDigitMatrix("Assets/data.txt");

        Console.WriteLine($"Part 1: {Part1(matrix)}, Part 2: {Part2(matrix)}");
    }
}
