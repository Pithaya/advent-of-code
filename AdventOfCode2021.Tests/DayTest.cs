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
        public void Assert_Day1_Results()
        {
            var day = new Day1("inputs");
            AssertDayPartOneResult(day, "7");
            AssertDayPartTwoResult(day, "5");
        }

        [TestMethod]
        public void Assert_Day2_Results()
        {
            var day = new Day2("inputs");
            AssertDayPartOneResult(day, "150");
            AssertDayPartTwoResult(day, "900");
        }

        [TestMethod]
        public void Assert_Day3_Results()
        {
            var day = new Day3("inputs");
            AssertDayPartOneResult(day, "198");
            AssertDayPartTwoResult(day, "230");
        }

        [TestMethod]
        public void Assert_Day4_Results()
        {
            var day = new Day4("inputs");
            AssertDayPartOneResult(day, "4512");
            AssertDayPartTwoResult(day, "1924");
        }

        [TestMethod]
        public void Assert_Day5_Results()
        {
            var day = new Day5("inputs");
            AssertDayPartOneResult(day, "5");
            AssertDayPartTwoResult(day, "12");
        }

        [TestMethod]
        public void Assert_Day6_Results()
        {
            var day = new Day6("inputs");
            AssertDayPartOneResult(day, "5934");
            AssertDayPartTwoResult(day, "26984457539");
        }

        [TestMethod]
        public void Assert_Day7_Results()
        {
            var day = new Day7("inputs");
            AssertDayPartOneResult(day, "37");
            AssertDayPartTwoResult(day, "168");
        }

        [TestMethod]
        public void Assert_Day8_Results()
        {
            var day = new Day8("inputs");
            AssertDayPartOneResult(day, "26");
            AssertDayPartTwoResult(day, "61229");
        }

        [TestMethod]
        public void Assert_Day9_Results()
        {
            var day = new Day9("inputs");
            AssertDayPartOneResult(day, "15");
            AssertDayPartTwoResult(day, "1134");
        }

        [TestMethod]
        public void Assert_Day10_Results()
        {
            var day = new Day10("inputs");
            AssertDayPartOneResult(day, "26397");
            AssertDayPartTwoResult(day, "288957");
        }

        [TestMethod]
        public void Assert_Day11_Results()
        {
            var day = new Day11("inputs");
            AssertDayPartOneResult(day, "1656");
            AssertDayPartTwoResult(day, "195");
        }

        [TestMethod]
        public void Assert_Day12_Results()
        {
            var day = new Day12("inputs");
            AssertDayPartOneResult(day, "226");
            AssertDayPartTwoResult(day, "3509");
        }

        [TestMethod]
        public void Assert_Day13_Results()
        {
            var day = new Day13("inputs");
            AssertDayPartOneResult(day, "17");
            AssertDayPartTwoResult(day, @"
#####
#   #
#   #
#   #
#####
");
        }

        [TestMethod]
        public void Assert_Day14_Results()
        {
            var day = new Day14("inputs");
            AssertDayPartOneResult(day, "1588");
            AssertDayPartTwoResult(day, "2188189693529");
        }

        [TestMethod]
        public void Assert_Day15_Results()
        {
            var day = new Day15("inputs");
            AssertDayPartOneResult(day, "40");
            AssertDayPartTwoResult(day, "315");
        }

        [TestMethod]
        public void Assert_Day16_Results()
        {
            var day = new Day16("inputs");
            AssertDayPartOneResult(day, "31");
            AssertDayPartTwoResult(day, "54");
        }

        [TestMethod]
        public void Assert_Day17_Results()
        {
            var day = new Day17("inputs");
            AssertDayPartOneResult(day, "45");
            AssertDayPartTwoResult(day, "112");
        }

        [TestMethod]
        public void Assert_Day18_Results()
        {
            var day = new Day18("inputs");
            AssertDayPartOneResult(day, "4140");
            AssertDayPartTwoResult(day, "3993");
        }
    }
}