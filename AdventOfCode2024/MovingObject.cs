namespace AdventOfCode2024;

public class MovingObject(Coordinate position, Coordinate velocity)
{
    public Coordinate Position { get; set; } = position;
    public Coordinate Velocity { get; } = velocity;
}
