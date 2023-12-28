namespace AdventOfCode2023.CSharp.Day14;

public class Rock
{
    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
