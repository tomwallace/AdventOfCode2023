namespace AdventOfCode2023.CSharp.Day08;

public class DayEight : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Haunted Wasteland [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 8;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day08\DayEightInput.txt";
        var map = new Map(filePath);

        return map.StepsToTraverse("AAA").ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day08\DayEightInput.txt";
        var map = new Map(filePath);

        return map.GhostStepsToTraverse().ToString();
    }
}