﻿using AdventOfCode2023.CSharp.Day08;

namespace AdventOfCode2022.CSharp.Tests;

public class DayEightTests
{
    [Theory]
    [InlineData(@"Day08\DayEightTestInputA.txt", 2)]
    [InlineData(@"Day08\DayEightTestInputB.txt", 6)]
    public void StepsToTraverse(string filePath, int expected)
    {
        var sut = new Map(filePath);
        var result = sut.StepsToTraverse("AAA");

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GhostStepsToTraverse()
    {
        var filePath = @"Day08\DayEightTestInputC.txt";
        var sut = new Map(filePath);
        var result = sut.GhostStepsToTraverse();

        Assert.Equal(6, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartA();

        Assert.Equal("22411", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartB();

        Assert.Equal("11188774513823", result);
    }
}