namespace AdventOfCode2024;

public sealed class Grid
{
    private readonly int Height;
    private readonly int Width;
    private readonly char[,] BackingArray;
    private readonly Dictionary<char, List<Coordinate>> CharacterLookupDict;

    public Grid(string[] input)
    {
        Height = input.Length;
        Width = input[0].ToCharArray().Length;

        char[,] grid = new char[Height, Width];
        Dictionary<char, List<Coordinate>> dict = [];

        for (int i = 0; i < Height; i++)
        {
            string row = input[i];
            List<char> chars = [.. row.ToCharArray()];

            for (int j = 0; j < Width; j++)
            {
                char element = chars[j];
                grid[i, j] = element;

                dict.TryGetValue(element, out List<Coordinate>? positions);
                positions ??= [];
                positions.Add(new Coordinate(j, Height - 1 - i));
                dict[element] = positions;
            }
        }

        BackingArray = grid;
        CharacterLookupDict = dict;
    }

    public List<Coordinate> GetCharacterCoordinates(char c)
    {
        return CharacterLookupDict.TryGetValue(c, out List<Coordinate>? positions) ? [.. positions] : [];
    }

    public char GetCharAt(Coordinate position)
    {
        (int row, int column) = ToArrayIndices(position);
        return BackingArray[row, column];
    }

    public void SetCharAt(Coordinate position, char character)
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

    public char GetCharAt(Coordinate position, char fallback) =>
        IsPositionOnGrid(position) ? GetCharAt(position) : fallback;

    public bool IsPositionOnGrid(Coordinate position) =>
        position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;

    private (int Row, int Column) ToArrayIndices(Coordinate position) => (Height - 1 - position.Y, position.X);
}
