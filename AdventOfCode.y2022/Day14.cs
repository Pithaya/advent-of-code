using AdventOfCode.Common;
using AdventOfCode.Common.Grids;
using System.Reflection.Metadata;

namespace AdventOfCode.y2022
{
    public class Day14 : Day
    {
        public Day14(string inputFolder) : base(inputFolder)
        { }

        private bool IsAvailable(Point position, CellBag<char> cave, HashSet<Point> sandAtRest, int? caveBottom)
        {
            return cave[position] == '.' && !sandAtRest.Contains(position) && (caveBottom.HasValue ? position.X < caveBottom.Value : true);
        }

        private Point GetDestination(Point position, CellBag<char> cave, HashSet<Point> sandAtRest, int? caveBottom)
        {
            Point below = new Point(position.X + 1, position.Y);
            if (IsAvailable(below, cave, sandAtRest, caveBottom))
            {
                return below;
            }

            Point belowLeft = new Point(position.X + 1, position.Y - 1);
            if (IsAvailable(belowLeft, cave, sandAtRest, caveBottom))
            {
                return belowLeft;
            }

            Point belowRight = new Point(position.X + 1, position.Y + 1);
            if (IsAvailable(belowRight, cave, sandAtRest, caveBottom))
            {
                return belowRight;
            }

            // Can't move
            return position;
        }

        private CellBag<char> CreateCave(IEnumerable<string> input)
        {
            CellBag<char> cave = new CellBag<char>('.');

            // Add all rocks
            foreach (string line in input)
            {
                List<Point> linePoints = line
                    .Split(" -> ")
                    .Select((string p) =>
                    {
                        return new Point()
                        {
                            X = int.Parse(p.Split(",").Last()),
                            Y = int.Parse(p.Split(",").First()),
                        };
                    })
                    .ToList();

                for (int i = 1; i < linePoints.Count; i++)
                {
                    Point previous = linePoints[i - 1];
                    Point current = linePoints[i];

                    for (int x = Math.Min(previous.X, current.X); x <= Math.Max(previous.X, current.X); x++)
                    {
                        for (int y = Math.Min(previous.Y, current.Y); y <= Math.Max(previous.Y, current.Y); y++)
                        {
                            cave[new Point(x, y)] = '#';
                        }
                    }
                }
            }

            return cave;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            CellBag<char> cave = CreateCave(input);

            Point sandSource = new Point(0, 500);
            int caveBottomRow = cave.Cells.Keys.Select(k => k.X).Max();
            HashSet<Point> sandAtRest = new HashSet<Point>();

            Point currentSandPosition = sandSource;

            while (true)
            {
                Point destination = GetDestination(currentSandPosition, cave, sandAtRest, null);

                if(destination == currentSandPosition)
                {
                    // At rest
                    sandAtRest.Add(currentSandPosition);
                    currentSandPosition = sandSource;
                    continue;
                }
                else if(destination.X > caveBottomRow) 
                {
                    // Going to abyss
                    break;
                }
                else
                {
                    currentSandPosition = destination;
                }
            }

            return sandAtRest.Count.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            CellBag<char> cave = CreateCave(input);

            Point sandSource = new Point(0, 500);
            int caveBottomRow = cave.Cells.Keys.Select(k => k.X).Max() + 2;
            HashSet<Point> sandAtRest = new HashSet<Point>();

            Point currentSandPosition = sandSource;

            while (true)
            {
                string caveState = cave.Print((Point p) =>
                {
                    if (sandAtRest.Contains(p) || currentSandPosition == p)
                    {
                        return 'o';
                    }
                    else
                    {
                        return cave[p];
                    }
                });

                Point destination = GetDestination(currentSandPosition, cave, sandAtRest, caveBottomRow);

                // Can't move
                if (destination == currentSandPosition)
                {
                    if(currentSandPosition == sandSource)
                    {
                        sandAtRest.Add(currentSandPosition);
                        break;
                    }

                    // At rest
                    sandAtRest.Add(currentSandPosition);
                    currentSandPosition = sandSource;
                    continue;
                }
                else
                {
                    currentSandPosition = destination;
                }
            }

            return sandAtRest.Count.ToString();
        }
    }
}
