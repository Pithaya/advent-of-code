using AdventOfCode.Common;
using AdventOfCode.Common.Grids;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{

    class Zone
    {
        /*
        public Point Top { get; init; }
        public Point Left { get; init; }

        public Point Bottom => new Point(Top.X + Height, Top.Y);
        public Point Right => new Point(Left.X , Left.Y + Width);

        public int Height { get; init; }
        public int Width { get; init; }
        */

        public Point Center { get; init; }
        public int Size { get; init; }

        public bool IsPointInZone(Point point)
        {
            //return (point.X >= Top.X && point.X <= Bottom.X) && (point.Y >= Left.Y && point.Y <= Right.Y);
            return Point.GetManhattanDistance(Center, point) <= Size;
        }
    }

    public class Day15 : Day
    {
        private readonly bool isTesting = false;

        public Day15(string inputFolder, bool isTesting) : base(inputFolder)
        {
            this.isTesting = isTesting;
        }

        private Regex inputRegex = new Regex("Sensor at x=(-*\\d+), y=(-*\\d+): closest beacon is at x=(-*\\d+), y=(-*\\d+)");

        private bool IsInRange(Point point, List<Zone> zone)
        {
            return zone.Any(z => z.IsPointInZone(point));
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Zone> sensorZones = new List<Zone>();
            HashSet<Point> beacons = new HashSet<Point>();

            foreach(string line in input)
            {
                var matches = inputRegex.Matches(line);
                Group[] groups = matches.Single().Groups.Values.Skip(1).ToArray();

                Point sensor = new Point(int.Parse(groups[1].Value), int.Parse(groups[0].Value));
                Point beacon = new Point(int.Parse(groups[3].Value), int.Parse(groups[2].Value));

                int sensorBeaconDistance = Point.GetManhattanDistance(sensor, beacon);

                sensorZones.Add(new Zone
                {
                    Center = sensor,
                    Size = sensorBeaconDistance
                });

                beacons.Add(beacon);
            }

            int impossibleLocations = 0;

            int rowIndex = isTesting ? 10 : 2000000;

            int minColumn = sensorZones.Select(z => z.Center.Y - z.Size).Min();
            int maxColumn = sensorZones.Select(z => z.Center.Y + z.Size).Max();

            for (int i = minColumn; i <= maxColumn; i++)
            {

                var point = new Point(rowIndex, i);
                if (!beacons.Contains(point) && IsInRange(point, sensorZones))
                {
                    impossibleLocations++;
                }
            }

            return impossibleLocations.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
