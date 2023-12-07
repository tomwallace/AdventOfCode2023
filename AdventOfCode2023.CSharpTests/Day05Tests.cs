using AdventOfCode2023.CSharp.Day05;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFiveTests
{
    [Fact]
    public void SumPartNumbers()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath);
        var result = sut.TransformSeedThroughMaps(79);

        Assert.Equal(82, result);
    }

    [Fact]
    public void FindLowestSeedAfterTransforms()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath);
        var result = sut.FindLowestSeedAfterTransforms();

        Assert.Equal(35, result);
    }

    [Fact]
    public void FindLowestLocationAfterTransforms()
    {
        var filePath = @"Day05\DayFiveTestInputA.txt";
        var sut = new FarmingPlan(filePath);
        var result = sut.FindLowestLocationAfterTransforms();

        Assert.Equal(46, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartA();

        Assert.Equal("165788812", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartB();

        Assert.Equal("1928058", result);
    }
}