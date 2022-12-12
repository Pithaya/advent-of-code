using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common.Graphs.Unweighted
{
    public class UnweightedGraph : IShortestPathStrategy
    {
        private readonly Dictionary<Point, HashSet<Point>> edges = new Dictionary<Point, HashSet<Point>>();
        private readonly UnweightedShortestPathStrategy shortestPathStrategy;
        private readonly bool directed;

        public UnweightedGraph(UnweightedShortestPathStrategy strategy, bool directed)
        {
            this.shortestPathStrategy = strategy;
            this.directed = directed;
        }

        public void AddEdge(Point start, Point end)
        {
            // Start point
            if (!edges.ContainsKey(start))
            {
                edges.Add(start, new HashSet<Point>());
            }

            if (!edges[start].Contains(end))
            {
                edges[start].Add(end);
            }

            if (!this.directed)
            {
                // End point
                if (!edges.ContainsKey(end))
                {
                    edges.Add(end, new HashSet<Point>());
                }

                if (!edges[end].Contains(start))
                {
                    edges[end].Add(start);
                }
            }
        }

        public int GetShortestPath(Point start, Point end)
        {
            IShortestPathStrategy strategy = shortestPathStrategy switch
            {
                UnweightedShortestPathStrategy.BFS => new BFSStrategy(edges),
            };

            return strategy.GetShortestPath(start, end);
        }

        private class Edge
        {
            public Point End { get; set; }
        }
    }
}
