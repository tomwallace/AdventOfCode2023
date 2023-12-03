using AdventOfCode2023.CSharp.Day03;

namespace AdventOfCode2022.CSharp.Tests;

public class DayThreeTests
{
    [Fact]
    public void SumPartNumbers()
    {
        var filePath = @"Day03\DayThreeTestInputA.txt";
        var sut = new DayThree();
        var result = sut.SumPartNumbers(filePath);

        Assert.Equal(4361, result);
    }

    [Fact]
    public void SumGearRatios()
    {
        var filePath = @"Day03\DayThreeTestInputA.txt";
        var sut = new DayThree();
        var result = sut.SumGearRatios(filePath);

        Assert.Equal(467835, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartA();

        Assert.Equal("535078", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartB();

        Assert.Equal("75312571", result);
    }
}