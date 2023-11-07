using AdventOfCode.Common;
using AdventOfCode.Common.Models;
using System.Text;

namespace AdventOfCode.y2022
{
    [DayNumber(9)]
    public class Day9 : Day
    {
        private Vector GetDirectionFromInput(string input)
        {
            string direction = input.Split(" ").First();

            return direction switch
            {
                "R" => new Vector(1, 0),
                "L" => new Vector(-1, 0),
                "U" => new Vector(0, 1),
                "D" => new Vector(0, -1),
                _ => throw new NotImplementedException()
            };
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Point tailPos = new Point();
            Point headPos = new Point();
            HashSet<Point> visitedPoints = new HashSet<Point>();

            foreach(string line in input)
            {
                Vector dir = GetDirectionFromInput(line);
                int steps = int.Parse(line.Split(" ").Last());

                for(int i = 0; i < steps; i++)
                {
                    visitedPoints.Add(tailPos);

                    // Move the head
                    headPos.X += dir.X;
                    headPos.Y += dir.Y;

                    Vector distance = new Vector(tailPos, headPos);

                    if (distance.Length < 2)
                    {
                        continue;
                    }

                    // Move the tail
                    Vector normalizedDistance = new Vector(
                        distance.X != 0 ? (distance.X > 0 ? 1 : -1) : 0, 
                        distance.Y != 0 ? (distance.Y > 0 ? 1 : -1) : 0);

                    tailPos.X += normalizedDistance.X;
                    tailPos.Y += normalizedDistance.Y;
                }
            }

            return visitedPoints.Count.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<PointInstance> rope = new List<PointInstance>();
            HashSet<Point> tailVisitedPoints = new HashSet<Point>();
            tailVisitedPoints.Add(new Point(0, 0));

            for (int i = 0; i < 10; i++)
            {
                rope.Add(new PointInstance(0, 0));
            }

            PointInstance head = rope.First();

            string v = Visualize("Initial", rope);

            foreach (string line in input)
            {
                Vector dir = GetDirectionFromInput(line);
                int steps = int.Parse(line.Split(" ").Last());

                for (int i = 0; i < steps; i++)
                {
                    // Move the head
                    head.X += dir.X;
                    head.Y += dir.Y;

                    // Move each knot
                    for(int ropeIndex = 1; ropeIndex < rope.Count; ropeIndex++)
                    {
                        PointInstance knot = rope[ropeIndex];

                        Point previousKnotPos = rope[ropeIndex - 1].ToPoint();
                        Point knotPos = knot.ToPoint();

                        Vector distance = new Vector(knotPos, previousKnotPos);

                        // If this knot doesn't move, ignore those that follow
                        if (distance.Length < 2)
                        {
                            break;
                        }

                        // Move the knot
                        Vector normalizedDistance = new Vector(
                            distance.X != 0 ? (distance.X > 0 ? 1 : -1) : 0,
                            distance.Y != 0 ? (distance.Y > 0 ? 1 : -1) : 0);

                        knot.X += normalizedDistance.X;
                        knot.Y += normalizedDistance.Y;

                        if (ropeIndex == 9)
                        {
                            // Tail moved
                            tailVisitedPoints.Add(knotPos);
                        }
                    }

                    v = Visualize(line + " - " + (i + 1), rope);
                }
            }

             v = Visualize("Final", tailVisitedPoints);

            // TODO: Find why the last point is missing
            return tailVisitedPoints.Count.ToString();
        }

        private string Visualize(string step, List<PointInstance> rope)
        {
            HashSet<Point> points = rope.Select(p => p.ToPoint()).ToHashSet();
            return Visualize(step, points);
        }

        private string Visualize(string step, HashSet<Point> points)
        {
            List<string> lines = new List<string>();

            int minX = points.Min(p => p.X) - 4;
            int maxX = points.Max(p => p.X) + 4;

            int minY = points.Min(p => p.Y) - 4;
            int maxY = points.Max(p => p.Y) + 4;

            int minGrid = Math.Min(minX, minY);
            int maxGrid = Math.Max(minY, maxY);

            for (int i = minGrid; i < maxGrid; i++)
            {
                lines.Add(string.Join(string.Empty, Enumerable.Range(minGrid, maxGrid - minGrid).Select<int, char>(val =>
                {
                    Point current = new Point(val, i);

                    if (current == Point.Zero)
                    {
                        return 's';
                    }

                    return points.Contains(current) ? '#' : '.';
                })));
            }

            StringBuilder builder = new();
            builder.AppendLine($" == {step} ==");

            lines.Reverse();

            foreach (var line in lines)
            {
                builder.AppendLine(line);
            }

            return builder.ToString();
        }
    }
}
