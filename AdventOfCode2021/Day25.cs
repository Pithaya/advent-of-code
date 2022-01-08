using AdventOfCode;
using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    public class Day25 : Day
    {
        public Day25(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var grid = new InfiniteGrid<char>(input.Count(), input.First().Count(), 1, (cells, point, repeatX, repeatY) => cells[point.X, point.Y]);

            int x = 0;
            foreach(var line in input)
            {
                int y = 0;
                foreach(var c in line)
                {
                    grid[x, y] = c;
                    y++;
                }

                x++;
            }

            bool moved = true;
            int steps = 0;

            while (moved)
            {
                steps++;
                moved = false;

                // Move right facing
                var movedEast = new InfiniteGrid<char>(grid.RowLength, grid.ColumnLength, 1, (cells, point, repeatX, repeatY) => cells[point.X, point.Y]);
                movedEast.Fill('.');
                for (int rowIndex = 0; rowIndex < grid.RowLength; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < grid.ColumnLength; columnIndex++)
                    {
                        if (grid[rowIndex, columnIndex] == 'v')
                        {
                            movedEast[rowIndex, columnIndex] = 'v';
                        }
                        if (grid[rowIndex, columnIndex] == '>')
                        {
                            if(grid[rowIndex, columnIndex + 1] == '.')
                            {
                                movedEast[rowIndex, columnIndex + 1] = '>';
                                moved = true;
                            }
                            else
                            {
                                movedEast[rowIndex, columnIndex] = '>';
                            }
                        }
                    }
                }

                // Move south facing
                var movedSouth = new InfiniteGrid<char>(grid.RowLength, grid.ColumnLength, 1, (cells, point, repeatX, repeatY) => cells[point.X, point.Y]);
                movedSouth.Fill('.');
                for (int rowIndex = 0; rowIndex < grid.RowLength; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < grid.ColumnLength; columnIndex++)
                    {
                        // Use updated position
                        if (movedEast[rowIndex, columnIndex] == '>')
                        {
                            movedSouth[rowIndex, columnIndex] = '>';
                        }
                        else if(movedEast[rowIndex, columnIndex] == 'v')
                        {
                            if (movedEast[rowIndex + 1, columnIndex] == '.')
                            {
                                movedSouth[rowIndex + 1, columnIndex] = 'v';
                                moved = true;
                            }
                            else
                            {
                                movedSouth[rowIndex, columnIndex] = 'v';
                            }
                        }
                    }
                }

                grid = movedSouth;
            }

            return steps.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
