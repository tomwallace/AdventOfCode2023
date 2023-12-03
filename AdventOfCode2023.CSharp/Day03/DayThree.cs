namespace AdventOfCode2023.CSharp.Day03;

public class DayThree : IAdventProblemSet
{    
    /// <inheritdoc />
    public string Description()
    {
        return "Gear Ratios";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 3;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day03\DayThreeInput.txt";
        var sum = SumPartNumbers(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day03\DayThreeInput.txt";
        var sum = SumGearRatios(filePath);

        return sum.ToString();
    }

    internal int SumPartNumbers(string filePath) {
        var engineSchematic = new EngineSchematic(filePath);
        return engineSchematic.SumPartNumbers();
    }

    internal int SumGearRatios(string filePath) {
        var engineSchematic = new EngineSchematic(filePath);
        return engineSchematic.SumGearRatios();
    }
}
