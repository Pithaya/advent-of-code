using AdventOfCode.Common;
using AdventOfCode.Common.Models;
using System.Text;

namespace AdventOfCode.y2021
{
    [DayNumber(13)]
    public class Day13 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Point> points = new List<Point>();

            int foldStart = 0;
            for(int i = 0; i < input.Count(); i++)
            {
                var line = input.ElementAt(i);
                if (string.IsNullOrWhiteSpace(line))
                {
                    foldStart = i;
                    break;
                }

                points.Add(new Point { X = int.Parse(line.Split(",").First()), Y = int.Parse(line.Split(",").Last()) });
            }

            foreach(var foldInstruction in input.Skip(foldStart + 1).Take(1))
            {
                var direction = foldInstruction.Split(" ").Last();

                var axis = direction.Split("=").First();
                var coordinate = int.Parse(direction.Split("=").Last());

                List<Point> pointsToAdd = new List<Point>();
                List<Point> pointsToRemove = new List<Point>();

                Func<Point, int> coordinateSelector;
                Func<Point, int, Point> coordinateSetter;

                if(axis == "y")
                {
                    coordinateSelector = (Point p) => p.Y;
                    coordinateSetter = (Point p, int pos) =>
                    {
                        p.Y = pos;
                        return p;
                    };
                }
                else
                {
                    coordinateSelector = (Point p) => p.X;
                    coordinateSetter = (Point p, int pos) =>
                    {
                        p.X = pos;
                        return p;
                    };
                }

                foreach (var pointToMove in points.Where(p => coordinateSelector(p) > coordinate))
                {
                    pointsToAdd.Add(GetFoldedPoint(pointToMove, coordinate, coordinateSelector, coordinateSetter));
                    pointsToRemove.Add(pointToMove);
                }

                points = points.Except(pointsToRemove).ToList();
                points.AddRange(pointsToAdd);
                points = points.Distinct().ToList();
            }

            return points
                .Count()
                .ToString();
        }

        private Point GetFoldedPoint(Point point, int coordinate, Func<Point, int> coordinateSelector, Func<Point, int, Point> coordinateSetter)
        {
            var offsetToCoordinate = coordinateSelector(point) - coordinate;
            var newPos = coordinate - offsetToCoordinate;

            var newPoint = new Point { X = point.X, Y = point.Y };
            newPoint = coordinateSetter(newPoint, newPos);

            return newPoint;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Point> points = new List<Point>();

            int foldStart = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                var line = input.ElementAt(i);
                if (string.IsNullOrWhiteSpace(line))
                {
                    foldStart = i;
                    break;
                }

                points.Add(new Point { X = int.Parse(line.Split(",").First()), Y = int.Parse(line.Split(",").Last()) });
            }

            foreach (var foldInstruction in input.Skip(foldStart + 1))
            {
                var direction = foldInstruction.Split(" ").Last();

                var axis = direction.Split("=").First();
                var coordinate = int.Parse(direction.Split("=").Last());

                List<Point> pointsToAdd = new List<Point>();
                List<Point> pointsToRemove = new List<Point>();

                Func<Point, int> coordinateSelector;
                Func<Point, int, Point> coordinateSetter;

                if (axis == "y")
                {
                    coordinateSelector = (Point p) => p.Y;
                    coordinateSetter = (Point p, int pos) =>
                    {
                        p.Y = pos;
                        return p;
                    };
                }
                else
                {
                    coordinateSelector = (Point p) => p.X;
                    coordinateSetter = (Point p, int pos) =>
                    {
                        p.X = pos;
                        return p;
                    };
                }

                foreach (var pointToMove in points.Where(p => coordinateSelector(p) > coordinate))
                {
                    pointsToAdd.Add(GetFoldedPoint(pointToMove, coordinate, coordinateSelector, coordinateSetter));
                    pointsToRemove.Add(pointToMove);
                }

                points = points.Except(pointsToRemove).ToList();
                points.AddRange(pointsToAdd);
                points = points.Distinct().ToList();
            }

            var maxX = points.Select(p => p.X).Max();
            var maxY = points.Select(p => p.Y).Max();

            var sb = new StringBuilder();
            sb.AppendLine();

            for (int y = 0; y < maxY + 1; y++)
            {
                for (int x = 0; x < maxX + 1; x++)
                {
                    var point = new Point { X = x, Y = y };
                    sb.Append(points.Contains(point) ? "#" : " ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
