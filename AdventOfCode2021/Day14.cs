using AdventOfCode;
using System.Text;

namespace AdventOfCode.y2021
{
    public class Day14 : Day
    {
        public Day14(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var polymer = input.First();
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            foreach(var line in input.Skip(2))
            {
                var pair = line.Split(" -> ").First();
                var value = line.Split(" -> ").Last();

                pairs.Add(pair, value);
            }

            var sb = new StringBuilder();
            for(int i = 0; i < 10; i++)
            {
                sb.Append(polymer[0]);

                for (int polymerIndex = 0; polymerIndex < polymer.Length - 1; polymerIndex++)
                {
                    var firstChar = polymer[polymerIndex];
                    var secondChar = polymer[polymerIndex + 1];
                    var currentPair = $"{polymer[polymerIndex]}{polymer[polymerIndex + 1]}";

                    sb.Append(pairs[currentPair]);
                    sb.Append(secondChar);
                }

                polymer = sb.ToString();
                sb.Clear();
            }

            Dictionary<char, int> letterOccurence = polymer
                .ToCharArray()
                .GroupBy(c => c)
                .ToDictionary(c => c.Key, c => c.Count());

            return (letterOccurence.Values.Max() - letterOccurence.Values.Min()).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<string, char> pairs = new Dictionary<string, char>();

            foreach (var line in input.Skip(2))
            {
                var pair = line.Split(" -> ").First();
                var value = line.Split(" -> ").Last().First();

                pairs.Add(pair, value);
            }

            Dictionary<string, long> pairsCount = new Dictionary<string, long>();
            Dictionary<char, long> lettersCount = new Dictionary<char, long>();

            var polymer = input.First();
            lettersCount.AddOrIncrement(polymer.First(), 1);

            for(int i = 0; i < polymer.Length - 1; i++)
            {
                var firstChar = polymer[i];
                var secondChar = polymer[i + 1];
                var currentPair = $"{polymer[i]}{polymer[i + 1]}";

                lettersCount.AddOrIncrement(secondChar, 1);
                pairsCount.AddOrIncrement(currentPair, 1);
            }

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> pairChanges = new Dictionary<string, long>();

                foreach(var pair in pairsCount)
                {
                    pairChanges.AddOrIncrement(pair.Key, -1 * pair.Value);

                    var newLetter = pairs[pair.Key];
                    lettersCount.AddOrIncrement(newLetter, pair.Value);

                    pairChanges.AddOrIncrement($"{pair.Key.First()}{newLetter}", pair.Value);
                    pairChanges.AddOrIncrement($"{newLetter}{pair.Key.Last()}", pair.Value);
                }

                foreach (var pair in pairChanges)
                {
                    pairsCount.AddOrIncrement(pair.Key, pair.Value);
                }
            }

            return (lettersCount.Values.Max() - lettersCount.Values.Min()).ToString();
        }
    }

    public static class Extensions
    {
        public static void AddOrIncrement<T>(this Dictionary<T, long> dic, T key, long value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] += value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static void AddOrIncrement<T>(this Dictionary<T, int> dic, T key, int value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] += value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
    }
}
