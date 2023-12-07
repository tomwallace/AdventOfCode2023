using AdventOfCode2023.CSharp.Day07;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySevenTests
{
    [Theory]
    [InlineData("32T3K 123", 20302100313)]
    [InlineData("AAAAA 123", 71414141414)]
    [InlineData("AA8AA 123", 61414081414)]
    [InlineData("23233 123", 50203020303)]
    [InlineData("TTT98 123", 41010100908)]
    [InlineData("23443 123", 30203040403)]
    [InlineData("23456 123", 10203040506)]
    public void HandScore(string cards, long expected)
    {
        var sut = new Hand(cards, false);
        var result = sut.Score();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateTotalWinnings()
    {
        var filePath = @"Day07\DaySevenTestInputA.txt";
        var sut = new DaySeven();
        var result = sut.CalculateTotalWinnings(filePath, false);

        Assert.Equal(6440, result);
    }

    [Theory]
    [InlineData("32T3K 123", 20302100313)]
    [InlineData("AAAAA 123", 71414141414)]
    [InlineData("AA8AA 123", 61414081414)]
    [InlineData("23233 123", 50203020303)]
    [InlineData("TTT98 123", 41010100908)]
    [InlineData("23443 123", 30203040403)]
    [InlineData("23456 123", 10203040506)]
    [InlineData("AAAAJ 123", 71414141401)]
    [InlineData("JJAAJ 123", 70101141401)]
    [InlineData("AA8JJ 123", 61414080101)]
    public void HandScoreWithJokers(string cards, long expected)
    {
        var sut = new Hand(cards, true);
        var result = sut.Score();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateTotalWinningsWithJokers()
    {
        var filePath = @"Day07\DaySevenTestInputA.txt";
        var sut = new DaySeven();
        var result = sut.CalculateTotalWinnings(filePath, true);

        Assert.Equal(5905, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartA();

        Assert.Equal("253866470", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartB();

        Assert.Equal("254494947", result);
    }
}