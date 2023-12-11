using AdventOfCode2023.CSharp.Day11;

namespace AdventOfCode2022.CSharp.Tests;

public class DayElevenTests
{
    [Theory]
    [InlineData(1, 374)]
    [InlineData(9, 1030)]
    [InlineData(99, 8410)]
    public void SumMinimumDistance(int expandSteps, long expected)
    {
        var filePath = @"Day11\DayElevenTestInputA.txt";
        var sut = new Universe(filePath, expandSteps);
        var result = sut.SumMinimumDistance();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartA();

        Assert.Equal("9370588", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartB();

        Assert.Equal("746207878188", result);
    }
}