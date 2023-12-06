using System.Diagnostics;

namespace Utils;

public class Chronograph
{
    Stopwatch stopwatch = new Stopwatch();

    public Chronograph()
    {
        stopwatch.Reset();
    }

    public void Toggle()
    {
        if (stopwatch.IsRunning)
        {
            stopwatch.Stop();
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} (ms)");
            stopwatch.Reset();
        }
        else
        {
            stopwatch.Start();
        }
    }
}

