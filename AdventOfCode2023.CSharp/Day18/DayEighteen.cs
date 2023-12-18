using System.Diagnostics;
using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day18;

public class DayEighteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Lavaduct Lagoon";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 18;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day18\DayEighteenInput.txt";
        var area = CalculateLagoonArea(filePath);

        return area.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day18\DayEighteenInput.txt";
        var area = CalculateLagoonAreaWithColors(filePath);

        return area.ToString();
    }

    internal int CalculateLagoonArea(string filePath) {
        var lagoon = new Lagoon(filePath);
        var area = lagoon.CalculateArea();
        return area;
    }

    // Part B has numbers so large, the Part A process would not work
    // Need to modify to do it by straight math on points
    // I had to look up an equation to calculate the area from collection of corners
    internal long CalculateLagoonAreaWithColors(string filePath) {
        var points = new List<BigPoint>();
        var lines = FileUtility.ParseFileToList(filePath);
        (long X, long Y) current = (0, 0);
        foreach(var line in lines) {
            var point = new BigPoint(line, current);
            points.Add(point);
            current = (point.X, point.Y);
        }

        var totalLength = points.Sum(p => p.Length);

        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        var laces = points.Pairwise().Select(l => l.Item1.X * l.Item2.Y - l.Item1.Y * l.Item2.X);
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
        
        var area = Math.Abs(laces.Sum()) / 2;
        
        area += totalLength / 2 + 1;
        
        return area;
    }

}

public class Lagoon {

    public List<Point> Outline { get; set; }

    public char[,] Map { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public Lagoon(string filePath) {
        var instructions = FileUtility.ParseFileToList(filePath);
        Outline = new List<Point>();
        (int X, int Y) current = (0, 0); 
        foreach (var inst in instructions) {
            var split = inst.Split(' ');
            var dir = split[0];
            var num = int.Parse(split[1]);
            var color = split[2];
            current = DigTrench(dir, num, color, current);
        }

        // Draw the map
        var minX = Outline.Min(o => o.X);
        var maxX = Outline.Max(o => o.X);
        var minY = Outline.Min(o => o.Y);
        var maxY = Outline.Max(o => o.Y);
        
        Width = maxX - minX + 3;
        Height = maxY - minY + 3;
        Map = new char[Width, Height];

        foreach(var point in Outline) {
            Map[point.X - minX + 1, point.Y - minY + 1] = '#';
        }

        //Print();
        FloodFill((0, 0));
        //Print();
    }

    public void FloodFill((int X, int Y) start) {
        var queue = new Queue<(int X, int Y)>();
        queue.Enqueue(start);

        do {
            var current = queue.Dequeue();
            Map[current.X, current.Y] = '*';
            // Up
            if (current.Y > 0 && Map[current.X, current.Y - 1] == '\0') {
                Map[current.X, current.Y - 1] = '*';
                queue.Enqueue((current.X, current.Y - 1));
            }
            // Right
            if (current.X < Width - 1 && Map[current.X + 1, current.Y] == '\0') {
                Map[current.X + 1, current.Y] = '*';
                queue.Enqueue((current.X + 1, current.Y));
            }
            // Down
            if (current.Y < Height - 1 && Map[current.X, current.Y + 1] == '\0') {
                Map[current.X, current.Y + 1] = '*';
                queue.Enqueue((current.X, current.Y + 1));
            }
            // Left
            if (current.X > 0 && Map[current.X - 1, current.Y] == '\0') {
                Map[current.X - 1, current.Y] = '*';
                queue.Enqueue((current.X - 1, current.Y));
            }

        } while (queue.Any());
    }

    public int CalculateArea() {
        var area = 0;
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                // Only thing left null is the enclosed
                // But also want to include trenches
                area += Map[x, y] == '\0' || Map[x, y] == '#' ? 1 : 0;
            }
        }

        return area;
    }

    public void Print() {
        var builder = new StringBuilder();
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                var printVal = Map[x, y] == '\0' ? '.' : Map[x, y];
                builder.Append(printVal);
            }
            Debug.WriteLine(builder.ToString());
            builder.Clear();
        }

        Debug.WriteLine("");
        Debug.WriteLine("");
    }

    private (int X, int Y) DigTrench(string dir, int num, string color, (int X, int Y) current) {
        (int X, int Y) mod = (0, 0);
        switch (dir) {
            case "U":
                mod = (0, -1);
                break;
            case "R":
                mod = (1, 0);
                break;
            case "D":
                mod = (0, 1);
                break;
            case "L":
                mod = (-1, 0);
                break;
            default:
                throw new Exception($"Unrecognized dir: {dir}");
        } 
        
        for(var i = 0; i < num; i++) {
            current = (current.X + mod.X, current.Y + mod.Y);
            Outline.Add(new Point(current, color));
        }

        return current;
    }
}

public class Point {
    public int X { get; set; }

    public int Y { get; set; }

    public string Color { get; set; }

    public Point((int X, int Y) point, string color) {
        X = point.X;
        Y = point.Y;
        Color = color;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}

public class BigPoint {
    public long X { get; set; }

    public long Y { get; set; }

    public long Length { get; set; }

    public BigPoint(string line, (long X, long Y) start) {
        var split = line.Split(' ');
        // Only work with the color
        var color = split[2].Replace("(", "").Replace(")", "");
        var numberPortion = color[1..6];
        Length = Convert.ToInt32(numberPortion, 16);
        var dir = color[6];

        switch (dir) {
            // Up
            case '3':
                X = start.X;
                Y = start.Y - Length;
                break;
            // Right
            case '0':
                X = start.X + Length;
                Y = start.Y;
                break;
            // Down
            case '1':
                X = start.X;
                Y = start.Y + Length;
                break;
            // Left
            case '2':
                X = start.X - Length;
                Y = start.Y;
                break;
            default:
                throw new Exception($"Unrecognized dir: {dir}");
        }
    }

    public override string ToString()
    {
        return $"{X},{Y},{Length}";
    }
}
