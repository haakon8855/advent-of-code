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

// Input preparation
var towels = lines[0].Split(", ");
var designs = new List<string>();
for (var i = 2; i < lines.Length; i++)
{
    designs.Add(lines[i]);
}

/*
 * Function checking if a design is possible
 */
bool IsDesignPossible(string design)
{
    // An empty design means the subdesign that created this function call is finished and the design is possible
    if (string.IsNullOrEmpty(design))
        return true;
    // Check every towel if it fits at the beginning of the design
    foreach (var towel in towels)
    {
        var len = towel.Length;
        if (design.Length >= towel.Length && design.Substring(0, len) == towel)
        {
            // Recursive call to check subdesign
            // If it is possible, escape recursion and return true
            if (IsDesignPossible(design.Substring(len)))
                return true;
        }
    }
    // The current subdesign is not possible
    return false;
}

// Count possible
var possible = 0;
foreach (var design in designs)
{
    possible += IsDesignPossible(design) ? 1 : 0;
}
Console.WriteLine($"Possible: {possible}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

// Cache of subDesigns storing the number of possible ways that design can be created
var subDesigns = new Dictionary<string, long>();

/*
 * Function counting number of ways a design can be created
 */
long CountDesignPossibilities(string design)
{
    // Start the count for this subdesign at 0
    var count = 0L;
    // An empty design means the subdesign that created this function call is finished and the design is possible
    if (string.IsNullOrEmpty(design))
        return 1;
    // Check every towel if it fits at the beginning of the design
    foreach (var towel in towels)
    {
        var len = towel.Length;
        if (design.Length >= len && design.Substring(0, len) == towel)
        {
            var subDesign = design.Substring(len);
            // Check if the subdesign is in the cache
            // If not, calculate its count and store it in the cache
            if (!subDesigns.ContainsKey(subDesign))
                subDesigns[subDesign] = CountDesignPossibilities(subDesign);
            count += subDesigns[subDesign];
        }
    }
    return count;
}

var allPossible = 0L;
foreach (var design in designs)
{
    allPossible += CountDesignPossibilities(design);
}
Console.WriteLine($"All possible: {allPossible}");

chronograph.Toggle();
