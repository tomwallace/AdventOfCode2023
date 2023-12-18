namespace AdventOfCode2023.CSharp.Day10;

public class Point {
    public int X { get; set; }

    public int Y { get; set; }

    public char? Value { get; set; }

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Point(int x, int y, char value) {
        X = x;
        Y = y;
        Value = value;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}


