using System.Text.RegularExpressions;
using Utils;

bool test = false;
Chronograph chronograph = new Chronograph();
string path = test ? "test.txt" : "input.txt";
string[] lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

var matches = Regex.Matches(string.Join("", lines), @"mul\(\d+,\d+\)");
var sum = matches.Sum(m => m.ToString()
    .Substring(0, m.Length - 1)
    .Substring(4)
    .Split(",")
    .Select(int.Parse)
    .Aggregate((a, b) => a * b));

Console.WriteLine($"Sum: {sum}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

matches = Regex.Matches(string.Join("", lines), @"(mul\(\d+,\d+\)|do\(\)|don't\(\))");
sum = 0;
var enabled = true;
foreach (var match in matches.Select(s => s.ToString()))
{
    if (match == "do()")
        enabled = true;
    else if (match == "don't()")
        enabled = false;
    else if (enabled)
    {
        sum += match
            .Substring(0, match.Length - 1)
            .Substring(4)
            .Split(",")
            .Select(int.Parse)
            .Aggregate((a, b) => a * b);
    }
}

Console.WriteLine($"Sum: {sum}");

chronograph.Toggle();