using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.y2021.Tests
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
        public void Assert_DayOne_Results()
        {
            var day = new Day1("inputs");
            AssertDayPartOneResult(day, "7");
            AssertDayPartTwoResult(day, "5");
        }

        [TestMethod]
        public void Assert_DayTwo_Results()
        {
            var day = new Day2("inputs");
            AssertDayPartOneResult(day, "150");
            AssertDayPartTwoResult(day, "900");
        }

        [TestMethod]
        public void Assert_DayThree_Results()
        {
            var day = new Day3("inputs");
            AssertDayPartOneResult(day, "198");
            AssertDayPartTwoResult(day, "230");
        }

        [TestMethod]
        public void Assert_DayFour_Results()
        {
            var day = new Day4("inputs");
            AssertDayPartOneResult(day, "4512");
            AssertDayPartTwoResult(day, "1924");
        }

        [TestMethod]
        public void Assert_DayFive_Results()
        {
            var day = new Day5("inputs");
            AssertDayPartOneResult(day, "5");
            AssertDayPartTwoResult(day, "12");
        }

        [TestMethod]
        public void Assert_DaySix_Results()
        {
            var day = new Day6("inputs");
            AssertDayPartOneResult(day, "5934");
            AssertDayPartTwoResult(day, "26984457539");
        }

        [TestMethod]
        public void Assert_DaySeven_Results()
        {
            var day = new Day7("inputs");
            AssertDayPartOneResult(day, "37");
            AssertDayPartTwoResult(day, "168");
        }

        [TestMethod]
        public void Assert_DayEight_Results()
        {
            var day = new Day8("inputs");
            AssertDayPartOneResult(day, "26");
            AssertDayPartTwoResult(day, "61229");
        }

        [TestMethod]
        public void Assert_DayNine_Results()
        {
            var day = new Day9("inputs");
            AssertDayPartOneResult(day, "15");
            AssertDayPartTwoResult(day, "1134");
        }

        [TestMethod]
        public void Assert_DayTen_Results()
        {
            var day = new Day10("inputs");
            AssertDayPartOneResult(day, "26397");
            AssertDayPartTwoResult(day, "288957");
        }

        [TestMethod]
        public void Assert_DayEleven_Results()
        {
            var day = new Day11("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }
    }
}