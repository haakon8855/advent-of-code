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

//int GetPossibilities(string springs, string numbers)
//{
//    int numSprings = numbers.Split(',').Select(int.Parse).Sum();

//    var qmarks = string.Join("", springs.Where(c => c == '?').Select(c => "1"));

//    int ways = 0;
//    for (int i = 0; i < Math.Pow(2, qmarks.Length); i++)
//    {
//        char[] possibleSprings = springs.ToCharArray();
//        string pattern = Convert.ToString(i, 2);

//        // if number of working springs in the proposed solution is not equal to the number of
//        // springs, then we dont have to check that solution.
//        if ((pattern + springs).Count(c => c == '1') != numSprings)
//        {
//            continue;
//        }

//        int lenDiff = possibleSprings.Length - pattern.Length;
//        if (lenDiff > 0)
//        {
//            pattern = new string('0', lenDiff) + pattern;
//        }

//        int offset = 0;
//        for (int j = possibleSprings.Length - 1; j >= 0; j--)
//        {
//            if (j - offset < 0)
//            {
//                break;
//            }
//            while (possibleSprings[j - offset] != '?')
//            {
//                offset++;
//                if (j - offset < 0)
//                {
//                    break;
//                }
//            }
//            if (j - offset < 0)
//            {
//                break;
//            }
//            possibleSprings[j - offset] = pattern[j];
//        }
//        string proposal = new string(possibleSprings);

//        if (Groups(proposal) == numbers)
//        {
//            ways++;
//        }
//    }

//    return ways;
//}

//int sum = 0;
//foreach (var line in lines)
//{
//    var springs = line.Split(' ')[0].Replace('.', '0').Replace('#', '1');
//    var numbers = line.Split(' ')[1];
//    sum += GetPossibilities(springs, numbers);
//}

//Console.WriteLine(sum);

//string Groups(string springs)
//{
//    return string.Join(',', springs.Split('0').Where(s => s != "").Select(s => s.Length).Select(s => s.ToString()));
//}

var memory = new Dictionary<string, long>();

long GetPossibilitiesRecursive(string springs, List<long> numbers)
{
    string key = springs + string.Join(',', numbers);
    if (memory.ContainsKey(key))
    {
        return memory[key];
    }
    long output = 0;
    if (springs.Length == 0)
    {
        if (numbers.Count == 0)
            return 1;
        else
            return 0;
    }
    if (springs[0] == '.')
    {
        output = GetPossibilitiesRecursive(springs.Substring(1), numbers);
        memory[key] = output;
        return output;
    }
    if (springs[0] == '?')
    {
        output = GetPossibilitiesRecursive("." + springs.Substring(1), numbers) +
               GetPossibilitiesRecursive("#" + springs.Substring(1), numbers);
        memory[key] = output;
        return output;
    }
    if (springs[0] == '#')
    {
        if (numbers.Count == 0)
        {
            return 0;
        }
        long length = numbers[0];
        if (springs.Length < length)
            return 0;
        string sub = springs.Substring(0, (int)length);
        if (!sub.Contains('.'))
        {
            List<long> newNumbers = new List<long>(numbers);
            newNumbers.RemoveAt(0);
            string newSprings = springs.Substring((int)length);
            if (newSprings.Length > 0)
            {
                char next = newSprings[0];
                if (next == '#')
                {
                    memory[key] = 0;
                    return 0;
                }
                else
                {
                    output = GetPossibilitiesRecursive(newSprings.Substring(1), newNumbers);
                    memory[key] = output;
                    return output;
                }
            }
            else
            {
                output = GetPossibilitiesRecursive(newSprings, newNumbers);
                memory[key] = output;
                return output;
            }
        }
        else
        {
            memory[key] = 0;
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

    memory.Clear();
    long possibilities = GetPossibilitiesRecursive(springs, numbersList);

    sum += possibilities;

    count++;
}

Console.WriteLine(sum);

chronograph.Toggle();

