namespace AdventOfCode2024;

public class Day02 : IDay
{
    public long SolvePart1(string[] input)
    {
        int validCount = 0;
        foreach (var report in input)
        {
            validCount = IsValidReport(report) ? validCount + 1 : validCount;
        }
        return validCount;
    }

    private static bool IsValidReport(string report)
    {
        var levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var diffs = levels.Zip(levels.Skip(1), (current, next) => next - current).ToList();

        return diffs.All(d => d is >= 1 and <= 3) || diffs.All(d => d is <= -1 and >= -3);
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
