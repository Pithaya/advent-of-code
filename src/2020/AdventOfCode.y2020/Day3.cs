using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(3)]
    public class Day3 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<int, char[]> map = new Dictionary<int, char[]>();
            for (int i = 0; i < input.Count(); i++)
            {
                map.Add(i, input.ElementAt(i).ToCharArray());
            }

            int numberOfTrees = 0;
            
            int currentX = 0; // up to down
            int currentY = 0; // left to right
            int mapLength = map[0].Length;
            while (currentX < map.Count()) // while in the map
            {
                char current = map[currentX].ElementAt(currentY % mapLength);

                if (current == '#')
                {
                    numberOfTrees++;
                }

                currentX += 1;
                currentY += 3;
            }

            return numberOfTrees.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<int, char[]> map = new Dictionary<int, char[]>();
            for (int i = 0; i < input.Count(); i++)
            {
                map.Add(i, input.ElementAt(i).ToCharArray());
            }

            List<Slope> slopes = new List<Slope>()
            {
                new Slope(1, 1),
                new Slope(1, 3),
                new Slope(1, 5),
                new Slope(1, 7),
                new Slope(2, 1),
            };

            int mapLength = map[0].Length;
            for (int i = 0; i < map.Count; i++)
            {
                foreach (Slope slope in slopes.Where(s => s.CurrentX == i))
                {
                    char current = map[i].ElementAt(slope.CurrentY % mapLength);
                    if (current == '#')
                    {
                        slope.NumberOfTrees++;
                    }

                    slope.CurrentX += slope.XOffset;
                    slope.CurrentY += slope.YOffset;
                }
            }

            int result = slopes.Select(s => s.NumberOfTrees).Aggregate((a, x) => a * x);
            return result.ToString();
        }

        public class Slope
        {
            public int XOffset { get; }
            public int YOffset { get; }
            public int NumberOfTrees { get; set; }
            public int CurrentX { get; set; }
            public int CurrentY { get; set; }

            public Slope(int xOffset, int yOffset)
            {
                XOffset = xOffset;
                YOffset = yOffset;
            }
        }
    }
}
