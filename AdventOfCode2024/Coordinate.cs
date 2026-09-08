namespace AdventOfCode2024;

public readonly record struct Coordinate(int X, int Y)
{
    public bool IsNegative => X < 0 || Y < 0;

    public Coordinate Add(Coordinate other)
    {
        return new Coordinate(X + other.X, Y + other.Y);
    }

    public Coordinate CalculateVector(Coordinate other)
    {
        return new Coordinate(other.X - X, other.Y - Y);
    }
}
