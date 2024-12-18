using Utils;
using Coord = (int row, int col);

var test = false;
var chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

var bytes = lines.Select(l => l.Split(',')).Select(l => new Coord(int.Parse(l[1]), int.Parse(l[0]))).ToList();
var width = bytes.Max(b => b.row + 1);
bytes = bytes.Select(b => (b.row + 1, b.col + 1)).ToList();


var root = new Coord(1, 1);
var target = new Coord(width, width);

Dictionary<Coord, Coord> BFS(int time)
{
    var map = new char[width + 2][];
    for (var i = 1; i < width + 1; i++)
    {
        map[i] = ("#" + new string('.', width) + "#").ToCharArray();
    }
    map[0] = new string('#', width + 2).ToCharArray();
    map[map.Length - 1] = new string('#', width + 2).ToCharArray();

    for (var i = 0; i < time; i++)
    {
        var b = bytes[i];
        map[b.row][b.col] = '#';
    }

    var queue = new Queue<Coord>();
    var visited = new List<Coord>();
    var parents = new Dictionary<Coord, Coord>();

    queue.Enqueue(root);
    while (queue.Any())
    {
        var v = queue.Dequeue();
        if (v == target)
            return parents;
        foreach (var w in GetNeighbours(v, map))
        {
            if (!visited.Contains(w))
            {
                visited.Add(w);
                parents[w] = v;
                queue.Enqueue(w);
            }
        }
    }
    return parents;
}

IEnumerable<Coord> GetNeighbours(Coord pos, char[][] map)
{
    if (map[pos.row - 1][pos.col] != '#')
        yield return new Coord(pos.row - 1, pos.col);
    if (map[pos.row + 1][pos.col] != '#')
        yield return new Coord(pos.row + 1, pos.col);
    if (map[pos.row][pos.col - 1] != '#')
        yield return new Coord(pos.row, pos.col - 1);
    if (map[pos.row][pos.col + 1] != '#')
        yield return new Coord(pos.row, pos.col + 1);
}

var simulateLength = test ? 12 : 1024;
var parentsPartOne = BFS(simulateLength);
var count = 0;
var node = target;
while (node != root)
{
    count++;
    node = parentsPartOne[node];
}

Console.WriteLine(count);

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var time = lines.Length;
var parentsPartTwo = new Dictionary<Coord, Coord>();
while (!parentsPartTwo.ContainsKey(target))
{
    time--;
    parentsPartTwo = BFS(time);
}

Console.WriteLine($"Index: {time}");
Console.WriteLine($"Coordinates: {lines[time]}");

chronograph.Toggle();
