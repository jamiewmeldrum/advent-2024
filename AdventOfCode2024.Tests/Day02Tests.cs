namespace AdventOfCode2024.Tests;

public class Day02Tests
{
    private static readonly string[] ExampleInput = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Inputs", "Day02", "sample.txt"));

    [Fact]
    public void Part1_ReturnsExpectedResult_ForExampleInput()
    {
        var day = new Day02();

        var result = day.SolvePart1(ExampleInput);

        Assert.Equal(2L, result);
    }

    [Fact]
    public void Part2_ReturnsExpectedResult_ForExampleInput()
    {
        var day = new Day02();

        var result = day.SolvePart2(ExampleInput);

        Assert.Equal(0L, result); // TODO: replace with the real expected value once you know it
    }
}
