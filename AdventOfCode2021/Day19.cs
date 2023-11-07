using AdventOfCode.Common;
using AdventOfCode.Common.Models;

namespace AdventOfCode.y2021
{
    [DayNumber(19)]
    public class Day19 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Scanner> scanners = new List<Scanner>();

            Scanner currentScanner = null!;
            int i = 0;

            foreach(var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if(line.StartsWith("--- scanner"))
                {
                    currentScanner = new Scanner() { Index = i };
                    scanners.Add(currentScanner);
                    i++;
                    continue;
                }

                var coordinates = line
                    .Split(",")
                    .Select(s => int.Parse(s))
                    .ToList();

                currentScanner.Beacons.Add(new Point3D(coordinates[0], coordinates[1], coordinates[2]));
            }

            // Overlapping scanners see at least 12 same beacons
            var startingScanner = scanners.First();
            HashSet<Point3D> totalBeacons = new HashSet<Point3D>();
            totalBeacons.AddRange(startingScanner.Beacons);
            scanners.Remove(startingScanner);

            while (scanners.Any())
            {
                List<Scanner> toRemove = new List<Scanner>();

                foreach (var scanner in scanners)
                {
                    bool isMatch = false;

                    // Try all 24 orientations
                    var allOrientations = GetAllOrientedPoints(scanner);

                    foreach (var orientedPoints in allOrientations)
                    {
                        foreach (var point in orientedPoints)
                        {
                            // Calculate the distances between this point and all other saved points
                            var distances = totalBeacons.Select(b => b - point).ToList();

                            // Foreach distance, translate all points and see if they match already saved points
                            foreach (var distance in distances)
                            {
                                var translatedPoints = orientedPoints.Select(o => o + distance).ToList();
                                if (translatedPoints.Count(t => totalBeacons.Contains(t)) >= 12)
                                {
                                    // Add translatedPoints to total beacons
                                    totalBeacons.AddRange(translatedPoints);
                                    toRemove.Add(scanner);

                                    isMatch = true;
                                    break;
                                }
                            }

                            if (isMatch)
                            {
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            break;
                        }
                    }
                }

                foreach(var s in toRemove)
                {
                    scanners.Remove(s);
                }
            }
          

            return totalBeacons
                .Count()
                .ToString();
        }

        private List<List<Point3D>> GetAllOrientedPoints(Scanner scanner)
        {
            var result = new List<List<Point3D>>();

            // facing Z
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons));
            // facing -Z
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons.Select(p => new Point3D(-p.X, p.Y, -p.Z)).ToList()));

            // facing X
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons.Select(p => new Point3D(-p.Z, p.Y, p.X)).ToList()));
            // facing -X
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons.Select(p => new Point3D(p.Z, p.Y, -p.X)).ToList()));

            // facing Y
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons.Select(p => new Point3D(p.X, -p.Z, p.Y)).ToList()));
            // facing -Y
            result.AddRange(GetAllRotatedPointsAroundZAxis(scanner.Beacons.Select(p => new Point3D(p.X, p.Z, -p.Y)).ToList()));

            return result;
        }

        /// <summary>
        /// Given a list of beacons seen from a scanner facing the local Z axis,
        /// return the beacons rotated 4 times
        /// </summary>
        /// <param name="beacons"></param>
        /// <returns></returns>
        private List<List<Point3D>> GetAllRotatedPointsAroundZAxis(List<Point3D> beacons)
        {
            // Rotate while looking towards Z: only Y and X change
            return new List<List<Point3D>>()
            {
                // Top is Y
                beacons,
                // Top is X
                beacons.Select(p => new Point3D(-p.Y, p.X, p.Z)).ToList(),
                // Top is -Y
                beacons.Select(p => new Point3D(-p.X, -p.Y, p.Z)).ToList(),
                // Top is -X
                beacons.Select(p => new Point3D(p.Y, -p.X, p.Z)).ToList()
            };
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Scanner> scanners = new List<Scanner>();

            Scanner currentScanner = null!;
            int i = 0;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith("--- scanner"))
                {
                    currentScanner = new Scanner() { Index = i };
                    scanners.Add(currentScanner);
                    i++;
                    continue;
                }

                var coordinates = line
                    .Split(",")
                    .Select(s => int.Parse(s))
                    .ToList();

                currentScanner.Beacons.Add(new Point3D(coordinates[0], coordinates[1], coordinates[2]));
            }

            // Overlapping scanners see at least 12 same beacons
            var startingScanner = scanners.First();
            startingScanner.Position = Point3D.Zero;
            HashSet<Point3D> totalBeacons = new HashSet<Point3D>();
            totalBeacons.AddRange(startingScanner.Beacons);

            var scannersToProcess = new List<Scanner>(scanners);
            scannersToProcess.Remove(startingScanner);

            while (scannersToProcess.Any())
            {
                List<Scanner> toRemove = new List<Scanner>();

                foreach (var scanner in scannersToProcess)
                {
                    bool isMatch = false;

                    // Try all 24 orientations
                    var allOrientations = GetAllOrientedPoints(scanner);

                    foreach (var orientedPoints in allOrientations)
                    {
                        foreach (var point in orientedPoints)
                        {
                            // Calculate the distances between this point and all other saved points
                            var distances = totalBeacons.Select(b => b - point).ToList();

                            // Foreach distance, translate all points and see if they match already saved points
                            foreach (var distance in distances)
                            {
                                var translatedPoints = orientedPoints.Select(o => o + distance).ToList();
                                if (translatedPoints.Count(t => totalBeacons.Contains(t)) >= 12)
                                {
                                    // Add translatedPoints to total beacons
                                    totalBeacons.AddRange(translatedPoints);
                                    scanner.Position = distance;
                                    toRemove.Add(scanner);

                                    isMatch = true;
                                    break;
                                }
                            }

                            if (isMatch)
                            {
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            break;
                        }
                    }
                }

                foreach (var s in toRemove)
                {
                    scannersToProcess.Remove(s);
                }
            }

            int maxDist = 0;

            foreach(var first in scanners)
            {
                foreach (var second in scanners)
                {
                    var manDistance = Point3D.ManhattanDistance(first.Position.Value, second.Position.Value);
                    if(manDistance > maxDist)
                    {
                        maxDist = manDistance;
                    }
                }
            }

            return maxDist.ToString();
        }
    }

    class Scanner
    {
        public int Index { get; set; }
        public Point3D? Position { get; set; }

        public List<Point3D> Beacons { get; set; } = new List<Point3D>();
    }
}
