namespace AdventOfCode.y2022
{
    class Elf
    {
        private List<int> calories;
        public int TotalCalories => calories.Sum();

        public Elf(List<int> calories)
        {
            this.calories = calories;
        }
    }

    public class Day1 : Day
    {
        public Day1(string inputFolder) : base(inputFolder)
        { }

        private List<Elf> GetElves(IEnumerable<string> input)
        {
            List<Elf> elves = new List<Elf>();
            List<int> currentCalories = new List<int>();

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(new Elf(currentCalories));
                    currentCalories = new List<int>();
                }
                else
                {
                    currentCalories.Add(int.Parse(line));
                }
            }

            // Handle the last elf
            if (currentCalories.Any())
            {
                elves.Add(new Elf(currentCalories));
            }

            return elves;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            return GetElves(input)
                .Select(e => e.TotalCalories)
                .Max()
                .ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return GetElves(input)
                .OrderByDescending(e => e.TotalCalories)
                .Take(3)
                .Select(e => e.TotalCalories)
                .Sum()
                .ToString();
        }
    }
}
