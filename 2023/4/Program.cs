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

Console.WriteLine(lines
    .Select(l => l.Split(" | "))
    .Select(l =>
        new
        {
            Winning = l[0].Split(": ")[1].Split(" ").Where(s => s != "").Select(int.Parse),
            Playing = l[1].Split(" ").Where(s => s != "").Select(int.Parse)
        })
    .Select(o => o.Playing.Where(o.Winning.Contains).Count())
    .Select(i => i == 0 ? 0 : Math.Pow(2, i - 1))
    .Sum());


chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var games = lines.Select(l => l.Split(" | "))
    .Select(l =>
        new
        {
            Winning = l[0].Split(": ")[1].Split(" ").Where(s => s != "").Select(int.Parse),
            Playing = l[1].Split(" ").Where(s => s != "").Select(int.Parse),
            Amount = 1
        }).ToList();
var amounts = games.Select(_ => 1).ToList();

for (int j = 0; j < games.Count; j++)
{
    var game = games[j];
    int wins = game.Playing.Where(game.Winning.Contains).Count();
    for (int i = 1; i <= wins && i + j < games.Count; i++)
    {
        amounts[j + i] += amounts[j];
    }
}

Console.WriteLine(amounts.Sum());

chronograph.Toggle();
