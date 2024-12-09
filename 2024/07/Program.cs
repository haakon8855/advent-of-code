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

var sum = 0L;
foreach (var line in lines)
{
    var equation = line.Split(": ");
    var answer = long.Parse(equation[0]);
    var numbers = equation[1].Split(" ").Select(long.Parse).ToArray();
    if (PopNumberAndApplyOperator(answer, numbers[0], numbers.Skip(1).ToArray(), true) ||
        PopNumberAndApplyOperator(answer, numbers[0], numbers.Skip(1).ToArray(), false))
    {
        sum += answer;
    }
}

Console.WriteLine($"Sum: {sum}");

bool PopNumberAndApplyOperator(long target, long current, long[] numbers, bool useAdd)
{
    if (useAdd)
        current += numbers[0];
    else
        current *= numbers[0];

    if (current > target)
        return false;
    if (numbers.Length == 1)
    {
        if (current == target)
            return true;
        return false;
    }

    return PopNumberAndApplyOperator(target, current, numbers.Skip(1).ToArray(), true) ||
           PopNumberAndApplyOperator(target, current, numbers.Skip(1).ToArray(), false);
}

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

sum = 0L;
foreach (var line in lines)
{
    var equation = line.Split(": ");
    var answer = long.Parse(equation[0]);
    var numbers = equation[1].Split(" ").Select(long.Parse).ToArray();
    if (PopNumberAndApplyOperatorPartTwo(answer, numbers[0], numbers.Skip(1).ToArray(), true) ||
        PopNumberAndApplyOperatorPartTwo(answer, numbers[0], numbers.Skip(1).ToArray(), false) ||
        PopNumberAndApplyOperatorPartTwo(answer, numbers[0], numbers.Skip(1).ToArray(), null))
    {
        sum += answer;
    }
}

Console.WriteLine($"Sum: {sum}");

bool PopNumberAndApplyOperatorPartTwo(long target, long current, long[] numbers, bool? useAdd)
{
    if (useAdd is null)
        current = long.Parse($"{current}{numbers[0]}");
    else if (useAdd.Value)
        current += numbers[0];
    else
        current *= numbers[0];

    if (current > target)
        return false;
    if (numbers.Length == 1)
    {
        if (current == target)
            return true;
        return false;
    }

    return PopNumberAndApplyOperatorPartTwo(target, current, numbers.Skip(1).ToArray(), true) ||
           PopNumberAndApplyOperatorPartTwo(target, current, numbers.Skip(1).ToArray(), false) ||
           PopNumberAndApplyOperatorPartTwo(target, current, numbers.Skip(1).ToArray(), null);
}

chronograph.Toggle();