namespace AdventOfCode2023.CSharp.Day10;

public class DayTen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Pipe Maze [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 10;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day10\DayTenInput.txt";
        var maze = new PipeMaze(filePath);
        var steps = maze.StepsFarthestInPipeLoop();

        return steps.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day10\DayTenInput.txt";
        var maze = new PipeMaze(filePath);
        var area = maze.AreaInPipeLoopByTraversal();

        return area.ToString();
    }
}