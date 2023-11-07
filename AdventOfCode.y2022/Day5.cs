using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{
    class MoveCommand
    {
        public int CrateCount { get; set; }
        public int SourceIndex { get; set; }
        public int DestinationIndex { get; set; }

        public MoveCommand(string command)
        {
            Regex regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
            var results = regex.Matches(command).First().Groups;

            CrateCount = int.Parse(results[1].Value);
            SourceIndex = int.Parse(results[2].Value) - 1;
            DestinationIndex = int.Parse(results[3].Value) - 1;
        }
    }

    [DayNumber(5)]
    public class Day5 : Day
    {
        /// <summary>
        /// Returns a list of crates, each crate composed of 3 char.
        /// </summary>
        private IEnumerable<char[]> GetCrates(string line)
        {
            return line.Where((l, index) => (index + 1) % 4 != 0).Chunk(3);
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int stackCount = GetCrates(input.First()).Count();
            Stack<char>[] stacks = new Stack<char>[stackCount];

            List<char>[] stackCrates = new List<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stackCrates[i] = new List<char>();
            }

            int separatorIndex = 0;

            // Fill stacks
            foreach (var (line, lineIndex) in input.WithIndex())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    separatorIndex = lineIndex;
                    break;
                }

                var crates = GetCrates(line);

                foreach(var (crate, stackIndex) in crates.WithIndex())
                {
                    char crateName = crate[1];

                    if(crateName == '1')
                    {
                        break;
                    }

                    if (char.IsWhiteSpace(crateName))
                    {
                        continue;
                    }

                    stackCrates[stackIndex].Add(crateName);
                }
            }

            for (int i = 0; i < stackCount; i++)
            {
                var crates = stackCrates[i];
                crates.Reverse();
                stacks[i] = new Stack<char>(crates);
            }


            // Fill moves
            List<MoveCommand> commands = input.Skip(separatorIndex + 1).Select(line => new MoveCommand(line)).ToList();

            // Move crates
            foreach(var command in commands)
            {
                for(int i = 0; i < command.CrateCount; i++)
                {
                    char crate = stacks[command.SourceIndex].Pop();
                    stacks[command.DestinationIndex].Push(crate);
                }
            }

            return string.Join(string.Empty, stacks.Select(s => s.Pop()));
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int stackCount = GetCrates(input.First()).Count();
            Stack<char>[] stacks = new Stack<char>[stackCount];

            List<char>[] stackCrates = new List<char>[stackCount];
            for (int i = 0; i < stackCount; i++)
            {
                stackCrates[i] = new List<char>();
            }

            int separatorIndex = 0;

            // Fill stacks
            foreach (var (line, lineIndex) in input.WithIndex())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    separatorIndex = lineIndex;
                    break;
                }

                var crates = GetCrates(line);

                foreach (var (crate, stackIndex) in crates.WithIndex())
                {
                    char crateName = crate[1];

                    if (crateName == '1')
                    {
                        break;
                    }

                    if (char.IsWhiteSpace(crateName))
                    {
                        continue;
                    }

                    stackCrates[stackIndex].Add(crateName);
                }
            }

            for (int i = 0; i < stackCount; i++)
            {
                var crates = stackCrates[i];
                crates.Reverse();
                stacks[i] = new Stack<char>(crates);
            }


            // Fill moves
            List<MoveCommand> commands = input.Skip(separatorIndex + 1).Select(line => new MoveCommand(line)).ToList();

            // Move crates
            foreach (var command in commands)
            {
                List<char> cratesToMove = new List<char>();
                for (int i = 0; i < command.CrateCount; i++)
                {
                    cratesToMove.Add(stacks[command.SourceIndex].Pop());
                }

                if(cratesToMove.Count > 1)
                {
                    cratesToMove.Reverse();
                }

                cratesToMove.ForEach((crate) =>
                {
                    stacks[command.DestinationIndex].Push(crate);
                });
            }

            return string.Join(string.Empty, stacks.Select(s => s.Pop()));
        }
    }
}
