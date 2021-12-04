namespace AdventOfCode2021
{
    public class DayTwo : BaseDay
    {
        public override string ExecutePartOne(string file)
        {
            IEnumerable<(string Command, int Amount)> input = ReadLines(nameof(DayTwo), file)
                .Select(s => s.Split(" "))
                .Select(s => (Command: s[0], Amount: int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;

            foreach(var command in input)
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

        public override string ExecutePartTwo(string file)
        {
            IEnumerable<(string Command, int Amount)> input = ReadLines(nameof(DayTwo), file)
                .Select(s => s.Split(" "))
                .Select(s => (Command: s[0], Amount: int.Parse(s[1])));

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (var command in input)
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
