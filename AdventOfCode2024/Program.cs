namespace AdventOfCode2024;

public class Program
{
    public static void Main(string[] args)
    {
        ParsedArgs? parsedArgs = ParseArgs(args);
        if (parsedArgs is null)
        {
            Console.WriteLine("Usage: dotnet run -- <day> [part]");
            Console.WriteLine("  part is 1 or 2; if omitted, both parts run");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  dotnet run -- 1      (runs day 1, both parts)");
            Console.WriteLine("  dotnet run -- 1 2     (runs day 1, part 2 only)");
            return;
        }
        
        int day = parsedArgs.Day;
        IDay solver = parsedArgs.Day switch
        {
            1 => new Day01(),
            2 => new Day02(),
            3 => new Day03(),
            4 => new Day04(),
            _ => throw new ArgumentException($"No solver registered for day {day}")
        };

        var inputPath = Path.Combine(AppContext.BaseDirectory, "Inputs", $"Day{day:D2}", "input.txt");
        var input = File.ReadAllLines(inputPath);

        int? part = parsedArgs.Part;
        if (part is null or 1)
        {
            Console.WriteLine($"Day {day} Part 1: {solver.SolvePart1(input)}");
        }

        if (part is null or 2)
        {
            Console.WriteLine($"Day {day} Part 2: {solver.SolvePart2(input)}");
        }
    }

    private static ParsedArgs? ParseArgs(string[] args)
    {
        if (args.Length is < 1 or > 2)
        {
            return null;
        }

        if (!int.TryParse(args[0], out int day))
        {
            return null;
        }

        int? part = null;
        if (args.Length == 2)
        {
            if (!int.TryParse(args[1], out var parsedPart) || parsedPart is not (1 or 2))
            {
                return null;
            }
            part = parsedPart;
        }

        return new ParsedArgs { Day = day, Part = part };
    }

    private record ParsedArgs
    {
        public int Day { get; init; }
        public int? Part { get; init; }
    }
}
