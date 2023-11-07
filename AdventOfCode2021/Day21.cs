using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(21)]
    public class Day21 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var firstPlayer = new Player(
              Convert.ToInt32(input
                      .First()
                      .Split(": ")
                      .Last()),
              0, true);

            var secondPlayer = new Player(
               Convert.ToInt32(input
                       .Last()
                       .Split(": ")
                       .Last()),
               0, false);

            var players = new List<Player>()
            {
                firstPlayer,
                secondPlayer
            };

            var dice = new DeterministicDice();
            var rolls = 0;

            bool playerWon = false;
            while (!playerWon)
            {
                foreach (var player in players)
                {
                    var r1 = dice.Roll();
                    var r2 = dice.Roll();
                    var r3 = dice.Roll();
                    var move = r1 + r2 + r3;
                    rolls += 3;

                    var newSpace = player.CurrentSpace + move;

                    if (newSpace > 10)
                    {
                        newSpace %= 10;

                        if (newSpace == 0)
                        {
                            newSpace = 10;
                        }
                    }

                    player.CurrentSpace = newSpace;
                    player.Score += newSpace;

                    if (player.Score >= 1000)
                    {
                        playerWon = true;
                        break;
                    }
                }
            }

            var losingPlayer = players.Single(p => p.Score < 1000);
            return (losingPlayer.Score * rolls).ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            var firstPlayer = new Player(
                Convert.ToInt32(input
                        .First()
                        .Split(": ")
                        .Last()),
                0, true);

            var secondPlayer = new Player(
               Convert.ToInt32(input
                       .Last()
                       .Split(": ")
                       .Last()),
               0, false);

            // Roll value -> number of universes to spawn
            Dictionary<int, ulong> possibleRolls = new Dictionary<int, ulong>();
            for (int roll1 = 1; roll1 < 4; roll1++)
            {
                for (int roll2 = 1; roll2 < 4; roll2++)
                {
                    for (int roll3 = 1; roll3 < 4; roll3++)
                    {
                        var roll = roll1 + roll2 + roll3;
                        possibleRolls.AddOrUpdate<int, ulong>(roll, 1, (r, v) => v + 1);
                    }
                }
            }

            var wins = GetWins(firstPlayer, secondPlayer, possibleRolls);

            return Math.Max(wins.FirstPlayerWin, wins.SecondPlayerWin).ToString();
        }

        private (ulong FirstPlayerWin, ulong SecondPlayerWin) GetWins(Player currentPlayer, Player otherPlayer, Dictionary<int, ulong> possibleRolls)
        {
            (ulong FirstPlayerWin, ulong SecondPlayerWin) childResults = (0, 0);

            foreach (var possibleRoll in possibleRolls)
            {
                var updatedPlayer = GetUpdatedPlayer(currentPlayer, possibleRoll.Key);

                if (updatedPlayer.IsWinning)
                {
                    (ulong, ulong) win = updatedPlayer.IsFirst ? ((ulong)1, (ulong)0) : (0, 1);
                    childResults.FirstPlayerWin += win.Item1 * possibleRoll.Value;
                    childResults.SecondPlayerWin += win.Item2 * possibleRoll.Value;
                }
                else
                {
                    var childWins = GetWins(otherPlayer, updatedPlayer, possibleRolls);
                    childResults.FirstPlayerWin += childWins.FirstPlayerWin * possibleRoll.Value;
                    childResults.SecondPlayerWin += childWins.SecondPlayerWin * possibleRoll.Value;
                }
            }

            return childResults;
        }

        private Player GetUpdatedPlayer(Player player, int roll)
        {
            var newSpace = (player.CurrentSpace + roll) % 10;

            if (newSpace == 0)
            {
                newSpace = 10;
            }

            return new Player(newSpace, player.Score + newSpace, player.IsFirst);
        }

        class Player
        {
            const int winningScore = 21;

            public bool IsFirst { get; set; }
            public int Score { get; set; }
            public int CurrentSpace { get; set; }

            public bool IsWinning => Score >= winningScore;

            public Player(int currentSpace, int score, bool isFirst)
            {
                Score = score;
                CurrentSpace = currentSpace;
                IsFirst = isFirst;
            }
        }

        abstract class Dice
        {
            public abstract int Roll();
        }

        class DeterministicDice : Dice
        {
            private int value = 0;

            public override int Roll()
            {
                value++;

                if(value == 101)
                {
                    value = 1;
                }

                return value;
            }
        }
    }
}
