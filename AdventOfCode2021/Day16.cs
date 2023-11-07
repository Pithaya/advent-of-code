using AdventOfCode.Common;
using System.Text;

namespace AdventOfCode.y2021
{
    [DayNumber(16)]
    public class Day16 : Day
    {
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
            return rootPacket.Value.ToString();
        }
    }

    abstract class Packet
    {
        public int Version { get; init; }
        public int TypeId { get; init; }

        /// <summary>
        /// Packet length in bits
        /// </summary>
        public abstract int PacketLength { get; }
        public abstract int VersionSum { get; }
        public abstract long Value { get; }

        public Packet(int version, int typeId)
        {
            this.Version = version;
            this.TypeId = typeId;
        }

        public static Packet CreatePacket(string packet)
        {
            var version = Convert.ToInt32(packet[0..3], 2);
            var type = Convert.ToInt32(packet[3..6], 2);

            if (type == 4)
            {
                return new LiteralPacket(packet[6..], version, type);
            }
            else
            {
                return new OperatorPacket(packet[6..], version, type);
            }
            
        }
    }

    class LiteralPacket : Packet
    {
        private int valueLength;

        private long value;

        public override int PacketLength => valueLength;
        public override int VersionSum => Version;
        public override long Value => value;

        public LiteralPacket(string packet, int version, int typeId) : base(version, typeId)
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

            value = Convert.ToInt64(sb.ToString(), 2);
        }
    }

    class OperatorPacket : Packet
    {
        private int outerPacketLength;
        private List<Packet> subPackets = new List<Packet>();

        public override int PacketLength => outerPacketLength + subPackets.Sum(s => s.PacketLength);
        public override int VersionSum => Version + subPackets.Sum(s => s.VersionSum);
        public override long Value
        {
            get
            {
                return TypeId switch
                {
                    0 => subPackets.Sum(s => s.Value),
                    1 => subPackets.Select(s => s.Value).Aggregate((p, s) => p * s),
                    2 => subPackets.Min(s => s.Value),
                    3 => subPackets.Max(s => s.Value),
                    5 => subPackets.First().Value > subPackets.Last().Value ? 1 : 0,
                    6 => subPackets.First().Value < subPackets.Last().Value ? 1 : 0,
                    7 => subPackets.First().Value == subPackets.Last().Value ? 1 : 0,
                };
            }
        }

        public OperatorPacket(string packet, int version, int typeId) : base(version, typeId)
        {
            int subPacketsLength = 0;
            int subPacketsCount = 0;
            int processedLength = 0;
            int processedSubPackets = 0;

            outerPacketLength = 7;

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
                outerPacketLength += 15;
            }
            else
            {
                Index startIndex = currentIndex + 1;
                Index endIndex = currentIndex + 12;
                subPacketsCount = Convert.ToInt32(packet[startIndex..endIndex], 2);
                currentIndex += 12;
                outerPacketLength += 11;
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
