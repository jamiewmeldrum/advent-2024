namespace AdventOfCode2024.Tests;

public abstract class DayTestBase<TDay> where TDay : IDay, new()
{
    protected virtual string Day => typeof(TDay).Name;
    protected abstract long ExpectedPart1 { get; }
    protected abstract long ExpectedPart2 { get; }

    [Fact]
    public void Part1_ReturnsExpectedResult_ForExampleInput()
    {
        var exampleInput = SampleInputLoader.Load(Day, 1);
        var result = new TDay().SolvePart1(exampleInput);
        Assert.Equal(ExpectedPart1, result);
    }

    [Fact]
    public void Part2_ReturnsExpectedResult_ForExampleInput()
    {
        var exampleInput = SampleInputLoader.Load(Day, 2);
        var result = new TDay().SolvePart2(exampleInput);
        Assert.Equal(ExpectedPart2, result);
    }
}
