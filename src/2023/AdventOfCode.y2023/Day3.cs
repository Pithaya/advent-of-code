using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2023
{
    [DayNumber(3)]
    public class Day3 : Day
    {
        private readonly Regex regex = new Regex(@"\d+");

        private bool IsPartNumber(string currentLine, int currentLineIndex, string part, int partIndex, List<string> allLines)
        {
            // Check for symbol before and after
            if (partIndex != 0 && currentLine[partIndex - 1] != '.')
            {
                return true;
            }

            // Index of the last number
            var endIndex = partIndex + part.Length - 1;
            if (endIndex != currentLine.Length - 1 && currentLine[endIndex + 1] != '.')
            {
                return true;
            }

            // Check for line above
            if (currentLineIndex != 0)
            {
                var aboveLine = allLines[currentLineIndex - 1];
                var above = aboveLine[Math.Max(partIndex - 1, 0)..Math.Min(endIndex + 2, aboveLine.Length - 1)];

                if (above.Any(c => c != '.'))
                {
                    return true;
                }
            }

            // Check for line below
            if (currentLineIndex != allLines.Count - 1)
            {
                var belowLine = allLines[currentLineIndex + 1];
                var below = belowLine[Math.Max(partIndex - 1, 0)..Math.Min(endIndex + 2, belowLine.Length - 1)];

                if (below.Any(c => c != '.'))
                {
                    return true;
                }
            }

            return false;
        }

        private List<int> GetAdjacentPartNumbers(string currentLine, int currentLineIndex, int partIndex, List<string> allLines)
        {
            throw new NotImplementedException();
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var lines = input.ToList();
            int sum = 0;

            foreach (var (line, index) in lines.WithIndex())
            {
                var matches = regex.Matches(line);

                foreach (Match match in matches)
                {
                    if (IsPartNumber(line, index, match.Value, match.Index, lines))
                    {
                        sum += int.Parse(match.Value);
                    }
                }
            }

            return sum.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var lines = input.ToList();
            int sum = 0;

            foreach (var (line, index) in lines.WithIndex())
            {
                var gearsIndex = line.AllIndexesOf("*");

                foreach (var gearIndex in gearsIndex)
                {
                    List<int> partNumbers = GetAdjacentPartNumbers(line, index, gearIndex, lines);

                    if (partNumbers.Count == 2)
                    {
                        sum += partNumbers.First() * partNumbers.Last();
                    }
                }
            }

            return sum.ToString();
        }
    }
}
