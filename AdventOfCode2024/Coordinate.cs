namespace AdventOfCode2024;

public readonly record struct Coordinate(int X, int Y)
{
    public bool IsNegative => X < 0 || Y < 0;

    public Coordinate Add(int x, int y)
    {
        return new Coordinate(X + x, Y + y);
    }

    public Coordinate Add(Coordinate other)
    {
        return new Coordinate(X + other.X, Y + other.Y);
    }

    public Coordinate CalculateVector(Coordinate other)
    {
        return new Coordinate(other.X - X, other.Y - Y);
    }

    private static readonly (int X, int Y)[] Offsets = [(1, 0), (-1, 0), (0, 1), (0, -1)];
    public static int CountOverlappingEdges(List<Coordinate> coordinates)
    {
        int overlaps = 0;
        foreach (Coordinate coordinate in coordinates) {
            overlaps += Offsets.Select(o => coordinate.Add(o.X, o.Y)).Count(coordinates.Contains);
        }
        return overlaps;
    }
}
