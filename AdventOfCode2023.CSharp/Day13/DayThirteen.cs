using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day13;

public class DayThirteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Point of Incidence [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 13;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day13\DayThirteenInput.txt";
        var sum = SummarizePatterns(filePath, false);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day13\DayThirteenInput.txt";
        var sum = SummarizePatterns(filePath, true);

        return sum.ToString();
    }
    
    internal int SummarizePatterns(string filePath, bool useSmudges)
    {
        // Create the patterns
        var lines = FileUtility.ParseFileToList(filePath);
        var patterns = new List<Pattern>();
        var gridInput = new List<List<char>>();
        for (int i = 0; i < lines.Count; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                var pattern = new Pattern();
                pattern.Grid = gridInput;
                patterns.Add(pattern);
                gridInput = new List<List<char>>();
                continue;
            }

            gridInput.Add(lines[i].ToCharArray().ToList());
        }

        // Add the last one
        var lastPattern = new Pattern();
        lastPattern.Grid = gridInput;
        patterns.Add(lastPattern);

        var sum = patterns.Sum(p => p.GetSummarizationScore(useSmudges));
        return sum;
    }
}