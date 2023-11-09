using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(5)]
    public class Day5 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int highestPassId = 0;
            int planeRows = 128;
            int planeColumn = 8;

            foreach (string pass in input)
            {
                int rangeStart = 0;
                int rangeEnd = planeRows - 1;
                for (int i = 0; i < 7; i++)
                {
                    char current = pass[i];
                    int numberOfRows = rangeEnd - rangeStart + 1;
                    int half = numberOfRows / 2;
                    if (current == 'F')
                    {
                        // lower half
                        rangeEnd -= half;
                    }
                    else
                    {
                        // upper half
                        rangeStart += half;
                    }
                }

                if (rangeStart != rangeEnd)
                {
                    throw new InvalidOperationException();
                }

                int row = rangeEnd;

                rangeStart = 0;
                rangeEnd = planeColumn - 1;
                for (int i = 7; i < 7 + 3; i++)
                {
                    char current = pass[i];
                    int numberOfColumns = rangeEnd - rangeStart + 1;
                    int half = numberOfColumns / 2;
                    if (current == 'L')
                    {
                        // lower half
                        rangeEnd -= half;
                    }
                    else
                    {
                        // upper half
                        rangeStart += half;
                    }
                }

                if (rangeStart != rangeEnd)
                {
                    throw new InvalidOperationException();
                }

                int column = rangeEnd;

                int seatId = (row * 8) + column;
                if (seatId > highestPassId)
                {
                    highestPassId = seatId;
                }
            }

            return highestPassId.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            HashSet<int> ids = new HashSet<int>();
            int[,] plane = new int[128, 8];
            foreach (string pass in input)
            {
                AddPassId(pass, plane, ids);
            }

            int myPass = 0;
            for (int row = 0; row < plane.GetLength(0); row++)
            {
                for (int col = 0; col < plane.GetLength(1); col++)
                {
                    int value = plane[row, col];
                    if (value != 0)
                    {
                        continue;
                    }

                    // check that id - 1 and id + 1 exist
                    if (ids.Contains(value - 1) && ids.Contains(value + 1))
                    {
                        myPass = value;
                    }
                }
            }

            return myPass.ToString();
        }

        private static void AddPassId(string pass, int[,] plane, HashSet<int> ids)
        {
            int planeRows = 128;
            int planeColumn = 8;

            int rangeStart = 0;
            int rangeEnd = planeRows - 1;

            for (int i = 0; i < 7; i++)
            {
                char current = pass[i];
                int numberOfRows = rangeEnd - rangeStart + 1;
                int half = numberOfRows / 2;
                if (current == 'F')
                {
                    // lower half
                    rangeEnd -= half;
                }
                else
                {
                    // upper half
                    rangeStart += half;
                }
            }

            if (rangeStart != rangeEnd)
            {
                throw new InvalidOperationException();
            }

            int row = rangeEnd;

            rangeStart = 0;
            rangeEnd = planeColumn - 1;
            for (int i = 7; i < 7 + 3; i++)
            {
                char current = pass[i];
                int numberOfColumns = rangeEnd - rangeStart + 1;
                int half = numberOfColumns / 2;
                if (current == 'L')
                {
                    // lower half
                    rangeEnd -= half;
                }
                else
                {
                    // upper half
                    rangeStart += half;
                }
            }

            if (rangeStart != rangeEnd)
            {
                throw new InvalidOperationException();
            }

            int column = rangeEnd;

            int id = (row * 8) + column;
            plane[row, column] = id;
            ids.Add(id);
        }
    }
}
