using AdventOfCode.Common.Graphs.Unweighted;
using AdventOfCode.Common.Models;

namespace AdventOfCode.Common
{
    /// <summary>
    /// A simple grid with a fixed width and height.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleGrid<T> : Grid<T>
    {
        public override int RowLength => cells.GetLength(0);

        public override int ColumnLength => cells.GetLength(1);

        public SimpleGrid(int rowCount, int columnCount) : base(rowCount, columnCount)
        {
        }

        public override T this[Point p]
        {
            get { return cells[p.X, p.Y]; }
            set { cells[p.X, p.Y] = value; }
        }

        public override T this[int i, int y]
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

        public UnweightedGraph ToUnweightedGraph(Func<T, T, bool> canTraverse)
        {
            UnweightedGraph graph = new UnweightedGraph(UnweightedShortestPathStrategy.BFS, true);

            for(int rowIndex = 0; rowIndex < this.RowLength; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < this.ColumnLength; columnIndex++)
                {
                    Point current = new Point(rowIndex, columnIndex);
                    IEnumerable<Point> neighbours = GetAdjacentCellsCoordinates(current, false);

                    foreach(Point neighbor in neighbours)
                    {
                        if (canTraverse(this[current], this[neighbor]))
                        {
                            graph.AddEdge(current, neighbor);
                        }
                    }
                }
            }

            return graph;
        }
    }
}
