using AdventOfCode.Common;

namespace AdventOfCode.y2020
{
    [DayNumber(11)]
    public class Day11 : Day
    {
        public const char EmptySeat = 'L';
        public const char OccupiedSeat = '#';
        public const char Floor = '.';

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            char[][] seats = new char[input.Count()][];

            for (int i = 0; i < input.Count(); i++)
            {
                seats[i] = input.ElementAt(i).ToCharArray();
            }

            int occupiedSeats;
            while (true)
            {
                occupiedSeats = 0;
                bool hasDifferentState = false;

                char[][] newState = new char[seats.Count()][];

                for (int i = 0; i < seats.Count(); i++)
                {
                    IEnumerable<char> newSeats = seats[i].Select((s, j) => GetNewState(seats, i, j));

                    int newRowOccupiedSeats = newSeats.Where(c => c == OccupiedSeat).Count();
                    int currentRowOccupiedSeats = seats[i].Where(c => c == OccupiedSeat).Count();

                    if (currentRowOccupiedSeats != newRowOccupiedSeats)
                    {
                        hasDifferentState = true;
                    }

                    occupiedSeats += newRowOccupiedSeats;
                    newState[i] = newSeats.ToArray();
                }

                if (!hasDifferentState)
                {
                    break;
                }

                seats = newState;
            }

            return occupiedSeats.ToString();
        }

        private static char GetNewState(char[][] seats, int iSeat, int ySeat)
        {
            char current = seats[iSeat][ySeat];

            // If floor: do nothing
            if (current == Floor)
            {
                return Floor;
            }

            int occupiedAdjacent = 0;

            for (int i = Math.Max(iSeat - 1, 0); i <= Math.Min(iSeat + 1, seats.Length - 1); i++)
            {
                for (int y = Math.Max(ySeat - 1, 0); y <= Math.Min(ySeat + 1, seats[0].Length - 1); y++)
                {
                    // Ignore current seat
                    if (iSeat == i && ySeat == y)
                    {
                        continue;
                    }

                    if (seats[i][y] == '#')
                    {
                        occupiedAdjacent++;
                    }
                }
            }

            if (current == EmptySeat && occupiedAdjacent == 0)
            {
                return OccupiedSeat;
            }

            if (current == OccupiedSeat && occupiedAdjacent >= 4)
            {
                return EmptySeat;
            }

            return current;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            char[][] seats = new char[input.Count()][];

            for (int i = 0; i < input.Count(); i++)
            {
                seats[i] = input.ElementAt(i).ToCharArray();
            }

            int occupiedSeats;
            while (true)
            {
                occupiedSeats = 0;
                bool hasDifferentState = false;

                char[][] newState = new char[seats.Count()][];

                for (int i = 0; i < seats.Count(); i++)
                {
                    IEnumerable<char> newSeats = seats[i].Select((s, j) => GetNewState2(seats, i, j));

                    int newRowOccupiedSeats = newSeats.Where(c => c == OccupiedSeat).Count();
                    int currentRowOccupiedSeats = seats[i].Where(c => c == OccupiedSeat).Count();

                    if (currentRowOccupiedSeats != newRowOccupiedSeats)
                    {
                        hasDifferentState = true;
                    }

                    occupiedSeats += newRowOccupiedSeats;
                    newState[i] = newSeats.ToArray();
                }

                if (!hasDifferentState)
                {
                    break;
                }

                seats = newState;
            }


            return occupiedSeats.ToString();
        }

        private static char GetNewState2(char[][] seats, int iSeat, int ySeat)
        {
            char current = seats[iSeat][ySeat];

            // If floor: do nothing
            if (current == Floor)
            {
                return Floor;
            }

            int occupiedAdjacent = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Ignore current seat
                    if (i == 0 && y == 0)
                    {
                        continue;
                    }

                    if (LookForSeat(seats, iSeat, ySeat, i, y) == OccupiedSeat)
                    {
                        occupiedAdjacent++;
                    }
                }
            }

            if (current == EmptySeat && occupiedAdjacent == 0)
            {
                return OccupiedSeat;
            }

            if (current == OccupiedSeat && occupiedAdjacent >= 5)
            {
                return EmptySeat;
            }

            return current;
        }

        private static char LookForSeat(char[][] seats, int iSeat, int ySeat, int iIncrement, int yIncrement)
        {
            int i = iSeat;
            int y = ySeat;
            int maxI = seats.Length;
            int maxY = seats[0].Length;

            while (true)
            {
                i += iIncrement;
                y += yIncrement;

                if (i < 0 || i == maxI)
                {
                    return Floor;
                }

                if (y < 0 || y == maxY)
                {
                    return Floor;
                }

                char seen = seats[i][y];

                if (seen == OccupiedSeat || seen == EmptySeat)
                {
                    return seen;
                }
            }
        }
    }
}
