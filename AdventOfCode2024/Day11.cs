namespace AdventOfCode2024;

public class Day11 : IDay
{
    public long SolvePart1(string[] input)
    {
        return new StoneCounter(ParseStones(input), 25).Count();
    }

    public long SolvePart2(string[] input)
    {
        return new StoneCounter(ParseStones(input), 75).Count();
    }

    private static List<long> ParseStones(string[] input) =>
        [.. input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)];
}
