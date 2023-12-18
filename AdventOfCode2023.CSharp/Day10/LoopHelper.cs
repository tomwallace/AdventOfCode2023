namespace AdventOfCode2023.CSharp.Day10;

public static class LoopHelper {
    public static Dictionary<char, List<Point>> Dirs = new Dictionary<char, List<Point>>() {
        { '|', new List<Point>() { new Point(0,-1), new Point(0,1) } },
        { '-', new List<Point>() { new Point(1,0), new Point(-1,0) } },
        { 'L', new List<Point>() { new Point(0,-1), new Point(1,0) } },
        { 'J', new List<Point>() { new Point(0,-1), new Point(-1,0) } },
        { '7', new List<Point>() { new Point(0,1), new Point(-1,0) } },
        { 'F', new List<Point>() { new Point(0,1), new Point(1,0) } }
    };
}


