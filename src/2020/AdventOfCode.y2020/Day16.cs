using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(16)]
    public class Day16 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int result = 0;

            List<int> invalidNumbers = new List<int>();
            List<Func<int, bool>> rules = new List<Func<int, bool>>();
            List<int> values = new List<int>();

            bool finishedRules = false;
            bool finishedMyTicket = false;

            foreach (var line in input)
            {
                if (!finishedRules)
                {
                    if (line == string.Empty)
                    {
                        finishedRules = true;
                        continue;
                    }

                    string ranges = line.Split(":").Last();
                    string firstRange = ranges.Split("or").First();
                    string lastRange = ranges.Split("or").Last();
                    int firstRangeFirst = int.Parse(firstRange.Split("-").First());
                    int firstRangeSecond = int.Parse(firstRange.Split("-").Last());
                    int secondRangeFirst = int.Parse(lastRange.Split("-").First());
                    int secondRangeSecond = int.Parse(lastRange.Split("-").Last());

                    rules.Add(x => x >= firstRangeFirst && x <= firstRangeSecond);
                    rules.Add(x => x >= secondRangeFirst && x <= secondRangeSecond);

                    continue;
                }

                if (!finishedMyTicket)
                {
                    if (line == string.Empty)
                    {
                        finishedMyTicket = true;
                        continue;
                    }

                    continue;
                }

                if (line.Contains("nearby"))
                {
                    continue;
                }

                values.AddRange(line.Split(",").Select(s => int.Parse(s)));
            }

            invalidNumbers = values.Where(v => rules.All(r => !r(v))).ToList();

            return invalidNumbers.Sum().ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<string, Func<int, bool>> rules = new Dictionary<string, Func<int, bool>>();
            Ticket myTicket = new Ticket(new List<int>());
            List<Ticket> nearbyTickets = new List<Ticket>();
            Dictionary<int, List<string>> possibleRulesForIndex = new Dictionary<int, List<string>>();

            bool finishedRules = false;
            bool finishedMyTicket = false;

            foreach (var line in input)
            {
                if (!finishedRules)
                {
                    if (line == string.Empty)
                    {
                        finishedRules = true;
                        continue;
                    }

                    string name = line.Split(":").First();
                    string ranges = line.Split(":").Last();
                    string firstRange = ranges.Split("or").First();
                    string lastRange = ranges.Split("or").Last();
                    int firstRangeFirst = int.Parse(firstRange.Split("-").First());
                    int firstRangeSecond = int.Parse(firstRange.Split("-").Last());
                    int secondRangeFirst = int.Parse(lastRange.Split("-").First());
                    int secondRangeSecond = int.Parse(lastRange.Split("-").Last());

                    rules.Add(name, x => (x >= firstRangeFirst && x <= firstRangeSecond) || (x >= secondRangeFirst && x <= secondRangeSecond));

                    continue;
                }

                if (!finishedMyTicket)
                {
                    if (line.Contains("ticket"))
                    {
                        continue;
                    }

                    if (line == string.Empty)
                    {
                        finishedMyTicket = true;
                        continue;
                    }

                    myTicket = new Ticket(line.Split(",").Select(s => int.Parse(s)).ToList());

                    continue;
                }

                if (line.Contains("nearby"))
                {
                    continue;
                }

                nearbyTickets.Add(new Ticket(line.Split(",").Select(s => int.Parse(s)).ToList()));
            }

            // remove invalid tickets
            nearbyTickets.RemoveAll(v => v.Values.Any(ticketValue => rules.All(r => !r.Value(ticketValue))));

            // Search for rule index
            foreach (var rule in rules)
            {
                for (int i = 0; i < nearbyTickets.First().Values.Count; i++)
                {
                    if (nearbyTickets.All(t => rule.Value(t.Values.ElementAt(i))))
                    {
                        if (!possibleRulesForIndex.ContainsKey(i))
                        {
                            possibleRulesForIndex.Add(i, new List<string>());
                        }
                        possibleRulesForIndex[i].Add(rule.Key);
                    }
                }
            }

            // Consolidate
            while (possibleRulesForIndex.Any(i => i.Value.Count > 1))
            {
                List<string> uniqueRulesPossibility = possibleRulesForIndex.Where(i => i.Value.Count == 1).SelectMany(p => p.Value).ToList();
                foreach (var rule in possibleRulesForIndex.Where(i => i.Value.Count > 1))
                {
                    rule.Value.RemoveAll(v => uniqueRulesPossibility.Contains(v));
                }
            }

            if (possibleRulesForIndex.Count() != possibleRulesForIndex.SelectMany(p => p.Value).Distinct().Count())
            {
                throw new InvalidOperationException();
            }

            // Find result
            int result = 1;
            var ticketRules = possibleRulesForIndex.Where(r => r.Value.First().Contains("departure")).Select(r => r.Key).ToList();
            var myTicketValues = myTicket.Values.Select((v, i) => (v, i)).Where(x => ticketRules.Contains(x.i));
            foreach (var ri in myTicketValues)
            {
                result *= ri.v;
            }

            return result.ToString();
        }

        public class Ticket
        {
            public List<int> Values { get; set; }

            public Ticket(List<int> values)
            {
                this.Values = values;
            }
        }
    }
}
