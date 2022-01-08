using AdventOfCode;
using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    public class Day22 : Day
    {
        public Day22(string inputFolder) : base(inputFolder)
        { }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<Point3D, bool> initializedCubes = new Dictionary<Point3D, bool>();
            List<RebootStep> steps = GetRebootSteps(input);

            foreach (var step in steps)
            {
                bool isInZone = (step.MinX >= -50 && step.MaxX <= 50
                && step.MinY >= -50 && step.MaxY <= 50
                && step.MinZ >= -50 && step.MaxZ <= 50);

                if (!isInZone)
                {
                    continue;
                }

                for(int x = step.MinX; x <= step.MaxX; x++)
                {
                    for (int y = step.MinY; y <= step.MaxY; y++)
                    {
                        for(int z = step.MinZ; z <= step.MaxZ; z++)
                        {
                            var point = new Point3D(x, y, z);
                            initializedCubes.AddOrUpdate(point, step.Value, (p, v) => step.Value);
                        }
                    }
                }
            }

            return initializedCubes.Count(c => c.Value).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Cuboid> cuboids = GetCuboids(input);
            List<Cuboid> processedCuboids = new List<Cuboid>();

            foreach (var cuboid in cuboids)
            {
                List<Cuboid> intersections = processedCuboids
                    .Select(c => cuboid.GetIntersectionCuboid(c))
                    .Where(c => c.IsValid())
                    .ToList();

                processedCuboids.AddRange(intersections);
                if (cuboid.On)
                {
                    processedCuboids.Add(cuboid);
                }
            }

            return processedCuboids.Sum(b => b.Volume * (b.On ? 1 : -1)).ToString();
        }

        private long AddCuboidVolumesWithoutOverlap(List<Cuboid> cuboids)
        {
            List<Cuboid> processedCuboids = new List<Cuboid>();
            long volume = 0;

            foreach (var cuboid in cuboids)
            {
                List<Cuboid> intersections = processedCuboids
                        .Select(c => c.GetIntersectionCuboid(cuboid))
                        .Where(c => c.Volume > 0)
                        .ToList();

                volume += cuboid.Volume - AddCuboidVolumesWithoutOverlap(intersections);

                processedCuboids.Add(cuboid);
            }

            return volume;
        }

        private List<RebootStep> GetRebootSteps(IEnumerable<string> input)
        {
            return input.Select(l =>
            {
                var value = l.Split(" ").First();
                var coordinates = l.Split(" ").Last().Split(",");
                var x = coordinates[0].Split("=").Last();
                var y = coordinates[1].Split("=").Last();
                var z = coordinates[2].Split("=").Last();

                return new RebootStep
                {
                    Value = value == "on" ? true : false,
                    MinX = int.Parse(x.Split("..").First()),
                    MaxX = int.Parse(x.Split("..").Last()),
                    MinY = int.Parse(y.Split("..").First()),
                    MaxY = int.Parse(y.Split("..").Last()),
                    MinZ = int.Parse(z.Split("..").First()),
                    MaxZ = int.Parse(z.Split("..").Last()),
                };
            }).ToList();
        }

        private List<Cuboid> GetCuboids(IEnumerable<string> input)
        {
            return input.Select(l => Cuboid.Parse(l)).ToList();
        }
    }

    class RebootStep
    {
        public bool Value { get; set; }

        public int MinX { get; set; }
        public int MaxX { get; set; }

        public int MinY { get; set; }
        public int MaxY { get; set; }

        public int MinZ { get; set; }
        public int MaxZ { get; set; }
    }

    struct Cuboid
    {
        public bool On { get; set; }

        public Point3D Start { get; set; }
        public Point3D End { get; set; }

        public long Width => (End.X - Start.X) + 1;
        public long Height =>(End.Y - Start.Y) + 1;
        public long Depth => (End.Z - Start.Z) + 1;


        public long Volume => CalculateVolume(Width, Height, Depth);

        public Cuboid GetIntersectionCuboid(Cuboid other)
        {
            return new Cuboid
            {
                // If we are 'on', the intersection will be 'off'
                // and if we negated a zone with 'off', the intersection will be 'on'
                On = !other.On,
                Start = new Point3D
                {
                    X = Math.Max(this.Start.X, other.Start.X),
                    Y = Math.Max(this.Start.Y, other.Start.Y),
                    Z = Math.Max(this.Start.Z, other.Start.Z),
                },
                End = new Point3D
                {
                    X = Math.Min(this.End.X, other.End.X),
                    Y = Math.Min(this.End.Y, other.End.Y),
                    Z = Math.Min(this.End.Z, other.End.Z),
                }
            };
        }

        public bool IsValid()
        {
            return Start.X <= End.X && Start.Y <= End.Y && Start.Z <= End.Z;
        }

        private long CalculateVolume(long width, long height, long depth)
        {
            return width * height * depth;
        }

        public override string ToString()
        {
            return $"{(On ? "on" : "off")} x={Start.X}..{End.X},y={Start.Y}..{End.Y},z={Start.Z}..{End.Z}";
        }

        public static Cuboid Parse(string s)
        {
            var value = s.Split(" ").First();
            var coordinates = s.Split(" ").Last().Split(",");
            var x = coordinates[0].Split("=").Last();
            var y = coordinates[1].Split("=").Last();
            var z = coordinates[2].Split("=").Last();

            return new Cuboid
            {
                On = value == "on" ? true : false,
                Start = new Point3D
                {
                    X = int.Parse(x.Split("..").First()),
                    Y = int.Parse(y.Split("..").First()),
                    Z = int.Parse(z.Split("..").First())
                },
                End = new Point3D
                {
                    X = int.Parse(x.Split("..").Last()),
                    Y = int.Parse(y.Split("..").Last()),
                    Z = int.Parse(z.Split("..").Last())
                }
            };
        }
    }
}
