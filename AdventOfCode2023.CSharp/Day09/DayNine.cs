using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day09;

public class DayNine : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Mirage Maintenance";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 9;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day09\DayNineInput.txt";
        var sum = SumExtrapolatedValues(filePath, false);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day09\DayNineInput.txt";
        var sum = SumExtrapolatedValues(filePath, true);

        return sum.ToString();
    }

    internal int SumExtrapolatedValues(string filePath, bool fromFront) 
    {
        var histories = FileUtility.ParseFileToList(filePath, line => new History(line));
        return histories.Sum(h => h.ExtrapolateNextValue(fromFront));
    }
}
