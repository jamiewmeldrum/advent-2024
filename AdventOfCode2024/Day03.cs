namespace AdventOfCode2024;
using System.Text.RegularExpressions;

public class Day03 : IDay
{
    public long SolvePart1(string[] input)
    {
        string flattenedInput = string.Join("", input);
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        MatchCollection matches = Regex.Matches(flattenedInput, pattern);
        return matches.Sum(x => long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value));
    }

    public long SolvePart2(string[] input)
    {
        string flattenedInput = string.Join("", input);
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";

        MatchCollection matches = Regex.Matches(flattenedInput, pattern);

        bool enabled = true;
        long sum = 0;
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                enabled = true;
            }
            else if (match.Value == "don't()")
            {
                enabled = false;
            }
            else if (enabled)
            {
                sum += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
            }
        }
        return sum;
    }
}
