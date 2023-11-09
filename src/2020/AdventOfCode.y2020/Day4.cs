using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.y2020
{
    [DayNumber(4)]
    public class Day4 : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
            passports.Add(new Dictionary<string, string>());
            var currentPassport = passports.First();
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentPassport = new Dictionary<string, string>();
                    passports.Add(currentPassport);
                    continue;
                }

                IEnumerable<string> keyValuePairs = line.Split(' ');
                foreach (var kvp in keyValuePairs)
                {
                    currentPassport.Add(kvp.Split(':').First(), kvp.Split(':').Last());
                }
            }

            int numberOfValidPassports = 0;
            List<string> requiredFields = new List<string>() {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };

            foreach (var passport in passports)
            {
                if (requiredFields.All(r => passport.ContainsKey(r)))
                {
                    numberOfValidPassports++;
                }
            }

            return numberOfValidPassports.ToString();
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
            passports.Add(new Dictionary<string, string>());
            var currentPassport = passports.First();
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentPassport = new Dictionary<string, string>();
                    passports.Add(currentPassport);
                    continue;
                }

                IEnumerable<string> keyValuePairs = line.Split(' ');
                foreach (var kvp in keyValuePairs)
                {
                    currentPassport.Add(kvp.Split(':').First(), kvp.Split(':').Last());
                }
            }

            int numberOfValidPassports = 0;
            List<(string, Func<string, bool>)> requiredFields = new List<(string, Func<string, bool>)>() {
                ("byr", s =>
                {
                    if(int.TryParse(s, out int result))
                    {
                        return result >= 1920 && result <= 2002;
                    }

                    return false;
                }),
                ("iyr", s =>
                {
                    if(int.TryParse(s, out int result))
                    {
                        return result >= 2010 && result <= 2020;
                    }

                    return false;
                }),
                ("eyr", s =>
                {
                    if(int.TryParse(s, out int result))
                    {
                        return result >= 2020 && result <= 2030;
                    }

                    return false;
                }),
                ("hgt", s =>
                {
                    if(s.EndsWith("cm"))
                    {
                        string number = s.Substring(0, s.Length - 2);
                        if(int.TryParse(number, out int result))
                        {
                            return result >= 150 && result <= 193;
                        }
                        return false;
                    }
                    else if (s.EndsWith("in"))
                    {
                        string number = s.Substring(0, s.Length - 2);
                        if(int.TryParse(number, out int result))
                        {
                            return result >= 59 && result <= 76;
                        }
                        return false;
                    }

                    return false;
                }),
                ("hcl", s =>
                {
                    Regex regex = new Regex("#([0-9]|[a-f]){6}");
                    return regex.IsMatch(s);
                }),
                ("ecl", s =>
                {
                    List<string> acceptedValues = new List<string>()
                    {
                        "amb",
                        "blu",
                        "brn",
                        "gry",
                        "grn",
                        "hzl",
                        "oth"
                    };
                    return acceptedValues.Any(a => a.Equals(s));
                }),
                ("pid", s =>
                {
                    Regex regex = new Regex("[0-9]{9}");
                    return regex.IsMatch(s);
                }),
            };

            foreach (var passport in passports)
            {
                if (requiredFields.All(r => passport.ContainsKey(r.Item1) && r.Item2(passport[r.Item1])))
                {
                    numberOfValidPassports++;
                }
            }

            return numberOfValidPassports.ToString();
        }
    }
}
