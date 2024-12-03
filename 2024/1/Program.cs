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

var left = lines.Select(line => int.Parse(line.Split("   ")[0])).Order().ToList();
var right = lines.Select(line => int.Parse(line.Split("   ")[1])).Order().ToList();

var error = 0;
for (int i = 0; i < left.Count; i++)
    error += Math.Abs(left[i] - right[i]);

Console.WriteLine($"error: {error}");

// ======================= 
chronograph.Toggle();

// ======================= 
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

left = lines.Select(line => int.Parse(line.Split("   ")[0])).ToList();
right = lines.Select(line => int.Parse(line.Split("   ")[1])).Order().ToList();
var distinctRight = right.Distinct().ToList();

error = 0;
foreach (var num in left)
{
    var startIndex = right.IndexOf(num);
    if (startIndex == -1)
        continue;
    
    var nextNumIndex = distinctRight[distinctRight.IndexOf(num) + 1];
    var endIndex = right.IndexOf(nextNumIndex);
    if (nextNumIndex == -1 || endIndex == -1)
        endIndex = right.Count;
    
    error += num * (endIndex - startIndex);
}

Console.WriteLine($"error: {error}");

// ======================= 
chronograph.Toggle();