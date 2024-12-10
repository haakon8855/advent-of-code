using System.Diagnostics;

namespace Utils;

public class Chronograph
{
    private readonly Stopwatch _stopwatch = new();

    public Chronograph()
    {
        _stopwatch.Reset();
    }

    public void Toggle()
    {
        if (_stopwatch.IsRunning)
        {
            _stopwatch.Stop();
            Console.WriteLine($"Time: {_stopwatch.ElapsedMilliseconds} (ms)");
            _stopwatch.Reset();
        }
        else
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }
    }
}