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
    }
}
