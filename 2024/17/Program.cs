using Utils;

var test = false;
var chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

/* Instructions:
 * 
 * 0: adv
 *   A = A / (2^CB)
 *   truncated to int
 * 
 * 1: bxl
 *   B = B xor LT
 * 
 * 2: bst
 *   B = CB % 8
 * 
 * 3: jnz
 *   if A != 0:
 *     PC = LT
 * 
 * 4: bxc
 *   B = B xor C
 * 
 * 5: out
 *   print(CB % 8)
 * 
 * 6: bdv
 *   B = A / (2^CB)
 * 
 * 7: cdv
 *   C = A / (2^CB)
 */

var PC = 0L;
var initA = long.Parse(lines[0].Split(": ")[1]);
var initB = long.Parse(lines[1].Split(": ")[1]);
var initC = long.Parse(lines[2].Split(": ")[1]);
var A = initA;
var B = initB;
var C = initC;

var outstream = new List<long>();

var initMem = lines[4].Split(": ")[1].Split(",").Select(long.Parse).ToArray();
var mem = lines[4].Split(": ")[1].Split(",").Select(long.Parse).ToArray();

long Combo(long op)
{
    return op switch
    {
        4 => A,
        5 => B,
        6 => C,
        _ => op
    };
}

long Adv(long op)
{
    A = (long)(A / (Math.Pow(2, Combo(op))));
    return PC + 2;
}

long Bxl(long op)
{
    B = B ^ op;
    return PC + 2;
}

long Bst(long op)
{
    B = Combo(op) % 8;
    return PC + 2;
}

long Jnz(long op)
{
    if (A != 0)
        return op;
    else
        return PC + 2;
}

long Bxc(long op)
{
    B = B ^ C;
    return PC + 2;
}

long Out(long op)
{
    outstream.Add(Combo(op) % 8);
    return PC + 2;
}

long Bdv(long op)
{
    B = (long)(A / (Math.Pow(2, Combo(op))));
    return PC + 2;
}

long Cdv(long op)
{
    C = (long)(A / (Math.Pow(2, Combo(op))));
    return PC + 2;
}

while (PC < mem.Length - 1)
{
    var opcode = mem[PC];
    var operand = mem[PC + 1];
    PC = opcode switch
    {
        0 => Adv(operand),
        1 => Bxl(operand),
        2 => Bst(operand),
        3 => Jnz(operand),
        4 => Bxc(operand),
        5 => Out(operand),
        6 => Bdv(operand),
        7 => Cdv(operand)
    };
}

var result = string.Join(",", outstream);
Console.WriteLine($"Output: {result}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

// The program in the puzzle input does this:
A = initA;
B = initB;
C = initC;
PC = 0;
do
{
    B = A % 8;
    B = B ^ 1;
    C = A / (long)Math.Pow(2, B);
    B = B ^ 5;
    A = A / 8;
    B = B ^ C;
    Console.WriteLine(B % 8);
} while (A != 0);
// ======================= 

// Solution got by running the program below with initA = 0 and adding 1 to initA every outer loop iteration.
// This shows which programs create part of the solution. The initA for these programs should end in the same digits in hexadecimal.
// Use one of these numbers as first initA and increase initA by 0x10^N where N is number of digits in first initA.
// So if we start by setting initA to 0xd279b5, we must add 0x1000000 to initA every outer loop iteration.

var partsA = new List<long>();
if (args.Length < 1)
    initA = 0;
else
    initA = long.Parse(args[0]);
var start = initA;
initA = 0xd279b5;
var length = 1;
while (true)
{
    PC = 0;
    initA += 0x1000000;
    A = initA;
    B = initB;
    C = initC;
    outstream.Clear();
    mem = lines[4].Split(": ")[1].Split(",").Select(long.Parse).ToArray();
    var check = false;

    while (PC < mem.Length - 1)
    {
        var opcode = mem[PC];
        var operand = mem[PC + 1];
        check = opcode == 5;
        PC = opcode switch
        {
            0 => Adv(operand),
            1 => Bxl(operand),
            2 => Bst(operand),
            3 => Jnz(operand),
            4 => Bxc(operand),
            5 => Out(operand),
            6 => Bdv(operand),
            7 => Cdv(operand)
        };
        if (check && outstream[outstream.Count - 1] != initMem[outstream.Count - 1])
            break;
    }
    if (outstream.Count > length)
    {
        length++;
        Console.WriteLine();
        Console.WriteLine($"out: {string.Join(",", outstream)}");
        Console.WriteLine($"{initA}");
        partsA.Add(initA);
        partsA.ForEach(n => Console.Write(Convert.ToString(n, 16) + ", "));;
        Console.WriteLine();
    }
}

// This loop will never end...

