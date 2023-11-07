using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(4)]
    public class Day4 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<int> bingoNumbers = input
                .First()
                .Split(",")
                .Select(s => int.Parse(s))
                .ToList();

            input = input.Skip(1);

            List<Board> boards = CreateBoards(input);

            Board? winningBoard = null;
            int finalValue = 0;
            foreach(var number in bingoNumbers)
            {
                foreach(var board in boards)
                {
                    board.Mark(number);

                    if (board.IsWinning())
                    {
                        winningBoard = board;
                        finalValue = number;
                        break;
                    }
                }

                if(winningBoard != null)
                {
                    break;
                }
            }

            return winningBoard.CalculateScore(finalValue).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<int> bingoNumbers = input
                .First()
                .Split(",")
                .Select(s => int.Parse(s))
                .ToList();

            input = input.Skip(1);

            List<Board> boards = CreateBoards(input);

            List<Board> winningBoards = new List<Board>();
            Board? lastWinningBoard = null;
            int finalValue = 0;
            foreach (var number in bingoNumbers)
            {
                foreach (var board in boards.Where(b => !winningBoards.Contains(b)))
                {
                    board.Mark(number);

                    if (board.IsWinning())
                    {
                        winningBoards.Add(board);

                        if(winningBoards.Count == boards.Count)
                        {
                            lastWinningBoard = board;
                            finalValue = number;
                            break;
                        }
                    }
                }

                if (winningBoards.Count == boards.Count)
                {
                    break;
                }
            }

            return lastWinningBoard.CalculateScore(finalValue).ToString();
        }

        private List<Board> CreateBoards(IEnumerable<string> input)
        {
            List<Board> boards = new List<Board>();
            Board? currentBoard = null;
            int currentRow = 0;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    currentBoard = new Board(5, 5);
                    currentRow = 0;
                    boards.Add(currentBoard);
                    continue;
                }

                if (currentBoard == null)
                {
                    throw new InvalidOperationException("The current board wasn't created.");
                }

                BoardCell[] row = line
                    .Split(" ")
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => new BoardCell
                    {
                        Value = int.Parse(s),
                        Marked = false
                    })
                    .ToArray();

                currentBoard[currentRow] = row;
                currentRow++;
            }

            return boards;
        }
    }

    public class Board : SimpleGrid<BoardCell>
    {
        public Board(int rowIndex, int columnIndex) : base(rowIndex, columnIndex)
        {}

        public bool IsWinning()
        {
            for(int rowIndex = 0; rowIndex < cells.GetLength(0); rowIndex++)
            {
                if (IsRowMarked(rowIndex))
                {
                    return true;
                }
            }

            for (int columnIndex = 0; columnIndex < cells.GetLength(1); columnIndex++)
            {
                if (IsColumnMarked(columnIndex))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsRowMarked(int rowIndex)
        {
            return GetRow(rowIndex).All(cell => cell.Marked);
        }

        public bool IsColumnMarked(int columnIndex)
        {
            return GetColumn(columnIndex).All(cell => cell.Marked);
        }

        public void Mark(int value)
        {
            for(var rowId = 0; rowId < cells.GetLength(0); rowId++)
            {
                foreach(var cell in GetRow(rowId).Where(c => c.Value == value))
                {
                    cell.Marked = true;
                }
            }
        }

        public int CalculateScore(int finalValue)
        {
            int sum = 0;
            for (var rowId = 0; rowId < cells.GetLength(0); rowId++)
            {
                sum += GetRow(rowId)
                    .Where(c => !c.Marked)
                    .Select(c => c.Value)
                    .Sum();
            }

            return sum * finalValue;
        }
    }

    public class BoardCell
    {
        public int Value { get; set; }
        public bool Marked { get; set; }
    }
}
