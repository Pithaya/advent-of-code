namespace AdventOfCode.Common.Models
{
    public record struct Vector
    {
        public int X;
        public int Y;

        public double Length => Math.Sqrt(X * X + Y * Y);

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point startPoint, Point endPoint)
        {
            X = endPoint.X - startPoint.X;
            Y = endPoint.Y - startPoint.Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector Zero => new Vector(0, 0);
    }
}
