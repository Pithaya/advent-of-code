using AdventOfCode.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{
    class Valve
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Neighbours { get; set; } = new List<string>();
        public int FlowRate { get; set; }
    }

    class TravelerState
    {
        public Valve CurrentValve { get; set; }

        public int TimeOfArrival { get; set; }

        public int Index { get; set; }

        public TravelerState Clone()
        {
            return new TravelerState
            {
                Index = Index,
                CurrentValve = CurrentValve,
                TimeOfArrival = TimeOfArrival,
            };
        }
    }

    class TupleComparer : IEqualityComparer<(string, string)>
    {
        public bool Equals((string, string) x, (string, string) y)
        {
            return (x.Item1 == y.Item1 && x.Item2 == y.Item2) || (x.Item1 == y.Item2 && x.Item1 == y.Item2);
        }

        public int GetHashCode([DisallowNull] (string, string) obj)
        {
            return obj.GetHashCode();
        }
    }

    [DayNumber(16)]
    public class Day16 : Day
    {
        private Regex inputRegex = new Regex("Valve (.+) has flow rate=(\\d+); tunnel(?:s)* lead(?:s)* to valve(?:s)* (.+,*)+");

        private int part2CurrentMaxFlow = 0;

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
            Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
            WeightedGraph<string> graph = new WeightedGraph<string>(WeightedShortestPathStrategy.Djikstra);

            foreach (string line in input)
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

                foreach (string n in current.Neighbours)
                {
                    graph.AddEdge(current.Name, n, 1, 1);
                }
            }

            // Precalculate distances between nodes
            Dictionary<string, Dictionary<string, int>> distances = new Dictionary<string, Dictionary<string, int>>();
            foreach (Valve firstValve in valves.Values.Where(v => v.Name == "AA" ? true : v.FlowRate != 0))
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

            var travelers = new List<TravelerState>()
            {
                new TravelerState()
                {
                    Index = 1,
                    CurrentValve = valves["AA"],
                    TimeOfArrival = 0,
                },
                new TravelerState()
                {
                    Index = 2,
                    CurrentValve = valves["AA"],
                    TimeOfArrival = 0,
                }
            };

            var visistedPairs = new HashSet<(string, string)>(new TupleComparer());

            int totalFlowRate = RecurseTunnelsWithElephant(travelers, distances, valves, new HashSet<string>(), visistedPairs, 0, 0, string.Empty);

            return totalFlowRate.ToString();
        }

        // TODO: The brute force is way too long :/
        private int RecurseTunnelsWithElephant(List<TravelerState> travelers, Dictionary<string, Dictionary<string, int>> distances, Dictionary<string, Valve> valves, HashSet<string> openedValves, HashSet<(string, string)> visitedPairs, int totalFlow, int currentTime, string debug)
        {
            if (currentTime >= 26)
            {
                // Out of time
                if(totalFlow > this.part2CurrentMaxFlow)
                {
                    part2CurrentMaxFlow = totalFlow;
                }

                return totalFlow;
            }

            var currentTraveler = travelers.Where(t => t.TimeOfArrival == currentTime).First();

            // Add this valve flow
            totalFlow += (26 - currentTime) * currentTraveler.CurrentValve.FlowRate;

            openedValves.Add(currentTraveler.CurrentValve.Name);

            debug += $"\n{currentTraveler.Index} got to {currentTraveler.CurrentValve.Name} at {currentTime}, total flow: {totalFlow};";

            // Not enough flow no matter where we go
            if(totalFlow + GetMaxPossibleFlow(valves, openedValves, currentTime) <= this.part2CurrentMaxFlow)
            {
                return part2CurrentMaxFlow;
            }

            List<Valve> possibleNextDestinations = valves.Values
                .Where(v => v.FlowRate != 0 && !openedValves.Contains(v.Name) && !travelers.Any(t => t.CurrentValve == v))
                .ToList();

            if (possibleNextDestinations.Count == 0)
            {
                // No more valve to open, this traveler can wait
                var newCurrentTraveler = currentTraveler.Clone();
                newCurrentTraveler.TimeOfArrival = 26;

                var otherTraveler = travelers.First(t => t != currentTraveler).Clone();

                var nextTravelerStates = new List<TravelerState>()
                {
                    newCurrentTraveler, otherTraveler
                };

                // Go to arrival time of the next traveler
                return RecurseTunnelsWithElephant(
                    nextTravelerStates, 
                    distances, 
                    valves, 
                    new HashSet<string>(openedValves),
                    new HashSet<(string, string)>(visitedPairs),
                    totalFlow, 
                    otherTraveler.TimeOfArrival, 
                    debug + $"\n{currentTraveler.Index} got to {currentTraveler.CurrentValve.Name}, stopping;");
            }

            List<int> pathResults = new List<int>();

            foreach(Valve v in possibleNextDestinations.OrderByDescending(v => v.FlowRate))
            {
                // Update current traveler state
                var newCurrentTraveler = currentTraveler.Clone();
                newCurrentTraveler.CurrentValve = v;
                newCurrentTraveler.TimeOfArrival = currentTime + distances[currentTraveler.CurrentValve.Name][v.Name] + 1;

                var otherTraveler = travelers.First(t => t != currentTraveler).Clone();

                var nextTravelerStates = new List<TravelerState>()
                {
                    newCurrentTraveler, otherTraveler
                };

                // Go to arrival time of the next traveler
                var result = RecurseTunnelsWithElephant(
                    nextTravelerStates,
                    distances,
                    valves,
                    new HashSet<string>(openedValves),
                    new HashSet<(string, string)>(visitedPairs),
                    totalFlow,
                    nextTravelerStates.Min(t => t.TimeOfArrival),
                    debug + $"\n{currentTraveler.Index} go from {currentTraveler.CurrentValve.Name} to {newCurrentTraveler.CurrentValve.Name} (Arrival at {newCurrentTraveler.TimeOfArrival});");

                pathResults.Add(result);
            }

            return pathResults.Any() ? pathResults.Max() : 0;
        }

        private int GetMaxPossibleFlow(Dictionary<string, Valve> valves, HashSet<string> openedValves, int currentTime)
        {
            // Flow if we open all remaining valves right now
            return valves
                .Where(v => !openedValves.Contains(v.Key))
                .Sum(v => (26 - currentTime) * v.Value.FlowRate);
        }
    }
}
