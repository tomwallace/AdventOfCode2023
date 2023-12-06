using AdventOfCode2023.CSharp.Day06;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySixTests
{
    [Theory]
    [InlineData(0, 7, 0)]
    [InlineData(1, 7, 6)]
    [InlineData(4, 7, 12)]
    public void DistanceCovered(long velocity, long time, long expected)
    {
        var sut = new Race(0, 0);
        var result = sut.DistanceCovered(time, velocity);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(7, 9, 4)]
    [InlineData(15, 40, 8)]
    [InlineData(30, 200, 9)]
    public void NumberOfWaysToBeat(long raceTime, long record, long expected)
    {
        var sut = new Race(raceTime, record);
        var result = sut.NumberOfWaysToBeat();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindProductOfWaysToBeat()
    {
        var races = new List<Race>()
        {
            new Race(7, 9),
            new Race(15, 40),
            new Race(30, 200)
        };
        var sut = new DaySix();
        var result = sut.FindProductOfWaysToBeat(races);

        Assert.Equal(288, result);
    }

    [Fact]
    public void FindProductOfWaysToBeat_PartB()
    {
        var races = new List<Race>()
        {
            new Race(71530, 940200)
        };
        var sut = new DaySix();
        var result = sut.FindProductOfWaysToBeat(races);

        Assert.Equal(71503, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartA();

        Assert.Equal("220320", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartB();

        Assert.Equal("34454850", result);
    }
}