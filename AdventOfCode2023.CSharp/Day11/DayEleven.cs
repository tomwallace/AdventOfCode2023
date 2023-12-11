namespace AdventOfCode2023.CSharp.Day11;

public class DayEleven : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Cosmic Expansion";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 11;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day11\DayElevenInput.txt";
        var universe = new Universe(filePath, 1);
        var sum = universe.SumMinimumDistance();

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day11\DayElevenInput.txt";
        var universe = new Universe(filePath, 999999);
        var sum = universe.SumMinimumDistance();

        return sum.ToString();
    }
}
