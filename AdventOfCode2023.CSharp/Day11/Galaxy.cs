namespace AdventOfCode2023.CSharp.Day11;

public class Galaxy {
    public int Id { get; }
    
    public int X { get; }

    public int Y { get; }

    public Galaxy(int id, int x, int y) {
        Id = id;
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{Id},{X},{Y}";
    }
}
