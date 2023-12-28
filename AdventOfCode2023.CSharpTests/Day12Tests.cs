using AdventOfCode2023.CSharp.Day12;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwelveTests
{
    [Theory]
    [InlineData("#.#.### 1,1,3", 1)]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 4)]
    public void FindValidCombinations(string line, long expected)
    {
        var sut = new Line(line);
        var result = sut.FindValidCombinations();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SumValidArrangements()
    {
        var filePath = @"Day12\DayTwelveTestInputA.txt";
        var sut = new DayTwelve();
        var result = sut.SumValidArrangements(filePath);

        Assert.Equal(21, result);
    }

    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 16384)]
    public void FindValidCombinations_Folded(string line, long expected)
    {
        var sut = new Line(line, true);
        var result = sut.FindValidCombinations();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SumValidArrangements_Folded()
    {
        var filePath = @"Day12\DayTwelveTestInputA.txt";
        var sut = new DayTwelve();
        var result = sut.SumValidArrangements(filePath, true);

        Assert.Equal(525152, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartA();

        Assert.Equal("7379", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartB();

        Assert.Equal("7732028747925", result);
    }
}