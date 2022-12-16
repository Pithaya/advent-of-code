using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common.Graphs.Unweighted
{
    public class BFSStrategy : IShortestPathStrategy<Point>
    {
        private readonly Dictionary<Point, HashSet<Point>> edges;

        public BFSStrategy(Dictionary<Point, HashSet<Point>> edges)
        {
            this.edges = edges;
        }

        public int GetShortestPath(Point start, Point end)
        {
            // Points to visit
            Queue<Point> queue = new Queue<Point>();

            // Visited points and their parent
            Dictionary<Point, Point> visited = new Dictionary<Point, Point>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();

                foreach(Point neighbour in edges[currentPoint])
                {
                    if (!visited.ContainsKey(neighbour))
                    {
                        visited.Add(neighbour, currentPoint);
                        queue.Enqueue(neighbour);

                        if(neighbour == end)
                        {
                            queue.Clear();
                            break;
                        }
                    }
                }
            }

            if (!visited.ContainsKey(end))
            {
                // No path found
                return int.MaxValue;
            }

            int path = 0;
            Point current = end;

            while(current != start)
            {
                current = visited[current];
                path++;
            }

            return path;
        }
    }
}
