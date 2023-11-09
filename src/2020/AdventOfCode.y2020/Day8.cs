using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(8)]
    public class Day8 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int acc = 0;
            HashSet<int> executed = new HashSet<int>();
            for (int i = 0; i < input.Count(); i++)
            {
                if (executed.Contains(i))
                {
                    break;
                }

                executed.Add(i);
                string instruction = input.ElementAt(i).Split(' ').First();
                if (instruction == "nop")
                {
                    continue;
                }
                else if (instruction == "jmp")
                {
                    int value = int.Parse(input.ElementAt(i).Split(' ').Last());
                    i += (value - 1);
                }
                else if (instruction == "acc")
                {
                    int value = int.Parse(input.ElementAt(i).Split(' ').Last());
                    acc += value;
                }
            }

            return acc.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            bool foundAnswer = false;

            // Replace nop with jump
            IEnumerable<int> nops = input.Select((n, i) => (n, i)).Where(x => x.n.Contains("nop")).Select(x => x.i).ToList();
            foreach (int nopIndex in nops)
            {
                List<string> currentInput = new List<string>(input);
                currentInput[nopIndex] = currentInput[nopIndex].Replace("nop", "jmp");
                (int acc, bool terminatedCorrectly) = ExecuteProgram(currentInput);
                if (terminatedCorrectly)
                {
                    foundAnswer = true;
                    return acc.ToString();
                }
            }

            // Replace jmp with nop
            IEnumerable<int> jmps = input.Select((n, i) => (n, i)).Where(x => x.n.Contains("jmp")).Select(x => x.i).ToList();
            foreach (int jmpIndex in jmps)
            {
                List<string> currentInput = new List<string>(input);
                currentInput[jmpIndex] = currentInput[jmpIndex].Replace("jmp", "nop");
                (int acc, bool terminatedCorrectly) = ExecuteProgram(currentInput);
                if (terminatedCorrectly)
                {
                    foundAnswer = true;
                    return acc.ToString();
                }
            }
            
            return string.Empty;
        }

        private static (int, bool) ExecuteProgram(IEnumerable<string> input)
        {
            int acc = 0;
            bool terminatedCorrectly = true;
            HashSet<int> executed = new HashSet<int>();

            for (int i = 0; i < input.Count(); i++)
            {
                if (executed.Contains(i))
                {
                    terminatedCorrectly = false;
                    break;
                }

                executed.Add(i);
                string instruction = input.ElementAt(i).Split(' ').First();
                if (instruction == "nop")
                {
                    continue;
                }
                else if (instruction == "jmp")
                {
                    int value = int.Parse(input.ElementAt(i).Split(' ').Last());
                    i += (value - 1);
                }
                else if (instruction == "acc")
                {
                    int value = int.Parse(input.ElementAt(i).Split(' ').Last());
                    acc += value;
                }
            }

            return (acc, terminatedCorrectly);
        }
    }
}
