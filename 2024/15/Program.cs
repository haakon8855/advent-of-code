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

var warehouseStrings = new List<string>();
var movements = "";
var readWarehouse = true;
var robot = new Coord(0, 0);
for (var i = 0; i < lines.Length; i++)
{
    if (string.IsNullOrWhiteSpace(lines[i]))
        readWarehouse = false;
    if (readWarehouse)
        warehouseStrings.Add(lines[i]);
    else
        movements += lines[i];
    var col = lines[i].IndexOf('@');
    if (col >= 0)
        robot = new Coord(i, col);
}

var map = warehouseStrings.Select(l => l.ToCharArray()).ToArray();


map[robot.row][robot.col] = '.';
foreach (var dir in movements)
{
    if (dir == '^')
    {
        // try to move up
        var up = map[robot.row - 1][robot.col];
        if (up == '.')
            robot.row--;
        else if (up == 'O')
        {
            // need to move boxes
            var j = 2;
            while (true)
            {
                up = map[robot.row - j][robot.col];
                if (up == '#')
                {
                    // cant move
                    break;
                }
                else if (up == '.')
                {
                    // empty spot, move boxes:
                    map[robot.row - j][robot.col] = 'O';
                    robot.row--;
                    map[robot.row][robot.col] = '.';
                    break;
                }
                j++;
            }
        }
    }
    if (dir == 'v')
    {
        // try to move down
        var down = map[robot.row + 1][robot.col];
        if (down == '.')
            robot.row++;
        else if (down == 'O')
        {
            // need to move boxes
            var j = 2;
            while (true)
            {
                down = map[robot.row + j][robot.col];
                if (down == '#')
                {
                    // cant move
                    break;
                }
                else if (down == '.')
                {
                    // empty spot, move boxes:
                    map[robot.row + j][robot.col] = 'O';
                    robot.row++;
                    map[robot.row][robot.col] = '.';
                    break;
                }
                j++;
            }
        }
    }
    if (dir == '<')
    {
        // try to move left
        var left = map[robot.row][robot.col - 1];
        if (left == '.')
            robot.col--;
        else if (left == 'O')
        {
            // need to move boxes
            var j = 2;
            while (true)
            {
                left = map[robot.row][robot.col - j];
                if (left == '#')
                {
                    // cant move
                    break;
                }
                else if (left == '.')
                {
                    // empty spot, move boxes:
                    map[robot.row][robot.col - j] = 'O';
                    robot.col--;
                    map[robot.row][robot.col] = '.';
                    break;
                }
                j++;
            }
        }
    }
    if (dir == '>')
    {
        // try to move right
        var up = map[robot.row][robot.col + 1];
        if (up == '.')
            robot.col++;
        else if (up == 'O')
        {
            // need to move boxes
            var j = 2;
            while (true)
            {
                up = map[robot.row][robot.col + j];
                if (up == '#')
                {
                    // cant move
                    break;
                }
                else if (up == '.')
                {
                    // empty spot, move boxes:
                    map[robot.row][robot.col + j] = 'O';
                    robot.col++;
                    map[robot.row][robot.col] = '.';
                    break;
                }
                j++;
            }
        }
    }
}

var gpsSum = 0;
for (var i = 0; i < map.Length; i++)
{
    for (var j = 0; j < map[0].Length; j++)
    {
        if (map[i][j] == 'O')
        {
            gpsSum += i * 100 + j;
        }
    }
}

