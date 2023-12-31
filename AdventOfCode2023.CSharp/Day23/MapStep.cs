namespace AdventOfCode2023.CSharp.Day23;

public class MapStep {
    public (int X, int Y) Position { get; set; }

    public int Steps { get; set; }

    public Dictionary<(int X, int Y), string> Previous { get; set; }

    public MapStep((int X, int Y) position, int steps) {
        Position = position;
        Steps = steps;
        Previous = new Dictionary<(int X, int Y), string>();
    }

    public MapStep((int X, int Y) newPosition, MapStep last) {
        Position = newPosition;
        Steps = last.Steps + 1;
        var newPrevious = last.Previous.ToDictionary();
        newPrevious.Add((last.Position.X, last.Position.Y), "");
        Previous = newPrevious;
    }
}
