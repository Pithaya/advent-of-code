using AdventOfCode.Common;
using AdventOfCode.Common.Grids;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2022
{
    [DayNumber(14)]
    public class Day14 : Day
    {
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


        private Point GetFinalDestination(Point position, CellBag<char> cave, HashSet<Point> sandAtRest, int caveBottom)
        {
            bool couldntMove = true;

            // Try to go straight down

            // Get wall cells
            List<Point> columnCells = cave.GetColumnCells(position.Y).Select(c => c.Key).ToList();

            // Add bottom
            columnCells.Add(new Point(caveBottom, position.Y));

            // Add fallen sand
            columnCells.AddRange(sandAtRest.Where(s => s.Y == position.Y));

            Point nearestBelowPoint = columnCells.Where(p => p.X > position.X).OrderBy(p => p.X).First();

            if (nearestBelowPoint.X != position.X + 1)
            {
                couldntMove = false;
                position = new Point(nearestBelowPoint.X - 1, nearestBelowPoint.Y);
            }

            // Try to go diagonally left
            // Go diagonally until diagonal is blocked (try to go right) or space below is empty (start over)

            Point firstAvailableSpace = new Point(position.X + 1, position.Y - 1);

            while (true)
            {
                if (IsAvailable(firstAvailableSpace, cave, sandAtRest, caveBottom))
                {
                    couldntMove = false;
                    position = firstAvailableSpace;

                    Point below = new Point(firstAvailableSpace.X + 1, firstAvailableSpace.Y);

                    // If we can start falling below
                    if (IsAvailable(below, cave, sandAtRest, caveBottom))
                    {
                        // Start over
                        return GetFinalDestination(position, cave, sandAtRest, caveBottom);
                    }

                    firstAvailableSpace = new Point(firstAvailableSpace.X + 1, firstAvailableSpace.Y - 1);
                    continue;
                }
                else
                {
                    break;
                }
            }

            // Go right

            firstAvailableSpace = new Point(position.X + 1, position.Y + 1);

            while (true)
            {
                if (IsAvailable(firstAvailableSpace, cave, sandAtRest, caveBottom))
                {
                    couldntMove = false;
                    position = firstAvailableSpace;

                    Point below = new Point(firstAvailableSpace.X + 1, firstAvailableSpace.Y);

                    if (IsAvailable(below, cave, sandAtRest, caveBottom))
                    {
                        // Start over
                        return GetFinalDestination(position, cave, sandAtRest, caveBottom);
                    }

                    firstAvailableSpace = new Point(firstAvailableSpace.X + 1, firstAvailableSpace.Y + 1);
                    continue;
                }
                else
                {
                    break;
                }
            }

            if (couldntMove)
            {
                // Stuck: This is our final position
                return position;
            }
            else
            {
                // Start over
                return GetFinalDestination(position, cave, sandAtRest, caveBottom);
            }
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

            while (true)
            {
                string caveState = cave.Print((Point p) =>
                {
                    if (sandAtRest.Contains(p))
                    {
                        return 'o';
                    }
                    else if(p.X > caveBottomRow)
                    {
                        return '#';
                    }
                    else
                    {
                        return cave[p];
                    }
                }, 10);
                
                Point destination = GetFinalDestination(sandSource, cave, sandAtRest, caveBottomRow);
                sandAtRest.Add(destination);

                if (destination == sandSource)
                {
                    break;
                }
            }

            return sandAtRest.Count.ToString();
        }
    }
}
