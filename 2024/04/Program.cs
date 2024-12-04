using System.Text.RegularExpressions;
using Three;
using Utils;

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 


var width = lines.Length;
var patterns = new[]
{
    $"(?=(xmas|samx))", // Left-right
    $"(?=(s.{{{width - 1}}}a.{{{width - 1}}}m.{{{width - 1}}}x|x.{{{width - 1}}}m.{{{width - 1}}}a.{{{width - 1}}}s))", // bottom-left > top-right diagonals
    $"(?=(x.{{{width}}}m.{{{width}}}a.{{{width}}}s|s.{{{width}}}a.{{{width}}}m.{{{width}}}x))", // Up-down
    $"(?=(x.{{{width + 1}}}m.{{{width + 1}}}a.{{{width + 1}}}s|s.{{{width + 1}}}a.{{{width + 1}}}m.{{{width + 1}}}x))" // top-left > bottom-right diagonals
};

var allLines = string.Join("\n", lines).ToLower();
var count = patterns.Sum(p => Regex.Matches(allLines, p, RegexOptions.Singleline).Count);
Console.WriteLine($"Regex solution count: {count}");
chronograph.Toggle();


chronograph.Toggle();
count = FirstSolution.PartOne(lines);
Console.WriteLine($"First solution count: {count}");
chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var pattern =
    $"(?=(?<=m.m.{{{width - 1}}})a(?=.{{{width - 1}}}s.s)|(?<=s.s.{{{width - 1}}})a(?=.{{{width - 1}}}m.m)|(?<=s.m.{{{width - 1}}})a(?=.{{{width - 1}}}s.m)|(?<=m.s.{{{width - 1}}})a(?=.{{{width - 1}}}m.s))";
count = Regex.Matches(allLines, pattern, RegexOptions.Singleline).Count;
Console.WriteLine($"Regex solution count: {count}");
chronograph.Toggle();


chronograph.Toggle();
count = FirstSolution.PartTwo(lines);
Console.WriteLine($"First solution count: {count}");

chronograph.Toggle();
