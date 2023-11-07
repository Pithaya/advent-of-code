using AdventOfCode.Common;

namespace AdventOfCode.y2022
{
    enum Hand
    {
        Rock,
        Paper,
        Scissors
    }

    enum PlayerResult
    {
        Win,
        Lose,
        Draw
    }

    [DayNumber(2)]
    public class Day2 : Day
    {
        // Elf hand -> Player hand and player result
        private Dictionary<Hand, Dictionary<Hand, PlayerResult>> outcomes = new Dictionary<Hand, Dictionary<Hand, PlayerResult>>()
        {
            { Hand.Rock, new Dictionary<Hand, PlayerResult>()
            {
                { Hand.Rock, PlayerResult.Draw },
                { Hand.Paper, PlayerResult.Win },
                { Hand.Scissors, PlayerResult.Lose },
            }},
            { Hand.Paper, new Dictionary<Hand, PlayerResult>()
            {
                { Hand.Rock, PlayerResult.Lose },
                { Hand.Paper, PlayerResult.Draw },
                { Hand.Scissors, PlayerResult.Win },
            }},
            { Hand.Scissors, new Dictionary<Hand, PlayerResult>()
            {
                { Hand.Rock, PlayerResult.Win },
                { Hand.Paper, PlayerResult.Lose },
                { Hand.Scissors, PlayerResult.Draw },
            }}
        };

        private static int GetScore(Hand hand)
        {
            return hand switch
            {
                Hand.Rock => 1,
                Hand.Paper => 2,
                Hand.Scissors => 3,
            };
        }

        private static Hand ParsePlayerHand(string hand)
        {
            return hand switch
            {
                "X" => Hand.Rock,
                "Y" => Hand.Paper,
                "Z" => Hand.Scissors,
                _ => throw new ArgumentException("Invalid hand.")
            };
        }

        private static Hand ParseElfHand(string hand)
        {
            return hand switch
            {
                "A" => Hand.Rock,
                "B" => Hand.Paper,
                "C" => Hand.Scissors,
                _ => throw new ArgumentException("Invalid hand.")
            };
        }

        private static PlayerResult ParseResult(string result)
        {
            return result switch
            {
                "X" => PlayerResult.Lose,
                "Y" => PlayerResult.Draw,
                "Z" => PlayerResult.Win,
                _ => throw new ArgumentException("Invalid result.")
            };
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int score = 0;

            foreach(var round in input)
            {
                var elfHand = ParseElfHand(round.Split(" ")[0]);
                var playerHand = ParsePlayerHand(round.Split(" ")[1]);

                score += outcomes[elfHand][playerHand] switch
                {
                    PlayerResult.Lose => 0,
                    PlayerResult.Draw => 3,
                    PlayerResult.Win => 6,
                };

                score += GetScore(playerHand);
            }

            return score.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            int score = 0;

            foreach (var round in input)
            {
                var elfHand = ParseElfHand(round.Split(" ")[0]);
                var playerExpectedResult = ParseResult(round.Split(" ")[1]);
                var playerHand = outcomes[elfHand].Single(entry => entry.Value == playerExpectedResult).Key;

                score += playerExpectedResult switch
                {
                    PlayerResult.Lose => 0,
                    PlayerResult.Draw => 3,
                    PlayerResult.Win => 6,
                };

                score += GetScore(playerHand);
            }

            return score.ToString();
        }
    }
}
