using System.Text;

namespace AdventOfCode2024;

public sealed class Grid<T> where T : IParsable<T>
{
    private readonly int _height;
    private readonly int _width;
    private readonly T[,] _backingArray;
    private readonly Dictionary<T, List<Coordinate>> _characterLookupDict;

    public Grid(string[] input)
    {
        _height = input.Length;
        _width = input[0].ToCharArray().Length;

        T[,] grid = new T[_height, _width];
        Dictionary<T, List<Coordinate>> dict = [];

        for (int i = 0; i < _height; i++)
        {
            string row = input[i];
            List<char> chars = [.. row.ToCharArray()];

            for (int j = 0; j < _width; j++)
            {
                T element = T.Parse(chars[j].ToString(), null);
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

    private Grid(int height, int width, T emptyValue)
    {
        _height = height;
        _width = width;

        T[,] grid = new T[height, width];
        List<Coordinate> emptyPositions = [];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[i, j] = emptyValue;
                emptyPositions.Add(new Coordinate(j, height - 1 - i));
            }
        }

        _backingArray = grid;
        _characterLookupDict = new() { [emptyValue] = emptyPositions };
    }

    public Grid<T> CreateEmptyCopy(T emptyValue) => new(_height, _width, emptyValue);

    public Grid<T> PopulateFromCoordinates(List<Coordinate> coordinates, T present)
    {
        foreach (Coordinate c in coordinates)
        {
            SetValueAt(c, present);
        }
        return this;
    }

    public HashSet<T> ListValues()
    {
         return [.. _characterLookupDict.Keys];
    }

    public List<Coordinate> GetCoordinatesOf(T value)
    {
        return _characterLookupDict.TryGetValue(value, out List<Coordinate>? positions) ? [.. positions] : [];
    }

    public T GetValueAt(Coordinate position)
    {
        (int row, int column) = ToArrayIndices(position);
        return _backingArray[row, column];
    }

    public void SetValueAt(Coordinate position, T value)
    {
        (int row, int column) = ToArrayIndices(position);
        T previousValue = _backingArray[row, column];
        List<Coordinate> previousPositions = _characterLookupDict[previousValue];
        previousPositions.Remove(position);
        if (previousPositions.Count == 0)
        {
            _characterLookupDict.Remove(previousValue);
        }

        _backingArray[row, column] = value;

        if (!_characterLookupDict.TryGetValue(value, out List<Coordinate>? positions))
        {
            positions = [];
            _characterLookupDict[value] = positions;
        }
        positions.Add(position);
    }

    public T GetValueAt(Coordinate position, T fallback) =>
        IsPositionOnGrid(position) ? GetValueAt(position) : fallback;

    public bool IsPositionOnGrid(Coordinate position) => IsPositionOnGrid(position.X, position.Y);
    public bool IsPositionOnGrid(int x, int y) => x >= 0 && x < _width && y >= 0 && y < _height;

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
