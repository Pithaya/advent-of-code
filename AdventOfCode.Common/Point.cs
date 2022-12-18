namespace AdventOfCode.Common
{
    public record struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Point Zero => new Point(0, 0);

        public static int GetManhattanDistance(Point first, Point second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    }
}
