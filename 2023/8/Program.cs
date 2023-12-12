using System.Numerics;
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

string directions = lines[0];

var paths = new Dictionary<string, string[]>();

for (int i = 2; i < lines.Length; i++)
{
    paths.Add(lines[i].Substring(0, 3), [lines[i].Substring(7, 3), lines[i].Substring(12, 3)]);
}

char direction;
int index;
int j = 0;
string currentLocation = "AAA";
while (currentLocation != "ZZZ")
{
    direction = directions[j % directions.Length];
    index = direction == 'L' ? 0 : 1;
    currentLocation = paths[currentLocation][index];
    j++;
}

Console.WriteLine(j);

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var loopLenghts = new List<int>();
var startLocations = paths.Keys.Where(k => k.EndsWith('A'));
foreach (var startLocation in startLocations)
{
    currentLocation = startLocation;
    j = 0;

    direction = directions[j % directions.Length];
    index = direction == 'L' ? 0 : 1;
    currentLocation = paths[currentLocation][index];
    j++;
    while (!(currentLocation.EndsWith('Z') && j % directions.Length == 0))
    {
        direction = directions[j % directions.Length];
        index = direction == 'L' ? 0 : 1;
        currentLocation = paths[currentLocation][index];
        j++;
        if (currentLocation.EndsWith('Z'))
        {
            loopLenghts.Add(j);
        }
    }
}

var leastCommonMultiple = loopLenghts
        .Select(n => (BigInteger)n)
        .Aggregate((a, b) => a * b / BigInteger.GreatestCommonDivisor(a, b));
Console.WriteLine(leastCommonMultiple);


chronograph.Toggle();

