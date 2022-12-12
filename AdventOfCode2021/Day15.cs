using AdventOfCode;
using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    public class Day15 : Day
    {
        public Day15(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var cave = new SimpleGrid<int>(input.Count(), input.First().Length);

            for(int i = 0; i < input.Count(); i++)
            {
                var line = input.ElementAt(i);
                cave.SetRow(i, line
                    .ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray());
            }

            var caveGraph = new WeightedGraph(WeightedShortestPathStrategy.Djikstra);

            for (int x = 0; x < cave.RowLength; x++)
            {
                for (int y = 0; y < cave.ColumnLength; y++)
                {
                    foreach(var point in cave.GetAdjacentCellsCoordinates(x, y, false))
                    {
                        var currentPoint = new Point(x, y);
                        caveGraph.AddEdge(currentPoint, point, cave[currentPoint], cave[point]);
                    }
                }
            }

            return caveGraph.GetShortestPath(Point.Zero, new Point(cave.RowLength - 1, cave.ColumnLength - 1)).ToString();
        }


        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var cave = new InfiniteGrid<int>(input.Count(), input.First().Length, 5, (c, p, ox, oy) => {
                var value = c[p.X, p.Y] + ox + oy;
                if(value > 9)
                {
                    value %= 9;
                }

                return value;
            });

            for (int i = 0; i < input.Count(); i++)
            {
                var line = input.ElementAt(i);
                cave.SetRow(i, line
                    .ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray());
            }

            var caveGraph = new WeightedGraph(WeightedShortestPathStrategy.Djikstra);

            for (int x = 0; x < cave.RowLength; x++)
            {
                for (int y = 0; y < cave.ColumnLength; y++)
                {
                    foreach (var point in cave.GetAdjacentCellsCoordinates(x, y, false))
                    {
                        var currentPoint = new Point(x, y);
                        caveGraph.AddEdge(currentPoint, point, cave[currentPoint], cave[point]);
                    }
                }
            }

            return caveGraph.GetShortestPath(Point.Zero, new Point(cave.RowLength - 1, cave.ColumnLength - 1)).ToString();
        }
    }
}
