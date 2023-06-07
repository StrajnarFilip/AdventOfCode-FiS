using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common.Structures
{
    public class Matrix<T>
    {
        readonly T[][] _matrix;

        public Matrix(T[,] values)
        {
            if (values is null)
            {
                throw new ArgumentException("Null was given to constructor.");
            }

            this._matrix = values.Cast<T>().Chunk(values.GetLength(1)).ToArray();
        }

        public Matrix(IEnumerable<IEnumerable<T>> values)
        {
            this._matrix = values.Select(row => row.ToArray()).ToArray();
        }

        public T this[int rowIndex, int columnIndex]
        {
            get => _matrix[rowIndex][columnIndex];
        }

        public T[][] MatrixCopy() =>
            _matrix.Select(row => row.Select(el => el).ToArray()).ToArray();

        public int RowCount => this._matrix.Length;
        public int ColumnsCount => this._matrix[0].Length;

        public T[] GetRow(int index) => this._matrix[index];

        public T[] GetColumn(int index) => this._matrix.Select(row => row[index]).ToArray();

        public T? GetElement(int rowIndex, int columnIndex)
        {
            if (_matrix[rowIndex] is null || _matrix[rowIndex][columnIndex] is null)
                return default;

            return _matrix[rowIndex][columnIndex];
        }

        public T[] AllValues()
        {
            return this._matrix.SelectMany(row => row.ToArray()).ToArray();
        }

        public bool RowIndexInBounds(int rowIndex) => rowIndex < RowCount && rowIndex >= 0;

        public bool ColumnIndexInBounds(int columnIndex) =>
            columnIndex < ColumnsCount && columnIndex >= 0;

        public bool IndicesInBounds(int rowIndex, int columnIndex) =>
            RowIndexInBounds(rowIndex) && ColumnIndexInBounds(columnIndex);

        public (int row, int column)[] GetNeighbourIndices(
            int rowIndex,
            int columnIndex,
            bool includeDiagonal = false
        )
        {
            List<(int row, int column)> validNeighbourIndices = new();
            // Left
            if (IndicesInBounds(rowIndex, columnIndex - 1))
                validNeighbourIndices.Add((rowIndex, columnIndex - 1));
            // Right
            if (IndicesInBounds(rowIndex, columnIndex + 1))
                validNeighbourIndices.Add((rowIndex, columnIndex + 1));
            // Up
            if (IndicesInBounds(rowIndex - 1, columnIndex))
                validNeighbourIndices.Add((rowIndex - 1, columnIndex));
            // Down
            if (IndicesInBounds(rowIndex + 1, columnIndex))
                validNeighbourIndices.Add((rowIndex + 1, columnIndex));

            if (includeDiagonal)
            {
                // Top Left
                if (IndicesInBounds(rowIndex - 1, columnIndex - 1))
                    validNeighbourIndices.Add((rowIndex - 1, columnIndex - 1));
                // Top Right
                if (IndicesInBounds(rowIndex - 1, columnIndex + 1))
                    validNeighbourIndices.Add((rowIndex - 1, columnIndex + 1));
                // Bottom Right
                if (IndicesInBounds(rowIndex + 1, columnIndex + 1))
                    validNeighbourIndices.Add((rowIndex + 1, columnIndex + 1));
                // Bottom Left
                if (IndicesInBounds(rowIndex + 1, columnIndex - 1))
                    validNeighbourIndices.Add((rowIndex + 1, columnIndex - 1));
            }

            return validNeighbourIndices.ToArray();
        }

        public T[] GetNeighbourValues(
            int rowIndex,
            int columnIndex,
            bool includeDiagonal = false
        ) =>
            GetNeighbourIndices(rowIndex, columnIndex, includeDiagonal)
                .Select(index => _matrix[index.row][index.column])
                .ToArray();

        public T[][] Slice(int rowIndexFrom, int rowIndexTo, int columnIndexFrom, int columnIndexTo)
        {
            return this._matrix
                .Skip(rowIndexFrom)
                .Take(rowIndexTo - rowIndexFrom + 1)
                .Select(
                    row =>
                        row.Skip(columnIndexFrom)
                            .Take(columnIndexTo - columnIndexFrom + 1)
                            .ToArray()
                )
                .ToArray();
        }

        public override string ToString()
        {
            return String.Join("\n", this._matrix.Select(row => String.Join(", ", row)));
        }
    }
}
