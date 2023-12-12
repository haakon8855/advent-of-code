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

List<List<List<int>>> sequences = new();

int sum = 0;
foreach (string line in lines)
{
    List<List<int>> lineSequences = [line.Split(" ").Select(int.Parse).ToList()];

    while (lineSequences.Last().Any(x => x != 0))
    {
        List<int> nextSequence = [];
        for (int i = 0; i < lineSequences.Last().Count - 1; i++)
        {
            nextSequence.Add(lineSequences.Last()[i + 1] - lineSequences.Last()[i]);
        }
        lineSequences.Add(nextSequence);
    }
    sequences.Add(lineSequences);

    lineSequences.Last().Add(0);
    for (int i = lineSequences.Count - 2; i >= 0; i--)
    {
        int upRight = lineSequences[i + 1].Last() + lineSequences[i].Last();
        lineSequences[i].Add(upRight);
    }

    sum += lineSequences[0].Last();
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
foreach (var lineSequences in sequences)
{
    lineSequences[lineSequences.Count - 1] = lineSequences.Last().Prepend(0).ToList();
    for (int i = lineSequences.Count - 2; i >= 0; i--)
    {
        int upLeft = lineSequences[i].First() - lineSequences[i + 1].First();
        lineSequences[i] = lineSequences[i].Prepend(upLeft).ToList();
    }
    sum += lineSequences[0].First();
}

Console.WriteLine(sum);

chronograph.Toggle();

