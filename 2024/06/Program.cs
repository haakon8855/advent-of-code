using Utils;

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path).ToList();

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

// add padding
var width = lines[0].Length;
for (var i = 0; i < lines.Count; i++)
{
    lines[i] = ' ' + lines[i] + ' ';
}

lines.Insert(0, new string(' ', width + 2));
lines.Add(new string(' ', width + 2));

// convert to char array
var clines = lines.Select(l => l.ToCharArray()).ToArray();

// get start position
(int row, int col) startCoords = (0, 0);
for (var i = 0; i < lines.Count; i++)
{
    var index = lines[i].IndexOf('^');
    if (index > -1)
        startCoords = (i, index);
}

bool InBounds(int row, int col)
{
    return row > 0 && col > 0 && row < clines.Length - 2 && col < clines[0].Length - 2;
}

var visited = new List<(int row, int col)> { startCoords };
var pos = startCoords;
while (InBounds(pos.row, pos.col))
{
    if (clines[pos.row][pos.col] == '^')
    {
        clines[pos.row][pos.col] = '.';
        if (clines[pos.row - 1][pos.col] == '#')
        {
            clines[pos.row][pos.col] = '>';
        }
        else
        {
            pos.row--;
            clines[pos.row][pos.col] = '^';
        }
    }
    else if (clines[pos.row][pos.col] == 'v')
    {
        clines[pos.row][pos.col] = '.';
        if (clines[pos.row + 1][pos.col] == '#')
        {
            clines[pos.row][pos.col] = '<';
        }
        else
        {
            pos.row++;
            clines[pos.row][pos.col] = 'v';
        }
    }
    else if (clines[pos.row][pos.col] == '<')
    {
        clines[pos.row][pos.col] = '.';
        if (clines[pos.row][pos.col - 1] == '#')
        {
            clines[pos.row][pos.col] = '^';
        }
        else
        {
            pos.col--;
            clines[pos.row][pos.col] = '<';
        }
    }
    else if (clines[pos.row][pos.col] == '>')
    {
        clines[pos.row][pos.col] = '.';
        if (clines[pos.row][pos.col + 1] == '#')
        {
            clines[pos.row][pos.col] = 'v';
        }
        else
        {
            pos.col++;
            clines[pos.row][pos.col] = '>';
        }
    }

    if (!visited.Contains(pos))
        visited.Add(pos);
}

Console.WriteLine($"Visited: {visited.Count - 1}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 


bool Halts(int row, int col)
{
    var visited2 = new List<(int row, int col, char dir)>();
    visited2.Add((startCoords.row, startCoords.col, '^'));
    clines = lines.Select(l => l.ToCharArray()).ToArray();
    clines[row][col] = '#';
    pos = startCoords;
    while (InBounds(pos.row, pos.col))
    {
        if (clines[pos.row][pos.col] == '^')
        {
            clines[pos.row][pos.col] = '.';
            if (clines[pos.row - 1][pos.col] == '#')
            {
                clines[pos.row][pos.col] = '>';
            }
            else
            {
                pos.row--;
                clines[pos.row][pos.col] = '^';
            }
        }
        else if (clines[pos.row][pos.col] == 'v')
        {
            clines[pos.row][pos.col] = '.';
            if (clines[pos.row + 1][pos.col] == '#')
            {
                clines[pos.row][pos.col] = '<';
            }
            else
            {
                pos.row++;
                clines[pos.row][pos.col] = 'v';
            }
        }
        else if (clines[pos.row][pos.col] == '<')
        {
            clines[pos.row][pos.col] = '.';
            if (clines[pos.row][pos.col - 1] == '#')
            {
                clines[pos.row][pos.col] = '^';
            }
            else
            {
                pos.col--;
                clines[pos.row][pos.col] = '<';
            }
        }
        else if (clines[pos.row][pos.col] == '>')
        {
            clines[pos.row][pos.col] = '.';
            if (clines[pos.row][pos.col + 1] == '#')
            {
                clines[pos.row][pos.col] = 'v';
            }
            else
            {
                pos.col++;
                clines[pos.row][pos.col] = '>';
            }
        }

        var dir = clines[pos.row][pos.col];
        if (visited2.Contains((pos.row, pos.col, dir)))
        {
            return false;
        }

        visited2.Add((pos.row, pos.col, dir));
    }

    return true;
}

var sum = 0;
for (int i = 1; i < clines.Length - 1; i++)
{
    Console.WriteLine(i);
    for (int j = 1; j < clines[0].Length - 1; j++)
    {
        if (lines[i][j] == '.')
        {
            if (!Halts(i, j))
            {
                sum++;
            }
        }
    }
}

Console.WriteLine($"Loops: {sum}");

chronograph.Toggle();