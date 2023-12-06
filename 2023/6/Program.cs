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

var times = lines[0].Split(" ").Where(s => !string.IsNullOrEmpty(s)).Skip(1).Select(long.Parse).ToList();
var dists = lines[1].Split(" ").Where(s => !string.IsNullOrEmpty(s)).Skip(1).Select(long.Parse).ToList();

var wtws = new long[times.Count];
long wtw = 0;

for (int i = 0; i < times.Count; i++)
{
    wtw = 0;
    for (int j = 0; j <= times[i]; j++)
    {
        if ((j * (times[i] - j)) > dists[i])
            wtw++;
    }
    wtws[i] = wtw;
}

Console.WriteLine(wtws.Aggregate((a, b) => a * b));

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

long time = long.Parse(string.Join("", times.Select(x => x.ToString())));
long dist = long.Parse(string.Join("", dists.Select(x => x.ToString())));

wtw = 0;
for (long j = 0; j <= time; j++)
{
    if ((j * (time - j)) > dist)
        wtw++;
}

Console.WriteLine(wtw);

chronograph.Toggle();

