using AdventOfCode2023.CSharp.Day19;

namespace AdventOfCode2022.CSharp.Tests;

public class DayNineteenTests
{
    [Fact]
    public void SumAcceptedPartsRatingNumbers()
    {
        var filePath = @"Day19\DayNineteenTestInputA.txt";
        var sut = new DayNineteen();
        var result = sut.SumAcceptedPartsRatingNumbers(filePath);

        Assert.Equal(19114, result);
    }

    [Fact]
    public void FindAcceptedRatingCombosRanges()
    {
        var filePath = @"Day19\DayNineteenTestInputA.txt";
        var sut = new DayNineteen();
        var result = sut.FindAcceptedRatingCombosRanges(filePath);

        Assert.Equal(167409079868000, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartA();

        Assert.Equal("421983", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartB();

        Assert.Equal("129249871135292", result);
    }
}