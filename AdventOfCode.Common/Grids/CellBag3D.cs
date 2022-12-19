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
    public class CellBag3D<T>
    {
        #region Cells

        protected readonly Dictionary<Point3D, T> cells = new Dictionary<Point3D, T>();
        public IReadOnlyDictionary<Point3D, T> Cells => cells;

        #endregion

        #region Default Value

        protected readonly Func<Point3D?, T> defaultValueBuilder;
        public Func<Point3D?, T> DefaultValueBuilder => defaultValueBuilder;

        public T DefaultValue => defaultValueBuilder(null);

        #endregion

        #region Bounds

        public Point3D? MinBounds { get; set; }
        public Point3D? MaxBounds { get; set; }

        #endregion

        public CellBag3D(T defaultValue)
        {
            this.defaultValueBuilder = (_) => defaultValue;
        }

        public CellBag3D(Func<Point3D?, T> defaultValueBuilder)
        {
            this.defaultValueBuilder = defaultValueBuilder;
        }

        public T this[Point3D p]
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

        public T this[int x, int y, int z]
        {
            get { return this[new Point3D(x, y, z)]; }
            set { this[new Point3D(x, y, z)] = value; }
        }

        public IEnumerable<Point3D> GetAdjacentCellsCoordinates(int cellRowIndex, int cellColumnIndex, int cellSliceIndex, bool withDiagonals = true)
        {
            return GetAdjacentCellsCoordinates(new Point3D(cellRowIndex, cellColumnIndex, cellSliceIndex), withDiagonals);
        }

        public IEnumerable<Point3D> GetAdjacentCellsCoordinates(Point3D currentPoint, bool withDiagonals = true)
        {
            if (withDiagonals)
            {
                for(int sliceIndex = currentPoint.Z - 1; sliceIndex <= currentPoint.Z + 1; sliceIndex++)
                {
                    for (int rowIndex = currentPoint.X - 1; rowIndex <= currentPoint.X + 1; rowIndex++)
                    {
                        for (int columnIndex = currentPoint.Y - 1; columnIndex <= currentPoint.Y + 1; columnIndex++)
                        {
                            var point = new Point3D(rowIndex, columnIndex, sliceIndex);
                            if (point != currentPoint && !IsOutOfBounds(point))
                            {
                                yield return point;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var point in new Point3D[]
                {
                    // Top
                    currentPoint with { X = currentPoint.X - 1 },
                    // Bottom
                    currentPoint with { X = currentPoint.X + 1 },
                    // Left
                    currentPoint with { Y = currentPoint.Y - 1 },
                    // Right
                    currentPoint with { Y = currentPoint.Y + 1 },
                    // Up slice
                    currentPoint with { Z = currentPoint.Z + 1 },
                    // Down slice
                    currentPoint with { Z = currentPoint.Z - 1 }
                })
                {
                    if (point != currentPoint && !IsOutOfBounds(point))
                    {
                        yield return point;
                    }
                }
            }
        }

        public bool IsOutOfBounds(Point3D point)
        {
            bool outOfBounds = false;

            if(MinBounds.HasValue)
            {
                outOfBounds |= point.X <= MinBounds.Value.X ||  point.Y <= MinBounds.Value.Y || point.Z <= MinBounds.Value.Z;
            }

            if (MaxBounds.HasValue)
            {
                outOfBounds |= point.X >= MaxBounds.Value.X || point.Y >= MaxBounds.Value.Y || point.Z >= MaxBounds.Value.Z;
            }

            return outOfBounds;
        }

        public string Print(Func<T, char> transform, int sliceIndex, int padding = 1)
        {
            return Print((Point3D p) => transform(this[p]), sliceIndex, padding);
        }

        public string Print(Func<Point3D, char> transform, int sliceIndex, int padding = 1)
        {
            int minX = 0;
            int maxX = 0;

            int minY = 0;
            int maxY = 0;

            if (cells.Keys.Any())
            {
                IEnumerable<Point3D> slicePoints = cells.Keys.Where(c => c.Z == sliceIndex);

                if (slicePoints.Any())
                {
                    minX = slicePoints.Min(p => p.X);
                    maxX = slicePoints.Max(p => p.X);

                    minY = slicePoints.Min(p => p.Y);
                    maxY = slicePoints.Max(p => p.Y);
                }
            }

            var sb = new StringBuilder();

            for (var rowIndex = minX - padding; rowIndex <= maxX + padding; rowIndex++)
            {
                for (var columnIndex = minY - padding; columnIndex <= maxY + padding; columnIndex++)
                {
                    sb.Append(transform(new Point3D(rowIndex, columnIndex, sliceIndex)));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
