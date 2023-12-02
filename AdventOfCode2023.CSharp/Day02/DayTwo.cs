using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day02;

public class DayTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Cube Conundrum";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 2;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day02\DayTwoInput.txt";
        var sumIds = SumIdsPossibleGames(filePath);

        return sumIds.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day02\DayTwoInput.txt";
        var sumPower = SumPowerOfLeastCubes(filePath);

        return sumPower.ToString();
    }

    
    internal int SumIdsPossibleGames(string filePath) {
        var games = FileUtility.ParseFileToList<Game>(filePath, line => new Game(line));
        var possibleGames = games.Where(g => g.IsGamePossible(14, 13, 12));
        var sumIds = possibleGames.Sum(g => g.Id);
        return sumIds;
    }

    internal int SumPowerOfLeastCubes(string filePath) {
        var games = FileUtility.ParseFileToList<Game>(filePath, line => new Game(line));
        var sumPower = games.Sum(g => g.PowerOfLeastCubes());
        return sumPower;
    }
    
}
