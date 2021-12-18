namespace AdventOfCode.Common
{
    interface IShortestPathStrategy
    {
        public int GetShortestPath(Point start, Point end);
    }
}
