namespace AdventOfCode2024;

public class MazeGrid(string[] input) : Grid(input)
{
    private const char Up = '^';
    private const char Right = '>';
    private const char Down = 'v';
    private const char Left = '<';
    private const char Wall = '#';
    private const char PassedThrough = 'x';

    private static readonly HashSet<char> CommandCharacters = [Up, Right, Down, Left];

    public bool GridContainsCommandChar()
    {
        return CommandCharacters.Any(CharacterLookupDict.ContainsKey);
    }

    public int CountLocationsPassedThrough()
    {
        return CharacterLookupDict[PassedThrough].Count;
    }

    public void UpdateMaze()
    {
        List<(char Command, Coordinate Position)> found =
        [
            .. CommandCharacters
                .Where(CharacterLookupDict.ContainsKey)
                .SelectMany(command => CharacterLookupDict[command].Select(position => (Command: command, Position: position)))
        ];

        if (found.Count > 1)
        {
            throw new InvalidOperationException($"Expected exactly one command character on the grid, found {found.Count}.");
        }
        else if (found.Count == 0)
        {
            return;
        }

        (char command, Coordinate position) = found[0];
        (int x, int y) = GetOffset(command);
        Coordinate nextPosition = new(position.X + x, position.Y + y);

        if (!IsPositionOnGrid(nextPosition))
        {
            SetCharacterAt(position, PassedThrough);
            return;
        }

        (int row, int column) = ToArrayIndices(nextPosition);
        if (BackingArray[row, column] == Wall)
        {
            SetCharacterAt(position, TurnClockwise(command));
            return;
        }

        SetCharacterAt(position, PassedThrough);
        SetCharacterAt(nextPosition, command);
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

    private void SetCharacterAt(Coordinate position, char character)
    {
        (int row, int column) = ToArrayIndices(position);
        char previousCharacter = BackingArray[row, column];
        List<Coordinate> previousPositions = CharacterLookupDict[previousCharacter];
        previousPositions.Remove(position);
        if (previousPositions.Count == 0)
        {
            CharacterLookupDict.Remove(previousCharacter);
        }

        BackingArray[row, column] = character;

        if (!CharacterLookupDict.TryGetValue(character, out List<Coordinate>? positions))
        {
            positions = [];
            CharacterLookupDict[character] = positions;
        }
        positions.Add(position);
    }
}
