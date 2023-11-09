using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(12)]
    public class Day12 : Day
    {
        private enum Direction
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270,
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<Direction, int> distances = new Dictionary<Direction, int>()
            {
                { Direction.North, 0 },
                { Direction.South, 0 },
                { Direction.East, 0 },
                { Direction.West, 0 },
            };
            Direction currentDirection = Direction.East;

            foreach (string instruction in input)
            {
                char direction = instruction[0];
                int value = int.Parse(instruction.Substring(1));

                if (direction == 'F')
                {
                    distances[currentDirection] += value;
                    continue;
                }

                if (direction == 'L')
                {
                    int newDirection = ((int)currentDirection - value) % 360;
                    if (newDirection < 0)
                    {
                        newDirection += 360;
                    }
                    currentDirection = (Direction)newDirection;
                    continue;
                }

                if (direction == 'R')
                {
                    currentDirection = (Direction)(((int)currentDirection + value) % 360);
                    continue;
                }

                // default: go to specified direction
                distances[DirectionFromChar(direction)] += value;
            }

            int result = GetManhattanDistance(distances);

            return result.ToString();
        }

        private static int GetManhattanDistance(Dictionary<Direction, int> distances)
        {
            int northSouthDistance = Math.Abs(distances[Direction.North] - distances[Direction.South]);
            int eastWestDistance = Math.Abs(distances[Direction.West] - distances[Direction.East]);

            return northSouthDistance + eastWestDistance;
        }

        private static Direction DirectionFromChar(char input)
        {
            return input switch
            {
                'N' => Direction.North,
                'S' => Direction.South,
                'W' => Direction.West,
                'E' => Direction.East,
                _ => throw new InvalidOperationException()
            };
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<Direction, int> distances = new Dictionary<Direction, int>()
            {
                { Direction.North, 0 },
                { Direction.South, 0 },
                { Direction.East, 0 },
                { Direction.West, 0 },
            };

            Dictionary<int, int> waypoint = new Dictionary<int, int>()
            {
                { 0, 1 },
                { 90, 10 },
                { 180, 0 },
                { 270, 0 },
            };

            foreach (string instruction in input)
            {
                char direction = instruction[0];
                int value = int.Parse(instruction.Substring(1));

                if (direction == 'F')
                {
                    foreach (Direction carDir in Enum.GetValues(typeof(Direction)))
                    {
                        distances[carDir] += waypoint[(int)carDir] * value;
                    }
                    continue;
                }

                if (direction == 'L')
                {
                    waypoint = RotateWaypoint(waypoint, -value);
                    continue;
                }

                if (direction == 'R')
                {
                    waypoint = RotateWaypoint(waypoint, value);
                    continue;
                }

                // default: go to specified direction
                waypoint[(int)DirectionFromChar(direction)] += value;
            }

            int result = GetManhattanDistance(distances);

            return result.ToString();
        }

        private static Dictionary<int, int> RotateWaypoint(Dictionary<int, int> waypoint, int degree)
        {
            if (degree < 0)
            {
                degree += 360;
            }

            return new Dictionary<int, int>
            {
                { (0 + degree) % 360, waypoint[0] },
                { (90 + degree) % 360, waypoint[90] },
                { (180 + degree) % 360, waypoint[180] },
                { (270 + degree) % 360, waypoint[270] },
            };
        }
    }
}
