using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{
    class Valve
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Neighbours { get; set; } = new List<string>();
        public int FlowRate { get; set; }
    }

    public class Day16 : Day
    {
        public Day16(string inputFolder) : base(inputFolder)
        { }

        private Regex inputRegex = new Regex("Valve (.+) has flow rate=(\\d+); tunnel(?:s)* lead(?:s)* to valve(?:s)* (.+,*)+");

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
            WeightedGraph<string> graph = new WeightedGraph<string>(WeightedShortestPathStrategy.Djikstra);
            
            foreach(string line in input) 
            {
                var matches = inputRegex.Matches(line);
                Group[] groups = matches.Single().Groups.Values.Skip(1).ToArray();

                Valve current = new Valve()
                {
                    Name = groups[0].Value,
                    FlowRate = int.Parse(groups[1].Value),
                    Neighbours = groups[2].Value.Split(", ").ToList()
                };

                valves.Add(current.Name, current);

                foreach(string n in current.Neighbours)
                {
                    graph.AddEdge(current.Name, n, 1, 1);
                }
            }

            // Precalculate distances between nodes
            Dictionary<string, Dictionary<string, int>> distances = new Dictionary<string, Dictionary<string, int>>();
            foreach(Valve firstValve in valves.Values.Where(v => v.Name == "AA" ? true : v.FlowRate != 0))
            {
                distances.Add(firstValve.Name, new Dictionary<string, int>());

                foreach (Valve secondValve in valves.Values.Where(v => (v.Name == "AA" ? true : v.FlowRate != 0) && v.Name != firstValve.Name))
                {
                    int distance;

                    if (distances.ContainsKey(secondValve.Name))
                    {
                        // Distance already calculated
                        distance = distances[secondValve.Name][firstValve.Name];
                    }
                    else
                    {
                        distance = graph.GetShortestPath(firstValve.Name, secondValve.Name);
                    }

                    distances[firstValve.Name][secondValve.Name] = distance;
                }
            }

            int totalFlowRate = RecurseTunnels(valves["AA"], distances, valves, new HashSet<string>(), 0, 0);

            return totalFlowRate.ToString();
        }

        private int RecurseTunnels(Valve currentValve, Dictionary<string, Dictionary<string, int>> distances, Dictionary<string, Valve> valves, HashSet<string> openedValves, int totalFlow, int currentTime)
        {
            if(currentTime >= 30)
            {
                // Out of time
                return totalFlow;
            }

            // Add this valve flow
            totalFlow += (30 - currentTime) * currentValve.FlowRate;
            openedValves.Add(currentValve.Name);

            List<Valve> possibleNextDestinations = valves.Values.Where(v => v.FlowRate != 0 && !openedValves.Contains(v.Name)).ToList();

            if (possibleNextDestinations.Count == 0)
            {
                // No more valve to open, we can wait
                return totalFlow;
            }

            return possibleNextDestinations
                .Select(v => RecurseTunnels(v, distances, valves, new HashSet<string>(openedValves), totalFlow, currentTime + distances[currentValve.Name][v.Name] + 1))
                .Max();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
