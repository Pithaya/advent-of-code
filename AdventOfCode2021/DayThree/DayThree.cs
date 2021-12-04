using System.Collections;
using System.Linq;

namespace AdventOfCode2021
{
    public class DayThree : BaseDay
    {
        public override string ExecutePartOne(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DayThree), file);

            int length = input.First().Length;
            BitArray gammaRate = new BitArray(length);
            BitArray epsilonRate = new BitArray(length);

            for(int i = 0; i < gammaRate.Length; i++)
            {
                IEnumerable<char> charsAtIndex = input.Select(l => l[i]);

                int numberOfOnes = charsAtIndex.Count(c => c == '1');
                int numberOfZeroes = charsAtIndex.Count(c => c == '0');

                if(numberOfOnes > numberOfZeroes)
                {
                    gammaRate[length - i - 1] = true;
                    epsilonRate[length - i - 1] = false;
                }
                else
                {
                    epsilonRate[length - i - 1] = true;
                    gammaRate[length - i - 1] = false;
                }
            }

            int[] gammaRateInt = new int[1];
            gammaRate.CopyTo(gammaRateInt, 0);

            int[] epsilonRateInt = new int[1];
            epsilonRate.CopyTo(epsilonRateInt, 0);

            return (epsilonRateInt.First() * gammaRateInt.First()).ToString();
        }

        public override string ExecutePartTwo(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DayThree), file);

            int length = input.First().Length;
            BitArray oxygenGeneratorRating = null!;
            BitArray co2ScrubberRating = null!;

            IEnumerable<string> oxygenReadings = input;
            IEnumerable<string> co2ScrubberReadings = input;

            for (int i = 0; i < length; i++)
            {
                char mostCommonValue = MostCommonValueAtIndex(oxygenReadings, i);
                oxygenReadings = oxygenReadings.Where(o => o[i] == mostCommonValue).ToList();
                if(oxygenReadings.Count() == 1)
                {
                    oxygenGeneratorRating = BitArrayFromString(oxygenReadings.Single());
                }
                
                char leastCommonValue = LeastCommonValueAtIndex(co2ScrubberReadings, i);
                co2ScrubberReadings = co2ScrubberReadings.Where(o => o[i] == leastCommonValue).ToList();
                if (co2ScrubberReadings.Count() == 1)
                {
                    co2ScrubberRating = BitArrayFromString(co2ScrubberReadings.Single());
                }
            }

            int[] oxygenGeneratorRatingInt = new int[1];
            oxygenGeneratorRating.CopyTo(oxygenGeneratorRatingInt, 0);

            int[] co2ScrubberRatingInt = new int[1];
            co2ScrubberRating.CopyTo(co2ScrubberRatingInt, 0);

            return (oxygenGeneratorRatingInt.First() * co2ScrubberRatingInt.First()).ToString();
        }

        private char MostCommonValueAtIndex(IEnumerable<string> bytes, int index)
        {
            IEnumerable<char> charsAtIndex = bytes.Select(l => l[index]);
            int numberOfOnes = charsAtIndex.Count(c => c == '1');
            int numberOfZeroes = charsAtIndex.Count() - numberOfOnes;

            if(numberOfOnes == numberOfZeroes)
            {
                return '1';
            }

            return numberOfOnes > numberOfZeroes ? '1' : '0';
        }

        private char LeastCommonValueAtIndex(IEnumerable<string> bytes, int index)
        {
            IEnumerable<char> charsAtIndex = bytes.Select(l => l[index]);
            int numberOfOnes = charsAtIndex.Count(c => c == '1');
            int numberOfZeroes = charsAtIndex.Count() - numberOfOnes;

            if (numberOfOnes == numberOfZeroes)
            {
                return '0';
            }

            return numberOfOnes > numberOfZeroes ? '0' : '1';
        }

        private BitArray BitArrayFromString(string @byte)
        {
            var bitArray = new BitArray(@byte.Length);
            int i = 0;
            foreach(char b in @byte)
            {
                bitArray[@byte.Length - 1 - i] = b == '1' ? true : false;
                i++;
            }

            return bitArray;
        }
    }
}
