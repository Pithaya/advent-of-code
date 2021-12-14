using AdventOfCode;

namespace AdventOfCode.y2021
{
    public class Day1 : Day
    {
        public Day1(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            IEnumerable<int> values = input.Select(s => int.Parse(s));

            int previous = int.MaxValue;
            int increases = 0;
            foreach(int i in values)
            {
                if(i > previous)
                {
                    increases++;
                }

                previous = i;
            }

            return increases.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int[] values = input.Select(s => int.Parse(s)).ToArray();

            int previous = int.MaxValue;
            int increases = 0;
            for(int i = 0; i < values.Length - 2; i++)
            {
                int currentWindow = values[i] + values[i + 1] + values[i + 2];
                if (currentWindow > previous)
                {
                    increases++;
                }

                previous = currentWindow;
            }

            return increases.ToString();
        }
    }
}
