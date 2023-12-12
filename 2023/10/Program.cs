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

int posY = lines.ToList().IndexOf(lines.Where(x => x.Contains("S")).First());
int posX = lines[posY].IndexOf("S");
int[,] distances = new int[lines.Length, lines[0].Length];

bool up = false, down = false, left = false, right = false;
char testChar;
// Resolve S
if (posY > 0)
{
    testChar = lines[posY - 1][posX];
    if (new char[] { '|', 'F', '7' }.Contains(testChar))
    {
        up = true;
    }
}
if (posY < lines.Length - 1)
{
    testChar = lines[posY + 1][posX];
    if (new char[] { '|', 'J', 'L' }.Contains(testChar))
    {
        down = true;
    }
}
if (posX > 0)
{
    testChar = lines[posY][posX - 1];
    if (new char[] { '-', 'F', 'L' }.Contains(testChar))
    {
        left = true;
    }
}
if (posX < lines[0].Length - 1)
{
    testChar = lines[posY][posX + 1];
    if (new char[] { '-', 'J', '7' }.Contains(testChar))
    {
        right = true;
    }
}

char startChar = '|';
if (up && down)
    startChar = '|';
if (left && right)
    startChar = '-';
if (up && left)
    startChar = 'J';
if (up && right)
    startChar = 'L';
if (down && left)
    startChar = '7';
if (down && right)
    startChar = 'F';

int x1 = posX, x2 = posX, y1 = posY, y2 = posY;

(int x1, int y1, int x2, int y2) VisitNode(ref int steps, int x, int y)
{
    char current = lines[y][x];
    steps++;
    distances[y, x] = steps;

    switch (current)
    {
        case '|':
            return (x, y + 1, x, y - 1);
        case '-':
            return (x - 1, y, x + 1, y);
        case 'F':
            return (x + 1, y, x, y + 1);
        case '7':
            return (x - 1, y, x, y + 1);
        case 'J':
            return (x - 1, y, x, y - 1);
        case 'L':
            return (x + 1, y, x, y - 1);
        default:
            // Should never happen
            return (x, y, x, y);
    }
}


distances[posY, posX] = -1;
lines[posY] = lines[posY].Replace('S', startChar);

int stepsA = 0;
int stepsB = 0;
while (true)
{
    var newCoords = VisitNode(ref stepsA, x1, y1);
    if (distances[newCoords.y1, newCoords.x1] != 0)
    {
        x1 = newCoords.x2;
        y1 = newCoords.y2;
    }
    else
    {
        x1 = newCoords.x1;
        y1 = newCoords.y1;
    }

    newCoords = VisitNode(ref stepsB, x2, y2);
    if (distances[newCoords.y2, newCoords.x2] != 0)
    {
        x2 = newCoords.x1;
        y2 = newCoords.y1;
    }
    else
    {
        x2 = newCoords.x2;
        y2 = newCoords.y2;
    }

    if (x1 == x2 && y1 == y2)
    {
        distances[y1, x1] = stepsA;
        break;
    }
}

Console.WriteLine(stepsA);

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 


var pipes = new List<List<string>>();
pipes.Add(new string('.', lines[0].Length + 2).Select(x => x.ToString()).ToList());

for (int i = 0; i < lines.Length; i++)
{
    var row = new List<string> { "." };
    for (int j = 0; j < lines[i].Length; j++)
    {
        row.Add(distances[i, j] != 0 ? "X" : ".");
    }
    row.Add(".");
    pipes.Add(row);
}
pipes.Add(new string('.', lines[0].Length + 2).Select(x => x.ToString()).ToList());

pipes
    .Select(r => r.Aggregate((a, b) => $"{a}{b}"))
    .ToList()
    .ForEach(Console.WriteLine);

