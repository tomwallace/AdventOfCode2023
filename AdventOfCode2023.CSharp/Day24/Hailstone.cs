namespace AdventOfCode2023.CSharp.Day24;

public class Hailstone {
    public int Id { get; }
    
    public (double X, double Y, double Z) Position { get; }

    public (double X, double Y, double Z) Velocity { get; }

    // ex: 19, 13, 30 @ -2,  1, -2
    public Hailstone(string line, int id) {
        var split = line.Split('@');
        var start = split[0].Split(',', StringSplitOptions.TrimEntries);
        Position = (double.Parse(start[0]), double.Parse(start[1]), double.Parse(start[2]));
        var velocity = split[1].Split(',', StringSplitOptions.TrimEntries);
        Velocity = (double.Parse(velocity[0]), double.Parse(velocity[1]), double.Parse(velocity[2]));
        Id = id;
    }

    public (double X, double Y)? DetermineIntersectionPoint(Hailstone other) {
        // Had no idea how to calculate the intersection point, so looked up 
        var a1 = Velocity.Y / Velocity.X;
        var b1 = Position.Y - a1 * Position.X;
        var a2 = other.Velocity.Y / other.Velocity.X;
        var b2 = other.Position.Y - a2 * other.Position.X;

        if (EqualsWithinTolerance(a1, a2))
            return null;

        var cx = (b2 - b1) / (a1 - a2);
        var cy = cx * a1 + b1;

        // Will they be at that point
        var future = (cx > Position.X) == (Velocity.X > 0) && (cx > other.Position.X) == (other.Velocity.X > 0);
        if (!future)
            return null;

        return (cx, cy);
    }

    private bool EqualsWithinTolerance(double left, double right)
    {
        return Math.Abs(right - left) < .000_000_001f;
    }
}
