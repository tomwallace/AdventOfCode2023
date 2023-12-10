using AdventOfCode2023.CSharp.Day09;

namespace AdventOfCode2022.CSharp.Tests;

public class DayNineTests
{
    [Theory]
    [InlineData("0 3 6 9 12 15", 18)]
    [InlineData("10 13 16 21 30 45", 68)]
    public void ExtrapolateNextValue(string line, int expected)
    {
        var sut = new History(line);
        var result = sut.ExtrapolateNextValue(false);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SumExtrapolatedValues()
    {
        var filePath = @"Day09\DayNineTestInputA.txt";
        var sut = new DayNine();
        var result = sut.SumExtrapolatedValues(filePath, false);

        Assert.Equal(114, result);
    }

    [Fact]
    public void SumExtrapolatedValues_FromFront()
    {
        var sut = new History("10 13 16 21 30 45");
        var result = sut.ExtrapolateNextValue(true);

        Assert.Equal(5, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartA();

        Assert.Equal("1731106378", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartB();

        Assert.Equal("1087", result);
    }
}