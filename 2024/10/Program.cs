using Utils;
using Coord = (int row, int col);

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Setup: ");
chronograph.Toggle();
// ======================= 

// Add padding to avoid checking boundary indices
var padded = lines.ToList();
var width = padded[0].Length;
padded = padded.Select(l => '0' + l + '0').ToList();
padded.Insert(0, new string('0', width + 2));
padded.Add(new string('0', width + 2));

// Convert to int array
var map = padded.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

chronograph.Toggle();
Console.WriteLine("\nPart 1: ");
chronograph.Toggle();

var visited = new List<Coord>();

int FindPeaksPartOne(Coord start, int targetHeight)
{
    // Check if we are allowed to be here
    if (map[start.row][start.col] != targetHeight)
        return 0;

    // Check if we have already visited this spot
    if (visited.Contains(start))
        return 0;
    visited.Add(start);

    // Check if we are at the end
    var height = map[start.row][start.col];
    if (height == 9)
        return 1;

    return FindPeaksPartOne((start.row - 1, start.col), height + 1)
           + FindPeaksPartOne((start.row + 1, start.col), height + 1)
           + FindPeaksPartOne((start.row, start.col - 1), height + 1)
           + FindPeaksPartOne((start.row, start.col + 1), height + 1);
}

var score = 0;
for (int i = 1; i < map.Length - 1; i++)
{
    for (int j = 1; j < map[i].Length - 1; j++)
    {
        var height = map[i][j];
        if (height == 0)
        {
            visited.Clear();
            score += FindPeaksPartOne((i, j), 0);
        }
    }
}

Console.WriteLine($"Sum: {score}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

int FindPeaksPartTwo(Coord start, int targetHeight)
{
    // Check if we are allowed to be here
    if (map[start.row][start.col] != targetHeight)
        return 0;

    // Check if we are at the end
    var currentHeight = map[start.row][start.col];
    if (currentHeight == 9)
        return 1;

    return FindPeaksPartTwo((start.row - 1, start.col), currentHeight + 1)
           + FindPeaksPartTwo((start.row + 1, start.col), currentHeight + 1)
           + FindPeaksPartTwo((start.row, start.col - 1), currentHeight + 1)
           + FindPeaksPartTwo((start.row, start.col + 1), currentHeight + 1);
}

score = 0;
for (int i = 1; i < map.Length - 1; i++)
{
    for (int j = 1; j < map[i].Length - 1; j++)
    {
        var height = map[i][j];
        if (height == 0)
        {
            score += FindPeaksPartTwo((i, j), 0);
        }
    }
}

Console.WriteLine($"Sum: {score}");

chronograph.Toggle();