Console.WriteLine($"GSP Sum: {gpsSum}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var needToMove = new List<Coord>();
bool CanMoveVertically(char[][] map, Coord pos, Func<Coord, Coord> move)
{
    var newPos = move.Invoke(pos);
    if (!needToMove.Contains(pos))
        needToMove.Add(pos);

    if (map[newPos.row][newPos.col] == '.')
        return true;
    if (map[newPos.row][newPos.col] == '#')
        return false;
    if (map[newPos.row][newPos.col] == '[')
    {
        return CanMoveVertically(map, newPos, move)
               && CanMoveVertically(map, new Coord(newPos.row, newPos.col + 1), move);
    }
    if (map[newPos.row][newPos.col] == ']')
    {
        return CanMoveVertically(map, newPos, move)
               && CanMoveVertically(map, new Coord(newPos.row, newPos.col - 1), move);
    }
    return false;
}

bool CanMoveHorizontally(char[][] map, Coord pos, Func<Coord, Coord> move)
{
    var newPos = move.Invoke(pos);
    needToMove.Add(pos);

    if (map[newPos.row][newPos.col] == '.')
        return true;
    if (map[newPos.row][newPos.col] == '#')
        return false;
    return CanMoveHorizontally(map, newPos, move);
}

// Widen the map:
var newWarehouseStrings = new List<string>();
for (var i = 0; i < warehouseStrings.Count(); i++)
{
    var line = "";
    for (var j = 0; j < warehouseStrings[i].Length; j++)
    {
        if (warehouseStrings[i][j] == '#')
            line += "##";
        else if (warehouseStrings[i][j] == 'O')
            line += "[]";
        else if (warehouseStrings[i][j] == '.')
            line += "..";
        else if (warehouseStrings[i][j] == '@')
            line += "@.";
    }
    newWarehouseStrings.Add(line);
    var col = line.IndexOf('@');
    if (col >= 0)
        robot = new Coord(i, col);
}

map = newWarehouseStrings.Select(l => l.ToCharArray()).ToArray();

map[robot.row][robot.col] = '.';
for (var i = 0; i < movements.Length; i++)
{
    var dir = movements[i];

    needToMove = [];

    if (dir == '^')
    {
        var move = (Coord pos) => new Coord(pos.row - 1, pos.col);
        if (CanMoveVertically(map, robot, move))
        {
            foreach (var toMove in needToMove.OrderBy(c => c.row))
            {
                map[toMove.row - 1][toMove.col] = map[toMove.row][toMove.col];
                if (!needToMove.Contains(new Coord(toMove.row + 1, toMove.col)))
                {
                    map[toMove.row][toMove.col] = '.';
                }
            }
            robot.row--;
        }
    }
    if (dir == 'v')
    {
        var move = (Coord pos) => new Coord(pos.row + 1, pos.col);
        if (CanMoveVertically(map, robot, move))
        {
            foreach (var toMove in needToMove.OrderByDescending(c => c.row))
            {
                map[toMove.row + 1][toMove.col] = map[toMove.row][toMove.col];
                if (!needToMove.Contains(new Coord(toMove.row - 1, toMove.col)))
                {
                    map[toMove.row][toMove.col] = '.';
                }
            }
            robot.row++;
        }
    }
    if (dir == '<')
    {
        var move = (Coord pos) => new Coord(pos.row, pos.col - 1);
        if (CanMoveHorizontally(map, robot, move))
        {
            needToMove.Reverse();
            foreach (var toMove in needToMove)
            {
                map[toMove.row][toMove.col - 1] = map[toMove.row][toMove.col];
                if (!needToMove.Contains(new Coord(toMove.row, toMove.col + 1)))
                {
                    map[toMove.row][toMove.col] = '.';
                }
            }
            robot.col--;
        }
    }
    if (dir == '>')
    {
        var move = (Coord pos) => new Coord(pos.row, pos.col + 1);
        if (CanMoveHorizontally(map, robot, move))
        {
            needToMove.Reverse();
            foreach (var toMove in needToMove)
            {
                map[toMove.row][toMove.col + 1] = map[toMove.row][toMove.col];
                if (!needToMove.Contains(new Coord(toMove.row, toMove.col - 1)))
                {
                    map[toMove.row][toMove.col] = '.';
                }
            }
            robot.col++;
        }
    }
}

gpsSum = 0;
for (var i = 0; i < map.Length; i++)
{
    for (var j = 0; j < map[0].Length; j++)
    {
        if (map[i][j] == '[')
        {
            gpsSum += i * 100 + j;
        }
    }
}

Console.WriteLine($"GSP Sum: {gpsSum}");

chronograph.Toggle();

