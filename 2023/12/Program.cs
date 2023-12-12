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

var cache = new Dictionary<string, long>();
long GetPossibilitiesRecursive(string springs, List<long> numbers)
{
    long output = 0;

    // Check if input is in cache
    string key = springs + string.Join(',', numbers);
    if (cache.ContainsKey(key))
    {
        return cache[key];
    }

    // Check if string is empty
    if (springs.Length == 0)
    {
        // if string is empty and there are no more sequences,
        // configuration is possible
        if (numbers.Count == 0)
            return 1;
        else
            return 0;
    }

    // If first char in string is '.'
    if (springs[0] == '.')
    {
        output = GetPossibilitiesRecursive(springs.Substring(1), numbers);
        cache[key] = output;
        return output;
    }

    // If first char in string is '?'
    if (springs[0] == '?')
    {
        output = GetPossibilitiesRecursive("." + springs.Substring(1), numbers) +
               GetPossibilitiesRecursive("#" + springs.Substring(1), numbers);
        cache[key] = output;
        return output;
    }

    // If first char in string is '#'
    if (springs[0] == '#')
    {
        // Check if there are more sequences left to place
        if (numbers.Count == 0)
        {
            return 0;
        }

        long length = numbers[0]; // Length of next sequence
        if (springs.Length < length) // If string has no more space for next sequence
        {
            return 0;
        }

        // Get the first n chars from the string, where n is the length of the next sequence
        string sub = springs.Substring(0, (int)length);

        // If no chars in the string are '.', we can fit the sequence in the substring
        if (!sub.Contains('.'))
        {
            // Find the remaining string and list of sequences
            List<long> newNumbers = new List<long>(numbers);
            newNumbers.RemoveAt(0);
            string newSprings = springs.Substring((int)length);

            // If remaining string is not empty, need to check next char to avoid collisions
            if (newSprings.Length > 0)
            {
                // If next char after the sequence is '#',
                // we are not allowed to put the sequence there
                if (newSprings[0] == '#')
                {
                    cache[key] = 0;
                    return 0;
                }
                else
                {
                    // We are allowed to put the sequnce
                    // Remove the first char from newSprings and call recursively
                    output = GetPossibilitiesRecursive(newSprings.Substring(1), newNumbers);
                    cache[key] = output;
                    return output;
                }
            }
            else
            {
                // Remaining string is empty,
                output = GetPossibilitiesRecursive(newSprings, newNumbers);
                cache[key] = output;
                return output;
            }
        }
        else
        {
            cache[key] = 0;
            return 0;
        }
    }
    return 0;
}

long sum = 0;
foreach (var line in lines)
{
    string springs = line.Split(' ')[0];
    string numbers = line.Split(' ')[1];
    List<long> numbersList = numbers.Split(',').Select(long.Parse).ToList();

    long tmp = GetPossibilitiesRecursive(springs, numbersList);
    sum += tmp;
}

Console.WriteLine(sum);

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

sum = 0;
long count = 0;
foreach (var line in lines)
{
    var compressedSprings = line.Split(' ')[0];
    var springs = compressedSprings;
    var compressedNumbers = line.Split(' ')[1];
    var numbers = compressedNumbers;

    for (int i = 0; i < 4; i++)
    {
        springs = springs + "?" + compressedSprings;
        numbers = numbers + "," + compressedNumbers;
    }

    List<long> numbersList = numbers.Split(',').Select(long.Parse).ToList();

    cache.Clear();
    long possibilities = GetPossibilitiesRecursive(springs, numbersList);
    sum += possibilities;
    count++;
}

Console.WriteLine(sum);

chronograph.Toggle();

