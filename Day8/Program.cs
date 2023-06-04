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

        var visibilityMatrix = new Matrix<bool>(Enumerable.Range(0, matrix.RowCount)
            .Select(rowIndex => Enumerable.Range(0, matrix.ColumnsCount).Select<int, bool>(columnIndex =>
            {
                // Trees on edges are automatically visible
                if (
                    columnIndex == 0 ||
                    columnIndex == matrix.ColumnsCount - 1 ||
                    rowIndex == 0 ||
                    rowIndex == matrix.RowCount - 1
                    )
                    return true;

                int treeSize = matrix[rowIndex, columnIndex];
                bool isVisibleFromTop = matrix.GetColumn(columnIndex).Take(columnIndex).All(tree => tree < treeSize);
                bool isVisibleFromBottom = matrix.GetColumn(columnIndex).Skip(columnIndex + 1).All(tree => tree < treeSize);
                bool isVisibleFromLeft = matrix.GetRow(columnIndex).Take(rowIndex).All(tree => tree < treeSize);
                bool isVisibleFromRight = matrix.GetRow(columnIndex).Skip(rowIndex + 1).All(tree => tree < treeSize);

                return isVisibleFromTop || isVisibleFromBottom || isVisibleFromLeft || isVisibleFromRight;
            })));

        Console.WriteLine(new Matrix<int>(visibilityMatrix.M.Select(row => row.Select(boolean => boolean ? 1 : 0))));

        var visibility = visibilityMatrix.AllValues().Select(boolean => boolean ? 1 : 0);

        Console.WriteLine(visibility.Sum());
    }
}