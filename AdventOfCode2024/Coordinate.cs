namespace AdventOfCode2024;

public readonly record struct Coordinate(int X, int Y)
{
    public bool IsNegative => X < 0 || Y < 0;
}
