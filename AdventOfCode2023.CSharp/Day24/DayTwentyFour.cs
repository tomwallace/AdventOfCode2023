using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day24;

public class DayTwentyFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Never Tell Me The Odds [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 24;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day24\DayTwentyFourInput.txt";
        var count = CountIntersectionsInTestArea(filePath, 200000000000000, 400000000000000);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day24\DayTwentyFourInput.txt";
        var newHailSolver = new NewHailSolver();
        // Could not get Part B to work, so used another persons code.
        var result = newHailSolver.PartTwo(filePath);

        return result.ToString();
    }

    public int CountIntersectionsInTestArea(string filePath, long min, long max) {
        var hailstones = new List<Hailstone>();
        var lines = FileUtility.ParseFileToList(filePath);
        for (int i = 0; i < lines.Count; i++) {
            hailstones.Add(new Hailstone(lines[i], i));
        }

        var intersections = 0;
        for (int f = 0; f < hailstones.Count; f++) {
            for (int s = f + 1; s < hailstones.Count; s++) {
                 var colPoint = hailstones[f].DetermineIntersectionPoint(hailstones[s]);
                if (colPoint == null)
                    continue;

                if (colPoint.Value.X >= min && colPoint.Value.X <= max && colPoint.Value.Y >= min && colPoint.Value.Y <= max)
                    intersections++;
            }
        }

        return intersections;
    }
}