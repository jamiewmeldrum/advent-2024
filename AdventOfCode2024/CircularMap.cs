using System.Text;

namespace AdventOfCode2024;

public class CircularMap(int width, int height)
{
    private readonly Dictionary<Coordinate, List<MovingObject>> _tiles = CreateEmptyTiles(width, height);

    public void AddMovingObjects(IEnumerable<MovingObject> movingObjects)
    {
        foreach (MovingObject movingObject in movingObjects)
        {
            _tiles[movingObject.Position].Add(movingObject);
        }
    }

    public void Update()
    {
        List<MovingObject> allObjects = [.. _tiles.Values.SelectMany(objects => objects)];

        foreach (List<MovingObject> objects in _tiles.Values)
        {
            objects.Clear();
        }

        foreach (MovingObject movingObject in allObjects)
        {
            movingObject.Position = Wrap(movingObject.Position.Add(movingObject.Velocity));
            _tiles[movingObject.Position].Add(movingObject);
        }
    }

    public int CountInRange(int xStart, int xEnd, int yStart, int yEnd) =>
        _tiles
            .Where(tile => tile.Key.X >= xStart && tile.Key.X < xEnd && tile.Key.Y >= yStart && tile.Key.Y < yEnd)
            .Sum(tile => tile.Value.Count);

    public bool HasNoOverlaps() => _tiles.Values.All(objects => objects.Count <= 1);

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                builder.Append(_tiles[new Coordinate(x, y)].Count > 0 ? '#' : '.');
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }

    private Coordinate Wrap(Coordinate position)
    {
        int x = ((position.X % width) + width) % width;
        int y = ((position.Y % height) + height) % height;
        return new Coordinate(x, y);
    }

    private static Dictionary<Coordinate, List<MovingObject>> CreateEmptyTiles(int width, int height)
    {
        Dictionary<Coordinate, List<MovingObject>> tiles = [];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[new Coordinate(x, y)] = [];
            }
        }
        return tiles;
    }
}
