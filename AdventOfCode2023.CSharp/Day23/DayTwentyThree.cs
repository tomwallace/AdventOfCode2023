namespace AdventOfCode2023.CSharp.Day23;

public class DayTwentyThree : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "A Long Walk [HARD]";
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
        var newAttempt = new NewAttempt();
        var steps = newAttempt.PartTwo(filePath);

        return steps.ToString();
    }

    // Part B - useSlipperySlopes = false = took too long.
    internal int FindMostStepsOnHike(string filePath, bool useSlipperySlopes) {
        var map = new TrailMap(filePath, useSlipperySlopes);
        var queue = new PriorityQueue<MapStep, int>();
        queue.Enqueue(new MapStep((1, 0), 0), 0);
        var mostSteps = 0;

        while (queue.TryDequeue(out var current, out var cost)) {
            // If at exit
            if (current.Position.X == map.Grid[0].Count - 2 && current.Position.Y == map.Grid.Count - 1) {
                mostSteps = mostSteps < current.Steps ? current.Steps : mostSteps;
                continue;
            }

            if (mostSteps > 100000)
                return mostSteps;

            foreach (var next in map.GetNeighbors(current.Position, current.Previous)) {
                var nextStep = new MapStep(next, current);
                queue.Enqueue(nextStep, nextStep.Steps);
            }
                
        };

        return mostSteps;
    }    
}