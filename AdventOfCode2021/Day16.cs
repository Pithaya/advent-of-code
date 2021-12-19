using AdventOfCode;
using System.Text;

namespace AdventOfCode.y2021
{
    public class Day16 : Day
    {
        public Day16(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            Dictionary<char, string> hexToBinary = new Dictionary<char, string>()
            {
                { '0', "0000" },
                { '1', "0001" },
                { '2', "0010" },
                { '3', "0011" },
                { '4', "0100" },
                { '5', "0101" },
                { '6', "0110" },
                { '7', "0111" },
                { '8', "1000" },
                { '9', "1001" },
                { 'A', "1010" },
                { 'B', "1011" },
                { 'C', "1100" },
                { 'D', "1101" },
                { 'E', "1110" },
                { 'F', "1111" },
            };

            var packet = input.First();
            var binaryPacket = string.Join(string.Empty, packet
                .ToCharArray()
                .Select(c => hexToBinary[c])
                .ToList());

            var rootPacket = Packet.CreatePacket(binaryPacket);
            return rootPacket.VersionSum.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }

    abstract class Packet
    {
        public int Version { get; init; }
        public int TypeId { get; init; }
        public abstract int PacketLength { get; }
        public abstract int VersionSum { get; }

        public static Packet CreatePacket(string packet)
        {
            var version = Convert.ToInt32(packet[0..3], 2);
            var type = Convert.ToInt32(packet[3..6], 2);

            if (type == 4)
            {
                return new LiteralPacket(packet[6..])
                {
                    Version = version,
                    TypeId = type
                };
            }
            else
            {
                return new OperatorPacket(packet[6..])
                {
                    Version = version,
                    TypeId = type
                };
            }
            
        }
    }

    class LiteralPacket : Packet
    {
        private int valueLength;

        public long Value { get; }

        public override int PacketLength => valueLength;
        public override int VersionSum => Version;

        public LiteralPacket(string packet)
        {
            var sb = new StringBuilder();
            var currentIndex = 0;

            valueLength = 6;

            bool process = true;
            while (process)
            {
                if(packet[currentIndex] == '0')
                {
                    // Last group
                    process = false;
                }

                Index startIndex = currentIndex + 1;
                Index endIndex = currentIndex + 5;
                sb.Append(packet[startIndex..endIndex]);

                currentIndex += 5;
                valueLength += 5;
            }

            Value = Convert.ToInt64(sb.ToString(), 2);
        }
    }

    class OperatorPacket : Packet
    {
        private int packetLength;
        private List<Packet> subPackets = new List<Packet>();

        public override int PacketLength => packetLength + subPackets.Sum(s => s.PacketLength);
        public override int VersionSum => Version + subPackets.Sum(s => s.VersionSum);

        public OperatorPacket(string packet)
        {
            int subPacketsLength = 0;
            int subPacketsCount = 0;
            int processedLength = 0;
            int processedSubPackets = 0;

            packetLength = 7;

            bool IsFinished()
            {
                if(subPacketsLength == 0)
                {
                    return processedSubPackets == subPacketsCount;
                }
                else
                {
                    return processedLength == subPacketsLength;
                }
            }

            var currentIndex = 0;
            if (packet[currentIndex] == '0')
            {
                Index startIndex = currentIndex + 1;
                Index endIndex = currentIndex + 16;
                subPacketsLength = Convert.ToInt32(packet[startIndex..endIndex], 2);
                currentIndex += 16;
                packetLength += 15;
            }
            else
            {
                Index startIndex = currentIndex + 1;
                Index endIndex = currentIndex + 12;
                subPacketsCount = Convert.ToInt32(packet[startIndex..endIndex], 2);
                currentIndex += 12;
                packetLength += 11;
            }

            while (!IsFinished())
            {
                var subPacket = Packet.CreatePacket(packet[currentIndex..]);
                subPackets.Add(subPacket);

                processedSubPackets++;
                processedLength += subPacket.PacketLength;
                currentIndex += subPacket.PacketLength;
            }
        }
    }
}
