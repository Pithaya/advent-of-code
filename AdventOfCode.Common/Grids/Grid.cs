using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public abstract class Grid<T>
    {
        protected readonly T[,] cells;

        public abstract int RowLength { get; }
        public abstract int ColumnLength { get; }

        public Grid(int rowCount, int columnCount)
        {
            cells = new T[rowCount, columnCount];
        }

        public T[] GetColumn(int columnIndex)
        {
            return Enumerable.Range(0, RowLength)
                    .Select(x => this[x, columnIndex])
                    .ToArray();
        }

        public T[] GetRow(int rowIndex)
        {
            return Enumerable.Range(0, ColumnLength)
                    .Select(x => this[rowIndex, x])
                    .ToArray();
        }

        public void SetRow(int rowIndex, T[] values)
        {
            if(cells.GetLength(1) != values.Length)
            {
                throw new ArgumentException("The provided values length does not match the row length.");
            }

            for (int index = 0; index < values.Length; index++)
            {
                cells[rowIndex, index] = values[index];
            }
        }

        public void SetColumn(int columnIndex, T[] values)
        {
            if (cells.GetLength(0) != values.Length)
            {
                throw new ArgumentException("The provided values length does not match the column length.");
            }

            for (int index = 0; index < values.Length; index++)
            {
                cells[index, columnIndex] = values[index];
            }
        }

        public abstract T this[int i, int y] { get; set; }

        public abstract T this[Point p] { get;set; }

        public IEnumerable<Point> GetAdjacentCellsCoordinates(int cellRowIndex, int cellColumnIndex, bool withDiagonals = true)
        {
            return GetAdjacentCellsCoordinates(new Point(cellRowIndex, cellColumnIndex), withDiagonals);
        }

        public IEnumerable<Point> GetAdjacentCellsCoordinates(Point currentPoint, bool withDiagonals = true)
        {
            if (withDiagonals)
            {
                // Get 3 rows
                for (int rowIndex = ClampX(currentPoint.X - 1); rowIndex <= ClampX(currentPoint.X + 1); rowIndex++)
                {
                    for (int columnIndex = ClampY(currentPoint.Y - 1); columnIndex <= ClampY(currentPoint.Y + 1); columnIndex++)
                    {
                        var point = new Point(rowIndex, columnIndex);
                        if (point != currentPoint)
                        {
                            yield return point;
                        }
                    }
                }
            }
            else
            {
                foreach (var point in new Point[]
                {
                    // Top
                    new Point() { X = ClampX(currentPoint.X - 1), Y = currentPoint.Y },
                    // Bottom
                    new Point() { X = ClampX(currentPoint.X + 1), Y = currentPoint.Y },
                    // Left
                    new Point() { X = currentPoint.X, Y = ClampY(currentPoint.Y - 1) },
                    // Right
                    new Point() { X = currentPoint.X, Y = ClampY(currentPoint.Y + 1) }
                })
                {
                    if (point != currentPoint)
                    {
                        yield return point;
                    }
                }
            }
        }

        protected int ClampX(int x)
        {
            return Math.Clamp(x, 0, RowLength - 1);
        }

        protected int ClampY(int y)
        {
            return Math.Clamp(y, 0, ColumnLength - 1);
        }

        public bool IsLastCell(Point point)
        {
            return IsLastCell(point.X, point.Y);
        }

        public bool IsLastCell(int rowIndex, int columnIndex)
        {
            return rowIndex == RowLength - 1 && columnIndex == ColumnLength - 1;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            for(var rowIndex = 0; rowIndex < RowLength; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < ColumnLength; columnIndex++)
                {
                    sb.Append(this[rowIndex, columnIndex]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
