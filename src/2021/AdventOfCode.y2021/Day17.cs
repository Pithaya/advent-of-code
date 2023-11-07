using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(17)]
    public class Day17 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var bounds = input.First().Split(": ").Last();
            var xBounds = bounds.Split(", ").First().Replace("x=", string.Empty).Trim();
            var yBounds = bounds.Split(", ").Last().Replace("y=", string.Empty);

            var xLowerBound = int.Parse(xBounds.Split("..").First());
            var xUpperBound = int.Parse(xBounds.Split("..").Last());

            var yLowerBound = int.Parse(yBounds.Split("..").First());
            var yUpperBound = int.Parse(yBounds.Split("..").Last());

            int y = 0;

            for(int yVelocity = (yLowerBound * -1) - 1; yVelocity != 0; yVelocity--)
            {
                y += yVelocity;
            }

            return y.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var bounds = input.First().Split(": ").Last();
            var xBounds = bounds.Split(", ").First().Replace("x=", string.Empty).Trim();
            var yBounds = bounds.Split(", ").Last().Replace("y=", string.Empty);

            var xLowerBound = int.Parse(xBounds.Split("..").First());
            var xUpperBound = int.Parse(xBounds.Split("..").Last());

            var yLowerBound = int.Parse(yBounds.Split("..").First());
            var yUpperBound = int.Parse(yBounds.Split("..").Last());

            List<(int, int)> possibleVelocities = new List<(int, int)>();

            for (int x = xLowerBound; x <= xUpperBound; x++)
            {
                for (int y = yLowerBound; y <= yUpperBound; y++)
                {
                    Console.WriteLine($"{x}, {y}");
                    for (int xVelocity = -100; xVelocity <= xUpperBound; xVelocity++)
                    {
                        for(int yVelocity = yLowerBound; yVelocity <= Math.Max(Math.Abs(yLowerBound), Math.Abs(yUpperBound)); yVelocity++)
                        {
                            if(CanReach(x, y, xVelocity, yVelocity, xUpperBound, yLowerBound))
                            {
                                possibleVelocities.Add((xVelocity, yVelocity));
                            }
                        }
                    }
                }
            }

            return possibleVelocities.Distinct().Count().ToString();
        }

        private bool CanReach(int x, int y, int xVelocity, int yVelocity, int maxX, int maxY)
        {
            var currentX = 0;
            var currentY = 0;
            var currentXVelocity = xVelocity;
            var currentYVelocity = yVelocity;

            while (currentX < maxX && currentY > maxY)
            {
                currentX += currentXVelocity;
                currentY += currentYVelocity;

                if(currentXVelocity != 0)
                {
                    currentXVelocity += currentXVelocity > 0 ? -1 : 1;
                }

                currentYVelocity--;

                if(currentX == x && currentY == y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
