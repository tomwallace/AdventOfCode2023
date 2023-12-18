using AdventOfCode2023.CSharp.Day18;

namespace AdventOfCode2022.CSharp.Tests;

public class DayEighteenTests
{
    [Fact]
    public void CalculateLagoonArea()
    {
        var filePath = @"Day18\DayEighteenTestInputA.txt";
        var sut = new DayEighteen();
        var result = sut.CalculateLagoonArea(filePath);

        Assert.Equal(62, result);
    }

    [Fact]
    public void TestBigPoint() {
        var input = "R 6 (#70c710)";
        var sut = new BigPoint(input, (0,0));

        Assert.Equal(461937, sut.Length);
        Assert.Equal(461937, sut.X);
        Assert.Equal(0, sut.Y);
    }

    [Fact]
    public void CalculateLagoonAreaWithColors()
    {
        var filePath = @"Day18\DayEighteenTestInputA.txt";
        var sut = new DayEighteen();
        var result = sut.CalculateLagoonAreaWithColors(filePath);

        Assert.Equal(952408144115, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartA();

        Assert.Equal("36679", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartB();

        Assert.Equal("88007104020978", result);
    }
}