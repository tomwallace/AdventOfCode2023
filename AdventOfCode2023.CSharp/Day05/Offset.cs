namespace AdventOfCode2023.CSharp.Day05;

public class Offset
{
    public long FromStart { get;}

    public long FromEnd { get; }
    
    public long Adjuster { get; }

    public long ToStart { get; }

    public long ToEnd { get; }

    public Offset(string line)
    {
        var entries = line.Split(' ');
        FromStart = long.Parse(entries[1]);
        Adjuster = long.Parse(entries[2]);
        FromEnd = FromStart + Adjuster - 1;
        ToStart = long.Parse(entries[0]);
        ToEnd = ToStart + Adjuster - 1;
    }

    public bool Contains(long input)
    {
        return input >= FromStart && input <= FromEnd;
    }

    public bool ContainsReverse(long input)
    {
        return input >= ToStart && input <= ToEnd;
    }

    public long Transform(long input)
    {
        var off = input - FromStart;
        if (input <= 0)
            throw new ArgumentException($"{input} is less than {FromStart}");

        return ToStart + off;
    }

    public long TransformReverse(long input)
    {
        var off = input - ToStart;
        if (input < 0)
            throw new ArgumentException($"{input} is less than {ToStart}");

        return FromStart + off;
    }
}
