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
        var levels = ParseLevels(report);
        return LevelSequenceIsValid(levels);
    }

    private static List<int> ParseLevels(string report)
    {
        return report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
    }

    private static bool LevelSequenceIsValid(List<int> levels)
    {
        List<int> diffs = [.. levels.Zip(levels.Skip(1), (current, next) => next - current)];
        return diffs.All(d => d is >= 1 and <= 3) || diffs.All(d => d is <= -1 and >= -3);
    }

    public long SolvePart2(string[] input)
    {
        int validCount = 0;
        foreach (var report in input)
        {
            validCount = IsValidReportAllowingForSingleError(report) ? validCount + 1 : validCount;
        }
        return validCount;
    }

    private static bool IsValidReportAllowingForSingleError(string report)
    {
        var levels = ParseLevels(report);

        if (LevelSequenceIsValid(levels))
        {
            return true;
        }

        for (int i = 0; i < levels.Count; i++)
        {
            var modifiedLevels = new List<int>(levels);
            modifiedLevels.RemoveAt(i);
            if (LevelSequenceIsValid(modifiedLevels))
            {
                return true;
            }
        }

        return false;
    }
}
