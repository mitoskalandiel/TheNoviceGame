using System.Diagnostics;

namespace TNG.Engine.Utils;

internal static class Time {
    private static float timeStarted = GetNanoseconds();

    private static long GetNanoseconds() {
        double timeStamp = Stopwatch.GetTimestamp();
        double nanoseconds = 1_000_000_000.0 * timeStamp / Stopwatch.Frequency;
        return (long)nanoseconds;
    }

    public static float GetTime() {
        return (float)((GetNanoseconds() - timeStarted) * 1E-9);
    }
}