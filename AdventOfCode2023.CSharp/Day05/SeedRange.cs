namespace AdventOfCode2023.CSharp.Day05;

public class SeedRange
{
    public long Start { get;  }

    public long End { get; }

    public SeedRange(long start, long end) 
    { 
        Start = start; 
        End = end; 
    }

    public bool Contains(long input)
    {
        return Start <= input && input <= End;
    }
}
