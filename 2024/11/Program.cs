using Utils;

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1 & 2: ");
chronograph.Toggle();
// ======================= 

var stones = lines[0]
    .Split(" ")
    .Select(long.Parse)
    .GroupBy(i => i)
    .ToDictionary(g => g.Key, g => (long)g.Count());

Dictionary<long, long> Blink()
{
    var output = new Dictionary<long, long>();
    foreach (var key in stones.Keys)
    {
        if (key == 0)
        {
            var count = stones[key];
            if (!output.TryAdd(1, count))
                output[1] += count;
        }
        else if (key.ToString().Length % 2 == 0)
        {
            var stoneString = key.ToString();
            var count = stones[key];

            var left = long.Parse(stoneString.Substring(0, stoneString.Length / 2));
            if (!output.TryAdd(left, count))
                output[left] += count;

            var right = long.Parse(stoneString.Substring(stoneString.Length / 2));
            if (!output.TryAdd(right, count))
                output[right] += count;
        }
        else
        {
            var count = stones[key];
            var num = key * 2024;
            if (!output.TryAdd(num, count))
                output[num] += count;
        }
    }

    return output;
}

for (int i = 0; i < 75; i++)
{
    stones = Blink();
    Console.WriteLine($"Blink no. {i + 1}: {stones.Values.Sum()} stones! ({stones.Count} unique)");
}

chronograph.Toggle();
