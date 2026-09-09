namespace AdventOfCode2024;

public class Day08 : IDay
{
    public long SolvePart1(string[] input)
    {
        Grid<char> grid = new(input);

        foreach (var (_, coordinates) in GetAntennaGroups(grid))
        {
            foreach (Coordinate coordinate in coordinates)
            {
                foreach (Coordinate otherCoordinate in coordinates.Where(c => c != coordinate))
                {
                    Coordinate vector = coordinate.CalculateVector(otherCoordinate);
                    Coordinate nextPosition = otherCoordinate.Add(vector);
                    if (grid.IsPositionOnGrid(nextPosition))
                    {
                        grid.SetValueAt(nextPosition, '#');
                    }
                }
            }
        }

        Console.WriteLine(grid);
        return grid.GetCoordinatesOf('#').Count;
    }

    public long SolvePart2(string[] input)
    {
        Grid<char> grid = new(input);

        foreach (var (_, coordinates) in GetAntennaGroups(grid))
        {
            foreach (Coordinate coordinate in coordinates)
            {
                foreach (Coordinate otherCoordinate in coordinates.Where(c => c != coordinate))
                {
                    Coordinate vector = coordinate.CalculateVector(otherCoordinate);
                    Coordinate nextPosition = coordinate.Add(vector);
                    while (grid.IsPositionOnGrid(nextPosition))
                    {
                        grid.SetValueAt(nextPosition, '#');
                        nextPosition = nextPosition.Add(vector);
                    }
                }
            }
        }

        Console.WriteLine(grid);
        return grid.GetCoordinatesOf('#').Count;
    }

    private static List<(char Antenna, List<Coordinate> Coordinates)> GetAntennaGroups(Grid<char> grid) =>
        [.. grid.ListValues()
            .Where(c => c != '.')
            .Select(c => (c, grid.GetCoordinatesOf(c)))];
}
