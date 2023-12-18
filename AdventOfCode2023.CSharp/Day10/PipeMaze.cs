using System.Diagnostics;
using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day10;

public class PipeMaze {

    public List<List<char>> Grid { get; }

    public Point Start { get; }

    public Loop PipeLoop { get; set; }
    
    public char[,] Map { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public PipeMaze(string filePath) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        Start = new Point(0,0);
        // Draw the map
        var minX = -1;
        var maxX = Grid[0].Count;
        var minY = -1;
        var maxY = Grid.Count;
        
        Width = maxX - minX + 1;
        Height = maxY - minY + 1;
        Map = new char[Width, Height];
        
        for(int y = 0; y < Grid.Count; y++) {
            for(int x = 0; x < Grid[0].Count; x++) {
                if (Grid[y][x] == 'S')
                    Start = new Point(x, y);
                Map[x+1, y+1] = Grid[y][x];
            }
        }

        PipeLoop = CreateLoop();
        RemoveJunk();
        //Print();
    }

    public Loop CreateLoop() {
        // Iterate over each potential Dir as Start, and see if it makes a loop
        foreach(var dir in LoopHelper.Dirs.Keys) {
            var loop = new Loop(Grid, Start, dir);
            if (loop.IsValid)
                return loop;
        }

        // At least one loop should be valid
        throw new Exception("None of the loops were valid");
    }

    public int StepsFarthestInPipeLoop() {
        return PipeLoop.Points.Count / 2;
    }

    public int AreaInPipeLoop() {
        var area = 0;
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                // Only thing left null is the enclosed
                area += Map[x, y] == '.' ? 1 : 0;
            }
        }

        return area;
    }

    // I struggled a lot with Part B, I could not calculate it successfully using straight
    // comparisons of X points.  Flood fill did not work because parallel pipes not separated
    // by a space still counted as spaced.  I found online someone discovered that an enclosed
    // space would cross over a pipe an odd number of times when going to the edge.
    // https://github.com/mattbutton/advent-of-code-2023/blob/master/Day10.cs
    public int AreaInPipeLoopByTraversal() {
        var enclosed = new List<(int X, int Y)>();

        for (var y = 0; y < Grid.Count; y++)
        {
            for (var x = 0; x < Grid[0].Count; x++)
            {
                if (IsEnclosed((x, y)))
                    enclosed.Add((x,y));
            }
        }

        return enclosed.Count;
    }

    private bool IsEnclosed((int X, int Y) current) {
        // It is a Pipe
        if (Map[current.X + 1, current.Y + 1] != '.')
            return false;
        
        var x = current.X + 1;
        var counter = 0;
		do
		{
			x++;
            var point = Map[x, current.Y + 1];
            if(point != '.' && point != '-' && point != 'J' && point != 'L')
				counter++;
		}
		while(x < Width - 1);

		return counter % 2 == 1;
    }

    public void RemoveJunk() {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var match = PipeLoop.Points.FirstOrDefault(p => p.X == x - 1 && p.Y == y - 1);
                if (match == null) {                    
                    Map[x,y] = '.';
                }
            }
        }
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
}