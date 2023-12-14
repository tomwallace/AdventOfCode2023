using AdventOfCode2023.CSharp.Day13;

namespace AdventOfCode2022.CSharp.Tests;

public class DayThirteenTests
{
    [Fact]
    public void SummarizePatterns()
    {
        var filePath = @"Day13\DayThirteenTestInputA.txt";
        var sut = new DayThirteen();
        var result = sut.SummarizePatterns(filePath, false);

        Assert.Equal(405, result);
    }

    [Fact]
    public void SummarizePatterns_Smudges()
    {
        var filePath = @"Day13\DayThirteenTestInputA.txt";
        var sut = new DayThirteen();
        var result = sut.SummarizePatterns(filePath, true);

        Assert.Equal(400, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartA();

        Assert.Equal("27742", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartB();

        Assert.Equal("32728", result);
    }
}