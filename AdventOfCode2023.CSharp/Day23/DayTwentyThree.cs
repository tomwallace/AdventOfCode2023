using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day23;

public class DayTwentyThree : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "A Long Walk";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 23;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day23\DayTwentyThreeInput.txt";
        var steps = FindMostStepsOnHike(filePath, true);

        return steps.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day23\DayTwentyThreeInput.txt";
        var steps = FindMostStepsOnHike(filePath, false);

        return steps.ToString();
    }


    internal int FindMostStepsOnHike(string filePath, bool useSlipperySlopes) {
        var map = new Map(filePath, useSlipperySlopes);
        var queue = new PriorityQueue<MapStep, int>();
        queue.Enqueue(new MapStep((1, 0), 0), 0);
        var mostSteps = 0;

        while (queue.TryDequeue(out var current, out var cost)) {
            // If at exit
            if (current.Position.X == map.Grid[0].Count - 2 && current.Position.Y == map.Grid.Count - 1) {
                mostSteps = mostSteps < current.Steps ? current.Steps : mostSteps;
                continue;
            }

            foreach (var next in map.GetNeighbors(current.Position, current.Previous)) {
                var nextStep = new MapStep(next, current);
                queue.Enqueue(nextStep, nextStep.Steps);
            }
                
        };

        return mostSteps;
    }
    
}

public class Map {
    public List<List<char>> Grid { get; }

    public bool UseSlipperySlopes { get; }

    public Map(string filePath, bool useSlipperySlopes) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        UseSlipperySlopes = useSlipperySlopes;
    }

    public bool IsValid((int X, int Y) possible, Dictionary<(int X, int Y), string> previous) {
        if (previous.ContainsKey(possible))
            return false;

        if (possible.X < 0 || possible.X > Grid[0].Count - 1 || possible.Y < 0 || possible.Y > Grid.Count - 1)
            return false;

        return Grid[possible.Y][possible.X] != '#';
    }

    public List<(int X, int Y)> GetNeighbors((int X, int Y) current, Dictionary<(int X, int Y), string> previous) {
        var mods = new List<(int X, int Y)>() { (0, -1), (1, 0), (0, 1), (-1, 0) };
        
        // Handle steep slope
        if (UseSlipperySlopes && Grid[current.Y][current.X] == '^')
            mods = new List<(int X, int Y)> { (0, -1) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == '>')
            mods = new List<(int X, int Y)> { (1, 0) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == 'v')
            mods = new List<(int X, int Y)> { (0, 1) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == '<')
            mods = new List<(int X, int Y)> { (-1, 0) };
        
        return mods.Select(m => {
                var localX = current.X + m.X;
                var localY = current.Y + m.Y;
                return (localX, localY);
            }).Where(m => IsValid(m, previous))
            .ToList();
    }
}

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
