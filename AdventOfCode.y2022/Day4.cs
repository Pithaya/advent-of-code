using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    class Pair
    {
        public int FirstRangeStart { get; set; }
        public int FirstRangeEnd { get; set; }

        public int SecondRangeStart { get; set; }
        public int SecondRangeEnd { get; set; }

        public bool HasOverlap => FirstRangeStart <= SecondRangeEnd && SecondRangeStart <= FirstRangeEnd;
        public bool FirstRangeContainsSecond => RangeContains(FirstRangeStart, FirstRangeEnd, SecondRangeStart, SecondRangeEnd);
        public bool SecondRangeContainsFirst => RangeContains(SecondRangeStart, SecondRangeEnd, FirstRangeStart, FirstRangeEnd);

        private bool RangeContains(int firstRangeStart, int firstRangeEnd, int secondRangeStart, int secondRangeEnd)
        {
            return (secondRangeStart >= firstRangeStart && secondRangeStart <= firstRangeEnd) && (secondRangeEnd >= firstRangeStart && secondRangeEnd <= firstRangeEnd);
        }

        public Pair(string ranges)
        {
            var rangesSplit = ranges.Split(',');
            var firstRangeSplit = rangesSplit[0].Split("-");
            var secondtRangeSplit = rangesSplit[1].Split("-");

            FirstRangeStart = int.Parse(firstRangeSplit[0]);
            FirstRangeEnd = int.Parse(firstRangeSplit[1]);

            SecondRangeStart = int.Parse(secondtRangeSplit[0]);
            SecondRangeEnd = int.Parse(secondtRangeSplit[1]);
        }
    }

    [DayNumber(4)]
    public class Day4 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Pair> pairs = input.Select(line => new Pair(line)).ToList();

            return pairs.Count(p => p.FirstRangeContainsSecond || p.SecondRangeContainsFirst).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Pair> pairs = input.Select(line => new Pair(line)).ToList();

            return pairs.Count(p => p.HasOverlap).ToString();
        }
    }
}
