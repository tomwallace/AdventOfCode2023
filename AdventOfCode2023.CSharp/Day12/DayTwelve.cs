using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day12;

public class DayTwelve : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Hot Springs [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 12;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day12\DayTwelveInput.txt";
        var sum = SumValidArrangements(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day12\DayTwelveInput.txt";
        var sum = SumValidArrangements(filePath, true);

        return sum.ToString();
    }

    internal long SumValidArrangements(string filePath, bool isFolded = false)
    {
        var lines = FileUtility.ParseFileToList<Line>(filePath, line => new Line(line, isFolded));
        return lines.Sum(l => l.FindValidCombinations());
    }
}