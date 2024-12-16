using Utils;

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

var height = 0;
var width = 0;
var robots = new List<Robot>();

foreach (var line in lines)
{
    var pos = new Vector(line.Split(' ')[0].Substring(2).Split(',').Select(int.Parse).ToArray());
    var vel = new Vector(line.Split(' ')[1].Substring(2).Split(',').Select(int.Parse).ToArray());
    width = Math.Max(width, pos.X + 1);
    height = Math.Max(height, pos.Y + 1);
    robots.Add(new(pos, vel));
}

for (var i = 0; i < robots.Count(); i++)
{
    var newPos = robots[i].Pos + robots[i].Vel * 100;
    newPos = new(newPos.X % width, newPos.Y % height);
    if (newPos.X < 0)
        newPos = new(newPos.X + width, newPos.Y);
    if (newPos.Y < 0)
        newPos = new(newPos.X, newPos.Y + height);
    robots[i].Pos = newPos;
}

var middleX = (width - 1) / 2;
var middleY = (height - 1) / 2;

var firstQuadrantCount = robots.Where(r => r.Pos.X > middleX && r.Pos.Y < middleY).Count();
var secondQuadrantCount = robots.Where(r => r.Pos.X < middleX && r.Pos.Y < middleY).Count();
var thirdQuadrantCount = robots.Where(r => r.Pos.X < middleX && r.Pos.Y > middleY).Count();
var fourthQuadrantCount = robots.Where(r => r.Pos.X > middleX && r.Pos.Y > middleY).Count();

var safetyFactor = firstQuadrantCount * secondQuadrantCount * thirdQuadrantCount * fourthQuadrantCount;

Console.WriteLine($"Safety factor: {safetyFactor}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

height = 0;
width = 0;
robots = new List<Robot>();

foreach (var line in lines)
{
    var pos = new Vector(line.Split(' ')[0].Substring(2).Split(',').Select(int.Parse).ToArray());
    var vel = new Vector(line.Split(' ')[1].Substring(2).Split(',').Select(int.Parse).ToArray());
    width = Math.Max(width, pos.X + 1);
    height = Math.Max(height, pos.Y + 1);
    robots.Add(new(pos, vel));
}

var safety = int.MaxValue;
for (var i = 0; i < 10000; i++)
{
    for (var j = 0; j < robots.Count(); j++)
    {
        var newPos = robots[j].Pos + robots[j].Vel;
        newPos = new(newPos.X % width, newPos.Y % height);
        if (newPos.X < 0)
            newPos = new(newPos.X + width, newPos.Y);
        if (newPos.Y < 0)
            newPos = new(newPos.X, newPos.Y + height);
        robots[j].Pos = newPos;
    }

    var first = robots.Where(r => r.Pos.X > middleX && r.Pos.Y < middleY).Count();
    var second = robots.Where(r => r.Pos.X < middleX && r.Pos.Y < middleY).Count();
    var third = robots.Where(r => r.Pos.X < middleX && r.Pos.Y > middleY).Count();
    var fourth = robots.Where(r => r.Pos.X > middleX && r.Pos.Y > middleY).Count();

    var currentSafety = first * second * third * fourth;
    if (currentSafety < safety)
    {
        safety = currentSafety;

        Console.WriteLine($"Iteration: {i+1}");
        var map = new List<char[]>();
        for (var j = 0; j < height; j++)
        {
            map.Add(new string('.', width).ToCharArray());
        }
        foreach (var robot in robots)
        {
            map[robot.Pos.Y][robot.Pos.X] = 'X';
        }
        foreach (var line in map)
        {
            Console.WriteLine(new string(line));
        }
    }
}

chronograph.Toggle();

public class Robot(Vector startPos, Vector velocity)
{
    public Vector Pos { get; set; } = startPos;
    public Vector Vel { get; } = velocity;
}

public readonly struct Vector(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public Vector(int[] coords) : this(coords[0], coords[1])
    {
    }

    public override string ToString() => $"({X}; {Y})";

    public static Vector operator +(Vector left, Vector right)
    {
        return new Vector(left.X + right.X, left.Y + right.Y);
    }

    public static Vector operator *(Vector vector, int factor)
    {
        return new Vector(vector.X * factor, vector.Y * factor);
    }
}

