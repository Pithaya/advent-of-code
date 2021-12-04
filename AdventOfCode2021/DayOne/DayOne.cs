namespace AdventOfCode2021
{
    public class DayOne : BaseDay
    {
        public override string ExecutePartOne(string file)
        {
            IEnumerable<int> input = ReadLines(nameof(DayOne), file).Select(s => int.Parse(s));

            int previous = int.MaxValue;
            int increases = 0;
            foreach(int i in input)
            {
                if(i > previous)
                {
                    increases++;
                }

                previous = i;
            }

            return increases.ToString();
        }

        public override string ExecutePartTwo(string file)
        {
            int[] input = ReadLines(nameof(DayOne), file).Select(s => int.Parse(s)).ToArray();

            int previous = int.MaxValue;
            int increases = 0;
            for(int i = 0; i < input.Length - 2; i++)
            {
                int currentWindow = input[i] + input[i + 1] + input[i + 2];
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
