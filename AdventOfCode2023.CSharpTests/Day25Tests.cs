using AdventOfCode2023.CSharp.Day25;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyFiveTests
{
    [Fact]
    public void FindProductOfGroupSizes()
    {
        var filePath = @"Day25\DayTwentyFiveTestInputA.txt";
        var sut = new DayTwentyFive();
        var result = sut.FindProductOfGroupSizes(filePath);

        Assert.Equal(54, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartA();

        Assert.Equal("567606", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartB();

        Assert.Equal("I have gotten all 50 stars!", result);
    }
}