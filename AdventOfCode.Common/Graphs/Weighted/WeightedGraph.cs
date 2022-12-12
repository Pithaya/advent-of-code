using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public class WeightedGraph : IShortestPathStrategy
    {
        private readonly Dictionary<Point, Dictionary<Point, int>> edges = new Dictionary<Point, Dictionary<Point, int>>();
        private readonly WeightedShortestPathStrategy shortestPathStrategy;

        public WeightedGraph(WeightedShortestPathStrategy strategy)
        {
            this.shortestPathStrategy = strategy;
        }

        public void AddEdge(Point start, Point end, int startWeight, int endWeight)
        {
            // Start point
            if(!edges.ContainsKey(start))
            {
                edges.Add(start, new Dictionary<Point, int>());
            }

            if (!edges[start].ContainsKey(end))
            {
                edges[start].Add(end, endWeight);
            }

            // End point
            if (!edges.ContainsKey(end))
            {
                edges.Add(end, new Dictionary<Point, int>());
            }

            if (!edges[end].ContainsKey(start))
            {
                edges[end].Add(start, startWeight);
            }
        }

        public int GetEdge(Point start, Point end)
        {
            return edges[start].First(e => e.Key == end).Value;
        }

        public int GetShortestPath(Point start, Point end)
        {
            IShortestPathStrategy strategy = shortestPathStrategy switch
            {
                WeightedShortestPathStrategy.Djikstra => new DjikstraStrategy(edges),
            };

            return strategy.GetShortestPath(start, end);
        }

        private class Edge
        {
            public Point End { get; set; }
            public int Weight { get; set; }
        }
    }
}
