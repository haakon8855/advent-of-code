using Utils;

bool test = false;
Chronograph chronograph = new Chronograph();
string path = test ? "test.txt" : "input.txt";
List<string> lines = File.ReadAllLines(path).ToList();

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

List<(int x, int y)> galaxies = new();

for (int i = 0; i < lines[0].Length; i++)
{
    bool foundGalaxy = false;
    for (int j = 0; j < lines.Count; j++)
    {
        if (lines[j][i] == '#')
        {
            foundGalaxy = true;
            break;
        }
    }
    if (!foundGalaxy)
    {
        for (int j = 0; j < lines.Count; j++)
        {
            lines[j] = lines[j].Substring(0, i) + "." + lines[j].Substring(i);
        }
        i++;
    }
}

for (int i = 0; i < lines.Count; i++)
{
    if (!lines[i].Contains("#"))
    {
        lines.Insert(i, lines[i]);
        i++;
    }
}

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '#')
        {
            galaxies.Add((j, i));
        }
    }
}

int sum = 0;
for (int i = 0; i < galaxies.Count; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        var xDist = Math.Abs(galaxies[i].x - galaxies[j].x);
        var yDist = Math.Abs(galaxies[i].y - galaxies[j].y);
        var dist = xDist + yDist;
        sum += dist;
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

lines = File.ReadAllLines(path).ToList();

List<long> expandedRows = new();
List<long> expandedCols = new();
long expansion = 1000000;

galaxies = new();

for (int i = 0; i < lines[0].Length; i++)
{
    bool foundGalaxy = false;
    for (int j = 0; j < lines.Count; j++)
    {
        if (lines[j][i] == '#')
        {
            foundGalaxy = true;
            break;
        }
    }
    if (!foundGalaxy)
    {
        expandedCols.Add(i);
    }
}

for (int i = 0; i < lines.Count; i++)
{
    if (!lines[i].Contains("#"))
    {
        expandedRows.Add(i);
    }
}

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '#')
        {
            galaxies.Add((j, i));
        }
    }
}

long bigSum = 0;
for (int i = 0; i < galaxies.Count; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        var startX = galaxies[i].x;
        var endX = galaxies[j].x;
        var startY = galaxies[i].y;
        var endY = galaxies[j].y;

        var baseDistX = Math.Abs(galaxies[i].x - galaxies[j].x);
        var baseDistY = Math.Abs(galaxies[i].y - galaxies[j].y);

        long extraX = expandedCols.Where(n => (n > startX && n < endX) || (n > endX && n < startX)).Count() * (expansion - 1);
        long extraY = expandedRows.Where(n => (n > startY && n < endY) || (n > endY && n < startY)).Count() * (expansion - 1);

        var dist = baseDistX + extraX + baseDistY + extraY;
        bigSum += dist;
    }
}

Console.WriteLine(bigSum);

chronograph.Toggle();

