using System.Diagnostics;
using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day21;

public class Garden {
    public List<List<char>> Grid { get; }

    public (int X, int Y) Start { get; }

    public bool UseInfiniteGarden { get; set; }

    public Garden(string filePath, bool useInfiniteGarden) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        UseInfiniteGarden = useInfiniteGarden;
        for (int y = 0; y < Grid.Count; y++) {
            for (int x = 0; x < Grid.Count; x++) {
                if (Grid[y][x] == 'S') {
                    Start = (x, y);
                    return;
                }
            }
        }
    }

    public bool IsValid(int x, int y) {
        if (!UseInfiniteGarden) {
            if (x < 0 || x > Grid[0].Count - 1 || y < 0 || y > Grid.Count - 1)
               return false;

            return Grid[y][x] != '#';
        }
        
        // Use infinite garden
        var xMod = x % Grid[0].Count;
        if (x < 0) {
            x = Grid[0].Count + xMod;
        }
        if (x >= Grid[0].Count) {
            x = xMod;
        }
        var yMod = y % Grid.Count;
        if (y < 0) {
            y = Grid.Count + yMod;
        }
        if (y >= Grid.Count) {
            y = yMod;
        }
        return Grid[y][x] != '#';
    }

    public List<(int X, int Y)> GetNeighbors(int x, int y) {
        var mods = new List<(int X, int Y)>() { (0, -1), (1, 0), (0, 1), (-1, 0) };
        
        
        return mods.Select(m => {
                var localX = x + m.X;
                var localY = y + m.Y;
                return (localX, localY);
            }).Where(m => IsValid(m.Item1, m.Item2)).ToList();
    }

    public void Print(HashSet<(int X, int Y)> visited) {
        var builder = new StringBuilder();
        for (int y = 0; y < Grid.Count; y++) {
            for (int x = 0; x < Grid.Count; x++) {
                if (visited.Any(v => v.X == x && v.Y == y))
                    builder.Append("O");
                else
                    builder.Append(Grid[y][x]);
            }
            Debug.WriteLine(builder.ToString());
            builder.Clear();
        }

        Debug.WriteLine("");
        Debug.WriteLine("");
    }
}
