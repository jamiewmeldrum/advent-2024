namespace AdventOfCode2024.Tests;

public class Day14Tests : DayTestBase<Day14>
{
    protected override long ExpectedPart1 => 12;
    // AoC doesn't publish a "tree" example for the small sample - with only 12 robots in an
    // 11x7 grid, no-overlap is trivially true after the first update, so this is a regression
    // pin on that behaviour rather than a verified puzzle answer (unlike Part1's 12).
    protected override long ExpectedPart2 => 1;

    protected override Day14 CreateDay() => new() { Width = 11, Height = 7 };
}
