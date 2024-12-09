using Utils;
using Coord = (int row, int col);

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

bool InBounds(Coord coord, int width)
{
    return coord.row >= 0 && coord.col >= 0 && coord.row < width && coord.col < width;
}

var antinodes = new List<Coord>();
var width = lines.Length;
var coords = new Dictionary<char, List<Coord>>();
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];
        if (c != '.')
        {
            if (!coords.Keys.Contains(c))
            {
                coords.Add(c, []);
            }

            foreach (var coord in coords[c])
            {
                Coord vector = (i - coord.row, j - coord.col);
                Coord antinodeA = (i + vector.row, j + vector.col);
                if (InBounds(antinodeA, width))
                {
                    antinodes.Add(antinodeA);
                }

                Coord antinodeB = (i - 2 * vector.row, j - 2 * vector.col);
                if (InBounds(antinodeB, width))
                {
                    antinodes.Add(antinodeB);
                }
            }

            coords[c].Add((i, j));
        }
    }
}

Console.WriteLine($"Count: {antinodes.Distinct().Count()}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

antinodes = new List<Coord>();
coords = new Dictionary<char, List<Coord>>();
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        var c = line[j];
        if (c != '.')
        {
            if (!coords.Keys.Contains(c))
            {
                coords.Add(c, []);
            }

            foreach (var coord in coords[c])
            {
                antinodes.Add(coord);
                antinodes.Add((i,j));
                Coord vector = (i - coord.row, j - coord.col);
                var k = 1;
                Coord antinodeA = (i + vector.row * k, j + vector.col * k);
                while (InBounds(antinodeA, width))
                {
                    antinodes.Add(antinodeA);
                    k++;
                    antinodeA = (i + vector.row * k, j + vector.col * k);
                }

                k = 2;
                Coord antinodeB = (i - k * vector.row, j - k * vector.col);
                while (InBounds(antinodeB, width))
                {
                    antinodes.Add(antinodeB);
                    k++;
                    antinodeB = (i - k * vector.row, j - k * vector.col);
                }
            }

            coords[c].Add((i, j));
        }
    }
}

Console.WriteLine($"Count: {antinodes.Distinct().Count()}");

chronograph.Toggle();