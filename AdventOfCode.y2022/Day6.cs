using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    public class Day6 : Day
    {
        public Day6(string inputFolder) : base(inputFolder)
        { }
     
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            string buffer = input.Single();

            for(int i = 3; i < buffer.Length; i++)
            {
                // Take the last four characters, i included
                char[] lastFourChars = buffer.Skip(i - 3).Take(4).ToArray();

                if(lastFourChars.Distinct().Count() == 4)
                {
                    return (i + 1).ToString();
                }
            }

            return "No result found.";
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            string buffer = input.Single();

            for (int i = 13; i < buffer.Length; i++)
            {
                // Take the last fourteen characters, i included
                char[] lastFourChars = buffer.Skip(i - 13).Take(14).ToArray();

                if (lastFourChars.Distinct().Count() == 14)
                {
                    return (i + 1).ToString();
                }
            }

            return "No result found.";
        }
    }
}
