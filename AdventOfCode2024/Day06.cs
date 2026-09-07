namespace AdventOfCode2024;

public class Day06 : IDay
{
    public long SolvePart1(string[] input)
    {
        MazeGrid grid = new(input);
        while (grid.GridContainsCommandChar())
        {
            grid.UpdateMaze();
        }

        return grid.CountLocationsPassedThrough();
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
