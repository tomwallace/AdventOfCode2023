using AdventOfCode2023.CSharp.Utility;

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
        var area = maze.AreaInPipeLoop();

        return area.ToString();
    }

    

    
}

public class PipeMaze {

    public List<List<char>> Grid { get; }

    public Point Start { get; }

    public List<Point> PipeLoop { get; set; } 

    public PipeMaze(string filePath) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        for(int y = 0; y < Grid.Count; y++) {
            for(int x = 0; x < Grid[0].Count; x++) {
                if (Grid[y][x] == 'S')
                    Start = new Point(x, y);
            }
        }
    }

    public int StepsFarthestInPipeLoop() {
        // Iterate over each potential Dir as Start, and see if it makes a loop
        foreach(var dir in LoopHelper.Dirs.Keys) {
            var loop = new Loop(Grid, Start, dir);
            if (loop.IsValid)
                return loop.Points.Count / 2;
        }

        // At least one loop should be valid
        throw new Exception("None of the loops were valid");
    }

    public int AreaInPipeLoop() {
        // Iterate over each potential Dir as Start, and see if it makes a loop
        foreach(var dir in LoopHelper.Dirs.Keys) {
            var loop = new Loop(Grid, Start, dir);
            if (loop.IsValid)
                return loop.CalculateArea();
        }

        // At least one loop should be valid
        throw new Exception("None of the loops were valid");
    }

}

public static class LoopHelper {
    public static Dictionary<char, List<Point>> Dirs = new Dictionary<char, List<Point>>() {
        { '|', new List<Point>() { new Point(0,-1), new Point(0,1) } },
        { '-', new List<Point>() { new Point(1,0), new Point(-1,0) } },
        { 'L', new List<Point>() { new Point(0,-1), new Point(1,0) } },
        { 'J', new List<Point>() { new Point(0,-1), new Point(-1,0) } },
        { '7', new List<Point>() { new Point(0,1), new Point(-1,0) } },
        { 'F', new List<Point>() { new Point(0,1), new Point(1,0) } }
    };
}

public class Loop {
    public bool IsValid { get; }

    public List<Point> Points { get; }

    public Point Start { get; }

    public Loop(List<List<char>> grid, Point start, char startOverride) {
        IsValid = false;
        Points = new List<Point>();
        start.Value = startOverride;
        Points.Add(start);
        Start = start;

        // Replace Start character for this loop
        grid[Start.Y][Start.X] = startOverride;
        var current = start;
        // Set starting last Point
        var lastMod = LoopHelper.Dirs[startOverride].First();
        var last = new Point (Start.X + lastMod.X, Start.Y + lastMod.Y);

        // Start following
        do {
            var xMod = last.X - current.X;
            var yMod = last.Y - current.Y;
            var dir = grid[current.Y][current.X];
            var nextMod = LoopHelper.Dirs[dir].First(d => d.ToString() != $"{xMod},{yMod}");
            // Check it out of bounds
            var next = new Point(current.X + nextMod.X, current.Y + nextMod.Y);
            next.Value = grid[next.Y][next.X];
            if (next.X < 0 || next.X >= grid[0].Count || next.Y < 0 || next.Y >= grid.Count)
                return;

            // Check if ground
            if (grid[next.Y][next.X] == '.')
                return;

            // If we have been to point before, exit as in continuous loop
            if (next.ToString() != Start.ToString() && Points.Any(p => next.ToString() == p.ToString()))
                return;
            Points.Add(next);
            last = current;
            current = next;

        } while (current.ToString() != Start.ToString());

        // Remove duplicate Start node
        Points.RemoveAt(Points.Count - 1);

        IsValid = true;
    }

    public int CalculateArea() {
        var area = 0;
        var sortedRows = Points.Select(p => p.Y).Distinct().OrderBy(p => p);

        foreach(var row in sortedRows) {
            var sortedXs = Points.Where(p => p.Y == row).OrderBy(r => r.X).ToList();
            for(int i = 0; i < sortedXs.Count(); i++) {
                if (i + i >= sortedXs.Count())
                    continue;

                if (sortedXs[i].Value == '-' || sortedXs[i+1].Value == '-')
                    continue;

                area += sortedXs[i + 1].X - sortedXs[i].X - 1;
                i++;
            }

            /*
            if (sortedXs.Count % 2 != 0)
                throw new Exception("Should only be odd");
            
            for(int i = 0; i < sortedXs.Count(); i += 2) {
                area += sortedXs[i + 1] - sortedXs[i] - 1;
            }
            */
        }

        return area;
    }
}


public class Point {
    public int X { get; set; }

    public int Y { get; set; }

    public char? Value { get; set; }

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Point(int x, int y, char value) {
        X = x;
        Y = y;
        Value = value;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}


