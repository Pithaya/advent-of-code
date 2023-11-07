using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(12)]
    public class Day12 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<string, List<string>> paths = new Dictionary<string, List<string>>();

            foreach(var line in input)
            {
                string start = line.Split("-").First();
                string end = line.Split("-").Last();

                if (!paths.ContainsKey(start))
                {
                    paths.Add(start, new List<string>());
                }

                if (!paths.ContainsKey(end))
                {
                    paths.Add(end, new List<string>());
                }

                paths[start].Add(end);
                paths[end].Add(start);
            }

            var startPaths = paths["start"];
            int pathCount = 0;
            foreach(var possiblePath in startPaths)
            {
                VisitPath(paths, possiblePath, new HashSet<string>() { "start" }, ref pathCount);
            }

            return pathCount.ToString();
        }

        private void VisitPath(Dictionary<string, List<string>> paths, string currentPlace, HashSet<string> pathHistory, ref int pathCount)
        {
            if(currentPlace == "end")
            {
                pathCount++;
                return;
            }

            var possibleSubPaths = paths[currentPlace]
                .Where(p => p.All(char.IsUpper) ? true : !pathHistory.Contains(p))
                .ToList();

            foreach (var subPath in possibleSubPaths)
            {
                VisitPath(paths, subPath, new HashSet<string>(pathHistory) { currentPlace }, ref pathCount);
            }
        }

        private void VisitPath(Dictionary<string, List<string>> paths, string currentPlace, Dictionary<string, int> pathHistory, ref int pathCount)
        {
            if (currentPlace == "end")
            {
                pathCount++;
                return;
            }

            var newHistory = new Dictionary<string, int>(pathHistory);

            if (!newHistory.ContainsKey(currentPlace))
            {
                newHistory.Add(currentPlace, 1);
            }
            else
            {
                newHistory[currentPlace]++;
            }

            var possibleSubPaths = paths[currentPlace]
                .Where(p => IsPathVisitable(p, newHistory))
                .ToList();

            foreach (var subPath in possibleSubPaths)
            {
                VisitPath(paths, subPath, newHistory, ref pathCount);
            }
        }

        private bool IsPathVisitable(string path, Dictionary<string, int> visitedPath)
        {
            if(path == "start")
            {
                return false;
            }

            if (path.All(char.IsLower))
            {
                var alreadyTwiceVisited = visitedPath
                    .Where(kvp => kvp.Key.All(char.IsLower))
                    .Any(kvp => kvp.Value == 2);

                if (alreadyTwiceVisited)
                {
                    return !visitedPath.ContainsKey(path);
                }
                else
                {
                    return !visitedPath.ContainsKey(path) || visitedPath[path] < 2; 
                }
            }

            // Uppercase
            return true;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<string, List<string>> paths = new Dictionary<string, List<string>>();

            foreach (var line in input)
            {
                string start = line.Split("-").First();
                string end = line.Split("-").Last();

                if (!paths.ContainsKey(start))
                {
                    paths.Add(start, new List<string>());
                }

                if (!paths.ContainsKey(end))
                {
                    paths.Add(end, new List<string>());
                }

                paths[start].Add(end);
                paths[end].Add(start);
            }

            var startPaths = paths["start"];
            int pathCount = 0;
            foreach (var possiblePath in startPaths)
            {
                VisitPath(paths, possiblePath, new Dictionary<string, int>() { { "start", 1 } }, ref pathCount);
            }

            return pathCount.ToString();
        }
    }
}
