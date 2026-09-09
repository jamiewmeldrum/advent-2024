namespace AdventOfCode2024;

public class Day04 : IDay
{
    public long SolvePart1(string[] input)
    {
        WordSearch wordSearch = new(new Grid<char>(input));
        return wordSearch.SearchForTextMatchesCount("XMAS");
    }

    public long SolvePart2(string[] input)
    {
        WordSearch wordSearch = new(new Grid<char>(input));
        return wordSearch.SearchForCrossTextMatchesCount("MAS");
    }
}
