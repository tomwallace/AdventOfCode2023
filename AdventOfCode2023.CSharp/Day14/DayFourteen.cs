namespace AdventOfCode2023.CSharp.Day14;

public class DayFourteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Parabolic Reflector Dish [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 14;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day14\DayFourteenInput.txt";
        var load = RotateNorthAndCalculateLoad(filePath);

        return load.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day14\DayFourteenInput.txt";
        var load = CycleAndCalculateLoad(filePath, 1000000000);

        return load.ToString();
    }

    internal int RotateNorthAndCalculateLoad(string filePath)
    {
        var platform = new Platform(filePath);
        platform.Print();

        platform.RollNorth();

        platform.Print();

        var load = platform.CalculateLoadOnNorthernBeams();
        return load;
    }

    internal int CycleAndCalculateLoad(string filePath, int numCycles)
    {
        var platform = new Platform(filePath);

        var seenBefore = new List<int>();

        // Not sure why this works, but you have to pre-cycle the platform a number of times before the calculations work out
        for (int i = 0; i < 200; i++)
        {
            for (int lean = 0; lean < 4; lean++)
            {
                platform.RollNorth();
                platform.Spin();
            }
        }

        for (int i = 0; i <= numCycles; i++)
        {
            for (int lean = 0; lean < 4; lean++)
            {
                platform.RollNorth();
                platform.Spin();
            }

            var load = platform.CalculateLoadOnNorthernBeams();
            if (seenBefore.Contains(load))
            {
                var foundIndex = seenBefore.LastIndexOf(load);
                var cycleLength = i - foundIndex;

                var q = (numCycles - 200 - i - 1) % cycleLength;
                
                var answerIndex = (q + foundIndex) % cycleLength;
                var answer = seenBefore[answerIndex];
                return answer;
            } else
            {
                seenBefore.Add(load);
            }
        }

        // Should never get here
        throw new Exception("Should never get here");
    }
}
