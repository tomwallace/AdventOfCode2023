namespace AdventOfCode2023.CSharp.Day05;

public class DayFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "If You Give A Seed A Fertilizer";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 5;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day05\DayFiveInput.txt";
        var farmingPlan = new FarmingPlan(filePath);

        return farmingPlan.FindLowestSeedAfterTransforms().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day05\DayFiveInput.txt";
        var farmingPlan = new FarmingPlan(filePath);

        return farmingPlan.FindLowestLocationAfterTransforms().ToString();
    }
}