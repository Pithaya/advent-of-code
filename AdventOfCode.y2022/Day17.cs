using AdventOfCode.Common;
using AdventOfCode.Common.Grids;

namespace AdventOfCode.y2022
{
    class Shape
    {
        public List<Point> Points { get; set; }

        public int Height { get; init; }

        public bool TryMoveRight(CellBag<char> cave, int rightWall)
        {
            List<Point> newPoints = Points.Select(p => new Point(p.X, p.Y + 1)).ToList();

            if(newPoints.Any(p => p.Y >= rightWall || cave[p] == '#'))
            {
                return false;
            }
            else
            {
                this.Points = newPoints;
                return true;
            }
        }

        public bool TryMoveLeft(CellBag<char> cave, int leftWall)
        {
            List<Point> newPoints = Points.Select(p => new Point(p.X, p.Y - 1)).ToList();

            if (newPoints.Any(p => p.Y <= leftWall || cave[p] == '#'))
            {
                return false;
            }
            else
            {
                this.Points = newPoints;
                return true;
            }
        }

        public bool TryMoveDown(CellBag<char> cave, int bottomWall)
        {
            List<Point> newPoints = Points.Select(p => new Point(p.X + 1, p.Y)).ToList();

            if (newPoints.Any(p => p.X >= bottomWall || cave[p] == '#'))
            {
                return false;
            }
            else
            {
                this.Points = newPoints;
                return true;
            }
        }

        public void SetSpawn(CellBag<char> cave)
        {
            int yOffset = 2;

            int xOffset = !cave.Cells.Any() ? 0 : cave.Cells.Keys.Min(k => k.X) - 4; // 3 spaces between this shape and highest
            xOffset -= this.Height - 1; // Shape top is at (0, 0)

            Point offset = new Point(xOffset, yOffset);

            this.Points = this.Points.Select(p => p + offset).ToList();
        }

