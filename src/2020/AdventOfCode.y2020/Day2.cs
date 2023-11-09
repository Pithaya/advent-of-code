using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020
{
    [DayNumber(2)]
    public class Day2 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int validPasswords = 0;

            foreach (string passwordInfo in input)
            {
                IEnumerable<string> splitted = passwordInfo.Split(' ');
                string numbers = splitted.First();
                string password = splitted.Last();
                string letter = splitted.ElementAt(1).Split(':').First();

                string min = numbers.Split('-').First();
                string max = numbers.Split('-').Last();
                Regex regex = new Regex("^([^" + letter + "]*" + letter + "){" + min + "," + max + "}[^" + letter + "]*$");
                if (regex.IsMatch(password))
                {
                    validPasswords++;
                }
            }

            return validPasswords.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int validPasswords = 0;
            
            foreach (string passwordInfo in input)
            {
                IEnumerable<string> splitted = passwordInfo.Split(' ');
                char[] password = splitted.Last().ToCharArray();
                char letter = splitted.ElementAt(1).Split(':').First().ToCharArray()[0];

                string numbers = splitted.First();
                int firstIndex = int.Parse(numbers.Split('-').First());
                int secondIndex = int.Parse(numbers.Split('-').Last());
                if (password[firstIndex - 1] == letter && password[secondIndex - 1] != letter)
                {
                    validPasswords++;
                }
                else if (password[secondIndex - 1] == letter && password[firstIndex - 1] != letter)
                {
                    validPasswords++;
                }
            }

            return validPasswords.ToString();
        }
    }
}
