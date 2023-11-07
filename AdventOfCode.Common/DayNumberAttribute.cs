namespace AdventOfCode.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DayNumberAttribute : Attribute
    {
        public int Number { get; set; }

        public DayNumberAttribute(int number)
        {
            Number = number;
        }
    }
}
