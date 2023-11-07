namespace AdventOfCode.Common.Models
{
    public class PointInstance
    {
        public int X;
        public int Y;

        public PointInstance(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }
    }
}
