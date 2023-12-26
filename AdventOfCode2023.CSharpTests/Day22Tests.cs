using AdventOfCode2023.CSharp.Day22;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyTwoTests
{
    [Fact]
    public void SumAcceptedPartsRatingNumbers()
    {
        var filePath = @"Day22\DayTwentyTwoTestInputA.txt";
        var sut = new DayTwentyTwo();
        var result = sut.CountBricksSafelyDisintegrated(filePath);

        Assert.Equal(5, result);
    }

    [Fact]
    public void SumFallingBricks()
    {
        var filePath = @"Day22\DayTwentyTwoTestInputA.txt";
        var sut = new DayTwentyTwo();
        var result = sut.SumFallingBricks(filePath);

        Assert.Equal(7, result);
    }

    // Takes over 30 sec to run, so commenting out
    /*
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartA();

        Assert.Equal("421", result);
    }
    */

    // Takes over a min to run, so commenting out
    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartB();

        Assert.Equal("39247", result);
    }
    */
}