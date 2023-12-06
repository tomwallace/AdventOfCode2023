using AdventOfCode2023.CSharp.Day05;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFiveTests
{
    [Fact]
    public void SumPartNumbers()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath, false);
        var result = sut.TransformSeedThroughMaps(79);

        Assert.Equal(82, result);
    }

    [Fact]
    public void FindLowestSeedAfterTransforms()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath, false);
        var result = sut.FindLowestSeedAfterTransforms();

        Assert.Equal(35, result);
    }

    [Fact]
    public void FindLowestSeedAfterTransforms_SeedRanges()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath, true);
        var result = sut.FindLowestSeedAfterTransforms();

        Assert.Equal(46, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartA();

        Assert.Equal("165788812", result);
    }

    /*
    // TODO: getting out of memory exception - STILL
    // TODO: Need to look at how the ranges might work - as caching not working
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
    */
}