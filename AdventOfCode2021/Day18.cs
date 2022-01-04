using AdventOfCode;
using PCRE;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2021
{
    public class Day18 : Day
    {
        public Day18(string inputFolder) : base(inputFolder)
        {}

        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            var additionResult = SnailFishNumber.Parse(input.First());

            foreach(var line in input.Skip(1))
            {
                var newResult = new SnailFishNumber
                {
                    Parent = null,
                    First = additionResult,
                    Second = SnailFishNumber.Parse(line)
                };

                newResult.First.Parent = newResult;
                newResult.Second.Parent = newResult;

                Reduce(newResult);
                additionResult = newResult;
            }

            return additionResult.Magnitude.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }

        private void Reduce(SnailFishNumber number)
        {
            bool isFullyReduced = false;

            Func<Number, bool> explodeAction = (n) =>
            {
                if (n is SnailFishNumber sfn)
                {
                    return sfn.Explode();
                }

                return false;
            };

            Func<Number, bool> splitAction = (n) =>
            {
                if (n is HumanNumber hn)
                {
                    return hn.Split();
                }

                return false;
            };

            while (!isFullyReduced)
            {
                if(ReduceWithAction(number, explodeAction))
                {
                    continue;
                }

                if(ReduceWithAction(number, splitAction))
                {
                    continue;
                }

                isFullyReduced = true;
            }
        }

        private bool ReduceWithAction(SnailFishNumber number, Func<Number, bool> reduceAction)
        {
            var stack = new Stack<Number>();
            stack.Push(number);

            while (true)
            {
                // If we checked all the pairs, stop
                if(stack.Count == 0)
                {
                    return false;
                }

                // Depth first search while no action is performed
                var current = stack.Pop();

                if (!reduceAction(current))
                {
                    if (current is SnailFishNumber snailFishNumber)
                    {
                        stack.Push(snailFishNumber.Second);
                        stack.Push(snailFishNumber.First);
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }

    abstract class Number
    {
        public Number? Parent { get; set; }

        public int NestingLevel => Parent == null ? 0 : Parent.NestingLevel + 1;

        public abstract int Magnitude { get; }
    }

    class HumanNumber : Number
    {
        public int Value { get; set; }
        public override int Magnitude => Value;

        public static HumanNumber Parse(string value, Number? parent = null)
        {
            return new HumanNumber
            {
                Parent = parent,
                Value = int.Parse(value),
            };
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Split()
        {
            if (this.Value >= 10)
            {
                var newElement = new SnailFishNumber()
                {
                    Parent = this.Parent
                };

                newElement.First = new HumanNumber
                {
                    Parent = newElement,
                    Value = (int)Math.Floor(this.Value / (double)2)
                };

                newElement.Second = new HumanNumber
                {
                    Parent = newElement,
                    Value = (int)Math.Ceiling(this.Value / (double)2)
                };

                if (this == (this.Parent as SnailFishNumber).First)
                {
                    (this.Parent as SnailFishNumber).First = newElement;
                }
                else
                {
                    (this.Parent as SnailFishNumber).Second = newElement;
                }

                return true;
            }

            return false;
        }
    }

    class SnailFishNumber : Number
    {
        private static PcreRegex parseRegex = new PcreRegex(@"\[(\d|(?R)),(\d|(?R))\]");

        public Number First { get; set; }
        public Number Second { get; set; }

        public override int Magnitude => First.Magnitude * 3 + Second.Magnitude * 2;

        public static SnailFishNumber Parse(string value, Number? parent = null)
        {
            var match = parseRegex.Match(value);

            if(match.Groups.Count != 3)
            {
                throw new ArgumentException("The value is not a pair.");
            }

            var first = match.Groups[1].Value;
            var second = match.Groups[2].Value;

            var current = new SnailFishNumber()
            {
                Parent = parent,
            };

            current.First = first.Contains(",") ? SnailFishNumber.Parse(first, current) : HumanNumber.Parse(first, current);
            current.Second = second.Contains(",") ? SnailFishNumber.Parse(second, current) : HumanNumber.Parse(second, current);

            return current;
        }

        public override string ToString()
        {
            return $"[{First},{Second}]";
        }

        public bool Explode()
        {
            if (NestingLevel == 3)
            {
                // Explode the leftmost pair
                var leftPair = this.First is SnailFishNumber ? this.First as SnailFishNumber : this.Second as SnailFishNumber;

                if(leftPair != null)
                {
                    this.IncrementClosestInDirection(leftPair, SearchDirection.Left, (leftPair.First as HumanNumber).Value);
                    this.IncrementClosestInDirection(leftPair, SearchDirection.Right, (leftPair.Second as HumanNumber).Value);

                    // Replace with 0
                    var newNumber = new HumanNumber()
                    {
                        Parent = this,
                        Value = 0
                    };
                
                    if(leftPair == this.First)
                    {
                        this.First = newNumber;
                    }
                    else
                    {
                        this.Second = newNumber;
                    }

                    return true;
                }
            }

            return false;
        }

        private void IncrementClosestInDirection(SnailFishNumber explodingPair, SearchDirection startDirection, int incrementValue)
        {
            var currentDirection = startDirection;
            var previousPair = explodingPair;
            var currentPair = explodingPair.Parent as SnailFishNumber;
            var found = false;

            while (!found)
            {
                var nextElement = currentPair.GetElement(currentDirection);
                if (nextElement is HumanNumber humanNumber)
                {
                    // Increment it
                    humanNumber.Value += incrementValue;
                    return;
                }
                else
                {
                    if(nextElement == previousPair)
                    {
                        // We're coming from this way, keep searching up
                        if (currentPair.Parent != null)
                        {
                            previousPair = currentPair;
                            currentPair = currentPair.Parent as SnailFishNumber;
                        }
                        else
                        {
                            // We're at the top and can't get further down
                            return;
                        }
                    }
                    else
                    {
                        // Invert the search direction (if not done already) and go down
                        if (currentDirection == startDirection)
                        {
                            currentDirection = currentDirection == SearchDirection.Left ? SearchDirection.Right : SearchDirection.Left;
                        }

                        previousPair = currentPair;
                        currentPair = nextElement as SnailFishNumber;
                    }
                }
            }
        }

        public Number GetElement(SearchDirection direction)
        {
            return direction == SearchDirection.Left ? First : Second;
        }
    }

    enum SearchDirection
    {
        Left,
        Right
    };
}
