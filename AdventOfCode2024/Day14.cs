namespace AdventOfCode2024;
using System.Text.RegularExpressions;

public class Day14 : IDay
{
    public int Width { get; set; } = 101;
    public int Height { get; set; } = 103;

    public long SolvePart1(string[] input)
    {
        List<MovingObject> movingObjects = ParseInput(input);

        CircularMap map = new(Width, Height);
        map.AddMovingObjects(movingObjects);

        for (int i = 0; i < 100; i++)
        {
            map.Update();
        }

        int midX = Width / 2;
        int midY = Height / 2;

        int topLeft = map.CountInRange(0, midX, 0, midY);
        int topRight = map.CountInRange(midX + 1, Width, 0, midY);
        int bottomLeft = map.CountInRange(0, midX, midY + 1, Height);
        int bottomRight = map.CountInRange(midX + 1, Width, midY + 1, Height);

        return (long)topLeft * topRight * bottomLeft * bottomRight;
    }

    public long SolvePart2(string[] input)
    {
        List<MovingObject> movingObjects = ParseInput(input);

        CircularMap map = new(Width, Height);
        map.AddMovingObjects(movingObjects);

        for (int second = 1; second <= 10000; second++)
        {
            map.Update();

            if (map.HasNoOverlaps())
            {
                return second;
            }
        }

        throw new InvalidOperationException("No overlap-free frame found within 10000 seconds.");
    }

    private static List<MovingObject> ParseInput(string[] input)
    {
        List<MovingObject> movingObjects = [];
        foreach (string line in input)
        {
            Match match = Regex.Match(line, @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)");
            Coordinate position = new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            Coordinate velocity = new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            movingObjects.Add(new MovingObject(position, velocity));
        }
        return movingObjects;
    }
}
