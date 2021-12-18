namespace AdventOfCode.Common
{
    public class DjikstraStrategy : IShortestPathStrategy
    {
        private readonly Dictionary<Point, Dictionary<Point, int>> edges;

        public DjikstraStrategy(Dictionary<Point, Dictionary<Point, int>> edges)
        {
            this.edges = edges;
        }

        public int GetShortestPath(Point start, Point end)
        {
            Dictionary<Point, int> points = edges.Keys.ToDictionary(k => k, k => k == start ? 0 : int.MaxValue);
            PriorityQueue<Point, int> queue = new PriorityQueue<Point, int>(edges.Keys.Select(k => (k, k == start ? 0 : int.MaxValue)));

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
