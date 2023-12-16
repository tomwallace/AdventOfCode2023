using AdventOfCode2023.CSharp.Day15;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFifteenTests
{
    [Fact]
    public void Hash()
    {
        var sut = new DayFifteen();
        var result = sut.Hash('H', 0);

        Assert.Equal(200, result);
    }

    [Fact]
    public void SumHashString()
    {
        var sut = new DayFifteen();
        var result = sut.CalculateHashString("HASH");

        Assert.Equal(52, result);
    }

    [Fact]
    public void SumInitializationSequence()
    {
        var sut = new DayFifteen();
        var result = sut.SumInitializationSequence("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7");

        Assert.Equal(1320, result);
    }

    [Fact]
    public void FindFocusingPower()
    {
        var sut = new DayFifteen();
        var result = sut.FindFocusingPower("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7");

        Assert.Equal(145, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartA();

        Assert.Equal("510273", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartB();

        Assert.Equal("212449", result);
    }
}