        public bool IsHorizontallyEqualWith(Shape other)
        {
            if(this.Points.Count != other.Points.Count || this.Height != other.Height)
            {
                return false;
            }

            foreach(var (point, index) in Points.WithIndex())
            {
                if(point.Y != other.Points.ElementAt(index).Y)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Day17 : Day
    {
        public Day17(string inputFolder) : base(inputFolder)
        { }

        /// <summary>
        /// Returns the shape for the current step.
        /// The top left of the shape is at 0,0.
        /// </summary>
        /// <param name="step">The current step.</param>
        /// <returns>The points of the shape.</returns>
        private Shape GetShapeForStep(int step)
        {
            if(step % 5 == 0)
            {
                return new Shape()
                {
                    Points = new List<Point>()
                    {
                        new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3)
                    },
                    Height = 1
                };
            }
            else if(step % 5 == 1)
            {
                return new Shape()
                {
                    Points = new List<Point>()
                    {
                        new Point(0, 1),
                        new Point(1, 0), new Point(1, 1), new Point(1, 2),
                        new Point(2, 1),
                    },
                    Height = 3
                };
            }
            else if(step % 5 == 2)
            {
                return new Shape()
                {
                    Points = new List<Point>()
                    {
                        new Point(0, 2),
                        new Point(1, 2),
                        new Point(2, 0), new Point(2, 1), new Point(2, 2),
                    },
                    Height = 3
                };
            }
            else if(step % 5 == 3)
            {
                return new Shape()
                {
                    Points = new List<Point>()
                    {
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(2, 0),
                        new Point(3, 0),
                    },
                    Height = 4
                };
            }
            else if(step % 5 == 4)
            {
                return new Shape()
                {
                    Points = new List<Point>()
                    {
                        new Point(0, 0), new Point(0, 1),
                        new Point(1, 0), new Point(1, 1),
                    },
                    Height = 2
                };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private Shape GetShapeForStep(ulong step)
        {
            return GetShapeForStep((int)(step % 5));
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int fallenRocks = 0;
            CellBag<char> cave = new CellBag<char>('.');
            char[] gasJets = input.Single().ToArray();
            int currentJet = 0;

            // Start the simulation with the first shape spawning at 0,0
            int leftWall = -1;
            int righWall = 7;
            int bottomWall = 4;

            string debug;

            while(fallenRocks < 2022)
            {
                Shape currentShape = GetShapeForStep(fallenRocks);
                currentShape.SetSpawn(cave);

                //debug = DrawState(cave, currentShape, leftWall, righWall, bottomWall);

                while (true)
                {
                    char direction = gasJets[currentJet];
                    currentJet = (currentJet + 1) % gasJets.Length;

                    if (direction == '<')
                    {
                        currentShape.TryMoveLeft(cave, leftWall);
                    }
                    else
                    {
                        currentShape.TryMoveRight(cave, righWall);
                    }

                    //debug = DrawState(cave, currentShape, leftWall, righWall, bottomWall);

                    if (!currentShape.TryMoveDown(cave, bottomWall))
                    {
                        // We can no longer fall
                        currentShape.Points.ForEach(p => cave[p] = '#');
                        fallenRocks++;
                        break;
                    }
                }
            }

            IEnumerable<int> caveRows = cave.Cells.Keys.Select(p => p.X);
            return ((caveRows.Max() - caveRows.Min()) + 1).ToString();
        }

        private string DrawState(CellBag<char> cave, Shape currentShape, int leftWall, int righWall, int bottomWall)
        {
            if (!cave.Cells.Any())
            {
                return string.Empty;
            }

            return cave.Print((Point p) =>
            {
                if (currentShape.Points.Contains(p))
                {
                    return '@';
                }

                if (p.X >= bottomWall)
                {
                    return '-';
                }

                if (p.Y <= leftWall || p.Y >= righWall)
                {
                    return '|';
                }

                return cave[p];
            }, 20);
        }

        private string DrawShapes(List<Shape> shapes)
        {
            CellBag<char> bag = new CellBag<char>('.');

            foreach (Shape shape in shapes)
            {
                foreach(Point point in shape.Points)
                {
                    bag[point] = '#';
                }
            }

            return bag.Print(c => c);
        }

        private int GetHeight(List<Point> points)
        {
            List<int> rows = points.Select(p => p.X).ToList();
            return ((rows.Max() - rows.Min()) + 1);
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            ulong fallenRocks = 0;
            CellBag<char> cave = new CellBag<char>('.');
            char[] gasJets = input.Single().ToArray();
            int currentJet = 0;

            List<Shape> history = new List<Shape>();
            List<Shape>? pattern = null;
            bool appliedPattern = false;
            ulong patternOffset = 0;

            // Start the simulation with the first shape spawning at 0,0
            int leftWall = -1;
            int righWall = 7;
            int bottomWall = 4;

            string debug;

            while (fallenRocks < 1000000000000)
            {
                if(pattern != null && !appliedPattern)
                {
                    appliedPattern = true;

                    ulong remainingShapes = 1000000000000 - (ulong)fallenRocks;
                    ulong remainingPatterns = remainingShapes / (ulong)pattern.Count;

                    ulong skippedShapes = remainingPatterns * (ulong)pattern.Count;
                    fallenRocks += skippedShapes;
                    //currentJet = (int)(((ulong)currentJet + skippedShapes) % (ulong)gasJets.Length);

                    patternOffset = (ulong)GetHeight(pattern.SelectMany(s => s.Points).ToList()) * remainingPatterns;

                    foreach (var shape in pattern)
                    {
                        foreach(var point in shape.Points)
                        {
                            cave[point] = '#';
                        }
                    }
                }

                Shape currentShape = GetShapeForStep(fallenRocks);
                currentShape.SetSpawn(cave);

                //debug = DrawState(cave, currentShape, leftWall, righWall, bottomWall);

                while (true)
                {
                    char direction = gasJets[currentJet];
                    currentJet = (currentJet + 1) % gasJets.Length;

                    if (direction == '<')
                    {
                        currentShape.TryMoveLeft(cave, leftWall);
                    }
                    else
                    {
                        currentShape.TryMoveRight(cave, righWall);
                    }

                    //debug = DrawState(cave, currentShape, leftWall, righWall, bottomWall);

                    if (!currentShape.TryMoveDown(cave, bottomWall))
                    {
                        // We can no longer fall
                        currentShape.Points.ForEach(p => cave[p] = '#');
                        fallenRocks++;

                        history.Add(currentShape);

                        // Search for patterns every 10000 blocks
                        if(pattern == null && fallenRocks % 6000 == 0)
                        {
                            pattern = SearchPattern(history);
                        }

                        break;
                    }
                }
            }

            IEnumerable<int> caveRows = cave.Cells.Keys.Select(p => p.X);
            ulong result = ((ulong)(caveRows.Max() - caveRows.Min()) + 1) + patternOffset;
            return result.ToString();
        }

        private List<Shape>? SearchPattern(List<Shape> history)
        {
            int blockSize = 1;
            int blockCount = 3; // Search for x continuous blocks

            while (blockSize * blockCount <= history.Count)
            {
                List<List<Shape>> blocks = new List<List<Shape>>();

                for(int currentBlockIndex = 1; currentBlockIndex <= blockCount; currentBlockIndex++)
                {
                    List<Shape> block = history
                        .Skip(history.Count - (blockSize * currentBlockIndex))
                        .Take(blockSize)
                        .ToList();

                    blocks.Add(block);
                }

                if (HaveSameShapes(blocks))
                {
                    return blocks.First();
                }
                else
                {
                    blockSize++;
                }
            }

            return null;
        }

        private bool HaveSameShapes(List<List<Shape>> blocks)
        {
            for(int shapeIndex = 0; shapeIndex < blocks.First().Count; shapeIndex++)
            {
                List<Shape> shapes = blocks.Select(list => list.ElementAt(shapeIndex)).ToList();

                foreach(var shape in shapes.Skip(1))
                {
                    if (!shape.IsHorizontallyEqualWith(shapes.First()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
