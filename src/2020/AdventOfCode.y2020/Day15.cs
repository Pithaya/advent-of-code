using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(15)]
    public class Day15 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<int> numbers = input.First().Split(',').Select(i => int.Parse(i)).ToList();
            int result = 0;

            for (int i = numbers.Count() - 1; i < 2019; i++)
            {
                int lastNumber = numbers.ElementAt(i);
                int whenLastSpoken = numbers
                    .Select((n, i) => (Number: n, Index: i))
                    .Where(x => x.Number == lastNumber && x.Index != i)
                    .DefaultIfEmpty((-1, -1))
                    .Last()
                    .Item2;

                if (whenLastSpoken == -1)
                {
                    numbers.Add(0);
                }
                else
                {
                    numbers.Add((i + 1) - (whenLastSpoken + 1));
                }
            }

            result = numbers.Last();

            return result.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<int, int> lastSpokenNumbers = input
                .First()
                .Split(',')
                .Select((n, i) => (Number: int.Parse(n), LastSpoken: i))
                .ToDictionary(x => x.Number, x => x.LastSpoken);

            int lastSpoken = lastSpokenNumbers.Last().Key;
            lastSpokenNumbers.Remove(lastSpoken);

            for (int i = lastSpokenNumbers.Count(); i < (30000000 - 1); i++)
            {
                // Never spoken before
                if (!lastSpokenNumbers.ContainsKey(lastSpoken))
                {
                    lastSpokenNumbers.Add(lastSpoken, i);
                    lastSpoken = 0;
                }
                else
                {
                    int nextLastSpoken = (i + 1) - (lastSpokenNumbers[lastSpoken] + 1);
                    lastSpokenNumbers[lastSpoken] = i;
                    lastSpoken = nextLastSpoken;
                }
            }

            return lastSpoken.ToString();
        }
    }
}
