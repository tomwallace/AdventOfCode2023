namespace AdventOfCode2023.CSharp.Day19;

public class MRange {
    public int Min { get; set; }
    public int Max { get; set; }

    public MRange(int min, int max) {
        Min = min;
        Max = max;
    }

    public int Length() {
        return Max - Min + 1;
    }

    public MRange Clone() {
        return new MRange(Min, Max);
    }
}
