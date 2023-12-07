using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day07;

public class DaySeven : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Camel Cards";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 7;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day07\DaySevenInput.txt";
        var totalWinnings = CalculateTotalWinnings(filePath, false);

        return totalWinnings.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day07\DaySevenInput.txt";
        var totalWinnings = CalculateTotalWinnings(filePath, true);

        return totalWinnings.ToString();
    }

    internal int CalculateTotalWinnings(string filePath, bool useJokers)
    {
        var hands = FileUtility.ParseFileToList(filePath, line => new Hand(line, useJokers));
        var winnings = hands.OrderBy(h => h.Score()).Select((h, i) => (i + 1) * h.Bid).Sum();

        return winnings;
    }
    
}