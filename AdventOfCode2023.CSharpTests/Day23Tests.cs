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
        var sut = new NewAttempt();
        var result = sut.PartTwo(filePath);

        Assert.Equal(154, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartA();

        Assert.Equal("2238", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartB();

        Assert.Equal("6398", result);
    }
}