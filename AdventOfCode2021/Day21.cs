using AdventOfCode;
using System.Diagnostics;

namespace AdventOfCode.y2021
{
    public class Day21 : Day
    {
        public Day21(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var playerOne = new Player
            {
                CurrentSpace = Convert.ToInt32(input
                    .First()
                    .Split(": ")
                    .Last()),
                Score = 0
            };

            var playerTwo = new Player
            {
                CurrentSpace = Convert.ToInt32(input
                   .Last()
                   .Split(": ")
                   .Last()),
                Score = 0
            };

            var players = new List<Player>()
            {
                playerOne,
                playerTwo
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

                        if(newSpace == 0)
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
            ulong playerOneWins = 0;
            ulong playerTwoWins = 0;

            Dictionary<Universe, ulong> universes = new Dictionary<Universe, ulong>()
            {
                {  
                    new Universe()
                    {
                        FirstPlayedLast = false,
                        FirstPlayer = new PlayerState
                        {
                            Score = 0,
                            CurrentSpace = Convert.ToInt32(input
                                .First()
                                .Split(": ")
                                .Last())
                        },
                        SecondPlayer = new PlayerState
                        {
                            Score = 0,
                            CurrentSpace = Convert.ToInt32(input
                                .Last()
                                .Split(": ")
                                .Last())
                        },
                    }, 
                    1
                }
            };

            Dictionary<(PlayerState state, int roll), PlayerState> stateCache = new Dictionary<(PlayerState state, int roll), PlayerState>();

            while (universes.Any())
            {
                var currentUniverse = universes.First();
                universes.Remove(currentUniverse.Key);

                var currentPlayer = currentUniverse.Key.FirstPlayedLast ? currentUniverse.Key.SecondPlayer : currentUniverse.Key.FirstPlayer;

                // Roll the dice three times, and generate new possibilities every time
                for(int roll1 = 1; roll1 < 4; roll1++)
                {
                    for (int roll2 = 1; roll2 < 4; roll2++)
                    {
                        for (int roll3 = 1; roll3 < 4; roll3++)
                        {
                            var roll = roll1 + roll2 + roll3;
                            PlayerState updatedPlayer;

                            if(stateCache.ContainsKey((currentPlayer, roll)))
                            {
                                updatedPlayer = stateCache[(currentPlayer, roll)];
                            }
                            else
                            {
                                updatedPlayer = CreateUpdatedPlayer(currentPlayer, roll);
                                stateCache.Add((currentPlayer, roll), updatedPlayer);
                            }

                            if (updatedPlayer.Score >= 21)
                            {
                                if (!currentUniverse.Key.FirstPlayedLast)
                                {
                                    playerOneWins += currentUniverse.Value;
                                    Debug.WriteLine(playerOneWins);
                                }
                                else
                                {
                                    playerTwoWins += currentUniverse.Value;
                                }
                            }
                            else
                            {
                                var newUniverse = new Universe()
                                {
                                    FirstPlayedLast = !currentUniverse.Key.FirstPlayedLast,
                                    FirstPlayer = !currentUniverse.Key.FirstPlayedLast ? updatedPlayer : currentUniverse.Key.FirstPlayer,
                                    SecondPlayer = currentUniverse.Key.FirstPlayedLast ? updatedPlayer : currentUniverse.Key.SecondPlayer,
                                };

                                if (universes.ContainsKey(newUniverse))
                                {
                                    universes[newUniverse] += currentUniverse.Value;
                                }
                                else
                                {
                                    universes.Add(newUniverse, currentUniverse.Value);
                                }
                            }
                        }
                    }
                }
            }

            return Math.Max(playerOneWins, playerTwoWins).ToString();
        }

        private PlayerState CreateUpdatedPlayer(PlayerState player, int roll)
        {
            var newSpace = player.CurrentSpace + roll;

            if (newSpace > 10)
            {
                newSpace %= 10;

                if (newSpace == 0)
                {
                    newSpace = 10;
                }
            }

            return new PlayerState()
            {
                CurrentSpace = newSpace,
                Score = player.Score + newSpace
            };
        }

        class Player
        {
            public int Score { get; set; }
            public int CurrentSpace { get; set; }
        }

        struct PlayerState
        {
            public int Score { get; set; }
            public int CurrentSpace { get; set; }
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

        struct Universe
        {
            public bool FirstPlayedLast { get; set; }
            public PlayerState FirstPlayer { get; set; }
            public PlayerState SecondPlayer { get; set; }
        }
    }
}
