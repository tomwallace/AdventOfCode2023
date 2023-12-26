namespace AdventOfCode2023.CSharp.Day22;

public class DayTwentyTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Sand Slabs [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 22;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day22\DayTwentyTwoInput.txt";
        var count = CountBricksSafelyDisintegrated(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day22\DayTwentyTwoInput.txt";
        var sum = SumFallingBricks(filePath);

        return sum.ToString();
    }

    internal int CountBricksSafelyDisintegrated(string filePath) {
        var column = new Column(filePath);
        column.SettleAndCountBricksNotSettled(true);

        var safe = column.Bricks.Count - column.CountSupportingBricks();
        return safe;
    }

    internal int SumFallingBricks(string filePath) {
        var column = new Column(filePath);
        column.SettleAndCountBricksNotSettled(true);

        var sum = column.SumFallingBricks();
        return sum;
    }    
}
