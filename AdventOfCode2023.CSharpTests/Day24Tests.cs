using AdventOfCode2023.CSharp.Day24;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyFourTests
{
    [Fact]
    public void CountIntersectionsInTestArea()
    {
        var filePath = @"Day24\DayTwentyFourTestInputA.txt";
        var sut = new DayTwentyFour();
        var result = sut.CountIntersectionsInTestArea(filePath, 7, 27);

        Assert.Equal(2, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartA();

        Assert.Equal("17244", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartB();

        Assert.Equal("1025019997186820", result);
    }
}