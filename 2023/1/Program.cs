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

Console.WriteLine(
    lines
    .Select(line => Regex.Replace(line, @"[a-zA-Z]", ""))
    .Select(line => $"{line[0]}{line[line.Length - 1]}")
    .Sum(int.Parse));




chronograph.Toggle();

// ======================= 
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 
Dictionary<string, int> numbers = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }, };

int GetCalibrationValueForLine(string line)
{
    string pattern = @"(1|2|3|4|5|6|7|8|9|one|two|three|four|five|six|seven|eight|nine)";
    var firstString = new Regex(pattern).Match(line).ToString(); // Left to right
    var lastString = new Regex(pattern, RegexOptions.RightToLeft).Match(line).ToString(); // Right to left
    if (!int.TryParse(firstString, out int firstInt))
        firstInt = numbers[firstString];
    if (!int.TryParse(lastString, out int lastInt))
        lastInt = numbers[lastString];
    return int.Parse($"{firstInt}{lastInt}");
}

Console.WriteLine(
    lines
    .Select(GetCalibrationValueForLine)
    .Sum());

chronograph.Toggle();

