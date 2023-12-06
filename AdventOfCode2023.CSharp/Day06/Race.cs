namespace AdventOfCode2023.CSharp.Day06;

public class Race
{
    public long Time { get; set; }

    public long Record { get; set; }

    public Race(long time, long record)
    {
        Time = time;
        Record = record;
    }

    public long NumberOfWaysToBeat()
    {
        var waysToBeat = 0;

        for (long i = 1; i <= Time; i++)
        {
            if (DistanceCovered(Time, i) > Record)
                waysToBeat++;
        }

        return waysToBeat;
    }

    internal long DistanceCovered(long raceTime, long buttonHeld)
    {
        return (raceTime - buttonHeld) * buttonHeld;
    }
}