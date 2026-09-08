using System.Text;

namespace AdventOfCode2024;

public sealed class Grid
{
    private readonly int _height;
    private readonly int _width;
    private readonly char[,] _backingArray;
    private readonly Dictionary<char, List<Coordinate>> _characterLookupDict;

    public Grid(string[] input)
    {
        _height = input.Length;
        _width = input[0].ToCharArray().Length;

        char[,] grid = new char[_height, _width];
        Dictionary<char, List<Coordinate>> dict = [];

        for (int i = 0; i < _height; i++)
        {
            string row = input[i];
            List<char> chars = [.. row.ToCharArray()];

            for (int j = 0; j < _width; j++)
            {
                char element = chars[j];
                grid[i, j] = element;

                dict.TryGetValue(element, out List<Coordinate>? positions);
                positions ??= [];
                positions.Add(new Coordinate(j, _height - 1 - i));
                dict[element] = positions;
            }
        }

        _backingArray = grid;
        _characterLookupDict = dict;
    }

    public HashSet<char> ListChars()
    {
         return [.. _characterLookupDict.Keys];
    }

    public List<Coordinate> GetCharacterCoordinates(char c)
    {
        return _characterLookupDict.TryGetValue(c, out List<Coordinate>? positions) ? [.. positions] : [];
    }

    public char GetCharAt(Coordinate position)
    {
        (int row, int column) = ToArrayIndices(position);
        return _backingArray[row, column];
    }

    public void SetCharAt(Coordinate position, char character)
    {
        (int row, int column) = ToArrayIndices(position);
        char previousCharacter = _backingArray[row, column];
        List<Coordinate> previousPositions = _characterLookupDict[previousCharacter];
        previousPositions.Remove(position);
        if (previousPositions.Count == 0)
        {
            _characterLookupDict.Remove(previousCharacter);
        }

        _backingArray[row, column] = character;

        if (!_characterLookupDict.TryGetValue(character, out List<Coordinate>? positions))
        {
            positions = [];
            _characterLookupDict[character] = positions;
        }
        positions.Add(position);
    }

    public char GetCharAt(Coordinate position, char fallback) =>
        IsPositionOnGrid(position) ? GetCharAt(position) : fallback;

    public bool IsPositionOnGrid(Coordinate position) =>
        position.X >= 0 && position.X < _width && position.Y >= 0 && position.Y < _height;

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                builder.Append(_backingArray[i, j]);
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }

    private (int Row, int Column) ToArrayIndices(Coordinate position) => (_height - 1 - position.Y, position.X);
}
