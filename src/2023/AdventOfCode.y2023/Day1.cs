using AdventOfCode.Common;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2023
{
    [DayNumber(1)]
    public class Day1 : Day
    {
        private readonly Dictionary<string, int> numbers = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        public Day1(IFileSystem? fileSystem = null) : base(fileSystem)
        {
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var total = 0;

            foreach (var line in input)
            {
                var firstDigit = line.First(char.IsDigit);
                var lastDigit = line.Last(char.IsDigit);
                total += int.Parse($"{firstDigit}{lastDigit}");
            }

            return total.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var total = 0;
            Regex regex = new Regex(@"(?=(\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)))");

            foreach (var line in input)
            {
                var matches = regex.Matches(line);
                var firstDigit = ParseDigit(matches.First().Groups.Values.First(v => !string.IsNullOrWhiteSpace(v.Value)).Value);
                var lastDigit = ParseDigit(matches.Last().Groups.Values.First(v => !string.IsNullOrWhiteSpace(v.Value)).Value);
                total += int.Parse($"{firstDigit}{lastDigit}");
            }

            return total.ToString();
        }

        private int ParseDigit(string number)
        {
            if (int.TryParse(number, out var digit))
            {
                return digit;
            }

            return numbers[number];
        }
    }
}
