using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(6)]
    public class Day6 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int totalCount = 0;
            List<string> groupAnswers = new List<string>();
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    int yesAnswers = groupAnswers
                        .SelectMany(a => a.ToCharArray())
                        .Distinct()
                        .Count();

                    totalCount += yesAnswers;
                    groupAnswers.Clear();
                }
                else
                {
                    groupAnswers.Add(line);
                }
            }

            if (groupAnswers.Count != 0)
            {
                int yesAnswers = groupAnswers
                        .SelectMany(a => a.ToCharArray())
                        .Distinct()
                        .Count();

                totalCount += yesAnswers;
                groupAnswers.Clear();
            }

            return totalCount.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int totalCount = 0;
            List<string> groupAnswers = new List<string>();
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    IEnumerable<char> allQuestions = groupAnswers
                        .SelectMany(a => a.ToCharArray())
                        .Distinct();

                    int yesAnswers = 0;
                    foreach (char q in allQuestions)
                    {
                        if (groupAnswers.All(a => a.Contains(q)))
                        {
                            yesAnswers++;
                        }
                    }

                    totalCount += yesAnswers;
                    groupAnswers.Clear();
                }
                else
                {
                    groupAnswers.Add(line);
                }
            }

            if (groupAnswers.Count != 0)
            {
                IEnumerable<char> allQuestions = groupAnswers
                        .SelectMany(a => a.ToCharArray())
                        .Distinct();

                int yesAnswers = 0;
                foreach (char q in allQuestions)
                {
                    if (groupAnswers.All(a => a.Contains(q)))
                    {
                        yesAnswers++;
                    }
                }

                totalCount += yesAnswers;
                groupAnswers.Clear();
            }

            return totalCount.ToString();
        }
    }
}
