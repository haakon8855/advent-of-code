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

var a = new Vector(0, 0);
var b = new Vector(0, 0);

ulong sum = 0;

foreach (var line in lines)
{
    if (line.Contains('A'))
    {
        a = new Vector(line.Split(": ")[1].Split(", ").Select(n => n.Split("+")[1]).Select(ulong.Parse).ToArray());
    }
    else if (line.Contains('B'))
    {
        b = new Vector(line.Split(": ")[1].Split(", ").Select(n => n.Split("+")[1]).Select(ulong.Parse).ToArray());
    }
    else if (line.Contains("Prize"))
    {
        var goal = new Vector(line.Split(": ")[1].Split(", ").Select(n => n.Split("=")[1]).Select(ulong.Parse)
            .ToArray());
        var cost = ulong.MaxValue;
        for (ulong i = 1; i < (goal.X / a.X) + 1; i++)
        {
            if (i == 100)
                break;
            var start = a * i;
            var remaining = goal.X - start.X;
            if (remaining % b.X == 0)
            {
                // could be solvable
                var bCount = remaining / b.X;
                if (goal.Y == i * a.Y + bCount * b.Y)
                {
                    var currentCost = i * 3 + bCount;
                    cost = Math.Min(cost, currentCost);
                }
            }
        }

        if (cost < ulong.MaxValue)
            sum += cost;
    }
}

Console.WriteLine($"Total cost: {sum}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 


a = new Vector(0, 0);
b = new Vector(0, 0);

sum = 0;

foreach (var line in lines)
{
    if (line.Contains("A"))
    {
        a = new(line.Split(": ")[1].Split(", ").Select(n => n.Split("+")[1]).Select(ulong.Parse).ToArray());
    }
    else if (line.Contains("B"))
    {
        b = new(line.Split(": ")[1].Split(", ").Select(n => n.Split("+")[1]).Select(ulong.Parse).ToArray());
    }
    else if (line.Contains("Prize"))
    {
        var c = new Vector(line
            .Split(": ")[1]
            .Split(", ")
            .Select(n => n.Split("=")[1])
            .Select(n => ulong.Parse(n) + 10000000000000)
            .ToArray());
        
        /* The claw machine should satisfy the following set of equations:
         * I:   a.X * n + b.X * m = c.X
         * II:  a.Y * n + b.Y * m = c.Y
         *
         * If we solve eq. I for n:
         * n = (c.X - b.X * m) / a.X
         * 
         * If we use this to solve eq. II for m:
         * m = (a.X * c.Y - a.Y * c.X) / (a.X * b.Y - a.Y * b.X)
         *
         * If both m and n are integers, we have a solution.
         * Since there is only one solution for each claw machine,
         * it must be the cheapest option.
         */

        var m = (a.X * c.Y - a.Y * (double)c.X) / (a.X * b.Y - a.Y * (double)b.X);
        var n = (c.X - b.X * m) / a.X;
        if (m % 1 == 0 && n % 1 == 0)
            sum += (ulong)(m + n * 3);
    }
}

Console.WriteLine($"Total cost: {sum}");

chronograph.Toggle();

public readonly struct Vector(ulong x, ulong y)
{
    public ulong X { get; } = x;
    public ulong Y { get; } = y;

    public Vector(ulong[] coords) : this(coords[0], coords[1])
    {
    }

    public override string ToString() => $"({X}; {Y})";

    public static Vector operator +(Vector left, Vector right)
    {
        return new Vector(left.X + right.X, left.Y + right.Y);
    }

    public static Vector operator *(Vector vector, ulong factor)
    {
        return new Vector(vector.X * factor, vector.Y * factor);
    }
}
