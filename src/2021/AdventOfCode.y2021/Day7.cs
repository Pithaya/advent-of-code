using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(7)]
    public class Day7 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<int> crabPositions = input
                .First()
                .Split(",")
                .Select(i => int.Parse(i))
                .ToList();

            var median = crabPositions.Median();

            var totalFuel = crabPositions
                .Select(c => Math.Abs(c - median))
                .Sum();

            return totalFuel.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<int> crabPositions = input
                .First()
                .Split(",")
                .Select(i => int.Parse(i))
                .ToList();

            var average = Math.Round(crabPositions.Average());

            var totalFuel = crabPositions
                .Select(c => Convert.ToInt32(Math.Abs(c - average)))
                .Select(step => Enumerable.Range(1, step))
                .Select(fuels => fuels.Sum())
                .Sum();

            return totalFuel.ToString();
        }
    }

    public static class ListExtensions
    {
        public static double Median(this IEnumerable<int> values)
        {
            int count = values.Count();
            int halfIndex = values.Count() / 2;
            var sortedValues = values.OrderBy(n => n);
            double median;

            if ((count % 2) == 0)
            {
                median = (sortedValues.ElementAt(halfIndex) + sortedValues.ElementAt(halfIndex - 1)) / 2;
            }
            else
            {
                median = sortedValues.ElementAt(halfIndex);
            }

            return median;
        }
    }
}
