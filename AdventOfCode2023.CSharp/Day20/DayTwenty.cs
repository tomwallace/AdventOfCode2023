namespace AdventOfCode2023.CSharp.Day20;

public class DayTwenty : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Pulse Propagation [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 20;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day20\DayTwentyInput.txt";
        var totalPulses = CalculateTotalPulses(filePath);

        return totalPulses.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day20\DayTwentyInput.txt";
        var presses = NumberOfButtonPressesUntilLowRx(filePath);

        return presses.ToString();
    }

    internal long CalculateTotalPulses(string filePath) {
        var machine = new Machine(filePath);
        long totalLow = 0;
        long totalHigh = 0;
        for (var button = 1; button <= 1000; button++) {
            var result = machine.SendPulses();
            totalLow += result.Low;
            totalHigh += result.High;
        }

        return totalLow * totalHigh;
    }

    internal long NumberOfButtonPressesUntilLowRx(string filePath) {
        var machine = new Machine(filePath);
        var penultimateKeys = machine.GetPenultimateKeys("rx");

        var receivedLow = new List<long>();
        foreach(var key in penultimateKeys) {
            var localMachine = new Machine(filePath);
            long presses = 0;
            while (1 == 1) {
                presses++;
                var result = localMachine.SendPulses(key);
                if (result == (0, 0)) {
                    receivedLow.Add(presses);
                    break;
                }
            }
        }

        return MathUtility.LeastCommonMultiple(receivedLow);
    }
}