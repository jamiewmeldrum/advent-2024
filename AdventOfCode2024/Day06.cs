namespace AdventOfCode2024;

public class Day06 : IDay
{
    public long SolvePart1(string[] input)
    {
        Maze maze = new(new Grid<char>(input));
        do
        {
            maze.UpdateMaze();
        }
        while (maze.GuardIsOnGrid);

        return maze.CountLocationsPassedThrough();
    }

    public long SolvePart2(string[] input)
    {
        Maze baseMaze = new(new Grid<char>(input));
        do
        {
            baseMaze.UpdateMaze();
        }
        while (baseMaze.GuardIsOnGrid);

        List<Coordinate> candidates =
        [
            .. baseMaze.GetVisitedPositions().Where(position => position != baseMaze.StartPosition)
        ];

        int closedLoops = 0;
        Parallel.ForEach(candidates, emptySpace =>
        {
            Maze maze = new(new Grid<char>(input));
            maze.AddObstacleAt(emptySpace);
            do
            {
                maze.UpdateMaze();
            }
            while (maze.GuardIsOnGrid && !maze.ConfirmedClosedLoop);

            if (maze.ConfirmedClosedLoop)
            {
                Interlocked.Increment(ref closedLoops);
            }
        });
        return closedLoops;
    }
}
