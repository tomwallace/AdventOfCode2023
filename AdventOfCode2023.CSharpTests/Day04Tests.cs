using AdventOfCode2023.CSharp.Day04;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFourTests
{
    [Theory]
    [InlineData("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    [InlineData("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2)]
    [InlineData("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1)]
    [InlineData("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0)]
    public void CalculatePoints(string line, int expected) {
        var sut = new Scratchcard(line);

        Assert.Equal(expected, sut.CalculatePoints());
    }
    
    [Fact]
    public void SumPoints()
    {
        var filePath = @"Day04\DayFourTestInputA.txt";
        var sut = new DayFour();
        var result = sut.SumPoints(filePath);

        Assert.Equal(13, result);
    }

    [Fact]
    public void RunScratchCardCopyRuleset()
    {
        var filePath = @"Day04\DayFourTestInputA.txt";
        var sut = new DayFour();
        var result = sut.RunScratchCardCopyRuleset(filePath);

        Assert.Equal(30, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartA();

        Assert.Equal("24706", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartB();

        Assert.Equal("13114317", result);
    }
}