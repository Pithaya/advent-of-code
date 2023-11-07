using AdventOfCode.Common.Models;
using System.Text;

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
        protected readonly Func<Point?, T> defaultValueBuilder;

        public IReadOnlyDictionary<Point, T> Cells => cells;
        public Func<Point?, T> DefaultValueBuilder => defaultValueBuilder;

        public T DefaultValue => defaultValueBuilder(null);

        public CellBag(T defaultValue)
        {
            this.defaultValueBuilder = (_) => defaultValue;
        }

        public CellBag(Func<Point?, T> defaultValueBuilder)
        {
            this.defaultValueBuilder = defaultValueBuilder;
        }

        public T this[Point p]
        {
            get 
            {
                if (cells.ContainsKey(p))
                {
                    return cells[p];
                }

                return defaultValueBuilder(p);
            }
            set
            {
                if (!cells.ContainsKey(p))
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
                    yield return point;
                }
            }
        }

        public IEnumerable<KeyValuePair<Point, T>> GetRowCells(int rowIndex)
        {
            return Cells.Where(c => c.Key.X == rowIndex).OrderBy(c => c.Key.Y);
        }

        public IEnumerable<KeyValuePair<Point, T>> GetColumnCells(int columnIndex)
        {
            return Cells.Where(c => c.Key.Y == columnIndex).OrderBy(c => c.Key.X);
        }

        public string Print(Func<T, char> transform, int padding = 1)
        {
            return Print((Point p) => transform(this[p]), padding);
        }

        public string Print(Func<Point, char> transform, int padding = 1)
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
                    sb.Append(transform(new Point(rowIndex, columnIndex)));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
