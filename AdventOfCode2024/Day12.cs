namespace AdventOfCode2024;

public class Day12 : IDay
{
    public long SolvePart1(string[] input)
    {
        Grid<char> grid = new(input);
        List<(char value, List<(int x, int y)>)> blocks = grid.GroupContiguousBlocks();

        int total = 0;
        foreach (var block in blocks)
        {
            var positions = block.Item2;
            int area = positions.Count;
            int overlaps = Coordinate.CountOverlappingEdges([.. positions.Select(p => new Coordinate(p.x, p.y))]);
            int perimeter = 4 * area - overlaps;

            int blockCost = area * perimeter;
            total += area * perimeter;
        }
        return total;
    }

    public long SolvePart2(string[] input)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}
