using AdventOfCode.Common;
using System.Text.Json.Nodes;

namespace AdventOfCode.y2022
{
    class PackagePair
    {
        public JsonNode Left { get; set; }
        public JsonNode Right { get; set; }

        public int Index { get; set; }

        public bool IsCorrectOrder => CompareNodes(Left, Right) < 0;

        public static int CompareNodes(JsonNode left, JsonNode right)
        {
            return CompareArray(left.AsArray(), right.AsArray());
        }

        public static int CompareArray(JsonArray left, JsonArray right)
        {
            /*
            Debug.WriteLine($"Compare arrays:");
            Debug.WriteLine($"Left: {left}");
            Debug.WriteLine($"Right: {right}");
            */

            for (int i = 0; i < Math.Max(left.Count, right.Count); i++)
            {
                // Left ran out of items: correct order
                if(i >= left.Count)
                {
                    return -1;
                }
                // Right ran out of items: wrong order
                else if(i >= right.Count)
                {
                    return 1;
                }

                JsonNode leftNode = left[i];
                JsonNode rightNode = right[i];

                /*
                Debug.WriteLine($"Left: {leftNode}");
                Debug.WriteLine($"Right: {rightNode}");
                Debug.WriteLine(string.Empty);
                */

                // Compare ints
                if (leftNode is JsonValue && rightNode is JsonValue)
                {
                    if(leftNode.GetValue<int>() < rightNode.GetValue<int>())
                    {
                        return -1;
                    }
                    else if(leftNode.GetValue<int>() > rightNode.GetValue<int>())
                    {
                        return 1;
                    }
                }
                else
                {
                    // Turn both into arrays and compare them
                    if (leftNode is not JsonArray)
                    {
                        leftNode = new JsonArray(JsonValue.Create(leftNode.GetValue<int>()));
                    }

                    if (rightNode is not JsonArray)
                    {
                        rightNode = new JsonArray(JsonValue.Create(rightNode.GetValue<int>()));
                    }

                    int arrayCompare = CompareArray(leftNode.AsArray(), rightNode.AsArray());
                    if (arrayCompare != 0)
                    {
                        return arrayCompare;
                    }
                }
            }

            return 0;
        }
    }

    [DayNumber(13)]
    public class Day13 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            int correctOrderPairsCount = 0;

            int pairIndex = 1;

            for (int i = 0; i < input.Count(); i += 3)
            {
                var currentPair = new PackagePair
                {
                    Left = JsonNode.Parse(input.ElementAt(i))!,
                    Right = JsonNode.Parse(input.ElementAt(i + 1))!,
                    Index = pairIndex
                };

                pairIndex++;

                /*
                Debug.WriteLine($"Compare pair: {currentPair.Index}");
                Debug.WriteLine($"Left: {currentPair.Left}");
                Debug.WriteLine($"Right: {currentPair.Right}");
                Debug.WriteLine(string.Empty);
                */

                if (currentPair.IsCorrectOrder)
                {
                    correctOrderPairsCount += currentPair.Index;
                }
            }

            return correctOrderPairsCount.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<JsonNode> packages = new List<JsonNode>();

            foreach(string line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                packages.Add(JsonNode.Parse(line)!);
            }

            var firstDividerPacket = JsonNode.Parse("[[2]]")!;
            var secondDividerPacket = JsonNode.Parse("[[6]]")!;

            packages.Add(firstDividerPacket);
            packages.Add(secondDividerPacket);

            packages.Sort(PackagePair.CompareNodes);

            int firstDividerIndex = packages.FindIndex(node => node == firstDividerPacket) + 1;
            int secondDividerIndex = packages.FindIndex(node => node == secondDividerPacket) + 1;

            return (firstDividerIndex * secondDividerIndex).ToString();
        }
    }
}
