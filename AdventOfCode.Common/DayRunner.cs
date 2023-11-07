using System.Reflection;

namespace AdventOfCode.Common
{
    public static class DayRunner
    {
        public static void Run()
        {
            Console.WriteLine("Run day:");

            string? input = Console.ReadLine();

            if(!int.TryParse(input, out int dayNumber))
            {
                Console.WriteLine($"'{input}' is not a valid number.");
                return;
            }

            Type? dayType = Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .SingleOrDefault(t => t.IsSubclassOf(typeof(Day)) && t.GetCustomAttribute<DayNumberAttribute>()?.Number == dayNumber);

            if(dayType == null)
            {
                Console.WriteLine($"Day {dayNumber} not found.");
                return;
            }

            var day = Activator.CreateInstance(dayType) as Day;

            day.PartOne();
            day.PartTwo();

            Console.ReadLine();
        }
    }
}
