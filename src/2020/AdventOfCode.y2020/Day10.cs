using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(10)]
    public class Day10 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int[] adapters = input.ToList().Select(s => int.Parse(s)).OrderBy(a => a).ToArray();
            int differences1jolt = 0;
            int differences3jolt = 0;
            int currentAdapter = 0;
            for (int i = 0; i < adapters.Length; i++)
            {
                int adapter = adapters[i];
                int difference = adapter - currentAdapter;
                if (difference > 3)
                {
                    throw new InvalidOperationException();
                }

                if (difference == 1)
                {
                    differences1jolt++;
                }
                else if (difference == 3)
                {
                    differences3jolt++;
                }

                currentAdapter = adapter;
            }
            differences3jolt++;

           return (differences1jolt * differences3jolt).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int[] parsed = input.ToList().Select(s => int.Parse(s)).OrderBy(a => a).ToArray();
            int[] adapters = new int[parsed.Length + 2];
            adapters[0] = 0;
            parsed.CopyTo(adapters, 1);
            adapters[adapters.Length - 1] = parsed.Last() + 3;
            Graph graph = new Graph(adapters);

            Console.WriteLine("Searching for paths...");
            double result = graph.GetAllPaths();

            return result.ToString();
        }

        public class Graph
        {
            public Dictionary<int, List<int>> Nodes { get; }

            private List<SubGraph> SubGraphs { get; set; }

            public Graph(int[] adapters)
            {
                Nodes = new Dictionary<int, List<int>>();
                for (int i = 0; i < adapters.Length; i++)
                {
                    int current = adapters[i];
                    List<int> adjacents = new List<int>();

                    // Check the next 3 numbers
                    for (int j = i + 1; j < Math.Min(i + 4, adapters.Length); j++)
                    {
                        int difference = adapters[j] - adapters[i];
                        if (difference > 3)
                        {
                            break;
                        }

                        adjacents.Add(adapters[j]);
                    }

                    Nodes.Add(current, adjacents);
                }

                Console.WriteLine($"Adapters: {PrintPath(Nodes.Select(n => n.Value.Count <= 1 ? $"{n.Key}!" : n.Key.ToString()).ToList())}");

                SubGraphs = new List<SubGraph>();

                int currentSubGraphStart = 0;
                for (int i = 0; i < Nodes.Count; i++)
                {
                    int current = Nodes.ElementAt(i).Key;
                    int next = Nodes.ElementAt(Math.Min(i + 1, (Nodes.Count - 1))).Key;
                    int difference = next - current;

                    if (difference == 3)
                    {
                        SubGraphs.Add(new SubGraph
                        {
                            Start = currentSubGraphStart,
                            End = current,
                            NumberOfPaths = 0
                        });

                        currentSubGraphStart = next;
                    }
                }
            }

            public double GetAllPaths()
            {
                int i = 0;
                foreach (var subGraph in SubGraphs)
                {
                    i++;
                    Console.WriteLine($"Searching subGraph {i} ({subGraph.Start} --> {subGraph.End})");
                    GetAllPaths(subGraph, subGraph.Start, subGraph.End, new List<int>() { subGraph.Start });
                    Console.WriteLine($"Found {subGraph.NumberOfPaths} paths.");
                }

                List<int> paths = SubGraphs.Select(s => s.NumberOfPaths).ToList();
                double result = 1;
                foreach (var subGraph in SubGraphs)
                {
                    result *= subGraph.NumberOfPaths;
                }
                return result;
            }

            private void GetAllPaths(SubGraph graph, int start, int end, List<int> localPath)
            {
                // Reached destination
                if (start == end)
                {
                    Console.WriteLine($"Found {PrintPath(localPath)}");
                    graph.NumberOfPaths++;
                    return;
                }

                // Recur
                foreach (int adjacent in Nodes[start].Where(a => a <= end))
                {
                    // Store the current node in the path
                    localPath.Add(adjacent);
                    GetAllPaths(graph, adjacent, end, localPath);

                    // Remove current node from path
                    localPath.Remove(adjacent);
                }
            }

            private string PrintPath(List<int> localPath)
            {
                return $"[{string.Join(", ", localPath)}]";
            }

            private string PrintPath(List<string> localPath)
            {
                return $"[{string.Join(", ", localPath)}]";
            }

            private void MarkAsVisited(Dictionary<int, bool> visited, int node)
            {
                if (!visited.ContainsKey(node))
                {
                    visited.Add(node, true);
                }
                else
                {
                    visited[node] = true;
                }
            }

            private bool IsVisited(Dictionary<int, bool> visited, int node)
            {
                if (!visited.ContainsKey(node))
                {
                    return false;
                }
                else
                {
                    return visited[node];
                }
            }
        }

        private class SubGraph
        {
            public int Start { get; set; }
            public int End { get; set; }
            public int NumberOfPaths { get; set; }
        }
    }
}
