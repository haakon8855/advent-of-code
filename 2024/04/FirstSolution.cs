namespace Four;

public static class FirstSolution
{
    public static int PartOne(string[] lines)
    {
        var target = "XMAS";
        var targetArray = target.ToCharArray();
        Array.Reverse(targetArray);
        var reverseTarget = new string(targetArray);

        var count = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == 'X')
                {
                    // if there is space for XMAS to the right
                    if (j <= line.Length - 4)
                    {
                        // check right
                        if (line.Substring(j, 4) == target)
                            count++;

                        // if there is space up
                        if (i >= 3)
                        {
                            // check up-right
                            count++;
                            for (var k = 0; k < 4; k++)
                            {
                                if (lines[i - k][j + k] != target[k])
                                {
                                    count--;
                                    break;
                                }
                            }
                        }

                        // if there is space down
                        if (i <= lines.Length - 4)
                        {
                            // check down-right
                            count++;
                            for (var k = 0; k < 4; k++)
                            {
                                if (lines[i + k][j + k] != target[k])
                                {
                                    count--;
                                    break;
                                }
                            }
                        }
                    }

                    // if there is space left
                    if (j >= 3)
                    {
                        // check left
                        if (line.Substring(j - 3, 4) == reverseTarget)
                            count++;

                        // if there is space up
                        if (i >= 3)
                        {
                            // check up-left
                            count++;
                            for (var k = 0; k < 4; k++)
                            {
                                if (lines[i - k][j - k] != target[k])
                                {
                                    count--;
                                    break;
                                }
                            }
                        }

                        // if there is space down
                        if (i <= lines.Length - 4)
                        {
                            // check down-left
                            count++;
                            for (var k = 0; k < 4; k++)
                            {
                                if (lines[i + k][j - k] != target[k])
                                {
                                    count--;
                                    break;
                                }
                            }
                        }
                    }

                    // if there is space up
                    if (i >= 3)
                    {
                        // check up
                        count++;
                        for (var k = 0; k < 4; k++)
                        {
                            if (lines[i - k][j] != target[k])
                            {
                                count--;
                                break;
                            }
                        }
                    }

                    // if there is space down
                    if (i <= lines.Length - 4)
                    {
                        // check down
                        count++;
                        for (var k = 0; k < 4; k++)
                        {
                            if (lines[i + k][j] != target[k])
                            {
                                count--;
                                break;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }

    public static int PartTwo(string[] lines)
    {
        var count = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == 'A')
                {
                    if (i > 0 && i < lines.Length - 1 && j > 0 && j < line.Length - 1)
                    {
                        if ((lines[i - 1][j - 1] == 'M' && lines[i + 1][j + 1] == 'S' ||
                             lines[i - 1][j - 1] == 'S' && lines[i + 1][j + 1] == 'M') &&
                            (lines[i + 1][j - 1] == 'M' && lines[i - 1][j + 1] == 'S' ||
                             lines[i + 1][j - 1] == 'S' && lines[i - 1][j + 1] == 'M'))
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }
}