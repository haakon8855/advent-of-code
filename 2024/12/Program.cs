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

// Add padding to avoid checking boundary indices
var padded = lines.ToList();
var width = padded[0].Length;
padded = padded.Select(l => '0' + l + '0').ToList();
padded.Insert(0, new string('0', width + 2));
padded.Add(new string('0', width + 2));

var visited = new List<Coord>();

var cost = 0;
for (int i = 1; i < padded.Count - 1; i++)
{
    for (int j = 1; j < padded[0].Length - 1; j++)
    {
        if (!visited.Contains((i, j)))
        {
            var plant = padded[i][j];
            var (fences, area) = GetFencesAndAreaForRegion((i, j), plant);
            cost += fences * area;
        }
    }
}

(int fences, int area) GetFencesAndAreaForRegion(Coord coord, char plant)
{
    // Check if the coordinate we have is of the same plant type
    if (padded[coord.row][coord.col] != plant)
    {
        // If not, we have stepped over a fence
        return (1, 0);
    }

    // Check if we have already been at this location before
    if (visited.Contains(coord))
    {
        // If yes, return empty
        return (0, 0);
    }

    visited.Insert(0, coord);

    var fences = 0;
    var area = 1;

    var (subfences, subarea) = GetFencesAndAreaForRegion((coord.row - 1, coord.col), plant);
    fences += subfences;
    area += subarea;

    (subfences, subarea) = GetFencesAndAreaForRegion((coord.row + 1, coord.col), plant);
    fences += subfences;
    area += subarea;

    (subfences, subarea) = GetFencesAndAreaForRegion((coord.row, coord.col - 1), plant);
    fences += subfences;
    area += subarea;

    (subfences, subarea) = GetFencesAndAreaForRegion((coord.row, coord.col + 1), plant);
    fences += subfences;
    area += subarea;

    return (fences, area);
}

Console.WriteLine($"Cost: {cost}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

visited = new List<Coord>();

cost = 0;
for (int i = 1; i < padded.Count - 1; i++)
{
    for (int j = 1; j < padded[0].Length - 1; j++)
    {
        if (!visited.Contains((i, j)))
        {
            var plant = padded[i][j];
            // We count corners, because number of corners == number of sides
            var (corners, area) = GetCornersAndAreaForRegion((i, j), plant);
            cost += corners * area;
        }
    }
}

(int corners, int area) GetCornersAndAreaForRegion(Coord coord, char plant)
{
    // Check if the coordinate we have is of the same plant type
    // OR if we have already been at this cell before
    if (padded[coord.row][coord.col] != plant || visited.Contains(coord))
        return (0, 0);

    visited.Insert(0, coord);

    var corners = CountCornersForCell(coord, plant);
    var area = 1;

    var (subcorners, subarea) = GetCornersAndAreaForRegion((coord.row - 1, coord.col), plant);
    corners += subcorners;
    area += subarea;

    (subcorners, subarea) = GetCornersAndAreaForRegion((coord.row + 1, coord.col), plant);
    corners += subcorners;
    area += subarea;

    (subcorners, subarea) = GetCornersAndAreaForRegion((coord.row, coord.col - 1), plant);
    corners += subcorners;
    area += subarea;

    (subcorners, subarea) = GetCornersAndAreaForRegion((coord.row, coord.col + 1), plant);
    corners += subcorners;
    area += subarea;

    return (corners, area);
}

int CountCornersForCell(Coord coord, char plant)
{
    var n = padded[coord.row - 1][coord.col] == plant;
    var s = padded[coord.row + 1][coord.col] == plant;
    var w = padded[coord.row][coord.col - 1] == plant;
    var e = padded[coord.row][coord.col + 1] == plant;

    var nw = padded[coord.row - 1][coord.col - 1] == plant ? 0 : 1;
    var ne = padded[coord.row - 1][coord.col + 1] == plant ? 0 : 1;
    var sw = padded[coord.row + 1][coord.col - 1] == plant ? 0 : 1;
    var se = padded[coord.row + 1][coord.col + 1] == plant ? 0 : 1;

    var corners = 0;

    if (n & s & w & e) // ┼
        corners += nw + ne + sw + se;
    else if (n & s & w & !e) // ┤
        corners += nw + sw;
    else if (n & s & !w & e) // ├
        corners += ne + se;
    else if (n & !s & w & e) // ┴
        corners += nw + ne;
    else if (!n & s & w & e) // ┬
        corners += sw + se;
    else if (n & !s & w & !e) // ┘
        corners += 1 + nw;
    else if (n & !s & !w & e) // └
        corners += 1 + ne;
    else if (!n & s & w & !e) // ┐
        corners += 1 + sw;
    else if (!n & s & !w & e) // ┌
        corners += 1 + se;
    else if (n & !s & !w & !e) // ^
        corners += 2;
    else if (!n & s & !w & !e) // v
        corners += 2;
    else if (!n & !s & w & !e) // <
        corners += 2;
    else if (!n & !s & !w & e) // >
        corners += 2;
    else if (n & s & !w & !e) // │
    {
    }
    else if (!n & !s & w & e) // ─
    {
    }
    else if (!n & !s & !w & !e)
        corners += 4;

    return corners;
}


Console.WriteLine($"Cost: {cost}");

chronograph.Toggle();