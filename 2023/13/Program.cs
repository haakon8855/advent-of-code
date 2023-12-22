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

List<string> block = new();
int sum = 0;
foreach (var line in lines)
{
    if (line == "")
    {
        List<int> mirrorVal = GetMirrorValue(block);
        sum += mirrorVal.FirstOrDefault();

        block.Clear();
    }
    else
    {
        block.Add(line);
    }
}

Console.WriteLine(sum);

List<int> GetMirrorValue(List<string> block)
{
    List<int> output = new List<int>();
    List<int> row = GetMirrorValueRowOrCol(block, block.Count, RowsAreEqual);
    output.AddRange(row.Select(x => x * 100));

    List<int> col = GetMirrorValueRowOrCol(block, block[0].Length, ColumnsAreEqual);
    output.AddRange(col);

    return output;
}


List<int> GetMirrorValueRowOrCol(List<string> block, int blockLength, Func<List<string>, int, int, bool> RowsOrColsAreEqual)
{
    var output = new List<int>();
    for (int i = 0; i < blockLength - 1; i++)
    {
        bool mirrored = false;
        if (RowsOrColsAreEqual(block, i, i + 1))
        {
            mirrored = true;
            int a = i - 1;
            int b = i + 2;
            while (a >= 0 && b < blockLength)
            {
                if (!RowsOrColsAreEqual(block, a, b))
                {
                    mirrored = false;
                    break;
                }
                a--;
                b++;
            }
        }
        if (mirrored)
            output.Add(i + 1);
    }
    return output;
}


bool RowsAreEqual(List<string> block, int col1, int col2)
{
    return block[col1] == block[col2];
}


bool ColumnsAreEqual(List<string> block, int col1, int col2)
{
    for (int i = 0; i < block.Count; i++)
    {
        if (block[i][col1] != block[i][col2])
        {
            return false;
        }
    }
    return true;
}


chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

block = new List<string>();
sum = 0;
foreach (var line in lines)
{
    if (line == "")
    {
        int mirrorVal = GetMirrorValue(block).FirstOrDefault();

        int differentMirrorVal = GetNewMirrorValue(block, mirrorVal);

        sum += differentMirrorVal;

        block.Clear();
    }
    else
    {
        block.Add(line);
    }
}

Console.WriteLine(sum);

int GetNewMirrorValue(List<string> block, int oldMirrorVal)
{
    List<int> possibilities = new();

    for (int i = 0; i < block.Count; i++)
    {
        for (int j = 0; j < block[0].Length; j++)
        {
            List<string> testBlock = new List<string>(block);
            string line = testBlock[i];
            char toggle = line[j];
            toggle = (toggle == '.') ? '#' : '.';
            testBlock[i] = line.Substring(0, j) + toggle + line.Substring(j + 1);
            List<int> mirrorVals = GetMirrorValue(testBlock);
            possibilities.AddRange(mirrorVals);
        }
    }

    return possibilities.Where(x => x != oldMirrorVal).First();
}

chronograph.Toggle();

