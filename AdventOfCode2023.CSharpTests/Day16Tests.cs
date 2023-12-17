using AdventOfCode2023.CSharp.Day16;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySixteenTests
{
    [Fact]
    public void CountEnergizedCells()
    {
        var filePath = @"Day16\DaySixteenTestInputA.txt";
        var sut = new DaySixteen();
        var result = sut.CountEnergizedCells(filePath);

        Assert.Equal(46, result);
    }

    [Fact]
    public void BestBeamLocationCountEnergizedCells()
    {
        var filePath = @"Day16\DaySixteenTestInputA.txt";
        var sut = new DaySixteen();
        var result = sut.BestBeamLocationCountEnergizedCells(filePath);

        Assert.Equal(51, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartA();

        Assert.Equal("7210", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartB();

        Assert.Equal("7673", result);
    }
}