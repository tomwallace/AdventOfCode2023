using AdventOfCode2023.CSharp.Day21;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyOneTests
{
    [Fact]
    public void SumAcceptedPartsRatingNumbers()
    {
        var filePath = @"Day21\DayTwentyOneTestInputA.txt";
        var sut = new DayTwentyOne();
        var result = sut.CountVisitedPlots(filePath, 6, false);

        Assert.Equal(16, result);
    }

    [Theory]
    [InlineData(6, 16)]
    [InlineData(10, 50)]
    [InlineData(50, 1594)]
    public void SumAcceptedPartsRatingNumbers_Infinite(int steps, int expected)
    {
        var filePath = @"Day21\DayTwentyOneTestInputA.txt";
        var sut = new DayTwentyOne();
        var result = sut.CountVisitedPlots(filePath, steps, true);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartA();

        Assert.Equal("3751", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}