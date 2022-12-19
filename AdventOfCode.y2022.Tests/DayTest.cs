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

        [TestMethod]
        public void Assert_Day3_Results()
        {
            var day = new Day3("inputs");
            AssertDayPartOneResult(day, "157");
            AssertDayPartTwoResult(day, "70");
        }

        [TestMethod]
        public void Assert_Day4_Results()
        {
            var day = new Day4("inputs");
            AssertDayPartOneResult(day, "2");
            AssertDayPartTwoResult(day, "4");
        }

        [TestMethod]
        public void Assert_Day5_Results()
        {
            var day = new Day5("inputs");
            AssertDayPartOneResult(day, "CMZ");
            AssertDayPartTwoResult(day, "MCD");
        }

        [TestMethod]
        public void Assert_Day6_Results()
        {
            var day = new Day6("inputs");
            AssertDayPartOneResult(day, "7");
            AssertDayPartTwoResult(day, "19");
        }

        [TestMethod]
        public void Assert_Day7_Results()
        {
            var day = new Day7("inputs");
            AssertDayPartOneResult(day, "95437");
            AssertDayPartTwoResult(day, "24933642");
        }

        [TestMethod]
        public void Assert_Day8_Results()
        {
            var day = new Day8("inputs");
            AssertDayPartOneResult(day, "21");
            AssertDayPartTwoResult(day, "8");
        }

        [TestMethod]
        public void Assert_Day9_Results()
        {
            var day = new Day9("inputs");
            // Simple example
            //AssertDayPartOneResult(day, "13");
            // Advanced example
            AssertDayPartOneResult(day, "87");
            AssertDayPartTwoResult(day, "36");
        }

        [TestMethod]
        public void Assert_Day10_Results()
        {
            var day = new Day10("inputs");
            AssertDayPartOneResult(day, "13140");
            AssertDayPartTwoResult(day, @"
##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....");
        }

        [TestMethod]
        public void Assert_Day11_Results()
        {
            var day = new Day11("inputs");
            AssertDayPartOneResult(day, "10605");
            AssertDayPartTwoResult(day, "2713310158");
        }

        [TestMethod]
        public void Assert_Day12_Results()
        {
            var day = new Day12("inputs");
            AssertDayPartOneResult(day, "31");
            AssertDayPartTwoResult(day, "29");
        }

        [TestMethod]
        public void Assert_Day13_Results()
        {
            var day = new Day13("inputs");
            AssertDayPartOneResult(day, "13");
            AssertDayPartTwoResult(day, "140");
        }

        [TestMethod]
        public void Assert_Day14_Results()
        {
            var day = new Day14("inputs");
            AssertDayPartOneResult(day, "24");
            AssertDayPartTwoResult(day, "93");
        }

        [TestMethod]
        public void Assert_Day15_Results()
        {
            var day = new Day15("inputs", true);
            AssertDayPartOneResult(day, "26");
            AssertDayPartTwoResult(day, "56000011");
        }

        [TestMethod]
        public void Assert_Day16_Results()
        {
            var day = new Day16("inputs");
            AssertDayPartOneResult(day, "1651");
            AssertDayPartTwoResult(day, "1707");
        }

        [TestMethod]
        public void Assert_Day17_Results()
        {
            var day = new Day17("inputs");
            AssertDayPartOneResult(day, "3068");
            AssertDayPartTwoResult(day, "1514285714288");
        }

        [TestMethod]
        public void Assert_Day18_Results()
        {
            var day = new Day18("inputs");
            AssertDayPartOneResult(day, "64");
            AssertDayPartTwoResult(day, "58");
        }

        [TestMethod]
        public void Assert_Day19_Results()
        {
            var day = new Day19("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day20_Results()
        {
            var day = new Day20("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day21_Results()
        {
            var day = new Day21("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day22_Results()
        {
            var day = new Day22("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day23_Results()
        {
            var day = new Day23("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day24_Results()
        {
            var day = new Day24("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }

        [TestMethod]
        public void Assert_Day25_Results()
        {
            var day = new Day25("inputs");
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }
    }
}