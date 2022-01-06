using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
