using AdventOfCode2023.CSharp.OldDay01;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Tests;

public class OldDayOneTests
{
    [Fact]
    public void CalculateCalorieTotals()
    {
        string filePath = @"OldDay01\OldDayOneTestInputA.txt";
        List<string> data = FileUtility.ParseFileToList(filePath);

        var sut = new OldDayOne();
        var result = sut.CalculateCalorieTotals(data);

        Assert.Equal(5, result.Count);
        Assert.Equal(24000, result.Max());
        Assert.Equal(4000, result.Min());
    }

    [Fact]
    public void TotalCaloriesTopThreeElves()
    {
        string filePath = @"OldDay01\OldDayOneTestInputA.txt";
        List<string> data = FileUtility.ParseFileToList(filePath);

        var sut = new OldDayOne();
        var result = sut.TotalCaloriesTopThreeElves(data);

        Assert.Equal(45000, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new OldDayOne();
        var result = sut.PartA();

        Assert.Equal("71924", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new OldDayOne();
        var result = sut.PartB();

        Assert.Equal("210406", result);
    }
}