using AdventOfCode;
using AdventOfCode.Common;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AdventOfCode.y2021
{
    public class Day21 : Day
    {
        public Day21(string inputFolder) : base(inputFolder)
        { }

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
            Dictionary<Universe, ulong> universes = new Dictionary<Universe, ulong>();
            universes.Add(new Universe()
            {
                IsFirstPlaying = true,
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
            }, 1);

            // Universe state -> new universes for all rolls
            Dictionary<Universe, Dictionary<Universe, ulong>> stateCache = new Dictionary<Universe, Dictionary<Universe, ulong>>();

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

            ulong playerOneWins = 0;
            ulong playerTwoWins = 0;

            // Fill cache
            for(int playerOneScore = 0; playerOneScore < 22; playerOneScore++)
            {
                for (int playerTwoScore = 0; playerTwoScore < 22; playerTwoScore++)
                {
                    for (int playerOneSpace = 1; playerOneSpace < 11; playerOneSpace++)
                    {
                        for (int playerTwoSpace = 1; playerTwoSpace < 11; playerTwoSpace++)
                        {
                            var currentUniverse = new Universe()
                            {
                                IsFirstPlaying = true,
                                FirstPlayer = new PlayerState
                                {
                                    Score = playerOneScore,
                                    CurrentSpace = playerOneSpace
                                },
                                SecondPlayer = new PlayerState
                                {
                                    Score = playerTwoScore,
                                    CurrentSpace = playerTwoSpace
                                },
                            };

                            var currentUniverse2 = new Universe()
                            {
                                IsFirstPlaying = false,
                                FirstPlayer = new PlayerState
                                {
                                    Score = playerOneScore,
                                    CurrentSpace = playerOneSpace
                                },
                                SecondPlayer = new PlayerState
                                {
                                    Score = playerTwoScore,
                                    CurrentSpace = playerTwoSpace
                                },
                            };

                            stateCache.Add(currentUniverse, CreateChildUniverses(currentUniverse, possibleRolls));
                            stateCache.Add(currentUniverse2, CreateChildUniverses(currentUniverse2, possibleRolls));
                        }
                    }
                }
            }

            var test = GetWins(universes.First().Key, stateCache, possibleRolls);

            return Math.Max(playerOneWins, playerTwoWins).ToString();
        }

        private (ulong FirstPlayerWin, ulong SecondPlayerWin) GetWins(Universe currentUniverse, Dictionary<Universe, Dictionary<Universe, ulong>> stateCache, Dictionary<int, ulong> possibleRolls)
        {
            if (currentUniverse.IsFirstPlayerWinning)
            {
                return (1, 0);
            }
            else if (currentUniverse.IsSecondPlayerWinning)
            {
                return (0, 1);
            }

            (ulong FirstPlayerWin, ulong SecondPlayerWin) childResults = (0, 0);
            Dictionary<Universe, ulong> children = stateCache[currentUniverse];
            foreach(var child in children)
            {
                var childWins = GetWins(child.Key, stateCache, possibleRolls);
                childResults.FirstPlayerWin += childWins.FirstPlayerWin * child.Value;
                childResults.SecondPlayerWin += childWins.SecondPlayerWin * child.Value;
            }

            return childResults;
        }

        private Dictionary<Universe, ulong> CreateChildUniverses(Universe universe, Dictionary<int, ulong> possibleRolls)
        {
            var result = new Dictionary<Universe, ulong>();

            if(universe.IsFirstPlayerWinning || universe.IsSecondPlayerWinning)
            {
                // No children
                return result;
            }

            foreach (var roll in possibleRolls)
            {
                var currentPlayer = universe.CurrentPlayer;

                var newSpace = currentPlayer.CurrentSpace + roll.Key;

                if (newSpace > 10)
                {
                    newSpace %= 10;

                    if (newSpace == 0)
                    {
                        newSpace = 10;
                    }
                }

                var updatedPlayer = new PlayerState()
                {
                    CurrentSpace = newSpace,
                    Score = currentPlayer.Score + newSpace
                };

                result.Add(new Universe()
                {
                    IsFirstPlaying = !universe.IsFirstPlaying,
                    FirstPlayer = universe.IsFirstPlaying ? updatedPlayer : universe.FirstPlayer,
                    SecondPlayer = universe.IsFirstPlaying ? universe.SecondPlayer : updatedPlayer,
                }, roll.Value);
            }

            return result;
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
            public bool IsFirstPlaying { get; set; }
            public PlayerState FirstPlayer { get; set; }
            public PlayerState SecondPlayer { get; set; }

            public PlayerState CurrentPlayer => IsFirstPlaying ? FirstPlayer : SecondPlayer;
            public PlayerState LastPlayer => IsFirstPlaying ? SecondPlayer : FirstPlayer;

            public bool IsFirstPlayerWinning => FirstPlayer.Score >= 21;
            public bool IsSecondPlayerWinning => SecondPlayer.Score >= 21;
        }
    }
}
