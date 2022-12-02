namespace AdventOfCode.y2022.Tests
{
    [TestClass]
    public class DayTest
    {
        private void AssertDayPartOneResult(Day day, string expectedResult)
        {
            Assert.AreEqual(expectedResult, day.PartOne());
        }

        private void AssertDayPartTwoResult(Day day, string expectedResult)
        {
            Assert.AreEqual(expectedResult, day.PartTwo());
        }

        [TestMethod]
        public void Assert_Day1_Results()
        {
            var day = new Day1("inputs");
            AssertDayPartOneResult(day, "24000");
            AssertDayPartTwoResult(day, "45000");
        }

        [TestMethod]
        public void Assert_Day2_Results()
        {
            var day = new Day2("inputs");
            AssertDayPartOneResult(day, "15");
            AssertDayPartTwoResult(day, "12");
        }
    }
}