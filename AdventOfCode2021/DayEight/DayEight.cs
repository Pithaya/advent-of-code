using System.Text;

namespace AdventOfCode2021
{
    public class DayEight : BaseDay
    {
        public override string ExecutePartOne(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DayEight), file);

            List<List<string>> outputValues = input
                .Select(i => i
                    .Split(" | ")
                    .Last()
                    .Split(" ")
                    .ToList())
                .ToList();

            var count = 0;
            foreach(var currentOutputValues in outputValues)
            {
                count += currentOutputValues.Count(s => s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7);
            }

            return count.ToString();
        }

        public override string ExecutePartTwo(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DayEight), file);

            var total = 0;

            foreach(var line in input)
            {
                var splitted = line.Split(" | ");
                List<string> observedValues = SplitAndOrderString(splitted.First());
                List<string> outputValues = SplitAndOrderString(splitted.Last());

                Dictionary<int, string> digits = new Dictionary<int, string>
                {
                    { 1, observedValues.Single(o => o.Length == 2) },
                    { 7, observedValues.Single(o => o.Length == 3) },
                    { 4, observedValues.Single(o => o.Length == 4) },
                    { 8, observedValues.Single(o => o.Length == 7) },
                };

                // 6 SEGMENTS ---

                var sixSegmentsDigits = observedValues.Where(o => o.Length == 6).ToList();

                // Find 6
                string one = digits[1];
                var six = sixSegmentsDigits.Single(o => !ContainsDigit(o, one));
                sixSegmentsDigits.Remove(six);
                digits.Add(6, six);

                // Find 0 and 9
                string four = digits[4];
                var nine = sixSegmentsDigits.Single(o => ContainsDigit(o, four));
                var zero = sixSegmentsDigits.Single(o => !ContainsDigit(o, four));
                digits.Add(0, zero);
                digits.Add(9, nine);

                // Set upper one segment
                char upperOneSegment = one
                    .ToList()
                    .Single(c => !ContainsDigit(six, c) && ContainsDigit(zero, c));

                //---------------

                // 5 SEGMENTS ---

                var fiveSegmentsDigits = observedValues.Where(o => o.Length == 5).ToList();

                // Find 3
                var three = fiveSegmentsDigits.Single(r => ContainsDigit(r, digits[1]));
                fiveSegmentsDigits.Remove(three);
                digits.Add(3, three);

                // Find 2 and 5
                var two = fiveSegmentsDigits.Single(r => ContainsDigit(r, upperOneSegment));
                var five = fiveSegmentsDigits.Single(r => !ContainsDigit(r, upperOneSegment));
                digits.Add(2, two);
                digits.Add(5, five);

                //---------------

                // Calculate score
                var outputDisplay = new StringBuilder();
                foreach(var output in outputValues)
                {
                    outputDisplay.Append(digits.Single(d => d.Value == output).Key);
                }

                total += int.Parse(outputDisplay.ToString());
            }

            return total.ToString();
        }

        private List<string> SplitAndOrderString(string value)
        {
            List<string> result = new List<string>();

            foreach (string splitted in value.Split(" "))
            {
                char[] chars = splitted.ToCharArray();
                Array.Sort(chars);
                result.Add(new string(chars));
            }

            return result;
        }

        private bool ContainsDigit(string value, string digit)
        {
            var array = value.ToCharArray();
            foreach (char c in digit.ToCharArray())
            {
                if (!array.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ContainsDigit(string value, char digit)
        {
            return value.ToCharArray().Contains(digit);
        }
    }
}
