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
            var day = new Day1();
            AssertDayPartOneResult(day, "7");
            AssertDayPartTwoResult(day, "5");
        }

        [TestMethod]
        public void Assert_Day2_Results()
        {
            var day = new Day2();
            AssertDayPartOneResult(day, "150");
            AssertDayPartTwoResult(day, "900");
        }

        [TestMethod]
        public void Assert_Day3_Results()
        {
            var day = new Day3();
            AssertDayPartOneResult(day, "198");
            AssertDayPartTwoResult(day, "230");
        }

        [TestMethod]
        public void Assert_Day4_Results()
        {
            var day = new Day4();
            AssertDayPartOneResult(day, "4512");
            AssertDayPartTwoResult(day, "1924");
        }

        [TestMethod]
        public void Assert_Day5_Results()
        {
            var day = new Day5();
            AssertDayPartOneResult(day, "5");
            AssertDayPartTwoResult(day, "12");
        }

        [TestMethod]
        public void Assert_Day6_Results()
        {
            var day = new Day6();
            AssertDayPartOneResult(day, "5934");
            AssertDayPartTwoResult(day, "26984457539");
        }

        [TestMethod]
        public void Assert_Day7_Results()
        {
            var day = new Day7();
            AssertDayPartOneResult(day, "37");
            AssertDayPartTwoResult(day, "168");
        }

        [TestMethod]
        public void Assert_Day8_Results()
        {
            var day = new Day8();
            AssertDayPartOneResult(day, "26");
            AssertDayPartTwoResult(day, "61229");
        }

        [TestMethod]
        public void Assert_Day9_Results()
        {
            var day = new Day9();
            AssertDayPartOneResult(day, "15");
            AssertDayPartTwoResult(day, "1134");
        }

        [TestMethod]
        public void Assert_Day10_Results()
        {
            var day = new Day10();
            AssertDayPartOneResult(day, "26397");
            AssertDayPartTwoResult(day, "288957");
        }

        [TestMethod]
        public void Assert_Day11_Results()
        {
            var day = new Day11();
            AssertDayPartOneResult(day, "1656");
            AssertDayPartTwoResult(day, "195");
        }

        [TestMethod]
        public void Assert_Day12_Results()
        {
            var day = new Day12();
            AssertDayPartOneResult(day, "226");
            AssertDayPartTwoResult(day, "3509");
        }

        [TestMethod]
        public void Assert_Day13_Results()
        {
            var day = new Day13();
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
            var day = new Day14();
            AssertDayPartOneResult(day, "1588");
            AssertDayPartTwoResult(day, "2188189693529");
        }

        [TestMethod]
        public void Assert_Day15_Results()
        {
            var day = new Day15();
            AssertDayPartOneResult(day, "40");
            AssertDayPartTwoResult(day, "315");
        }

        [TestMethod]
        public void Assert_Day16_Results()
        {
            var day = new Day16();
            AssertDayPartOneResult(day, "31");
            AssertDayPartTwoResult(day, "54");
        }

        [TestMethod]
        public void Assert_Day17_Results()
        {
            var day = new Day17();
            AssertDayPartOneResult(day, "45");
            AssertDayPartTwoResult(day, "112");
        }

        [TestMethod]
        public void Assert_Day18_Results()
        {
            var day = new Day18();
            AssertDayPartOneResult(day, "4140");
            AssertDayPartTwoResult(day, "3993");
        }

        [TestMethod]
        public void Assert_Day19_Results()
        {
            var day = new Day19();
            AssertDayPartOneResult(day, "79");
            AssertDayPartTwoResult(day, "3621");
        }

        [TestMethod]
        public void Assert_Day20_Results()
        {
            var day = new Day20();
            AssertDayPartOneResult(day, "35");
            AssertDayPartTwoResult(day, "3351");
        }

        [TestMethod]
        public void Assert_Day21_Results()
        {
            var day = new Day21();
            AssertDayPartOneResult(day, "739785");
            AssertDayPartTwoResult(day, "444356092776315");
        }

        [TestMethod]
        public void Assert_Day22_Results()
        {
            var day = new Day22();
            AssertDayPartOneResult(day, "474140");
            AssertDayPartTwoResult(day, "2758514936282235");
        }

        [TestMethod]
        public void Assert_Day23_Results()
        {
            var day = new Day23();
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day24_Results()
        {
            var day = new Day24();
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day25_Results()
        {
            var day = new Day25();
            AssertDayPartOneResult(day, "58");
        }
    }
}