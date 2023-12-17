using AdventOfCode2023.CSharp.Day17;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySeventeenTests
{
    [Fact]
    public void CalculateMinimumHeatLoss()
    {
        var filePath = @"Day17\DaySeventeenTestInputA.txt";
        var sut = new DaySeventeen();
        var result = sut.CalculateMinimumHeatLoss(filePath);

        Assert.Equal(102, result);
    }
    // 864 is too high
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartA();

        Assert.Equal("-1", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}