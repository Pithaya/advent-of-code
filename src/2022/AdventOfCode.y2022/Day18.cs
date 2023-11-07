using AdventOfCode.Common;
using AdventOfCode.Common.Grids;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2022
{
    [DayNumber(18)]
    public class Day18 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            CellBag3D<bool> points = new CellBag3D<bool>(false);

            foreach(string line in input)
            {
                int[] values = line.Split(',').Select(c => int.Parse(c)).ToArray();
                points[new Point3D(values[0], values[1], values[2])] = true;
            }

            int count = 0;

            foreach(var point in points.Cells.Keys)
            {
                count += points.GetAdjacentCellsCoordinates(point, false).Count(neighbour => !points[neighbour]);
            }

            return count.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            CellBag3D<char> points = new CellBag3D<char>('.');

            foreach (string line in input)
            {
                int[] values = line.Split(',').Select(c => int.Parse(c)).ToArray();
                points[new Point3D(values[0], values[1], values[2])] = '#';
            }

            var minPoint = new Point3D(
                points.Cells.Keys.Min(p => p.X),
                points.Cells.Keys.Min(p => p.Y),
                points.Cells.Keys.Min(p => p.Z));

            var maxPoint = new Point3D(
               points.Cells.Keys.Max(p => p.X),
               points.Cells.Keys.Max(p => p.Y),
               points.Cells.Keys.Max(p => p.Z));

            points.MinBounds = minPoint - new Point3D(2, 2, 2); 
            points.MaxBounds = maxPoint + new Point3D(2, 2, 2);

            var filledPoints = FloodFill(minPoint - new Point3D(1, 1, 1), points);

            int count = 0;

            foreach (var point in filledPoints)
            {
                count += points.GetAdjacentCellsCoordinates(point, false).Count(neighbour => points[neighbour] == '#');
            }

            return count.ToString();
        }

        private HashSet<Point3D> FloodFill(Point3D start, CellBag3D<char> points)
        {
            Queue<Point3D> queue = new Queue<Point3D>();
            HashSet<Point3D> visitedPoints = new HashSet<Point3D>();

            queue.Enqueue(start);
            visitedPoints.Add(start);

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();

                var adjacentPoints = points.GetAdjacentCellsCoordinates(point, false);

                foreach (var neighbour in adjacentPoints.Where(p => !visitedPoints.Contains(p) && points[p] == '.'))
                {
                    queue.Enqueue(neighbour);
                    visitedPoints.Add(neighbour);
                }
            }

            return visitedPoints;
        }
    }
}
