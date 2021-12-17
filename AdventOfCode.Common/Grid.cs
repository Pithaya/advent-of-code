using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public class Grid<T>
    {
        protected readonly T[,] cells;

        public T[,] Cells { get => cells; }

        public Grid(int rowCount, int columnCount)
        {
            cells = new T[rowCount, columnCount];
        }

        public T[] GetColumn(int columnIndex)
        {
            return Enumerable.Range(0, cells.GetLength(0))
                    .Select(x => cells[x, columnIndex])
                    .ToArray();
        }

        public T[] GetRow(int rowIndex)
        {
            return Enumerable.Range(0, cells.GetLength(1))
                    .Select(x => cells[rowIndex, x])
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

        public T this[int i, int y]
        {
            get { return cells[i, y]; }
            set { cells[i, y] = value; }
        }

        public T[] this[int i]
        {
            get { return GetRow(i); }
            set
            {
                SetRow(i, value);
            }
        }

        public T this[Point p]
        {
            get { return cells[p.X, p.Y]; }
            set { cells[p.X, p.Y] = value; }
        }

        public IEnumerable<Point> GetAdjacentCellsCoordinates(Point currentPoint, bool withDiagonals = true)
        {
            if (withDiagonals)
            {
                // Get 3 rows
                for (int rowIndex = ClampX(currentPoint.X - 1); rowIndex < ClampX(currentPoint.X + 2); rowIndex++)
                {
                    for (int columnIndex = ClampY(currentPoint.Y - 1); columnIndex < ClampY(currentPoint.Y + 2); columnIndex++)
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

        private int ClampX(int x)
        {
            return Math.Clamp(x, 0, cells.GetLength(0) - 1);
        }

        private int ClampY(int y)
        {
            return Math.Clamp(y, 0, cells.GetLength(1) - 1);
        }


        public IEnumerable<Point> GetAdjacentCellsCoordinates(int cellRowIndex, int cellColumnIndex, bool withDiagonals = true)
        {
            return GetAdjacentCellsCoordinates(new Point(cellRowIndex, cellColumnIndex), withDiagonals);
        }

        public bool IsLastCell(Point point)
        {
            return IsLastCell(point.X, point.Y);
        }

        public bool IsLastCell(int rowIndex, int columnIndex)
        {
            return rowIndex == cells.GetLength(0) - 1 && columnIndex == cells.GetLength(1) - 1;
        }
    }
}
