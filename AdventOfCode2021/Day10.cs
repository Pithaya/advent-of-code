using System.Text.RegularExpressions;

namespace AdventOfCode.y2021
{
    public class Day10 : Day
    {
        public Day10(string inputFolder) : base(inputFolder)
        { }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<char, int> scores = new Dictionary<char, int>()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };

            var closingTags = scores.Keys;
            int score = 0;

            var tagsRegex = new Regex(@"({})|(<>)|(\[\])|(\(\))");
            foreach(var line in input)
            {
                string value = line;
                while (tagsRegex.IsMatch(value))
                {
                    value = tagsRegex.Replace(value, string.Empty);
                }

                char firstClosing = value.FirstOrDefault(c => closingTags.Contains(c));

                if(firstClosing == 0)
                {
                    // line is incomplete
                    continue;
                }

                score += scores[firstClosing];
            }

            return score.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<char, ulong> scores = new Dictionary<char, ulong>()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
            };

            Dictionary<char, char> tags = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            var closingTags = scores.Keys;
            List<ulong> totalScores = new List<ulong>();

            var tagsRegex = new Regex(@"({})|(<>)|(\[\])|(\(\))");
            foreach (var line in input)
            {
                string value = line;
                while (tagsRegex.IsMatch(value))
                {
                    value = tagsRegex.Replace(value, string.Empty);
                }

                if(value.Any(c => closingTags.Contains(c)))
                {
                    // line is corrupted
                    continue;
                }

                var endTags = value
                    .Reverse()
                    .Select(c => tags[c])
                    .ToList();

                ulong score = 0;

                foreach(var tag in endTags)
                {
                    score *= 5;
                    score += scores[tag];
                }

                totalScores.Add(score);
            }

            totalScores = totalScores.OrderBy(t => t).ToList();

            return totalScores.ElementAt(totalScores.Count / 2).ToString();
        }
    }
}
