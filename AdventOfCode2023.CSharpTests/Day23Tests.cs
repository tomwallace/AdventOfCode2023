using AdventOfCode2023.CSharp.Day23;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyThreeTests
{
    [Fact]
    public void FindMostStepsOnHike()
    {
        var filePath = @"Day23\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.FindMostStepsOnHike(filePath, true);

        Assert.Equal(94, result);
    }

    [Fact]
    public void FindMostStepsOnHike_NoSlipperySlopes()
    {
        var filePath = @"Day23\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.FindMostStepsOnHike(filePath, false);

        Assert.Equal(154, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartA();

        Assert.Equal("2238", result);
    }

    // TODO: Ran overnight and still did not resolve.
    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
    */
}