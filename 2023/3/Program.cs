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

int sum = 0;
for (int i = 0; i < lines.Length; i++)
{
    string num = "";
    bool valid = false;
    for (int j = 0; j < lines[i].Length; j++)
    {
        char c = lines[i][j];

        if (char.IsNumber(c))
        {
            num = num += c;
            if (valid && j == lines[i].Length - 1)
            {
                int number = int.Parse(num);
                sum += number;
                break;
            }

            if (!valid)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        try
                        {
                            char check = lines[i + k][j + l];
                            if (!char.IsDigit(check) && check != '.')
                            {
                                valid = true;
                                break;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (valid)
                        break;
                }
            }
        }
        else if (valid)
        {
            int number = int.Parse(num);
            sum += number;
            valid = false;
            num = "";
        }
        else
        {
            num = "";
        }
    }
}
Console.WriteLine(sum);

chronograph.Toggle();

// ======================= 
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var cogs = new Dictionary<(int, int), List<int>>();

sum = 0;
for (int i = 0; i < lines.Length; i++)
{
    string num = "";
    bool valid = false;
    (int, int) coord = (0, 0);
    for (int j = 0; j < lines[i].Length; j++)
    {
        char c = lines[i][j];

        if (char.IsNumber(c))
        {
            num = num += c;
            if (valid && j == lines[i].Length - 1)
            {
                int number = int.Parse(num);
                if (!cogs.ContainsKey(coord))
                {
                    cogs[coord] = new List<int>();
                }
                cogs[coord].Add(number);
                break;
            }

            if (!valid)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        try
                        {
                            char check = lines[i + k][j + l];
                            if (check == '*')
                            {
                                valid = true;
                                coord = (i + k, j + l);
                                break;
                            }
                        }
                        catch (Exception) { }
                    }
                    if (valid)
                        break;
                }
            }
        }
        else if (valid)
        {
            int number = int.Parse(num);
            if (!cogs.ContainsKey(coord))
            {
                cogs[coord] = new List<int>();
            }
            cogs[coord].Add(number);
            valid = false;
            num = "";
        }
        else
        {
            num = "";
        }
    }
}
Console.WriteLine(cogs.Values.Where(v => v.Count == 2).Select(v => v.Aggregate((a, b) => a * b)).Sum());

chronograph.Toggle();

