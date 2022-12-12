using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public class PointInstance
    {
        public int X;
        public int Y;

        public PointInstance(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }
    }
}
