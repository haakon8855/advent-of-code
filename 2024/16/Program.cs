using Utils;
using Coord = (int row, int col);
using Node = (int row, int col, Dir dir);

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

var maze = lines.Select(l => l.ToCharArray()).ToArray();

Coord target = new(0, 0);
Node source = new(0, 0, Dir.E);
var dist = new Dictionary<Node, int>();
var prev = new Dictionary<Node, List<Node>?>();
var queue = new Dictionary<Node, int>();

for (var i = 0; i < maze.Length; i++)
{
    for (var j = 0; j < maze[0].Length; j++)
    {
        if (maze[i][j] == 'E')
            target = new(i, j);
        if (maze[i][j] == 'S')
            source = new(i, j, Dir.E);
        if (maze[i][j] != '#')
        {
            foreach (Dir dir in Enum.GetValues<Dir>())
            {
                var v = new Node(i, j, dir);
                dist[v] = int.MaxValue;
                queue[v] = int.MaxValue;
                prev[v] = null;
            }
        }
    }
}
if (target == (0, 0) || source == (0, 0, Dir.E))
    throw new Exception("Couldn't find source or target!");

dist[source] = 0;
queue[source] = 0;

var final = new Node(0, 0, 0);
while (queue.Count > 0)
{
    var u = queue.MinBy(d => d.Value).Key;
    if (u.row == target.row && u.col == target.col)
    {
        final = u;
        break;
    }
    queue.Remove(u);

    var neighbours = GetNeighbours(u);
    foreach (var v in neighbours)
    {
        if (!queue.Keys.Contains(v))
            continue;

        var alt = dist[u] + GetCost(u, v);
        if (alt < dist[v])
        {
            dist[v] = alt;
            queue[v] = alt;
            prev[v] = [u];
        }
        else if (alt == dist[v])
        {
            prev[v].Add(u);
        }
    }
}

Console.WriteLine($"Best path cost: {dist[final]}");

var visited = new List<Node>();
void Visit(Node node)
{
    if (visited.Contains(node))
        return;
    visited.Add(node);
    foreach (var child in prev[node] ?? [])
    {
        Visit(child);
    }
}

Visit(final);
var visitCount = visited.Select(l => (l.row, l.col)).Distinct().Count();
Console.WriteLine($"Nodes part of a best path: {visitCount}");

List<Node> GetNeighbours(Node node)
{
    var output = new List<Node>();
    output.Add(new Node(node.row, node.col, (Dir)(((int)node.dir + 1) % 4)));
    output.Add(new Node(node.row, node.col, (Dir)(((int)node.dir + 3) % 4)));
    if (node.dir == Dir.N && maze[node.row - 1][node.col] != '#')
        output.Add(new Node(node.row - 1, node.col, node.dir));
    else if (node.dir == Dir.S && maze[node.row + 1][node.col] != '#')
        output.Add(new Node(node.row + 1, node.col, node.dir));
    else if (node.dir == Dir.W && maze[node.row][node.col - 1] != '#')
        output.Add(new Node(node.row, node.col - 1, node.dir));
    else if (node.dir == Dir.E && maze[node.row][node.col + 1] != '#')
        output.Add(new Node(node.row, node.col + 1, node.dir));
    return output;
}

int GetCost(Node first, Node second)
{
    if (first == second)
        throw new Exception("Equal!");
    if (first.row == second.row && first.col == second.col)
        return 1000;
    if (first.dir != second.dir) 
        throw new Exception("Nodes are not neighbours, because direction");
    if (first.row == second.row && Math.Abs(first.col - second.col) > 1
        || first.col == second.col && Math.Abs(first.row-second.row) > 1)
        throw new Exception("Nodes are not neighbours, because too far");
    return 1;
}

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 



chronograph.Toggle();


public enum Dir
{
    N,
    E,
    S,
    W
}

