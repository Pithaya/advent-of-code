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

        public Grid(int rowIndex, int columnIndex)
        {
            cells = new T[rowIndex, columnIndex];
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
    }
}
