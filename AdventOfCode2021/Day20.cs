using AdventOfCode.Common;
using AdventOfCode.Common.Grids;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2021
{
    [DayNumber(20)]
    public class Day20 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            bool[] algorithm = input
                .First()
                .ToCharArray()
                .Select(c => c == '#' ? true : false)
                .ToArray();

            // The void will be a square of the default value, 0 at the start
            var grid = new CellBag<bool>(false);

            int x = 0;
            foreach(var line in input.Skip(2))
            {
                int y = 0;
                foreach(char c in line.ToCharArray())
                {
                    if(c == '#')
                    {
                        grid[x, y] = true;
                    }

                    y++;
                }

                x++;
            }

            var t = grid.Print((b) => b ? '#' : '.');

            for(int i = 0; i < 2; i++)
            {
                grid = Enhance(grid, algorithm);
                t = grid.Print((b) => b ? '#' : '.');
            }

            return grid.Cells.Count(c => c.Value).ToString();
        }

        private CellBag<bool> Enhance(CellBag<bool> input, bool[] algorithm)
        {
            var previousDefaultValue = string.Join(string.Empty, Enumerable.Repeat<char>(input.DefaultValue ? '1' : '0', 9).ToList());
            var newDefaultValue = algorithm[Convert.ToInt32(previousDefaultValue, 2)];
            var output = new CellBag<bool>(newDefaultValue);

            var minX = input.Cells.Keys.MinBy(p => p.X).X;
            var maxX = input.Cells.Keys.MaxBy(p => p.X).X;

            var minY = input.Cells.Keys.MinBy(p => p.Y).Y;
            var maxY = input.Cells.Keys.MaxBy(p => p.Y).Y;

            var padding = 1;
            for (var x = minX - padding; x <= maxX + padding; x++)
            {
                for (var y = minY - padding; y <= maxY + padding; y++)
                {
                    int outputPixelIndex = GetOutputPixelIndex(input, x, y);
                    var pixelValue = algorithm[outputPixelIndex];

                    // Only keep values that are not the default
                    if(pixelValue != newDefaultValue)
                    {
                        output[x, y] = pixelValue;
                    }
                }
            }

            return output;
        }

        private int GetOutputPixelIndex(CellBag<bool> input, int x, int y)
        {
            string binary = string.Empty;

            for (int rowIndex = x - 1; rowIndex <= x + 1; rowIndex++)
            {
                for (int columnIndex = y - 1; columnIndex <= y + 1; columnIndex++)
                {
                    var point = new Point(rowIndex, columnIndex);
                    binary += input[point] ? '1' : '0';
                }
            }

            return Convert.ToInt32(binary, 2);
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            bool[] algorithm = input
                .First()
                .ToCharArray()
                .Select(c => c == '#' ? true : false)
                .ToArray();

            // The void will be a square of the default value, 0 at the start
            var grid = new CellBag<bool>(false);

            int x = 0;
            foreach (var line in input.Skip(2))
            {
                int y = 0;
                foreach (char c in line.ToCharArray())
                {
                    if (c == '#')
                    {
                        grid[x, y] = true;
                    }

                    y++;
                }

                x++;
            }

            var t = grid.Print((b) => b ? '#' : '.');

            for (int i = 0; i < 50; i++)
            {
                grid = Enhance(grid, algorithm);
                t = grid.Print((b) => b ? '#' : '.');
            }

            return grid.Cells.Count(c => c.Value).ToString();
        }
    }
}
