namespace AdventOfCode2023.CSharp.Day10;

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
}