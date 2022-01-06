using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public record struct Point3D
    {
        public int X;
        public int Y;
        public int Z;

        public Point3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Point3D Zero => new Point3D(0, 0, 0);

        public static double Distance(Point3D first, Point3D second)
        {
            var x = Math.Pow(first.X - second.X, 2);
            var y = Math.Pow(first.Y - second.Y, 2);
            var z = Math.Pow(first.Z - second.Z, 2);
            return Math.Sqrt(x + y + z);
        }

        public static int ManhattanDistance(Point3D first, Point3D second)
        {
            var distance = second - first;
            return Math.Abs(distance.X) + Math.Abs(distance.Y) + Math.Abs(distance.Z);
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        }
    }
}
