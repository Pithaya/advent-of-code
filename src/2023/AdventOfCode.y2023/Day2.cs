using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2023
{
    [DayNumber(2)]
    public class Day2 : Day
    {
        private readonly Regex gameRegex = new Regex("Game (?<id>\\d+):(?<set>.+)");

        private List<Game> GetGames(IEnumerable<string> input)
        {
            List<Game> games = new List<Game>();

            foreach (var line in input)
            {
                // End line with ; to make regex easier
                var matches = gameRegex.Match(line + ";");

                var game = new Game
                {
                    Id = int.Parse(matches.Groups["id"].Value),
                    Cubes = new Dictionary<string, int>()
                };

                foreach (var set in matches.Groups["set"].Value.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                {
                    foreach (var cubeCount in set.Split(','))
                    {
                        var splitted = cubeCount.Trim().Split(" ");
                        var color = splitted[1];
                        var count = int.Parse(splitted[0]);

                        if (game.Cubes.ContainsKey(color))
                        {
                            if (game.Cubes[color] < count)
                            {
                                game.Cubes[color] = count;
                            }
                        }
                        else
                        {
                            game.Cubes.Add(color, count);
                        }
                    }
                }

                games.Add(game);
            }

            return games;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Game> games = GetGames(input);

            int total = 0;

            foreach (var game in games)
            {
                if (game.IsPossible(new Dictionary<string, int> { { "red", 12 }, { "green", 13 }, { "blue", 14 } }))
                {
                    total += game.Id;
                }
            }

            return total.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Game> games = GetGames(input);

            double total = games.Sum(g => g.GetPower());

            return total.ToString();
        }
    }

    class Game
    {
        public int Id { get; set; }
        public Dictionary<string, int> Cubes { get; set; }

        public bool IsPossible(Dictionary<string, int> cubes)
        {
            foreach (var cubeCount in cubes)
            {
                if (Cubes.GetValueOrDefault(cubeCount.Key) > cubeCount.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public double GetPower()
        {
            return Cubes["red"] * Cubes["green"] * Cubes["blue"];
        }
    }
}
