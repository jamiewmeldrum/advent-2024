namespace AdventOfCode2024;

public class Grid
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
                positions.Add(new Coordinate(i, j));
                dict[element] = positions;
            }
        }

        BackingArray = grid;
        CharacterLookupDict = dict;
    }

    public int SearchForTextMatchesCount(string text)
    {
        int textLength = text.Length;
        char startChar = text[0];
        List<Coordinate> positions = CharacterLookupDict[startChar];

        List<string> permutations = [];
        foreach (Coordinate position in positions)
        {
            int x = position.X;
            int y = position.Y;

            string[] results = new string[8];
            for (int k = 0; k < textLength; k++)
            {
                results[0] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y);
                results[1] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y+k);
                results[2] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x, y+k);
                results[3] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y+k);
                results[4] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y);
                results[5] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x-k, y-k);
                results[6] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x, y-k);
                results[7] += GetCharAtPositionOrReturnEmptyIfOutOfRange(x+k, y-k);
            }
            permutations.AddRange(results);
        }

        return permutations.Count(x => x == text);
    }

    private char GetCharAtPositionOrReturnEmptyIfOutOfRange(int x, int y)
    {
        if (IsPositionInRange(x, y))
        {
            return BackingArray[x, y];
        }
        return ' ';
    }

    private bool IsPositionInRange(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false;
        }
        return true;
    }
}
