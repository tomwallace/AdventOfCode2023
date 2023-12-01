using AdventOfCode2023.CSharp.Day01;

namespace AdventOfCode2022.CSharp.Tests;

public class DayOneTests
{
    [Theory]
    [InlineData("1abc2",12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void ProcessLine(string input, int expected)
    {
        var sut = new DayOne();
        var result = sut.ProcessLine(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SumLinesOfFirstAndLastDigits()
    {
        string filePath = @"Day01\DayOneTestInputA.txt";

        var sut = new DayOne();
        var result = sut.SumLinesOfFirstAndLastDigits(filePath, false);

        Assert.Equal(142, result);
    }

    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("1234oneight", 18)]
    [InlineData("7one718onegfqtdbtxfcmd", 71)]
    public void ProcessLineAsWords(string input, int expected)
    {
        var sut = new DayOne();
        var result = sut.ProcessLineAsWords(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SumLinesOfFirstAndLastDigits_WithWords()
    {
        string filePath = @"Day01\DayOneTestInputB.txt";

        var sut = new DayOne();
        var result = sut.SumLinesOfFirstAndLastDigits(filePath, true);

        Assert.Equal(281, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartA();

        Assert.Equal("55108", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartB();

        Assert.Equal("56324", result);
    }
}