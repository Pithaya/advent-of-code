using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public abstract class BaseDay
    {
        public void PartOne(string file)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string result = ExecutePartOne(file);

            watch.Stop();
            Console.WriteLine($"Found result : {result} (in {watch.ElapsedMilliseconds}ms)");
        }

        public void PartTwo(string file)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string result = ExecutePartTwo(file);

            watch.Stop();
            Console.WriteLine($"Found result : {result} (in {watch.ElapsedMilliseconds}ms)");
        }

        public abstract string ExecutePartOne(string file);
        public abstract string ExecutePartTwo(string file);

        protected IEnumerable<string> ReadLines(string day, string filename)
        {
            return File.ReadLines($"{day}/{filename}.txt");
        }
    }
}
