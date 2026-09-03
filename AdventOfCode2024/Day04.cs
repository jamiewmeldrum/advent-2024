namespace AdventOfCode2024;

public class Day04 : IDay
{
    public long SolvePart1(string[] input)
    {
        Grid grid = new(input);
        return grid.SearchForTextMatchesCount("XMAS");
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
