namespace AdventOfCode2024;

public class Maze
{
    private const char Up = '^';
    private const char Right = '>';
    private const char Down = 'v';
    private const char Left = '<';
    private const char Wall = '#';

    private static readonly HashSet<char> CommandCharacters = [Up, Right, Down, Left];

    private readonly Grid grid;
    private readonly List<(char Command, Coordinate Position)> path;
    private readonly HashSet<(char Command, Coordinate Position)> visitedStates;
    private bool hasExited;
    private bool closedLoopConfirmed;

    public Maze(Grid grid)
    {
        this.grid = grid;

        (char Command, Coordinate Position) start = FindCommandState();
        path = [start];
        visitedStates = [start];
    }

    public bool GuardIsOnGrid => !hasExited;

    public bool ConfirmedClosedLoop => closedLoopConfirmed;

    public Coordinate StartPosition => path[0].Position;

    public void AddObstacleAt(Coordinate position)
    {
        grid.SetCharAt(position, Wall);
    }

    public List<Coordinate> GetVisitedPositions()
    {
        return [.. path.Select(state => state.Position).Distinct()];
    }

    public int CountLocationsPassedThrough()
    {
        return GetVisitedPositions().Count;
    }

    public void UpdateMaze()
    {
        if (hasExited)
        {
            closedLoopConfirmed = false;
            return;
        }

        (char command, Coordinate position) = path[^1];
        (int x, int y) = GetOffset(command);
        Coordinate nextPosition = new(position.X + x, position.Y + y);

        if (!grid.IsPositionOnGrid(nextPosition))
        {
            hasExited = true;
            closedLoopConfirmed = false;
            return;
        }

        if (grid.GetCharAt(nextPosition) == Wall)
        {
            (char Command, Coordinate Position) turnedState = (TurnClockwise(command), position);
            path.Add(turnedState);
            closedLoopConfirmed = !visitedStates.Add(turnedState);
            return;
        }

        (char Command, Coordinate Position) movedState = (command, nextPosition);
        path.Add(movedState);
        closedLoopConfirmed = !visitedStates.Add(movedState);
    }

    private (char Command, Coordinate Position) FindCommandState()
    {
        List<(char Command, Coordinate Position)> found =
        [
            .. CommandCharacters
                .SelectMany(command => grid.GetCharacterCoordinates(command).Select(position => (Command: command, Position: position)))
        ];

        if (found.Count != 1)
        {
            throw new InvalidOperationException($"Expected exactly one command character on the grid, found {found.Count}.");
        }

        return found[0];
    }

    private static (int X, int Y) GetOffset(char command) => command switch
    {
        Up => (0, 1),
        Right => (1, 0),
        Down => (0, -1),
        Left => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(command), command, "Unknown command character.")
    };

    private static char TurnClockwise(char command) => command switch
    {
        Up => Right,
        Right => Down,
        Down => Left,
        Left => Up,
        _ => throw new ArgumentOutOfRangeException(nameof(command), command, "Unknown command character.")
    };
}
