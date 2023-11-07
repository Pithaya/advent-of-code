using AdventOfCode.Common;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2021
{
    [DayNumber(5)]
    public class Day5 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Line> lines = new List<Line>();

            foreach(string s in input)
            {
                var startAndEnd = s.Split(" -> ");
                var start = startAndEnd
                    .First()
                    .Split(",")
                    .Select(x => int.Parse(x));
                var end = startAndEnd
                    .Last()
                    .Split(",")
                    .Select(x => int.Parse(x));

                var line = new Line
                {
                    Start = new Point
                    {
                        X = start.First(),
                        Y = start.Last()
                    },
                    End = new Point
                    {
                        X = end.First(),
                        Y = end.Last()
                    }
                };

                if(line.IsHorizontal || line.IsVertical)
                {
                    lines.Add(line);
                }
            }

            Dictionary<Point, int> points = new Dictionary<Point, int>();

            foreach(var line in lines)
            {
                Point currentPoint = line.Start;

                currentPoint.X -= line.XDirection;
                currentPoint.Y -= line.YDirection;

                while (currentPoint != line.End)
                {
                    currentPoint.X += line.XDirection;
                    currentPoint.Y += line.YDirection;

                    if (points.ContainsKey(currentPoint))
                    {
                        points[currentPoint]++;
                    }
                    else
                    {
                        points.Add(currentPoint, 1);
                    }
                }
            }

            return points.Count(p => p.Value > 1).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Line> lines = new List<Line>();

            foreach (string s in input)
            {
                var startAndEnd = s.Split(" -> ");
                var start = startAndEnd
                    .First()
                    .Split(",")
                    .Select(x => int.Parse(x));
                var end = startAndEnd
                    .Last()
                    .Split(",")
                    .Select(x => int.Parse(x));

                var line = new Line
                {
                    Start = new Point
                    {
                        X = start.First(),
                        Y = start.Last()
                    },
                    End = new Point
                    {
                        X = end.First(),
                        Y = end.Last()
                    }
                };

                lines.Add(line);
            }

            Dictionary<Point, int> points = new Dictionary<Point, int>();

            foreach (var line in lines)
            {
                Point currentPoint = line.Start;

                currentPoint.X -= line.XDirection;
                currentPoint.Y -= line.YDirection;

                while (currentPoint != line.End)
                {
                    currentPoint.X += line.XDirection;
                    currentPoint.Y += line.YDirection;

                    if (points.ContainsKey(currentPoint))
                    {
                        points[currentPoint]++;
                    }
                    else
                    {
                        points.Add(currentPoint, 1);
                    }
                }
            }

            return points.Count(p => p.Value > 1).ToString();
        }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public bool IsHorizontal => Start.X == End.X;
        public bool IsVertical => Start.Y == End.Y;

        public int XDirection
        {
            get
            {
                if(Start.X == End.X)
                {
                    return 0;
                }
                else
                {
                    return Start.X < End.X ? 1 : -1;
                }
            }
        }

        public int YDirection
        {
            get
            {
                if (Start.Y == End.Y)
                {
                    return 0;
                }
                else
                {
                    return Start.Y < End.Y ? 1 : -1;
                }
            }
        }

        public override string ToString()
        {
            return $"({Start.X}, {Start.Y}) -> ({End.X}, {End.Y})";
        }
    }
}
