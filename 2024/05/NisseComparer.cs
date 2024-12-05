namespace Five;

public class NisseComparer(List<(int before, int after)> rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (rules.Contains((x, y)))
        {
            return -1;
        }
        return 1;
    }
}
