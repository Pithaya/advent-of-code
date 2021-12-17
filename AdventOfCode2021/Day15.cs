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
            var cave = new Grid<int>(input.Count(), input.First().Length);

            for(int i = 0; i < input.Count(); i++)
            {
                var line = input.ElementAt(i);
                cave.SetRow(i, line
                    .ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray());
            }

            var caveGraph = new WeightedGraph();

            for (int x = 0; x < cave.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < cave.Cells.GetLength(1); y++)
                {
                    foreach(var point in cave.GetAdjacentCellsCoordinates(x, y, false))
                    {
                        var currentPoint = new Point(x, y);
                        caveGraph.AddEdge(currentPoint, point, cave[currentPoint], cave[point]);
                    }
                }
            }

            return caveGraph.GetShortestPath(Point.Zero, new Point(cave.Cells.GetLength(0) - 1, cave.Cells.GetLength(1) - 1)).ToString();
        }


        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
