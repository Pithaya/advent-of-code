using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    public class Day8 : Day
    {
        public Day8(string inputFolder) : base(inputFolder)
        { }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            HashSet<Point> visibleTrees = new HashSet<Point>();
            var trees = new SimpleGrid<short>(input.Count(), input.First().Length);

            foreach(var (line, index) in input.WithIndex())
            {
                var values = line.Select(c => short.Parse(c.ToString())).ToArray();
                trees.SetRow(index, values);
            }

            int maxTreeHeight = 0;

            // Get trees visible for each row
            for(int rowIndex = 0; rowIndex < trees.RowLength; rowIndex++)
            {
                var row = trees.GetRow(rowIndex);

                short firstTreeHeight = row.First();
                short lastTreeHeight = row.Last();

                // Mark edges as visible
                visibleTrees.Add(new Point(rowIndex, 0));
                visibleTrees.Add(new Point(rowIndex, row.Length - 1));

                // Start left
                maxTreeHeight = firstTreeHeight;

                for(int columnIndex = 1; columnIndex < row.Length - 1; columnIndex++)
                {
                    short currentTreeHeight = row[columnIndex];

                    if (currentTreeHeight > maxTreeHeight)
                    {
                        maxTreeHeight = currentTreeHeight;
                        visibleTrees.Add(new Point(rowIndex, columnIndex));
                    }
                }

                // Start right
                maxTreeHeight = lastTreeHeight;

                for (int columnIndex = row.Length - 2; columnIndex > 0; columnIndex--)
                {
                    short currentTreeHeight = row[columnIndex];

                    if (currentTreeHeight > maxTreeHeight)
                    {
                        maxTreeHeight = currentTreeHeight;
                        visibleTrees.Add(new Point(rowIndex, columnIndex));
                    }
                }
            }

            // Get trees visible for each column
            // Skip first and last colmuns, since row edges have already been marked
            for (int columnIndex = 1; columnIndex < trees.ColumnLength - 1; columnIndex++)
            {
                var column = trees.GetColumn(columnIndex);

                short firstTreeHeight = column.First();
                short lastTreeHeight = column.Last();

                // Mark edges as visible
                visibleTrees.Add(new Point(0, columnIndex));
                visibleTrees.Add(new Point(column.Length - 1, columnIndex));

                // Start top
                maxTreeHeight = firstTreeHeight;

                for (int rowIndex = 1; rowIndex < column.Length - 1; rowIndex++)
                {
                    short currentTreeHeight = column[rowIndex];

                    if (currentTreeHeight > maxTreeHeight)
                    {
                        maxTreeHeight = currentTreeHeight;
                        visibleTrees.Add(new Point(rowIndex, columnIndex));
                    }
                }

                // Start bottom
                maxTreeHeight = lastTreeHeight;

                for (int rowIndex = column.Length - 2; rowIndex > 0; rowIndex--)
                {
                    short currentTreeHeight = column[rowIndex];

                    if (currentTreeHeight > maxTreeHeight)
                    {
                        maxTreeHeight = currentTreeHeight;
                        visibleTrees.Add(new Point(rowIndex, columnIndex));
                    }
                }
            }

            return visibleTrees.Count.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var trees = new SimpleGrid<short>(input.Count(), input.First().Length);

            foreach (var (line, index) in input.WithIndex())
            {
                var values = line.Select(c => short.Parse(c.ToString())).ToArray();
                trees.SetRow(index, values);
            }

            ulong maxScenicScore = 0;

            // Skip edges as they will always have a score of zero
            for (int rowIndex = 1; rowIndex < trees.RowLength - 1; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex < trees.ColumnLength - 1; columnIndex++)
                {
                    ulong treeScore = GetTreeScenicScore(trees, new Point(rowIndex, columnIndex));
                    if(treeScore > maxScenicScore)
                    {
                        maxScenicScore = treeScore;
                    }
                }
            }

            return maxScenicScore.ToString();
        }

        private ulong GetTreeScenicScore(SimpleGrid<short> trees, Point treePosition)
        {
            var treeRowIndex = treePosition.X;
            var treeColumnIndex = treePosition.Y;
            var treeHeight = trees[treeRowIndex][treeColumnIndex];

            // Up
            var upScore = 0;
            for (int i = treeRowIndex - 1; i >= 0; i--)
            {
                var currentTreeHeight = trees[i][treeColumnIndex];

                upScore++;

                if(currentTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            // Down
            var downScore = 0;
            for (int i = treeRowIndex + 1; i < trees.ColumnLength; i++)
            {
                var currentTreeHeight = trees[i][treeColumnIndex];

                downScore++;

                if (currentTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            // Left
            var leftScore = 0;
            for (int i = treeColumnIndex - 1; i >= 0; i--)
            {
                var currentTreeHeight = trees[treeRowIndex][i];

                leftScore++;

                if (currentTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            // Right
            var rightScore = 0;
            for (int i = treeColumnIndex + 1; i < trees.RowLength; i++)
            {
                var currentTreeHeight = trees[treeRowIndex][i];

                rightScore++;

                if (currentTreeHeight >= treeHeight)
                {
                    break;
                }
            }

            return (ulong)(upScore * downScore * leftScore * rightScore);
        }
    }
}
