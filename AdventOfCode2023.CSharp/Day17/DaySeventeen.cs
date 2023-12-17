using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day17;

public class DaySeventeen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Clumsy Crucible";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 17;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day17\DaySeventeenInput.txt";
        var minLoss = CalculateMinimumHeatLoss(filePath, 0, 3);

        return minLoss.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day17\DaySeventeenInput.txt";
        var minLoss = CalculateMinimumHeatLoss(filePath, 4, 10);

        return minLoss.ToString();
    }

    internal int CalculateMinimumHeatLoss(string filePath, int minSteps, int maxSteps) {
        var grid = FileUtility.ParseFileToList(filePath, r => r.ToCharArray().Select(c => int.Parse(c.ToString())).ToList());
        var visited = new HashSet<string>();
        var queue = new PriorityQueue<MapState, int>();
        queue.Enqueue(new MapState(0, 0, MapState.East), 0);
        queue.Enqueue(new MapState(0, 0, MapState.South), 0);

        while (queue.TryDequeue(out var currentState, out var cost)) {
            // End state
            if (currentState.X == grid[0].Count - 1 && currentState.Y == grid.Count - 1
                && currentState.StepsStraight >= minSteps - 1) {
                return cost;
            }
 
            foreach(var next in currentState.AvailableSteps(minSteps)) {
                // If we are going straight and have already gone 3 steps, skip
                var newSteps = next == currentState.Direction ? currentState.StepsStraight + 1 : 0;
                if (newSteps == maxSteps)
                    continue;

                var x = currentState.X + next.ModX;
                var y = currentState.Y + next.ModY;
                // If it would exit the board, skip 
                if (x < 0 || x > grid[0].Count -1 || y < 0 || y > grid.Count - 1)
                    continue;
                
                var heatAdded = grid[y][x];                

                // Only add if we have not been there before
                var key = $"{currentState.X},{currentState.Y},{x},{y},{newSteps}";
                if (visited.Add(key)) {
                    var newState = new MapState(x, y, next, newSteps);
                    queue.Enqueue(newState, cost + heatAdded);
                }
            }
        };

        throw new Exception("Should never get here");
    }
}

public class MapState {

    public static readonly (int, int) North = (0, -1);
    
    public static readonly (int, int) East = (1, 0);
    
    public static readonly (int, int) South = (0, 1);
    
    public static readonly (int, int) West = (-1, 0);

    public int X { get; set; }

    public int Y { get; set; }

    public (int ModX, int ModY) Direction { get; set; }

    public int StepsStraight { get; set; }

    public MapState(int x, int y, (int ModX, int ModY) direction) {
        X = x;
        Y = Y;
        Direction = direction;
        StepsStraight = 1;
    }

    public MapState(int x, int y, (int ModX, int ModY) direction, int stepsStraight) {
        X = x;
        Y = y;
        Direction = direction;
        StepsStraight = stepsStraight;
    }

    public List<(int ModX, int ModY)> AvailableSteps(int minSteps) {
        if (StepsStraight < minSteps - 1)
            return new List<(int ModX, int ModY)>() { Direction };
        
        var directions = new List<(int ModX, int ModY)>();
        if (Direction != South)
        {
            directions.Add(North);
        }
        
        if (Direction != West)
        {
            directions.Add(East);
        }
        
        if (Direction != North)
        {
            directions.Add(South);
        }

        if (Direction != East)
        {
            directions.Add(West);
        }
        
        return directions;
    }

    public override string ToString()
    {
        return $"{X},{Y},{Direction.ModX},{Direction.ModY}";
    }
}