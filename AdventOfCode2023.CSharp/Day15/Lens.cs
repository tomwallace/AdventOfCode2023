namespace AdventOfCode2023.CSharp.Day15;

public class Lens {
    public string Id {get; set;}

    public int FocalLength { get; set; }

    public Lens(string id, int focalLength) {
        Id = id;
        FocalLength = focalLength;
    }

    public override string ToString()
    {
        return $"{Id} {FocalLength}";
    }
}