(int x1, int y1, int x2, int y2) VisitNodeWithColoring(int x, int y, int bx, int by, bool other = false)
{
    string leftColor = "L";
    string rightColor = "R";
    if (other)
        (rightColor, leftColor) = (leftColor, rightColor);
    char current = lines[y - 1][x - 1];
    distances[y, x] = 1;

    switch (current)
    {
        case '|':
            if (by < y)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y][x - 1] != "X")
                pipes[y][x - 1] = leftColor;
            if (pipes[y][x + 1] != "X")
                pipes[y][x + 1] = rightColor;
            return (x, y + 1, x, y - 1);
        case '-':
            if (bx > x)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y - 1][x] != "X")
                pipes[y - 1][x] = leftColor;
            if (pipes[y + 1][x] != "X")
                pipes[y + 1][x] = rightColor;
            return (x - 1, y, x + 1, y);
        case 'F':
            if (bx > x)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y - 1][x] != "X")
                pipes[y - 1][x] = leftColor;
            if (pipes[y][x - 1] != "X")
                pipes[y][x - 1] = leftColor;
            return (x + 1, y, x, y + 1);
        case '7':
            if (bx < x)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y][x + 1] != "X")
                pipes[y][x + 1] = rightColor;
            if (pipes[y - 1][x] != "X")
                pipes[y - 1][x] = rightColor;
            return (x - 1, y, x, y + 1);
        case 'J':
            if (bx == x)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y + 1][x] != "X")
                pipes[y + 1][x] = rightColor;
            if (pipes[y][x + 1] != "X")
                pipes[y][x + 1] = rightColor;
            return (x - 1, y, x, y - 1);
        case 'L':
            if (bx == x)
            {
                (rightColor, leftColor) = (leftColor, rightColor);
            }
            if (pipes[y + 1][x] != "X")
                pipes[y + 1][x] = leftColor;
            if (pipes[y][x - 1] != "X")
                pipes[y][x - 1] = leftColor;
            return (x + 1, y, x, y - 1);
        default:
            // Should never happen
            return (x, y, x, y);
    }
}

distances = new int[lines.Length + 2, lines[0].Length + 2];

posY += 1;
posX += 1;

up = false;
down = false;
left = false;
right = false;

x1 = posX;
x2 = posX;
y1 = posY;
y2 = posY;

distances[posY, posX] = -1;
lines[posY - 1] = lines[posY - 1].Replace('S', startChar);
int bx1 = x1 - 1;
int by1 = y1 - 1;
int bx2 = x2 + 1;
int by2 = y2 + 1;

while (true)
{
    var newCoords = VisitNodeWithColoring(x1, y1, bx1, by1);
    bx1 = x1;
    by1 = y1;
    if (distances[newCoords.y1, newCoords.x1] != 0)
    {
        x1 = newCoords.x2;
        y1 = newCoords.y2;
    }
    else
    {
        x1 = newCoords.x1;
        y1 = newCoords.y1;
    }

    newCoords = VisitNodeWithColoring(x2, y2, bx2, by2, true);
    bx2 = x2;
    by2 = y2;
    if (distances[newCoords.y2, newCoords.x2] != 0)
    {
        x2 = newCoords.x1;
        y2 = newCoords.y1;
    }
    else
    {
        x2 = newCoords.x2;
        y2 = newCoords.y2;
    }

    if (x1 == x2 && y1 == y2)
    {
        distances[y1, x1] = 1;
        break;
    }
}

bool edited = true;
while (edited)
{
    edited = false;
    for (int i = 1; i < pipes.Count - 1; i++)
    {
        for (int j = 1; j < pipes[i].Count - 1; j++)
        {
            if (pipes[i][j] == "R")
            {
                string color = pipes[i][j];
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if (pipes[i + k][j + l] == ".")
                        {
                            pipes[i + k][j + l] = color;
                            edited = true;
                        }
                    }
                }
            }
        }
    }
}

pipes
    .Select(r => r.Aggregate((a, b) => $"{a}{b}"))
    .ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine(pipes.SelectMany(x => x).Where(x => x == "R").Count());

chronograph.Toggle();

