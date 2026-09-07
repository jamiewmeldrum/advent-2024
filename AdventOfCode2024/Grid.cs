namespace AdventOfCode2024;

public abstract class Grid
{
    protected readonly int Height;
    protected readonly int Width;
    protected readonly char[,] BackingArray;
    protected readonly Dictionary<char, List<Coordinate>> CharacterLookupDict;

    protected Grid(string[] input)
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

    protected char GetCharAtPositionOrReturnEmptyIfOutOfRange(int x, int y)
    {
        if (IsPositionInRange(x, y))
        {
            return BackingArray[Height - 1 - y, x];
        }
        return ' ';
    }

    protected bool IsPositionInRange(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false;
        }
        return true;
    }

    public bool IsPositionOnGrid(Coordinate position) => IsPositionInRange(position.X, position.Y);

    protected (int Row, int Column) ToArrayIndices(Coordinate position) => (Height - 1 - position.Y, position.X);
}
