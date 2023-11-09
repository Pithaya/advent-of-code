namespace AdventOfCode.y2020.Tests
{
    public static class DayExtensions
    {
        public static void AssertPartOneResult(this Day day, string expectedResult)
        {
            Assert.That(day.PartOne(), Is.EqualTo(expectedResult));
        }

        public static void AssertPartTwoResult(this Day day, string expectedResult)
        {
            Assert.That(day.PartTwo(), Is.EqualTo(expectedResult));
        }
    }

    [TestFixture]
    public class DayTest
    {
        [Test]
        public void Assert_Day1_Results()
        {
            var day = new Day1();
            day.AssertPartOneResult("514579");
            day.AssertPartTwoResult("241861950");
        }

        [Test]
        public void Assert_Day2_Results()
        {
            var day = new Day2();
            day.AssertPartOneResult("2");
            day.AssertPartTwoResult("1");
        }

        [Test]
        public void Assert_Day3_Results()
        {
            var day = new Day3();
            day.AssertPartOneResult("7");
            day.AssertPartTwoResult("336");
        }

        [Test]
        public void Assert_Day4_Results()
        {
            var day = new Day4();
            day.AssertPartOneResult("2");
            day.AssertPartTwoResult("");
        }

        [Test]
        public void Assert_Day5_Results()
        {
            var day = new Day5();
            day.AssertPartOneResult("357");
            day.AssertPartTwoResult("");
        }

        [Test]
        public void Assert_Day6_Results()
        {
            var day = new Day6();
            day.AssertPartOneResult("11");
            day.AssertPartTwoResult("6");
        }

        [Test]
        public void Assert_Day7_Results()
        {
            var day = new Day7();
            day.AssertPartOneResult("4");
            day.AssertPartTwoResult("32");
        }

        [Test]
        public void Assert_Day8_Results()
        {
            var day = new Day8();
            day.AssertPartOneResult("5");
            day.AssertPartTwoResult("8");
        }

        [Test]
        public void Assert_Day9_Results()
        {
            var day = new Day9();
            day.AssertPartOneResult("127");
            day.AssertPartTwoResult("62");
        }

        [Test]
        public void Assert_Day10_Results()
        {
            var day = new Day10();
            day.AssertPartOneResult("220");
            day.AssertPartTwoResult("19208");
        }

        [Test]
        public void Assert_Day11_Results()
        {
            var day = new Day11();
            day.AssertPartOneResult("37");
            day.AssertPartTwoResult("26");
        }

        [Test]
        public void Assert_Day12_Results()
        {
            var day = new Day12();
            day.AssertPartOneResult("25");
            day.AssertPartTwoResult("286");
        }

        [Test]
        public void Assert_Day13_Results()
        {
            var day = new Day13();
            day.AssertPartOneResult("295");
            day.AssertPartTwoResult("1068781");
        }

        [Test]
        public void Assert_Day14_Results()
        {
            /*
            var day = new Day14();
            day.AssertPartOneResult("165");
            day.AssertPartTwoResult("208");
            */
        }

        [Test]
        public void Assert_Day15_Results()
        {
            var day = new Day15();
            day.AssertPartOneResult("1836");
            day.AssertPartTwoResult("362");
        }

        [Test]
        public void Assert_Day16_Results()
        {
            var day = new Day16();
            day.AssertPartOneResult("71");
            day.AssertPartTwoResult("");
        }

        [Test]
        public void Assert_Day17_Results()
        {
            var day = new Day17();
            day.AssertPartOneResult("112");
            day.AssertPartTwoResult("848");
        }

        [Test]
        public void Assert_Day18_Results()
        {
            var day = new Day18();
            day.AssertPartOneResult("13632");
            day.AssertPartTwoResult("23340");
        }

        [Test]
        public void Assert_Day19_Results()
        {
            var day = new Day19();
            day.AssertPartOneResult("2");
            day.AssertPartTwoResult("");
        }

        [Test]
        public void Assert_Day20_Results()
        {
            var day = new Day20();
            day.AssertPartOneResult("20899048083289");
            day.AssertPartTwoResult("273");
        }

        [Test]
        public void Assert_Day21_Results()
        {
            var day = new Day21();
            day.AssertPartOneResult("5");
            day.AssertPartTwoResult("mxmxvkd,sqjhc,fvjkl");
        }

        [Test]
        public void Assert_Day22_Results()
        {
            var day = new Day22();
            day.AssertPartOneResult("306");
            day.AssertPartTwoResult("291");
        }

        [Test]
        public void Assert_Day23_Results()
        {
            var day = new Day23();
            day.AssertPartOneResult("67384529");
            day.AssertPartTwoResult("149245887792");
        }

        [Test]
        public void Assert_Day24_Results()
        {
            var day = new Day24();
            day.AssertPartOneResult("10");
            day.AssertPartTwoResult("2208");
        }

        [Test]
        public void Assert_Day25_Results()
        {
            var day = new Day25();
            day.AssertPartOneResult("14897079");
            day.AssertPartTwoResult("");
        }
    }
}
