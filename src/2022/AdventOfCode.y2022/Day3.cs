using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    class Rucksack
    {
        public IEnumerable<char> FirstCompartment { get; set; }
        public IEnumerable<char> SecondCompartment { get; set; }

        public Rucksack(IEnumerable<char> sackContent) 
        {
            FirstCompartment = sackContent.Take(sackContent.Count() / 2);
            SecondCompartment = sackContent.TakeLast(sackContent.Count() / 2);
        }
    }

    class RucksackSet
    {
        public HashSet<char> Content { get; set; }

        public RucksackSet(IEnumerable<char> sackContent)
        {
            Content = sackContent.Distinct().ToHashSet();
        }
    }

    [DayNumber(3)]
    public class Day3 : Day
    {
        private int GetPriority(char item)
        {
            return char.IsLower(item) ? (int)item - 96 : (int)item - 38;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int sum = 0;
            List<Rucksack> sacks = input.Select(line => new Rucksack(line)).ToList();

            foreach(Rucksack sack in sacks)
            {
                char itemInBoth = sack.FirstCompartment.Intersect(sack.SecondCompartment).Single();
                sum += GetPriority(itemInBoth);
            }

            return sum.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int sum = 0;
            List<RucksackSet> sacks = input.Select(line => new RucksackSet(line)).ToList();

            IEnumerable<RucksackSet[]> groups = sacks.Chunk(3);

            foreach(var group in groups)
            {
                IEnumerable<char> commonItems = group.First().Content;

                foreach(var content in group.Skip(1).Select(s => s.Content))
                {
                    commonItems = commonItems.Intersect(content);
                }

                sum += GetPriority(commonItems.Single());
            }

            return sum.ToString();
        }
    }
}
