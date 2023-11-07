namespace AdventOfCode.Common
{
    interface IShortestPathStrategy<T>
        where T : IEquatable<T>
    {
        public int GetShortestPath(T start, T end);
    }
}
