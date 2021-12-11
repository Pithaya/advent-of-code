// See https://aka.ms/new-console-template for more information
using AdventOfCode2021;

var inputFileName = "input";
var currentDay = new DayEleven();

Console.WriteLine($"Part one: {currentDay.ExecutePartOne(inputFileName)}");
Console.WriteLine($"Part two: {currentDay.ExecutePartTwo(inputFileName)}");

Console.ReadLine();