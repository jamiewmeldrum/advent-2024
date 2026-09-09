namespace AdventOfCode2024;

public class Day10 : IDay
{
    public long SolvePart1(string[] input)
    {
        Grid<int> grid = new(input);
        
        List<Coordinate> trailHeads = grid.GetCoordinatesOf(0);

        return trailHeads
        .SelectMany(t => TracePath(grid, t))
        .Where(trail => grid.GetValueAt(trail.Last()) == 9)
        .GroupBy(trail => (Start: trail.First(), End: trail.Last()))
        .Count();
    }

    public long SolvePart2(string[] input)
    {
        Grid<int> grid = new(input);
        
        List<Coordinate> trailHeads = grid.GetCoordinatesOf(0);

        return trailHeads
        .SelectMany(t => TracePath(grid, t))
        .Count(trail => grid.GetValueAt(trail.Last()) == 9);
    }

    private static List<List<Coordinate>> TracePath(Grid<int> grid, Coordinate coordinate)
    {
        List<Coordinate> adjacentPostions = AdjacentPositions(grid, coordinate);
        int currentVal = grid.GetValueAt(coordinate);

        List<Coordinate> nextCoordinates = [.. adjacentPostions.Where(c => grid.GetValueAt(c) == currentVal + 1)];

        if (nextCoordinates.Count == 0)
        {
            return [[coordinate]];
        }

        List<List<Coordinate>> trails = [];
        foreach (Coordinate next in nextCoordinates)
        {
            foreach (List<Coordinate> subPath in TracePath(grid, next))
            {
                trails.Add([coordinate, .. subPath]);
            }
        }

        return trails;
    }

    private static readonly (int X, int Y)[] Offsets = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    private static List<Coordinate> AdjacentPositions(Grid<int> grid, Coordinate coordinate) =>
        [.. Offsets
            .Select(o => coordinate.Add(o.X, o.Y))
            .Where(grid.IsPositionOnGrid)];
}
