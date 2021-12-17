using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    public class WeightedGraph
    {
        private readonly Dictionary<Point, Dictionary<Point, int>> edges = new Dictionary<Point, Dictionary<Point, int>>();

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
            Dictionary<Point, (bool Visited, int Distance)> points = edges.Keys.ToDictionary(k => k, k => (false, int.MaxValue));

            points[start] = (true, 0);

            var currentNode = start;
            while(currentNode != end)
            {
                currentNode = VisitNode(currentNode, points);
            }

            return points[end].Distance;
        }

        private Point VisitNode(Point currentNode, Dictionary<Point, (bool Visited, int Distance)> points)
        {
            var unvisitedNeighbors = edges[currentNode].Where(e => !points[e.Key].Visited).ToList();

            foreach(var edge in unvisitedNeighbors)
            {
                // Compute new distance
                var distance = points[currentNode].Distance + edge.Value;
                if(distance < points[edge.Key].Distance)
                {
                    points[edge.Key] = (false, distance);
                }
            }

            // Mark as visited
            points[currentNode] = (true, points[currentNode].Distance);

            var nextNode = points.Where(p => !p.Value.Visited).MinBy(e => e.Value.Distance).Key;
            return nextNode;
        }

        private class Edge
        {
            public Point End { get; set; }
            public int Weight { get; set; }
        }
    }
}
