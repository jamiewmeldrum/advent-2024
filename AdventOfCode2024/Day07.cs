namespace AdventOfCode2024;

public class Day07 : IDay
{
    public long SolvePart1(string[] input) => Solve(input, [Calculations.Add, Calculations.Multiply]);

    public long SolvePart2(string[] input) => Solve(input, [Calculations.Add, Calculations.Multiply, Calculations.Concat]);

    private static long Solve(string[] input, List<Func<long, long, long>> operators)
    {
        LeftToRightCalculator calculator = new(operators);

        long total = 0;
        Parallel.ForEach(input, line =>
        {
            (long target, List<long> inputs) = Parse(line);
            if (calculator.CanReachTarget(target, inputs))
            {
                Interlocked.Add(ref total, target);
            }
        });

        return total;
    }

    private static (long Target, List<long> Inputs) Parse(string line)
    {
        string[] parts = line.Split(':');
        long target = long.Parse(parts[0]);
        List<long> inputs = [.. parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)];
        return (target, inputs);
    }
}
