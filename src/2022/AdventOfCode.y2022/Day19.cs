using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{
    class Blueprint
    {
        public int Index { get; init; }

        public int OreRobotCost { get; init; }
        public int ClayRobotCost { get; init; }
        public (int Ore, int Clay) ObsidianRobotCost { get; init; }
        public (int Ore, int Obsidian) GeodeRobotCost { get; init; }
    }

    [DayNumber(19)]
    public class Day19 : Day
    {
        private Regex blueprintRegex = new Regex("Blueprint (\\d+): Each ore robot costs (\\d+) ore\\. Each clay robot costs (\\d+) ore\\. Each obsidian robot costs (\\d+) ore and (\\d+) clay\\. Each geode robot costs (\\d+) ore and (\\d+) obsidian\\.");

        private List<Blueprint> ParseBlueprints(IEnumerable<string> input)
        {
            List<Blueprint> blueprints = new List<Blueprint>();

            foreach (string line in input)
            {
                var groups = blueprintRegex.GetCapturingGroups(line);

                blueprints.Add(new Blueprint()
                {
                    Index = int.Parse(groups[0].Value),
                    OreRobotCost = int.Parse(groups[1].Value),
                    ClayRobotCost = int.Parse(groups[2].Value),
                    ObsidianRobotCost = (int.Parse(groups[3].Value), int.Parse(groups[4].Value)),
                    GeodeRobotCost = (int.Parse(groups[4].Value), int.Parse(groups[5].Value))
                });
            }

            return blueprints;
        }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Blueprint> blueprints = ParseBlueprints(input);

            return string.Empty;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
