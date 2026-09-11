namespace AdventOfCode2024;

public static class SimultaneousLinearEquationSolver
{
    public static (double xVal, double yVal) Solve(SimultaneousEquation simultaneousEquation)
    {
        var (eq1, eq2) = simultaneousEquation;

        LinearEquation scaledEq1 = ScaleByXCoeff(eq1, eq2.XCoeff);
        LinearEquation scaledEq2 = ScaleByXCoeff(eq2, eq1.XCoeff);

        LinearEquation eliminated = scaledEq2.Minus(scaledEq1);
        double yAns = eliminated.Ans / eliminated.YCoeff;
        double xAns = (scaledEq1.Ans - scaledEq1.YCoeff * yAns) / scaledEq1.XCoeff;

        return (xAns, yAns);
    }

    private static LinearEquation ScaleByXCoeff(LinearEquation eq, double factor) =>
        new(eq.XCoeff * factor, eq.YCoeff * factor, eq.Ans * factor);
}
