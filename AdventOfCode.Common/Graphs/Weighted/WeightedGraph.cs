using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    /// <summary>
    /// Undirected weighted graph.
    /// </summary>
    public class WeightedGraph<T> : IShortestPathStrategy<T>
        where T : IEquatable<T>
    {
        private readonly Dictionary<T, Dictionary<T, int>> edges = new Dictionary<T, Dictionary<T, int>>();
        private readonly WeightedShortestPathStrategy shortestPathStrategy;

        public WeightedGraph(WeightedShortestPathStrategy strategy)
        {
            this.shortestPathStrategy = strategy;
        }

        /// <summary>
        /// Add an edge to the graph.
        /// Start and end elements will be added to the graph if they do not yet exist.
        /// </summary>
        /// <param name="start">Starting element of the edge.</param>
        /// <param name="end">End element of the edge.</param>
        /// <param name="startWeight">Weight when travelling to start point.</param>
        /// <param name="endWeight">Weight when travelling to end point.</param>
        public void AddEdge(T start, T end, int startWeight, int endWeight)
        {
            // Start point
            if(!edges.ContainsKey(start))
            {
                edges.Add(start, new Dictionary<T, int>());
            }

            if (!edges[start].ContainsKey(end))
            {
                edges[start].Add(end, endWeight);
            }

            // End point
            if (!edges.ContainsKey(end))
            {
                edges.Add(end, new Dictionary<T, int>());
            }

            if (!edges[end].ContainsKey(start))
            {
                edges[end].Add(start, startWeight);
            }
        }

        public int GetEdge(T start, T end)
        {
            return edges[start].First(e => e.Key.Equals(end)).Value;
        }

        public int GetShortestPath(T start, T end)
        {
            IShortestPathStrategy<T> strategy = shortestPathStrategy switch
            {
                WeightedShortestPathStrategy.Djikstra => new DjikstraStrategy<T>(edges),
            };

            return strategy.GetShortestPath(start, end);
        }
    }
}
