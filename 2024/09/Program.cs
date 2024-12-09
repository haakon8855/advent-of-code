using Utils;

var test = false;
Chronograph chronograph = new Chronograph();
var path = test ? "test.txt" : "input.txt";
var input = File.ReadAllLines(path)[0];

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Setup:");
chronograph.Toggle();
// ======================= 

// Create disk image
var diskImage = new List<long>();
var isWritingData = true;
var id = 0;
foreach (var c in input)
{
    if (isWritingData)
    {
        // Write data to disk image
        for (long i = 0; i < long.Parse(c.ToString()); i++)
        {
            diskImage.Add(id);
        }

        id++;
    }
    else
    {
        // Write free space to disk image
        for (long i = 0; i < long.Parse(c.ToString()); i++)
        {
            diskImage.Add(-1);
        }
    }

    isWritingData = !isWritingData;
}

chronograph.Toggle();
Console.WriteLine("\nPart 1: ");
chronograph.Toggle();

// Move data to leftmost available space (fragmentation allowed)
var newDiskImage = new List<long>();
newDiskImage.AddRange(diskImage);
for (int i = 0; i < newDiskImage.Count; i++)
{
    if (diskImage[i] == -1)
    {
        while (newDiskImage[^1] == -1)
        {
            newDiskImage.RemoveAt(newDiskImage.Count - 1);
        }

        if (i >= newDiskImage.Count)
        {
            break;
        }

        newDiskImage[i] = newDiskImage[^1];
        newDiskImage.RemoveAt(newDiskImage.Count - 1);
    }
}

// Calculate disk image checksum
var checksum = newDiskImage.Select((n, i) => n * i).Sum();
Console.WriteLine($"CHECKSUM: {checksum}");

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

var earliestFree = new Dictionary<int, int>();

// Find an index where there are *size* bytes of free space on the given disk
int FindFreeSpace(List<long> disk, int size)
{
    earliestFree.TryGetValue(size, out var start);
    for (int i = start; i < disk.Count; i++)
    {
        if (disk[i] == -1)
        {
            var free = true;
            for (int j = 1; j < size; j++)
            {
                if (i + j >= disk.Count || disk[i + j] != -1)
                {
                    free = false;
                    break;
                }
            }

            if (free)
            {
                earliestFree[size] = i + size;
                return i;
            }
        }
    }

    return -1;
}

// Reset the new disk image
newDiskImage = [];
newDiskImage.AddRange(diskImage);

// Move data to leftmost available space (fragmentation not allowed)
for (int i = diskImage.Count - 1; i >= 0; i--)
{
    var num = diskImage[i];
    if (num != -1)
    {
        var length = 1;
        while (i - length >= 0 && diskImage[i - length] == num)
        {
            length++;
        }

        var destination = FindFreeSpace(newDiskImage, length);
        var sourceIndex = i - (length - 1);
        if (destination != -1 && destination < sourceIndex)
        {
            for (int j = 0; j < length; j++)
            {
                newDiskImage[destination + j] = diskImage[sourceIndex + j];
                newDiskImage[sourceIndex + j] = -1;
            }
        }

        i -= (length - 1);
    }
}

// Calculate disk image checksum
checksum = newDiskImage.Select(n => Math.Max(0, n)).Select((n, i) => n * i).Sum();
Console.WriteLine($"CHECKSUM: {checksum}");

chronograph.Toggle();