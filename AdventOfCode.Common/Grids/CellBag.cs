using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common.Grids
{
    /// <summary>
    /// A grid made of a bag of cells.
    /// Points with no cell return a default value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CellBag<T>
    {
        protected readonly Dictionary<Point, T> cells = new Dictionary<Point, T>();
        protected readonly T defaultValue;

        public IReadOnlyDictionary<Point, T> Cells => cells;
        public T DefaultValue => defaultValue;

        public CellBag(T defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public T this[Point p]
        {
            get 
            {
                if (cells.ContainsKey(p))
                {
                    return cells[p];
                }

                return defaultValue;
            }
            set
            {
                if (cells.ContainsKey(p))
                {
                    cells.Add(p, value);
                }
                else
                {
                    cells[p] = value;
                }
            }
        }

        public T this[int i, int y]
        {
            get { return this[new Point(i, y)]; }
            set { this[new Point(i, y)] = value; }
        }

        public IEnumerable<Point> GetAdjacentCellsCoordinates(int cellRowIndex, int cellColumnIndex, bool withDiagonals = true)
        {
            return GetAdjacentCellsCoordinates(new Point(cellRowIndex, cellColumnIndex), withDiagonals);
        }

        public IEnumerable<Point> GetAdjacentCellsCoordinates(Point currentPoint, bool withDiagonals = true)
        {
            if (withDiagonals)
            {
                // Get 3 rows
                for (int rowIndex = currentPoint.X - 1; rowIndex <= currentPoint.X + 1; rowIndex++)
                {
                    for (int columnIndex = currentPoint.Y - 1; columnIndex <= currentPoint.Y + 1; columnIndex++)
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
                    new Point() { X = currentPoint.X - 1, Y = currentPoint.Y },
                    // Bottom
                    new Point() { X = currentPoint.X + 1, Y = currentPoint.Y },
                    // Left
                    new Point() { X = currentPoint.X, Y = currentPoint.Y - 1 },
                    // Right
                    new Point() { X = currentPoint.X, Y = currentPoint.Y + 1 }
                })
                {
                    if (point != currentPoint)
                    {
                        yield return point;
                    }
                }
            }
        }

        public string Print(Func<T, char> transform, int padding = 1)
        {
            var sb = new StringBuilder();

            var minX = cells.Keys.MinBy(p => p.X).X;
            var maxX = cells.Keys.MaxBy(p => p.X).X;

            var minY = cells.Keys.MinBy(p => p.Y).Y;
            var maxY = cells.Keys.MaxBy(p => p.Y).Y;

            for (var rowIndex = minX - padding; rowIndex <= maxX + padding; rowIndex++)
            {
                for (var columnIndex = minY - padding; columnIndex <= maxY + padding; columnIndex++)
                {
                    sb.Append(transform(this[rowIndex, columnIndex]));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
