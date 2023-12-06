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

var bag = new Dictionary<string, int>
{
    {"red", 12 },
    {"green", 13 },
    {"blue", 14 },
};

int sum = 0;
foreach (string line in lines)
{
    string[] splitLine = line.Split(": ");
    int gameId = int.Parse(splitLine[0].Split(" ")[1]);
    var isImpossible = splitLine[1]
                            .Split("; ")
                            .Select(s => s.Split(", "))
                            .SelectMany(s => s)
                            .Any(s => int.Parse(s.Split(" ")[0]) > bag[s.Split(" ")[1]]);
    if (!isImpossible)
    {
        sum += gameId;
    }
}

Console.WriteLine(sum);

chronograph.Toggle();

// ======================= 
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

sum = 0;
foreach (string line in lines)
{
    var samples = line.Split(": ")[1]
                            .Split("; ")
                            .Select(s => s.Split(", "))
                            .SelectMany(s => s)
                            .Select(s => new { Amount = int.Parse(s.Split(" ")[0]), Color = s.Split(" ")[1] })
                            .GroupBy(s => s.Color)
                            .Select(g => g.Max(t => t.Amount))
                            .Aggregate((a, b) => a * b);
    sum += samples;
}

Console.WriteLine(sum);

chronograph.Toggle();

