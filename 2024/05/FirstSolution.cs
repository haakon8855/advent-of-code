namespace Five;

public static class FirstSolution
{
    public static int PartTwo(string[] lines)
    {
        var rules = new List<(int before, int after)>();
        var rulesAreRead = false;
        var sum = 0;
        foreach (var line in lines)
        {
            if (line == "")
            {
                // switch from reading rules to checking updates
                rulesAreRead = true;
                continue;
            }

            if (!rulesAreRead)
            {
                // read rules
                var nums = line.Split('|').Select(int.Parse).ToList();
                rules.Add((nums[0], nums[1]));
            }
            else
            {
                // check updates
                var changed = false;
                var nums = line.Split(',').Select(int.Parse).ToList();
                for (var i = 0; i < rules.Count; i++)
                {
                    var sorted = true;
                    foreach (var rule in rules)
                    {
                        var beforeIndex = nums.IndexOf(rule.before);
                        var afterIndex = nums.IndexOf(rule.after);
                        if (afterIndex > -1 && beforeIndex > afterIndex)
                        {
                            // numbers are out of order => swap them
                            changed = true;
                            sorted = false;
                            (nums[beforeIndex], nums[afterIndex]) = (nums[afterIndex], nums[beforeIndex]);
                        }
                    }

                    if (sorted)
                        // numbers are sorted, we can stop trying to sort
                        break;
                }

                if (changed)
                {
                    // update was invalid but is now sorted
                    sum += nums[nums.Count / 2];
                }
            }
        }

        return sum;
    }
}