namespace AdventOfCode2023.CSharp.Day22;

public class Brick {
    public int Id { get; }

    public List<(int X, int Y, int Z)> Points { get; set; }

    // Ex: 1,0,1~1,2,1
    public Brick(string line, int id) {
        var split = line.Split('~').ToList();
        var splitOne = split[0].Split(',');
        (int X, int Y, int Z) start = (int.Parse(splitOne[0]), int.Parse(splitOne[1]), int.Parse(splitOne[2]));
        var splitTwo = split[1].Split(',');
        (int X, int Y, int Z) end = (int.Parse(splitTwo[0]), int.Parse(splitTwo[1]), int.Parse(splitTwo[2]));
        
        // Ensure we are starting at highest point
        if (end.Z < start.Z) {
            var temp = end;
            end = start;
            start = temp;
        }

        Points = new List<(int X, int Y, int Z)>() { start };
        // Make the points inbetween
        while (start != end) {
            start = (Converge(start.X, end.X), Converge(start.Y, end.Y), Converge(start.Z, end.Z));
            Points.Add(start);
        }

        Id = id;
    }

    public Brick(int id, List<(int X, int Y, int Z)> points) {
        Id = id;
        Points = points;
    }

    private int Converge(int input, int target)
    {
        if (input == target)
        {
            return input;
        }

        if (target > input)
        {
            return input + 1;
        }

        return input - 1;
    }
}