namespace AdventOfCode.Common
{
    public class DjikstraStrategy<T> : IShortestPathStrategy<T>
        where T : IEquatable<T>
    {
        private readonly Dictionary<T, Dictionary<T, int>> edges;

        public DjikstraStrategy(Dictionary<T, Dictionary<T, int>> edges)
        {
            this.edges = edges;
        }

        public int GetShortestPath(T start, T end)
        {
            Dictionary<T, int> points = edges.Keys.ToDictionary(k => k, k => k.Equals(start) ? 0 : int.MaxValue);
            PriorityQueue<T, int> queue = new PriorityQueue<T, int>(edges.Keys.Select(k => (k, k.Equals(start) ? 0 : int.MaxValue)));

            while(queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();
                var pointEdges = edges[currentPoint];

                foreach(var adjacentEdge in pointEdges)
                {
                    var destination = adjacentEdge.Key;
                    var newDistance = adjacentEdge.Value + points[currentPoint];
                    if(newDistance < points[destination])
                    {
                        points[destination] = newDistance;
                        queue.Enqueue(destination, newDistance);
                    }
                }
            }

            return points[end];
        }
    }
}
