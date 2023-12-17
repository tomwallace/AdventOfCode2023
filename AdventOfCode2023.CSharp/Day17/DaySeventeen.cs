using AdventOfCode2023.CSharp.Utility;
using Microsoft.VisualBasic;

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
        var minLoss = CalculateMinimumHeatLoss(filePath);

        return minLoss.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day17\DaySeventeenInput.txt";
        //var sumPower = SumPowerOfLeastCubes(filePath);

        return "";
    }

    internal int CalculateMinimumHeatLoss(string filePath) {
        var grid = FileUtility.ParseFileToList(filePath, r => r.ToCharArray().Select(c => int.Parse(c.ToString())).ToList());
        var minimumHeatLoss = int.MaxValue;
        var visited = new HashSet<string>();
        var queue = new PriorityQueue<MapState, int>();
        var initialState = new MapState(0, 0);
        queue.Enqueue(initialState, 0);

        while (queue.TryDequeue(out var currentState, out var cost)) {
            // End state
            if (currentState.X == grid[0].Count - 1 && currentState.Y == grid.Count - 1) {
                return cost;
            }

            foreach(var next in currentState.AvailableSteps()) {
                // If we are going straight and have already gone 3 steps, skip
                if (next.Straight && currentState.StepsStraight == 2)
                    continue;

                // If it would exit the board, skip 
                if (next.X < 0 || next.X > grid[0].Count -1 || next.Y < 0 || next.Y > grid.Count - 1)
                    continue;
                
                var heatAdded = grid[next.Y][next.X];                
                var newState = new MapState(currentState, next, heatAdded);

                // Only add if we have not been there before
                var key = $"{currentState.X},{currentState.Y},{newState.X},{newState.Y},{newState.StepsStraight}";
                if (visited.Add(key))
                    queue.Enqueue(newState, newState.HeatIncurred);
            }

        };

        throw new Exception("Should never get here");
    }
    
}

public class MapState {
    public int X { get; set; }

    public int Y { get; set; }

    public int ModX { get; set; }

    public int ModY { get; set; }

    public int StepsStraight { get; set; }

    public int HeatIncurred { get; set; }

    public MapState(int x, int y) {
        X = x;
        Y = Y;
        ModX = 1;
        ModY = 0;
        StepsStraight = 1;
        HeatIncurred = 0;
    }

    public MapState(MapState existing, Next next, int heatInCell) {
        X = next.X;
        Y = next.Y;
        ModX = next.ModX;
        ModY = next.ModY;
        StepsStraight = next.Straight ? existing.StepsStraight + 1 : 0;
        HeatIncurred += existing.HeatIncurred + heatInCell;
    }

    public List<Next> AvailableSteps() {
        var nextList = new List<Next>();
        // Facing North
        if (ModX == 0 && ModY == -1) {
            nextList.Add(new Next(X + -1, Y + 0, -1, 0, false));
            nextList.Add(new Next(X + 0, Y + -1, 0, -1, true));
            nextList.Add(new Next(X + 1, Y + 0, 1, 0, false));
        }
        // Facing East
        if (ModX == 1 && ModY == 0) {
            nextList.Add(new Next(X + 0, Y + -1, 0, -1, false));
            nextList.Add(new Next(X + 1, Y + 0, 1, 0, true));
            nextList.Add(new Next(X + 0, Y + 1, 0, 1, false));
        }
        // Facing South
        if (ModX == 0 && ModY == 1) {
            nextList.Add(new Next(X + 1, Y + 0, 1, 0, false));
            nextList.Add(new Next(X + 0, Y + 1, 0, 1, true));
            nextList.Add(new Next(X + -1, Y + 0, -1, 0, false));
        }
        // Facing West
        if (ModX == -1 && ModY == 0) {
            nextList.Add(new Next(X + 0, Y + 1, 0, 1, false));
            nextList.Add(new Next(X + -1, Y + 0, -1, 0, true));
            nextList.Add(new Next(X + 0, Y + -1, 0, -1, false));
        }
        
        return nextList;
    }

    public override string ToString()
    {
        return $"{X},{Y},{ModX},{ModY}";
    }
}

public class Next {
    public int X { get; set; }

    public int Y { get; set; }

    public int ModX { get; set; }

    public int ModY { get; set; }

    public bool Straight { get; set; }

    public Next(int x, int y, int modX, int modY, bool straight) {
        X = x;
        Y = y;
        ModX = modX;
        ModY = modY;
        Straight = straight;
    }

    public override string ToString()
    {
        return $"{X},{Y},{ModX},{ModY}";
    }
}
