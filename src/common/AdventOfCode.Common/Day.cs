using System.Diagnostics;

namespace AdventOfCode
{
    public abstract class Day
    {
        private const string inputFolder = "inputs";
        private readonly string filePath;

        public Day()
        {
            var filename = GetType()
                .ToString()
                .Split(".")
                .Last()
                .ToLower();

            this.filePath = Path.Combine(inputFolder, $"{filename}.txt");
        }

        public string PartOne()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string result = ExecutePartOne(ReadLines());

            watch.Stop();
            Console.WriteLine($"Part one: {result} ({watch.ElapsedMilliseconds}ms)");

            return result;
        }

        public string PartTwo()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string result = ExecutePartTwo(ReadLines());

            watch.Stop();
            Console.WriteLine($"Part two: {result} ({watch.ElapsedMilliseconds}ms)");

            return result;
        }

        protected abstract string ExecutePartOne(IEnumerable<string> input);
        protected abstract string ExecutePartTwo(IEnumerable<string> input);

        private IEnumerable<string> ReadLines()
        {
            return File.ReadLines(this.filePath);
        }
    }
}
