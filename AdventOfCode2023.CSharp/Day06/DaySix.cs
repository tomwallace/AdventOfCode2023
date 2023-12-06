namespace AdventOfCode2023.CSharp.Day06;

public class DaySix : IAdventProblemSet
{
    //Time:        59     79     65     75
    //Distance:   597   1234   1032   1328
    private List<Race> input = new List<Race>()
    {
        new Race(59, 597),
        new Race(79, 1234),
        new Race(65, 1032),
        new Race(75, 1328)
    };

    private List<Race> input_PartB = new List<Race>()
    {
        new Race(59796575, 597123410321328)
    };
    
    /// <inheritdoc />
    public string Description()
    {
        return "Wait For It";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 6;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var product = FindProductOfWaysToBeat(input);

        return product.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var product = FindProductOfWaysToBeat(input_PartB);

        return product.ToString();
    }

    internal long FindProductOfWaysToBeat(List<Race> races)
    {
        return races.Select(r => r.NumberOfWaysToBeat()).Aggregate(1, (long x, long y) => x * y);
    }
}