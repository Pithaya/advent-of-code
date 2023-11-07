using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(2)]
    public class Day2 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            IEnumerable<(string Command, int Amount)> commands = input
                .Select(s => s.Split(" "))
                .Select(s => (Command: s[0], Amount: int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;

            foreach(var command in commands)
            {
                switch (command.Command)
                {
                    case "forward":
                        horizontalPosition += command.Amount;
                        break;
                    case "up":
                        depth -= command.Amount;
                        break;
                    case "down":
                        depth += command.Amount;
                        break;
                }
            }

            return (horizontalPosition * depth).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            IEnumerable<(string Command, int Amount)> commands = input
                .Select(s => s.Split(" "))
                .Select(s => (Command: s[0], Amount: int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (var command in commands)
            {
                switch (command.Command)
                {
                    case "forward":
                        horizontalPosition += command.Amount;
                        depth += aim * command.Amount;
                        break;
                    case "up":
                        aim -= command.Amount;
                        break;
                    case "down":
                        aim += command.Amount;
                        break;
                }
            }

            return (horizontalPosition * depth).ToString();
        }
    }
}
