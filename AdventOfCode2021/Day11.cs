using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    public class Day11 : Day
    {
        public Day11(string inputFolder) : base(inputFolder)
        { }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            OctopusGrid grid = new OctopusGrid(input.Count(), input.First().Count());

            for(int i = 0; i < input.Count(); i++)
            {
                grid.SetRow(i, input
                    .ElementAt(i)
                    .ToCharArray()
                    .Select(s => int.Parse(s.ToString()))
                    .ToArray());
            }

            for (int i = 0; i < 100; i++)
            {
                grid.DoStep();
            }

            return grid.Flashes.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            OctopusGrid grid = new OctopusGrid(input.Count(), input.First().Count());

            for (int i = 0; i < input.Count(); i++)
            {
                grid.SetRow(i, input
                    .ElementAt(i)
                    .ToCharArray()
                    .Select(s => int.Parse(s.ToString()))
                    .ToArray());
            }

            int step = 0;
            while(true)
            {
                grid.DoStep();
                step++;

                bool allZeroes = true;

                for (int rowIndex = 0; rowIndex < grid.Cells.GetLength(0); rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < grid.Cells.GetLength(0); columnIndex++)
                    {
                        if(grid.Cells[rowIndex, columnIndex] != 0)
                        {
                            allZeroes = false;
                        }
                    }
                }

                if (allZeroes)
                {
                    return step.ToString();
                }
            }

            return "Step not found.";
        }
    }

    class OctopusGrid : Grid<int>
    {
        public int Flashes { get; set; } = 0;

        public OctopusGrid(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
        {
        }


        public void DoStep()
        {
            // Increase all energy by 1
            for (int rowIndex = 0; rowIndex < cells.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < cells.GetLength(0); columnIndex++)
                {
                    cells[rowIndex, columnIndex]++;
                }
            }

            // Flash
            for (int rowIndex = 0; rowIndex < cells.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < cells.GetLength(0); columnIndex++)
                {
                    if(cells[rowIndex, columnIndex] > 9)
                    {
                        cells[rowIndex, columnIndex] = 0;
                        Flashes++;
                        FlashAdjacentCells(rowIndex, columnIndex);
                    }
                }
            }
        }

        private void FlashAdjacentCells(int rowIndex, int columnIndex)
        {
            for(int i = Math.Clamp(rowIndex - 1, 0, cells.GetLength(0)); i < Math.Clamp(rowIndex + 2, 0, cells.GetLength(0)); i++)
            {
                for (int y = Math.Clamp(columnIndex - 1, 0, cells.GetLength(1)); y < Math.Clamp(columnIndex + 2, 0, cells.GetLength(1)); y++)
                {
                    if(cells[i, y] != 0)
                    {
                        cells[i, y]++;
                    }

                    if (cells[i, y] > 9)
                    {
                        cells[i, y] = 0;
                        Flashes++;
                        FlashAdjacentCells(i, y);
                    }
                }
            }
        }
    }
}
