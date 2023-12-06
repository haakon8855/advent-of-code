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

var seeds = lines[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();

var mappings = new List<List<List<long>>>();

for (int i = 2; i < lines.Length; i++)
{
    if (lines[i].Contains(':'))
    {
        mappings.Add(new List<List<long>>());
        while (i + 1 < lines.Length && lines[i + 1] != "")
        {
            i++;
            mappings[mappings.Count - 1].Add(lines[i].Split(" ").Select(long.Parse).ToList());
        }
    }
}

long Map(long num, List<List<List<long>>> mappings)
{
    for (int k = 0; k < mappings.Count; k++)
    {
        for (int i = 0; i < mappings[k].Count; i++)
        {
            (var dst, var src, var len) = (mappings[k][i][0], mappings[k][i][1], mappings[k][i][2]);
            if (num >= src && num < src + len)
            {
                num = num + (dst - src);
                break;
            }
        }
    }
    return num;
}

Console.WriteLine(seeds.Select(n => Map(n, mappings)).Min());

chronograph.Toggle();


// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var ranges = lines[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();

long InverseMap(long num, List<List<List<long>>> mappings)
{
    for (int k = mappings.Count - 1; k >= 0; k--)
    {
        for (int i = 0; i < mappings[k].Count; i++)
        {
            (var src, var dst, var len) = (mappings[k][i][0], mappings[k][i][1], mappings[k][i][2]);
            if (num >= src && num < src + len)
            {
                num = num + (dst - src);
                break;
            }
        }
    }
    return num;
}

bool IsInRange(long num, List<long> ranges)
{
    for (int i = 0; i < ranges.Count - 1; i += 2)
    {
        if (num >= ranges[i] && num < ranges[i] + ranges[i + 1])
        {
            return true;
        }
    }
    return false;
}

long candidate = 0;
long j = 0;
while (true)
{
    candidate = InverseMap(j, mappings);

    if (IsInRange(candidate, ranges))
    {
        Console.WriteLine($"{j} => {candidate}");
        break;
    }
    j++;
}

chronograph.Toggle();

