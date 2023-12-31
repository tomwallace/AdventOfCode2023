using AdventOfCode2023.CSharp.Day10;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTenTests
{
    [Theory]
    [InlineData(@"Day10\DayTenTestInputA.txt", 4)]
    [InlineData(@"Day10\DayTenTestInputB.txt", 8)]
    public void StepsFarthestInPipeLoop(string line, int expected)
    {
        var sut = new PipeMaze(line);
        var result = sut.StepsFarthestInPipeLoop();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AreaInPipeLoopByTraversal_Narrow()
    {
        var sut = new PipeMaze(@"Day10\DayTenTestInputF.txt");
        var result = sut.AreaInPipeLoopByTraversal();

        Assert.Equal(4, result);
    }

    [Theory]
    [InlineData(@"Day10\DayTenTestInputC.txt", 4)]
    [InlineData(@"Day10\DayTenTestInputD.txt", 8)]
    [InlineData(@"Day10\DayTenTestInputE.txt", 10)]
    public void AreaInPipeLoopByTraversal(string line, int expected)
    {
        var sut = new PipeMaze(line);
        var result = sut.AreaInPipeLoopByTraversal();

        Assert.Equal(expected, result);
    }

    /*
    // Optimize as it takes 18 sec for PartA
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartA();

        Assert.Equal("6690", result);
    }
    */

    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartB();

        Assert.Equal("525", result);
    }
    */
}