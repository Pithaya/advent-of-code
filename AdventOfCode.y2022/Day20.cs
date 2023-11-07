using System.Collections.Generic;

namespace AdventOfCode.y2022
{
    struct Number
    {
        public int Id;
        public long Value;
    }

    public class Day20 : Day
    {
        public Day20(string inputFolder) : base(inputFolder)
        { }

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Number> values = input.Select((line, index) => new Number
            {
                Value = long.Parse(line),
                Id = index
            }).ToList();

            LinkedList<Number> linkedList = new LinkedList<Number>();

            // Create the linked list
            foreach(Number value in values)
            {
                linkedList.AddLast(value);
            }

            // Move the nodes
            Mix(values, linkedList);

            Number zero = values.Find(x => x.Value == 0);
            LinkedListNode<Number> zeroNode = linkedList.Find(zero);

            Number first = GetNextNode(1000, zeroNode).Value;
            Number second = GetNextNode(2000, zeroNode).Value;
            Number third = GetNextNode(3000, zeroNode).Value;

            return (first.Value + second.Value + third.Value).ToString();
        }

        private void Mix(List<Number> values, LinkedList<Number> linkedList)
        {
            foreach (Number value in values)
            {
                LinkedListNode<Number> currentNode = linkedList.Find(value);
                LinkedListNode<Number> nextNode;
                long currentValue = value.Value;

                if (currentValue == 0)
                {
                    continue;
                }
                else if (currentValue > 0)
                {
                    nextNode = GetNextNode(currentValue % (linkedList.Count - 1), currentNode);
                }
                else
                {
                    // We take one more so that addAfter works (append)
                    nextNode = GetPreviousNode((currentValue - 1) % (linkedList.Count - 1), currentNode);
                }

                if (currentNode.Value.Id == nextNode.Value.Id)
                {
                    continue;
                }

                linkedList.Remove(currentNode);
                linkedList.AddAfter(nextNode, value);
            }
        }

        private LinkedListNode<T> GetNextNode<T>(long currentValue, LinkedListNode<T> currentNode)
        {
            LinkedListNode<T> nextNode = currentNode;

            for (long i = currentValue; i > 0; i--)
            {
                if (nextNode.Next == null)
                {
                    nextNode = currentNode.List.First;
                }
                else
                {
                    nextNode = nextNode.Next;
                }
            }

            return nextNode!;
        }

        private LinkedListNode<T> GetPreviousNode<T>(long currentValue, LinkedListNode<T> currentNode)
        {
            LinkedListNode<T> nextNode = currentNode;

            for (long i = currentValue; i < 0; i++)
            {
                if (nextNode.Previous == null)
                {
                    nextNode = currentNode.List.Last;
                }
                else
                {
                    nextNode = nextNode.Previous;
                }
            }

            return nextNode!;
        }

        private string PrintList(LinkedList<Number> list)
        {
            return string.Join(", ", list.Select(x => x.Value));
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Number> values = input.Select((line, index) => new Number
            {
                Value = long.Parse(line) * 811589153,
                Id = index
            }).ToList();

            LinkedList<Number> linkedList = new LinkedList<Number>();

            // Create the linked list
            foreach (Number value in values)
            {
                linkedList.AddLast(value);
            }

            // Move the nodes
            for(int i = 0; i < 10; i++)
            {
                Mix(values, linkedList);
            }

            Number zero = values.Find(x => x.Value == 0);
            LinkedListNode<Number> zeroNode = linkedList.Find(zero);

            Number first = GetNextNode(1000, zeroNode).Value;
            Number second = GetNextNode(2000, zeroNode).Value;
            Number third = GetNextNode(3000, zeroNode).Value;

            return (first.Value + second.Value + third.Value).ToString();
        }
    }
}
