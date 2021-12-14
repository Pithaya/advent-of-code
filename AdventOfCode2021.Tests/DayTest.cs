using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021.Tests
{
    [TestClass]
    public class DayTest
    {
        private void AssertDayPartOneResult(BaseDay day, string expectedResult)
        {
            Assert.AreEqual(expectedResult, day.ExecutePartOne("example"));
        }

        private void AssertDayPartTwoResult(BaseDay day, string expectedResult)
        {
            Assert.AreEqual(expectedResult, day.ExecutePartTwo("example"));
        }

        [TestMethod]
        public void Assert_DayOne_Results()
        {
            var day = new DayOne();
            AssertDayPartOneResult(day, "7");
            AssertDayPartTwoResult(day, "5");
        }

        [TestMethod]
        public void Assert_DayTwo_Results()
        {
            var day = new DayTwo();
            AssertDayPartOneResult(day, "150");
            AssertDayPartTwoResult(day, "900");
        }

        [TestMethod]
        public void Assert_DayThree_Results()
        {
            var day = new DayThree();
            AssertDayPartOneResult(day, "198");
            AssertDayPartTwoResult(day, "230");
        }

        [TestMethod]
        public void Assert_DayFour_Results()
        {
            var day = new DayFour();
            AssertDayPartOneResult(day, "4512");
            AssertDayPartTwoResult(day, "1924");
        }

        [TestMethod]
        public void Assert_DayFive_Results()
        {
            var day = new DayFive();
            AssertDayPartOneResult(day, "5");
            AssertDayPartTwoResult(day, "12");
        }

        [TestMethod]
        public void Assert_DaySix_Results()
        {
            var day = new DaySix();
            AssertDayPartOneResult(day, "5934");
            AssertDayPartTwoResult(day, "26984457539");
        }

        [TestMethod]
        public void Assert_DaySeven_Results()
        {
            var day = new DaySeven();
            AssertDayPartOneResult(day, "37");
            AssertDayPartTwoResult(day, "168");
        }

        [TestMethod]
        public void Assert_DayEight_Results()
        {
            var day = new DayEight();
            AssertDayPartOneResult(day, "26");
            AssertDayPartTwoResult(day, "61229");
        }

        [TestMethod]
        public void Assert_DayNine_Results()
        {
            var day = new DayNine();
            AssertDayPartOneResult(day, "15");
            AssertDayPartTwoResult(day, "1134");
        }

        [TestMethod]
        public void Assert_DayTen_Results()
        {
            var day = new DayTen();
            AssertDayPartOneResult(day, "26397");
            AssertDayPartTwoResult(day, "288957");
        }

        [TestMethod]
        public void Assert_DayEleven_Results()
        {
            var day = new DayEleven();
            AssertDayPartOneResult(day, "");
            AssertDayPartTwoResult(day, "");
        }
    }
}