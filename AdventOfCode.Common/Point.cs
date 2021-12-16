namespace AdventOfCode.Common
{
    public record struct Point
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
