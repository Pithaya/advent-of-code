using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2022
{
    class PackagePair
    {
        public JsonNode Left { get; set; }
        public JsonNode Right { get; set; }

        public int Index { get; set; }

        public bool IsCorrectOrder => CompareNodes(Left, Right);

        private bool CompareNodes(JsonNode left, JsonNode right)
        {
            return CompareArray(left.AsArray(), right.AsArray()) ?? throw new ArgumentException("Both nodes are equal");
        }

        private bool? CompareArray(JsonArray left, JsonArray right)
        {
            Debug.WriteLine($"Compare arrays:");
            Debug.WriteLine($"Left: {left}");
            Debug.WriteLine($"Right: {right}");

            for (int i = 0; i < Math.Max(left.Count, right.Count); i++)
            {
                // Left ran out of items: correct order
                if(i >= left.Count)
                {
                    return true;
                }
                // Right ran out of items: wrong order
                else if(i >= right.Count)
                {
                    return false;
                }

                JsonNode leftNode = left[i];
                JsonNode rightNode = right[i];

                Debug.WriteLine($"Left: {leftNode}");
                Debug.WriteLine($"Right: {rightNode}");
                Debug.WriteLine(string.Empty);

                // Compare ints
                if (leftNode is JsonValue && rightNode is JsonValue)
                {
                    if(leftNode.GetValue<int>() < rightNode.GetValue<int>())
                    {
                        return true;
                    }
                    else if(leftNode.GetValue<int>() > rightNode.GetValue<int>())
                    {
                        return false;
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

                    bool? arrayCompare = CompareArray(leftNode.AsArray(), rightNode.AsArray());
                    if (arrayCompare != null)
                    {
                        return arrayCompare;
                    }
                }
            }

            return null;
        }
    }

    public class Day13 : Day
    {
        public Day13(string inputFolder) : base(inputFolder)
        { }

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

                Debug.WriteLine($"Compare pair: {currentPair.Index}");
                Debug.WriteLine($"Left: {currentPair.Left}");
                Debug.WriteLine($"Right: {currentPair.Right}");
                Debug.WriteLine(string.Empty);

                if (currentPair.IsCorrectOrder)
                {
                    correctOrderPairsCount += currentPair.Index;
                }
            }

            return correctOrderPairsCount.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
