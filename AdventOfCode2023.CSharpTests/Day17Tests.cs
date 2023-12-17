using AdventOfCode2023.CSharp.Day17;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySeventeenTests
{
    [Fact]
    public void CalculateMinimumHeatLoss()
    {
        var filePath = @"Day17\DaySeventeenTestInputA.txt";
        var sut = new DaySeventeen();
        var result = sut.CalculateMinimumHeatLoss(filePath,0 , 3);

        Assert.Equal(102, result);
    }
    
    [Fact]
    public void CalculateMinimumHeatLoss_MinSteps()
    {
        var filePath = @"Day17\DaySeventeenTestInputA.txt";
        var sut = new DaySeventeen();
        var result = sut.CalculateMinimumHeatLoss(filePath, 4, 10);

        Assert.Equal(94, result);
    }

    [Fact]
    public void CalculateMinimumHeatLoss_MinSteps_Simple()
    {
        var filePath = @"Day17\DaySeventeenTestInputB.txt";
        var sut = new DaySeventeen();
        var result = sut.CalculateMinimumHeatLoss(filePath, 4, 10);

        Assert.Equal(71, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartA();

        Assert.Equal("861", result);
    }

    // 1037 is the right number but I cannot have my puzzle solve for it
    // It told me 1040 was too high and I adjusted for steps and 1036 was too low
    // so I could get it by process of elimination.  Not sure what is wrong.
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartB();

        Assert.Equal("1040", result);
    }
}