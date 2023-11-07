using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(9)]
    public class Day9 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Heightmap heightmap = new Heightmap(input);

            return heightmap.GetLowPointsValues()
                .Select(l => l + 1)
                .Sum()
                .ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Heightmap heightmap = new Heightmap(input);

            return heightmap.GetBasinSizes()
                .OrderByDescending(b => b)
                .Take(3)
                .Aggregate((x, y) => x * y)
                .ToString();
        }
    }

    public class Heightmap
    {
        private readonly short[][] points;
        private readonly HashSet<(int, int)> visitedPoints = new HashSet<(int, int)>();

        public Heightmap(IEnumerable<string> input)
        {
            points = new short[input.Count()][];

            for (int i = 0; i < input.Count(); i++)
            {
                points[i] = input
                    .ElementAt(i)
                    .ToCharArray()
                    .Select(c => short.Parse(c.ToString()))
                    .ToArray();
            }
        }

        public List<(int row, int column)> GetAdjacentPoints(int row, int column)
        {
            List<(int row, int column)> adjacentLocations = new List<(int row, int column)>();

            // Up
            if (row > 0)
            {
                adjacentLocations.Add((row - 1, column));
            }

            // Down
            if (row + 1 < points.Length)
            {
                adjacentLocations.Add((row + 1,  column));
            }

            // Left
            if (column > 0)
            {
                adjacentLocations.Add((row, column - 1));
            }

            // Right
            if (column + 1 < points[row].Length)
            {
                adjacentLocations.Add((row, column + 1));
            }

            return adjacentLocations;
        }

        public bool IsLowPoint(int row, int column)
        {
            return GetAdjacentPoints(row, column).All(a => points[a.row][a.column] > points[row][column]);
        }

        public List<short> GetLowPointsValues()
        {
            List<short> lowPoints = new List<short>();

            foreach(var lowPoint in GetLowPoints())
            {
                lowPoints.Add(points[lowPoint.row][lowPoint.column]);
            }

            return lowPoints;
        }

        public List<(int row, int column)> GetLowPoints()
        {
            List<(int row, int column)> lowPoints = new List<(int row, int column)>();

            for (int i = 0; i < points.Length; i++)
            {
                for (int y = 0; y < points[i].Length; y++)
                {
                    if (IsLowPoint(i, y))
                    {
                        lowPoints.Add((i, y));
                    }
                }
            }

            return lowPoints;
        }

        public List<int> GetBasinSizes()
        {
            List<int> basinSizes = new List<int>();
            var lowPoints = GetLowPoints();

            foreach(var lowPoint in lowPoints)
            {
                basinSizes.Add(CalculateBasinSize(lowPoint.row, lowPoint.column));
            }

            return basinSizes;
        }

        public int CalculateBasinSize(int row, int column)
        {
            int basinSize = 0;
            Queue<(int row, int column)> pointQueue = new Queue<(int row, int column)>();
            pointQueue.Enqueue((row, column));

            while(pointQueue.Count > 0)
            {
                var currentPoint = pointQueue.Dequeue();

                if(!visitedPoints.Contains((currentPoint.row, currentPoint.column)))
                {
                    visitedPoints.Add((currentPoint.row, currentPoint.column));
                    basinSize++;
                }

                foreach(var adjacentPoint in GetAdjacentPoints(currentPoint.row, currentPoint.column)
                    .Where(a => points[a.row][a.column] != 9 && !visitedPoints.Contains(a)))
                {
                    pointQueue.Enqueue(adjacentPoint);
                }
            }

            return basinSize;
        }
    }
}
