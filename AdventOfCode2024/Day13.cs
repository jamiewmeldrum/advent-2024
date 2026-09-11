namespace AdventOfCode2024;
using System.Text.RegularExpressions;

public class Day13 : IDay
{
    public long SolvePart1(string[] input)
    {
        List<SimultaneousEquation> simultaneousEquations = [];
        foreach (string[] block in input.Chunk(4))
        {
            double[] line1Params = ExtractNumbers(block[0]);
            double[] line2Params = ExtractNumbers(block[1]);
            double[] equationAnswers = ExtractNumbers(block[2]);

            LinearEquation eq1 = new(line1Params[0], line2Params[0], equationAnswers[0]);
            LinearEquation eq2 = new(line1Params[1], line2Params[1], equationAnswers[1]);
            simultaneousEquations.Add(new SimultaneousEquation(eq1, eq2));
        }
        return CalculateTokensRequired(simultaneousEquations);
    }

    public long SolvePart2(string[] input)
    {
        long prizePostionModifier = 10000000000000;
        List<SimultaneousEquation> simultaneousEquations = [];
        foreach (string[] block in input.Chunk(4))
        {
            double[] line1Params = ExtractNumbers(block[0]);
            double[] line2Params = ExtractNumbers(block[1]);
            double[] equationAnswers = ExtractNumbers(block[2]);

            LinearEquation eq1 = new(line1Params[0], line2Params[0], equationAnswers[0] + prizePostionModifier);
            LinearEquation eq2 = new(line1Params[1], line2Params[1], equationAnswers[1] + prizePostionModifier);
            simultaneousEquations.Add(new SimultaneousEquation(eq1, eq2));
        }
        return CalculateTokensRequired(simultaneousEquations);
    }

    private static long CalculateTokensRequired(List<SimultaneousEquation> simultaneousEquations)
    {
        long tokens = 0;
        foreach (SimultaneousEquation simultaneousEquation in simultaneousEquations)
        {
            (double xVal, double yVal) = SimultaneousLinearEquationSolver.Solve(simultaneousEquation);
            if ((long)xVal != xVal || (long)yVal != yVal)
            {
                continue;
            }

            tokens += 3 * (long)xVal + (long)yVal;
        }

        return tokens;
    }


    private static double[] ExtractNumbers(string line) =>
        [.. Regex.Matches(line, @"\d+").Select(m => double.Parse(m.Value))];
}
