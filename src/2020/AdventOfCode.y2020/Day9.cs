using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(9)]
    public class Day9 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int preamble = 25;
            double[] parsed = input.ToList().Select(i => double.Parse(i)).ToArray();
            HashSet<double> currentRange;

            double foundValue = 0;
            for (int i = preamble; i < parsed.Count(); i++)
            {
                Index start = i - preamble;
                currentRange = new HashSet<double>(parsed[start..i]);

                double current = parsed[i];
                bool found = false;
                foreach (double value in currentRange)
                {
                    if (value != current / 2 && currentRange.Contains(current - value))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    // finished
                    foundValue = current;
                    break;
                }
            }

            return foundValue.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            double[] parsed = input.ToList().Select(i => double.Parse(i)).ToArray();
            double expectedSum = 10884537;
            //double expectedSum = 127;

            int startIndex = 0;
            int endIndex = -1;

            // Build sum table
            double[] sumTable = new double[parsed.Length];
            sumTable[0] = parsed[0];
            for (int i = 1; i < parsed.Count(); i++)
            {
                double current = parsed[i];
                double previousSum = sumTable[i - 1];
                double result = previousSum + current;
                sumTable[i] = result;
                if (result > expectedSum && endIndex == -1)
                {
                    endIndex = i;
                }
            }

            double foundResult = 0;
            while (true)
            {
                double rangeSum = sumTable[endIndex] - sumTable[startIndex];
                if (rangeSum == expectedSum)
                {
                    Index start = startIndex + 1;
                    double[] range = parsed[start..endIndex];
                    foundResult = range.Min() + range.Max();
                    break;
                }
                else if (rangeSum > expectedSum)
                {
                    startIndex++;
                }
                else
                {
                    endIndex++;
                }
            }

            return foundResult.ToString();
        }
    }
}
