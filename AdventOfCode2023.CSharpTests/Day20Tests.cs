using AdventOfCode2023.CSharp.Day20;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyTests
{
    [Fact]
    public void CalculateTotalPulses()
    {
        var filePath = @"Day20\DayTwentyTestInputA.txt";
        var sut = new DayTwenty();
        var result = sut.CalculateTotalPulses(filePath);

        Assert.Equal(32000000, result);
    }

    [Fact]
    public void CalculateTotalPulses_B()
    {
        var filePath = @"Day20\DayTwentyTestInputB.txt";
        var sut = new DayTwenty();
        var result = sut.CalculateTotalPulses(filePath);

        Assert.Equal(11687500, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartA();

        Assert.Equal("886347020", result);
    }

    // TODO: Runs much too long.  Must be a short cut
    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
    */
}