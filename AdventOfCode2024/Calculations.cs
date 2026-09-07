namespace AdventOfCode2024;

public static class Calculations
{
    public static long Add(long a, long b) => a + b;

    public static long Multiply(long a, long b) => a * b;

    public static long Concat(long a, long b) => long.Parse($"{a}{b}");
}
