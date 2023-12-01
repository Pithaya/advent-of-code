using System.Diagnostics;
using System.IO.Abstractions;

namespace AdventOfCode
{
    public abstract class Day
    {
        public const string InputFolder = "inputs";
        private readonly string filePath;
        private readonly IFileSystem fileSystem;

        public Day(IFileSystem? fileSystem = null)
        {
            this.fileSystem = fileSystem ?? new FileSystem();

            var filename = GetType()
                .ToString()
                .Split(".")
                .Last()
                .ToLower();

            this.filePath = this.fileSystem.Path.Combine(InputFolder, $"{filename}.txt");
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
            return fileSystem.File.ReadLines(this.filePath);
        }
    }
}
