using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(1)]
    public class Day1 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<int> parsed = input.Select(int.Parse).ToList();

            HashSet<int> values = new HashSet<int>();

            foreach (int value in parsed)
            {
                int complement = 2020 - value;
                if (values.Contains(complement))
                {
                    return (value * complement).ToString();
                }
                else
                {
                    if (!values.Contains(value))
                    {
                        values.Add(value);
                    }
                }
            }

            return string.Empty;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<int> parsed = input.Select(int.Parse).ToList();

            foreach (int value1 in parsed)
            {
                foreach (int value2 in parsed)
                {
                    foreach (int value3 in parsed)
                    {
                        if (value1 + value2 + value3 == 2020)
                        {
                            return (value1 * value2 * value3).ToString();
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}
