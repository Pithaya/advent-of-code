using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(11)]
    public class Day11 : Day
    {
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
                var result = grid.Print();
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

                for (int rowIndex = 0; rowIndex < grid.RowLength; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < grid.ColumnLength; columnIndex++)
                    {
                        if(grid[rowIndex, columnIndex] != 0)
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

    class OctopusGrid : SimpleGrid<int>
    {
        public int Flashes { get; set; } = 0;

        public OctopusGrid(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
        {
        }


        public void DoStep()
        {
            // Increase all energy by 1
            for (int rowIndex = 0; rowIndex < RowLength; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < ColumnLength; columnIndex++)
                {
                    cells[rowIndex, columnIndex]++;
                }
            }

            // Flash
            for (int rowIndex = 0; rowIndex < RowLength; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < ColumnLength; columnIndex++)
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
            foreach(var point in this.GetAdjacentCellsCoordinates(rowIndex, columnIndex))
            {
                if (cells[point.X, point.Y] != 0)
                {
                    cells[point.X, point.Y]++;
                }

                if (cells[point.X, point.Y] > 9)
                {
                    cells[point.X, point.Y] = 0;
                    Flashes++;
                    FlashAdjacentCells(point.X, point.Y);
                }
            }
        }
    }
}
