using System.Diagnostics;
using System.Security;
using System.Text;

namespace AdventOfCode.y2022
{
    public class Day10 : Day
    {
        public Day10(string inputFolder) : base(inputFolder)
        { }
     
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int x = 1;
            int signalStrength = 0;
            int[] cycles = new int[] { 20, 60, 100, 140, 180, 220 };

            int wait = 0;
            int waitValue = 0;

            bool continueCycles = true;
            int currentCycle = 0;

            int currentLineIndex = 0;

            while(continueCycles)
            {
                currentCycle++;
                Debug.WriteLine("Cycle: " + currentCycle + ", X: " + x + ", Strength: " + currentCycle * x);

                if (cycles.Contains(currentCycle))
                {
                    signalStrength += currentCycle * x;
                }

                // End of the program
                if (currentLineIndex == input.Count() && wait == 0)
                {
                    continueCycles = false;
                    break;
                }

                if (wait != 0)
                {
                    wait--;

                    if (wait == 0)
                    {
                        x += waitValue;
                        waitValue = 0;
                    }

                    continue;
                }

                string line = input.ElementAt(currentLineIndex);
                currentLineIndex++;

                if (line == "noop")
                {
                    continue;
                }
                else
                {
                    wait = 1;
                    waitValue = int.Parse(line.Split(" ").Last());
                }
            }

            return signalStrength.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            StringBuilder sb = new StringBuilder();

            int x = 1;

            int wait = 0;
            int waitValue = 0;

            bool continueCycles = true;
            int currentCycle = 0;

            int currentLineIndex = 0;

            while (continueCycles)
            {
                // End of the program
                if (currentLineIndex == input.Count() && wait == 0)
                {
                    continueCycles = false;
                    break;
                }

                // Increment cycle
                currentCycle++;

                // Draw pixel
                int rowPos = (currentCycle - 1) % 40;
                if (rowPos == 0)
                {
                    sb.Append("\r\n");
                }

                if (x == rowPos || x - 1 == rowPos || x + 1 == rowPos)
                {
                    sb.Append("#");
                }
                else
                {
                    sb.Append(".");
                }

                // Execute pending instructions
                if (wait != 0)
                {
                    wait--;

                    if (wait == 0)
                    {
                        x += waitValue;
                        waitValue = 0;
                    }

                    continue;
                }

                // Execute next instruction
                string line = input.ElementAt(currentLineIndex);
                currentLineIndex++;

                if (line == "noop")
                {
                    continue;
                }
                else
                {
                    wait = 1;
                    waitValue = int.Parse(line.Split(" ").Last());
                }
            }

            return sb.ToString();
        }
    }
}
