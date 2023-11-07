using AdventOfCode.Common;
using AdventOfCode.Common.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{

    class Zone
    {

        public Point Center { get; init; }
        public int Size { get; init; }

        public bool IsPointInZone(Point point)
        {
            //return (point.X >= Top.X && point.X <= Bottom.X) && (point.Y >= Left.Y && point.Y <= Right.Y);
            return Point.GetManhattanDistance(Center, point) <= Size;
        }

        public int GetWidthAtRow(int rowIndex)
        {
            int verticalDistance = Math.Abs(Center.X - rowIndex);
            return (Size - verticalDistance) + 1;
        }
    }

    [DayNumber(15)]
    public class Day15 : Day
    {
        private readonly bool isTesting = false;

        public Day15() : base()
        {
            this.isTesting = false;
        }

        public Day15(bool isTesting) : base()
        {
            this.isTesting = isTesting;
        }

        private Regex inputRegex = new Regex("Sensor at x=(-*\\d+), y=(-*\\d+): closest beacon is at x=(-*\\d+), y=(-*\\d+)");

        private bool IsInRange(Point point, List<Zone> zone)
        {
            return zone.Any(z => z.IsPointInZone(point));
        }

        private Zone? FirstZoneInRange(Point point, List<Zone> zone)
        {
            return zone.FirstOrDefault(z => z.IsPointInZone(point));
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
            List<Zone> sensorZones = new List<Zone>();

            foreach (string line in input)
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
            }

            sensorZones.OrderByDescending(z => z.Size);

            int maxCoordinates = isTesting ? 20 : 4000000;

            int rowIndex = 0;
            int columnIndex = 0;

            Point currentPoint = new Point(rowIndex, columnIndex);

            while (true)
            {
                Zone? currentZone = FirstZoneInRange(currentPoint, sensorZones);

                if(currentZone == null) 
                {
                    // Found the beacon
                    break;
                }

                // Go to the other side of the zone
                currentPoint = new Point(currentPoint.X, currentZone.Center.Y + currentZone.GetWidthAtRow(currentPoint.X));

                if(currentPoint.Y > maxCoordinates)
                {
                    // Go to next row
                    currentPoint = new Point(currentPoint.X + 1, 0);
                    continue;
                }

                if (currentPoint.X > maxCoordinates)
                {
                    // Beacon not found
                    throw new InvalidOperationException();
                }
            }

            ulong tuningFrequency = ((ulong)currentPoint.Y * (ulong)4000000 + (ulong)currentPoint.X);
            return tuningFrequency.ToString();
        }
    }
}
