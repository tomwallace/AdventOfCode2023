using AdventOfCode2023.CSharp.Day14;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFourteenTests
{
    [Fact]
    public void RotateNorthAndCalculateLoad()
    {
        var filePath = @"Day14\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.RotateNorthAndCalculateLoad(filePath);

        Assert.Equal(136, result);
    }

    [Fact]
    public void CycleAndCalculateLoad()
    {
        var filePath = @"Day14\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.CycleAndCalculateLoad(filePath, 1000000000);

        Assert.Equal(64, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartA();

        Assert.Equal("111979", result);
    }

    // 106504 too high
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}