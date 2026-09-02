namespace AdventOfCode2024;

public class Day01 : IDay
{
    public long SolvePart1(string[] input)
    {
        var column1 = new List<int>();
        var column2 = new List<int>();

        foreach (var line in input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            column1.Add(int.Parse(parts[0]));
            column2.Add(int.Parse(parts[1]));
        }

        column1.Sort();
        column2.Sort();

        return column1.Zip(column2, (l, r) => (long)Math.Abs(l - r)).Sum();
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
