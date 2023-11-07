using AdventOfCode.Common;

namespace AdventOfCode.y2021
{
    [DayNumber(24)]
    public class Day24 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            long modelNumber = 99999999999999;
            bool isValid = false;

            while (!isValid)
            {
                var digits = GetDigits(modelNumber);

                if (digits.Contains(0))
                {
                    modelNumber--;
                    continue;
                }

                var alu = new ALU(digits);

                foreach (var operation in input)
                {
                    alu.Execute(operation);
                }

                if (alu.IsValid)
                {
                    isValid = true;
                }
                else
                {
                    modelNumber--;
                }
            }

            return modelNumber.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }

        private List<int> GetDigits(long number)
        {
            List<long> result = new List<long>();
            while (number > 0)
            {
                result.Add(number % 10);
                number /= 10;
            }

            result.Reverse();
            return result
                .Select(r => Convert.ToInt32(r))
                .ToList();
        }
    }

    class ALU
    {
        private Dictionary<string, int> variables = new Dictionary<string, int>()
        {
            { "w", 0 },
            { "x", 0 },
            { "y", 0 },
            { "z", 0 },
        };

        private Queue<int> inputs = new Queue<int>();

        public bool IsValid => variables["z"] == 0;

        public ALU(List<int> inputs)
        {
            foreach(var input in inputs)
            {
                this.inputs.Enqueue(input);
            }
        }

        public void Execute(string operation)
        {
            string op = operation.Split(" ").First();
            string parameters = string.Join(" ", operation.Split(" ").Skip(1).ToList());

            switch (op)
            {
                case "inp":
                    Input(parameters);
                    break;
                case "add":
                    Add(parameters);
                    break;
                case "mul":
                    Mul(parameters);
                    break;
                case "div":
                    Div(parameters);
                    break;
                case "mod":
                    Mod(parameters);
                    break;
                case "eql":
                    Eql(parameters);
                    break;
            }
        }

        private void Input(string parameters)
        {
            this.variables[parameters] = inputs.Dequeue();
        }

        private void Add(string parameters)
        {
            var a = parameters.Split(" ").First();
            var b = parameters.Split(" ").Last();

            if(!int.TryParse(b, out int bValue))
            {
                bValue = variables[b];
            }

            variables[a] += bValue;
        }

        private void Mul(string parameters)
        {
            var a = parameters.Split(" ").First();
            var b = parameters.Split(" ").Last();

            if (!int.TryParse(b, out int bValue))
            {
                bValue = variables[b];
            }

            variables[a] *= bValue;
        }

        private void Div(string parameters)
        {
            var a = parameters.Split(" ").First();
            var b = parameters.Split(" ").Last();

            if (!int.TryParse(b, out int bValue))
            {
                bValue = variables[b];
            }

            variables[a] = (int)Math.Floor(variables[a] / (double)bValue);
        }

        private void Mod(string parameters)
        {
            var a = parameters.Split(" ").First();
            var b = parameters.Split(" ").Last();

            if (!int.TryParse(b, out int bValue))
            {
                bValue = variables[b];
            }

            variables[a] %= bValue;
        }

        private void Eql(string parameters)
        {
            var a = parameters.Split(" ").First();
            var b = parameters.Split(" ").Last();

            if (!int.TryParse(b, out int bValue))
            {
                bValue = variables[b];
            }

            if(variables[a] == bValue)
            {
                variables[a] = 1;
            }
            else
            {
                variables[a] = 0;
            }
        }
    }
}
