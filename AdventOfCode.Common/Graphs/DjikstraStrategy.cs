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
            Dictionary<Point, (bool Visited, int Distance)> points = edges.Keys.ToDictionary(k => k, k => (false, int.MaxValue));

            points[start] = (true, 0);

            var currentNode = start;
            while (currentNode != end)
            {
                currentNode = VisitNode(currentNode, points);
            }

            return points[end].Distance;
        }

        private Point VisitNode(Point currentNode, Dictionary<Point, (bool Visited, int Distance)> points)
        {
            var unvisitedNeighbors = edges[currentNode].Where(e => !points[e.Key].Visited).ToList();

            foreach (var edge in unvisitedNeighbors)
            {
                // Compute new distance
                var distance = points[currentNode].Distance + edge.Value;
                if (distance < points[edge.Key].Distance)
                {
                    points[edge.Key] = (false, distance);
                }
            }

            // Mark as visited
            points[currentNode] = (true, points[currentNode].Distance);

            var nextNode = points.Where(p => !p.Value.Visited).MinBy(e => e.Value.Distance).Key;
            return nextNode;
        }
    }
}
