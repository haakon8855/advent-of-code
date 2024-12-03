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

var safe = 0;
foreach (var line in lines)
{
    var nums = line.Split(" ").Select(int.Parse).ToList();

    if (CheckReport(nums))
        safe += 1;
}

bool CheckReport(List<int> nums)
{
    var sign = Math.Sign(nums[0] - nums[1]);
    for (var i = 0; i < nums.Count - 1; i++)
    {
        var diff = nums[i] - nums[i + 1];
        if (Math.Sign(diff) != sign || Math.Abs(diff) == 0 || Math.Abs(diff) > 3)
            return false;
    }

    return true;
}

Console.WriteLine($"Safe: {safe}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

safe = 0;
foreach (var line in lines)
{
    var nums = line.Split(" ").Select(int.Parse).ToList();

    if (CheckReportButAllowOneError(nums))
        safe += 1;
}

bool CheckReportButAllowOneError(List<int> nums)
{
    var sign = Math.Sign(nums[0] - nums[1]);
    for (var i = 0; i < nums.Count - 1; i++)
    {
        var diff = nums[i] - nums[i + 1];
        if (Math.Sign(diff) != sign || Math.Abs(diff) == 0 || Math.Abs(diff) > 3)
        {
            var superFirstRemoved = new List<int>(nums);
            superFirstRemoved.RemoveAt(0);
            var firstRemoved = new List<int>(nums);
            firstRemoved.RemoveAt(i);
            var secondRemoved = new List<int>(nums);
            secondRemoved.RemoveAt(i+1);
            return (CheckReport(superFirstRemoved) || CheckReport(firstRemoved) || CheckReport(secondRemoved));
        }
    }

    return true;
}

Console.WriteLine($"Safe: {safe}");

chronograph.Toggle();