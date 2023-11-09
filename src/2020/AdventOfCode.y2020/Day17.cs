using AdventOfCode.Common;
using System.Collections.Immutable;

namespace AdventOfCode.y2020
{
    [DayNumber(17)]
    public class Day17 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Cube> activeCubes = new List<Cube>();

            for (int x = 0; x < input.Count(); x++)
            {
                string currentLine = input.ElementAt(x);
                List<Cube> lineCubes = currentLine
                    .ToCharArray()
                    .Where(c => c == '#')
                    .Select((c, i) => new Cube(x, i, 0))
                    .ToList();
                activeCubes.AddRange(lineCubes);
            }

            int dimensions = 3;
            int step = 6;

            for (int i = 0; i < step; i++)
            {
                List<Cube> newCubes = new List<Cube>(activeCubes);
                foreach (var cube in activeCubes)
                {
                    int activeNeighbours = cube.GetNeighbours().Count(n => activeCubes.Contains(n));
                    if (activeNeighbours < 2 || activeNeighbours > 3)
                    {
                        newCubes.Remove(cube);
                    }
                }

                foreach (var inactiveCube in GetAllInactiveNeighbours(activeCubes))
                {
                    var activeNeighbourCount = inactiveCube.GetNeighbours().Count(pt => activeCubes.Contains(pt));
                    if (activeNeighbourCount == 3)
                    {
                        newCubes.Add(inactiveCube);
                    }
                }

                //activeCubes = newCubes;
            }

            int result = activeCubes.Count();
            return result.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }

        private class Cube
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            private static readonly ImmutableArray<Cube> neighbourOffsets
            = ImmutableArray.CreateRange(from x in Enumerable.Range(-1, 3)
                                         from y in Enumerable.Range(-1, 3)
                                         from z in Enumerable.Range(-1, 3)
                                         let p = (x, y, z)
                                         where p != (0, 0, 0)
                                         select new Cube(x, y, z));

            public Cube(int x, int y, int z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public IEnumerable<Cube> GetNeighbours()
            {
                return neighbourOffsets.Select(o => new Cube(
                    this.X + o.X,
                    this.Y + o.Y,
                    this.Z + o.Z));
            }

            public override bool Equals(Object? other)
            {
                if (other != null && other is Cube cube)
                {
                    return this.X == cube.X && this.Y == cube.Y && this.Z == cube.Z;
                }
                return false;
            }
        }

        private static List<Cube> GetAllInactiveNeighbours(List<Cube> activeCubes)
        {
            return activeCubes.SelectMany(c => c.GetNeighbours())
                              .Where(pt => !activeCubes.Contains(pt))
                              .ToList();
        }
    }
}
