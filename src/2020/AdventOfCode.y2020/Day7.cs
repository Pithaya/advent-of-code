using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020
{
    [DayNumber(7)]
    public class Day7 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<string, HashSet<string>> bags = new Dictionary<string, HashSet<string>>();
            foreach (string i in input)
            {
                string bagColor = i.Split("contain").First().Replace("bags", "").Trim();

                bags.Add(bagColor, new HashSet<string>());
                string otherBags = i.Split("contain").Last().Trim();
                if (otherBags.Contains("no other"))
                {
                    continue;
                }

                foreach (string bagAndQuantity in otherBags.Split(',').Select(s => s.Trim()))
                {
                    Regex regex = new Regex("(?<=[0-9] )(.*)(?= bag(s)?)");
                    string color = regex.Match(bagAndQuantity).Value;
                    bags[bagColor].Add(color);
                }
            }

            List<string> validColors = new List<string>();
            foreach (var rule in bags)
            {
                SearchForColor(rule.Key, rule.Value, new List<string>(), validColors, bags);
            }

            return validColors.Distinct().Count().ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();
            foreach (string i in input)
            {
                string bagColor = i.Split("contain").First().Replace("bags", "").Trim();

                bags.Add(bagColor, new Dictionary<string, int>());
                string otherBags = i.Split("contain").Last().Trim();
                if (otherBags.Contains("no other"))
                {
                    continue;
                }

                foreach (string bagAndQuantity in otherBags.Split(',').Select(s => s.Trim()))
                {
                    Regex regex = new Regex("(?<=[0-9] )(.*)(?= bag(s)?)");
                    int quantity = int.Parse(bagAndQuantity.Split(' ').First());
                    string color = regex.Match(bagAndQuantity).Value;
                    bags[bagColor].Add(color, quantity);
                }
            }

            int numberOfBags = CountForColor("shiny gold", 1, bags) - 1;

            return numberOfBags.ToString();
        }

        private static void SearchForColor(string currentColor, HashSet<string> childColors, List<string> parentColors, List<string> validColors, Dictionary<string, HashSet<string>> bags)
        {
            if (currentColor == "shiny gold")
            {
                validColors.AddRange(parentColors);
                return;
            }
            else
            {
                parentColors.Add(currentColor);
                foreach (string child in childColors)
                {
                    if (bags.ContainsKey(child))
                    {
                        SearchForColor(child, bags[child], new List<string>(parentColors), validColors, bags);
                    }
                }
            }
        }

        private static int CountForColor(string currentColor, int currentQuantity, Dictionary<string, Dictionary<string, int>> bags)
        {
            int totalBags = 0;
            Dictionary<string, int> currentChildren = bags[currentColor];
            totalBags += currentQuantity;

            foreach (var child in currentChildren)
            {
                if (bags.ContainsKey(child.Key))
                {
                    totalBags += currentQuantity * CountForColor(child.Key, child.Value, bags);
                }
            }

            return totalBags;
        }
    }
}
