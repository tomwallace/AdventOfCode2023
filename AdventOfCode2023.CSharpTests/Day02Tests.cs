using AdventOfCode2023.CSharp.Day02;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwoTests
{
    [Fact]
    public void SumIdsPossibleGames()
    {
        var filePath = @"Day02\DayTwoTestInputA.txt";
        var sut = new DayTwo();
        var result = sut.SumIdsPossibleGames(filePath);

        Assert.Equal(8, result);
    }

    [Fact]
    public void SumPowerOfLeastCubes()
    {
        var filePath = @"Day02\DayTwoTestInputA.txt";
        var sut = new DayTwo();
        var result = sut.SumPowerOfLeastCubes(filePath);

        Assert.Equal(2286, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartA();

        Assert.Equal("3059", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartB();

        Assert.Equal("65371", result);
    }
}