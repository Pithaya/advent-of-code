using AdventOfCode.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020
{
    [DayNumber(14)]
    public class Day14 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Dictionary<int, bool> mask = new Dictionary<int, bool>(); // 36 bits
            long result = 0;

            foreach (string line in input)
            {
                if (line.Contains("mask"))
                {
                    // Update mask
                    mask = line
                        .Split(" = ")
                        .Last()
                        .Reverse()
                        .Select((c, i) => (Char: c, Index: i))
                        .Where(x => x.Char != 'X')
                        .Select(x => (Value: x.Char == '1' ? true : false, Index: x.Index))
                        .ToDictionary(x => x.Index, x => x.Value);

                    continue;
                }

                // Update the memory
                Regex indexCapture = new Regex("(?<=mem\\[)[0-9]*(?=\\])");
                long memoryIndex = long.Parse(indexCapture.Match(line.Split(" = ").First()).Value);
                long value = long.Parse(line.Split(" = ").Last());

                BitArray valueBits = new BitArray(BitConverter.GetBytes(value));
                foreach (var masked in mask)
                {
                    valueBits.Set(masked.Key, masked.Value);
                }
                byte[] array = new byte[64];
                valueBits.CopyTo(array, 0);
                long converted = BitConverter.ToInt64(array);
                memory[memoryIndex] = converted;
            }

            result = memory.Sum(m => m.Value);

            return result.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Dictionary<int, bool> mask = new Dictionary<int, bool>(); // 36 bits, true == overwrite, false == floating
            long result = 0;

            foreach (string line in input)
            {
                if (line.Contains("mask"))
                {
                    // Update mask
                    mask = line
                        .Split(" = ")
                        .Last()
                        .Reverse()
                        .Select((c, i) => (Char: c, Index: i))
                        .Where(x => x.Char != '0')
                        .Select(x => (Value: x.Char == '1' ? true : false, Index: x.Index))
                        .ToDictionary(x => x.Index, x => x.Value);

                    continue;
                }

                // Update the memory
                Regex indexCapture = new Regex("(?<=mem\\[)[0-9]*(?=\\])");
                long memoryIndex = long.Parse(indexCapture.Match(line.Split(" = ").First()).Value);
                long value = long.Parse(line.Split(" = ").Last());

                // Compute the new address
                BitArray memoryIndexBits = new BitArray(BitConverter.GetBytes(memoryIndex));

                // Overwrite 1
                foreach (var masked in mask.Where(m => m.Value == true))
                {
                    memoryIndexBits.Set(masked.Key, masked.Value);
                }

                // Permutate and save all values
                var permutationMask = mask.Where(m => m.Value == false);
                if (permutationMask.Any())
                {
                    Recurse(memory, memoryIndexBits, value, 0, permutationMask, true);
                    Recurse(memory, memoryIndexBits, value, 0, permutationMask, false);
                }
            }

            result = memory.Sum(m => m.Value);

            return result.ToString();
        }

        private static void Recurse(Dictionary<long, long> memory, BitArray memoryIndexBits, long valueToSet, int currentMaskIndex, IEnumerable<KeyValuePair<int, bool>> mask, bool maskValue)
        {
            if (currentMaskIndex == mask.Count())
            {
                byte[] array = new byte[64];
                memoryIndexBits.CopyTo(array, 0);
                long converted = BitConverter.ToInt64(array);
                memory[converted] = valueToSet;
                return;
            }

            memoryIndexBits.Set(mask.ElementAt(currentMaskIndex).Key, maskValue);

            Recurse(memory, new BitArray(memoryIndexBits), valueToSet, currentMaskIndex + 1, mask, true);
            Recurse(memory, new BitArray(memoryIndexBits), valueToSet, currentMaskIndex + 1, mask, false);
        }
    }
}
