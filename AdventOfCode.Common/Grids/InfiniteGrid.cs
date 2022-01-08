using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    /// <summary>
    /// An infinitely repeating grid.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InfiniteGrid<T> :  Grid<T>
    {
        public override int RowLength => cells.GetLength(0) * repeat;

        public override int ColumnLength => cells.GetLength(1) * repeat;

        private readonly int repeat;
        private readonly Func<T[,], Point, int, int, T> outOfBoundAccessor;

        public InfiniteGrid(int rowCount, int columnCount, int repeat, Func<T[,], Point, int, int, T> outOfBoundAccessor) : base(rowCount, columnCount)
        {
            this.repeat = repeat;
            this.outOfBoundAccessor = outOfBoundAccessor;
        }

        public override T this[Point p] 
        {
            get => this[p.X, p.Y];
            set => this[p.X, p.Y] = value; 
        }
        public override T this[int i, int y]
        {
            get
            {
                var cellsX = i % cells.GetLength(0);
                var cellsY = y % cells.GetLength(1);

                var repeatX = (int)Math.Floor((double)i / cells.GetLength(0));
                var repeatY = (int)Math.Floor((double)y / cells.GetLength(1));

                return outOfBoundAccessor(cells, new Point(cellsX, cellsY), repeatX, repeatY);
            }
            set
            {
                var cellsX = i % cells.GetLength(0);
                var cellsY = y % cells.GetLength(1);

                cells[cellsX, cellsY] = value;
            }
        }
    }
}
