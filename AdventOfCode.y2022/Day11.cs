using System.Linq;

namespace AdventOfCode.y2022
{
    class Item
    {
        public int WorryLevel { get; set; }
    }

    class Monkey
    {
        public List<Item> StartingItems { get; set; } = new List<Item>();
        public Func<int, int> InspectionOperation { get; set; }
        public Func<int, int> DecideThrow { get; set; }

        public int InspectedItems { get; set; } = 0;
    }

    public class Day11 : Day
    {
        public Day11(string inputFolder) : base(inputFolder)
        { }

        private List<Monkey> ParseMonkeys(IEnumerable<string> input)
        {
            List<Monkey> result = new List<Monkey>();
            Monkey currentMonkey = null!;

            for(int i = 0; i < input.Count(); i++)
            {
                string line = input.ElementAt(i);

                if (line.StartsWith("Monkey"))
                {
                    currentMonkey = new Monkey();
                    result.Add(currentMonkey);
                }
                else if (line.Contains("Starting items:"))
                {
                    currentMonkey.StartingItems = line
                        .Split(":")
                        .Last()
                        .Split(", ")
                        .Select(i => new Item { WorryLevel = int.Parse(i) })
                        .ToList();
                }
                else if (line.Contains("Operation:"))
                {
                    var parts = line.Split("Operation: new = ").Last().Split(" ");
                    string secondValue = parts.Last();

                    if (parts[1] == "*")
                    {
                        if(secondValue == "old") 
                        {
                            currentMonkey.InspectionOperation = (val) => val * val;
                        }
                        else
                        {
                            int secondValueParsed = int.Parse(secondValue);
                            currentMonkey.InspectionOperation = (val) => val * secondValueParsed;
                        }
                    }
                    else
                    {
                        if (secondValue == "old")
                        {
                            currentMonkey.InspectionOperation = (val) => val + val;
                        }
                        else
                        {
                            int secondValueParsed = int.Parse(secondValue);
                            currentMonkey.InspectionOperation = (val) => val + secondValueParsed;
                        }
                    }
                }
                else if (line.Contains("Test:"))
                {
                    int division = int.Parse(line.Split("Test: divisible by ").Last());
                    int trueMonkey = int.Parse(input.ElementAt(i + 1).Split("throw to monkey ").Last());
                    int falseMonkey = int.Parse(input.ElementAt(i + 2).Split("throw to monkey ").Last());

                    currentMonkey.DecideThrow = (val) => val % division == 0 ? trueMonkey : falseMonkey;

                    i += 3;
                }
                else if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
            }

            return result;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Monkey> monkeys = ParseMonkeys(input);
            int rounds = 20;

            for(int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    List<Item> itemsToRemove = new List<Item>();

                    foreach (var item in monkey.StartingItems)
                    {
                        // Inspect
                        item.WorryLevel = monkey.InspectionOperation(item.WorryLevel);
                        monkey.InspectedItems++;

                        // Divide
                        item.WorryLevel = (int)Math.Round(item.WorryLevel / (decimal)3, MidpointRounding.ToZero);

                        // Throw
                        int targetMonkey = monkey.DecideThrow(item.WorryLevel);
                        monkeys.ElementAt(targetMonkey).StartingItems.Add(item);
                        itemsToRemove.Add(item);
                    }

                    itemsToRemove.ForEach(item => monkey.StartingItems.Remove(item));
                }
            }

            return monkeys.Select(m => m.InspectedItems).OrderByDescending(i => i).Take(2).Aggregate((agg, curr) => agg * curr).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
