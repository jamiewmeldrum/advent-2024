namespace AdventOfCode2024.Tests;

public class Day01Tests
{
    private static readonly string[] ExampleInput = File.ReadAllLines(Path.Combine("Inputs", "Day01", "sample.txt"));

    [Fact]
    public void Part1_ReturnsExpectedResult_ForExampleInput()
    {
        var day = new Day01();

        var result = day.SolvePart1(ExampleInput);

        Assert.Equal("expected", result);
    }

    [Fact]
    public void Part2_ReturnsExpectedResult_ForExampleInput()
    {
        var day = new Day01();

        var result = day.SolvePart2(ExampleInput);

        Assert.Equal("expected", result);
    }
}
