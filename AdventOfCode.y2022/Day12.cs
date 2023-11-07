using AdventOfCode.Common;
using AdventOfCode.Common.Graphs.Unweighted;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2022
{
    [DayNumber(12)]
    public class Day12 : Day
    {
        private int GetHeight(char c)
        {
            return c switch
            {
                'S' => GetHeight('a'),
                'E' => GetHeight('z'),
                _ => c - 97
            };
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            SimpleGrid<int> grid = new SimpleGrid<int>(input.Count(), input.First().Count());

            Point start = Point.Zero;
            Point end = Point.Zero;

            foreach ((string line, int index) in input.WithIndex())
            {
                grid.SetRow(index, line.Select(GetHeight).ToArray());

                foreach(var (c, i) in line.WithIndex())
                {
                    if(c == 'S')
                    {
                        start = new Point(index, i);
                    }
                    else if(c == 'E')
                    {
                        end = new Point(index, i);
                    }
                }
            }

            UnweightedGraph graph = grid.ToUnweightedGraph((int currentValue, int neighborValue) => neighborValue <= currentValue + 1);

            return graph.GetShortestPath(start, end).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            SimpleGrid<int> grid = new SimpleGrid<int>(input.Count(), input.First().Count());

            int shortest = int.MaxValue;
            List<Point> start = new List<Point>();
            Point end = Point.Zero;

            foreach ((string line, int index) in input.WithIndex())
            {
                var values = line.Select(GetHeight).ToArray();

                grid.SetRow(index, values);

                foreach (var (c, i) in line.WithIndex())
                {
                    if (c == 'S' || c == 'a')
                    {
                        start.Add(new Point(index, i));
                    }
                }

                foreach (var (c, i) in line.WithIndex())
                {
                    if (c == 'E')
                    {
                        end = new Point(index, i);
                    }
                }
            }

            UnweightedGraph graph = grid.ToUnweightedGraph((int currentValue, int neighborValue) => neighborValue <= currentValue + 1);

            foreach(var startPoint in start)
            {
                var path = graph.GetShortestPath(startPoint, end);

                if(path < shortest)
                {
                    shortest = path;
                }
            }

            return shortest.ToString();
        }
    }
}
