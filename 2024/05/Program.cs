using Five;
using Utils;

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

// Part 1 Implementation

var rules = new List<(int before, int after)>();
var rulesAreRead = false;
var sum = 0;
foreach (var line in lines)
{
    if (line == "")
    {
        // switch from reading rules to checking updates
        rulesAreRead = true;
        continue;
    }

    if (!rulesAreRead)
    {
        // read rules
        var nums = line.Split('|').Select(int.Parse).ToList();
        rules.Add((nums[0], nums[1]));
    }
    else
    {
        // check updates
        var nums = line.Split(',').Select(int.Parse).ToList();
        sum += nums[nums.Count / 2];
        foreach (var rule in rules)
        {
            var beforeIndex = nums.IndexOf(rule.before);
            var afterIndex = nums.IndexOf(rule.after);
            if (afterIndex > -1 && beforeIndex > afterIndex)
            {
                // update is invalid
                sum -= nums[nums.Count / 2];
                break;
            }
        }
    }
}

Console.WriteLine($"Sum of middle page numbers (valid updates): {sum}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

// sum = FirstSolution.PartTwo(lines);

rules = [];
rulesAreRead = false;
sum = 0;
foreach (var line in lines)
{
    if (line == "")
    {
        // switch from reading rules to checking updates
        rulesAreRead = true;
        continue;
    }

    if (!rulesAreRead)
    {
        // read rules
        var nums = line.Split('|').Select(int.Parse).ToList();
        rules.Add((nums[0], nums[1]));
    }
    else
    {
        // check updates
        var nums = line.Split(',').Select(int.Parse).ToList();
        var comparer = new NisseComparer(rules);

        var ordered = nums.Order(comparer).ToList();
        if (ordered.Zip(nums).Any(a => a.First != a.Second))
            sum += ordered[ordered.Count / 2];
    }
}

Console.WriteLine($"Sum of middle page numbers (Repaired updates): {sum}");

chronograph.Toggle();