namespace AdventOfCode2024;

public record LinearEquation(double XCoeff, double YCoeff, double Ans)
{
    public LinearEquation Minus(LinearEquation other) =>
        new(XCoeff - other.XCoeff, YCoeff - other.YCoeff, Ans - other.Ans);
}
