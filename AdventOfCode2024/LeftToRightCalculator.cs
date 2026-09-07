namespace AdventOfCode2024;

public class LeftToRightCalculator(IReadOnlyList<Func<long, long, long>> operators)
{
    public bool CanReachTarget(long target, IReadOnlyList<long> inputs)
    {
        return CanReachTarget(target, inputs, 1, inputs[0]);
    }

    private bool CanReachTarget(long target, IReadOnlyList<long> inputs, int index, long runningValue)
    {
        if (runningValue > target)
        {
            return false;
        }

        if (index == inputs.Count)
        {
            return runningValue == target;
        }

        foreach (Func<long, long, long> operatorFunc in operators)
        {
            if (CanReachTarget(target, inputs, index + 1, operatorFunc(runningValue, inputs[index])))
            {
                return true;
            }
        }

        return false;
    }
}
