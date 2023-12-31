using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day23;

public class Map {
    public List<List<char>> Grid { get; }

    public bool UseSlipperySlopes { get; set; }

    public Map(string filePath, bool useSlipperySlopes) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        UseSlipperySlopes = useSlipperySlopes;
    }

    public bool IsValid((int X, int Y) possible, Dictionary<(int X, int Y), string> previous) {
        if (previous.ContainsKey(possible))
            return false;

        if (possible.X < 0 || possible.X > Grid[0].Count - 1 || possible.Y < 0 || possible.Y > Grid.Count - 1)
            return false;

        return Grid[possible.Y][possible.X] != '#';
    }

    public List<(int X, int Y)> GetNeighbors((int X, int Y) current, Dictionary<(int X, int Y), string> previous) {
        var mods = new List<(int X, int Y)>() { (0, -1), (1, 0), (0, 1), (-1, 0) };
        
        // Handle steep slope
        if (UseSlipperySlopes && Grid[current.Y][current.X] == '^')
            mods = new List<(int X, int Y)> { (0, -1) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == '>')
            mods = new List<(int X, int Y)> { (1, 0) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == 'v')
            mods = new List<(int X, int Y)> { (0, 1) };

        if (UseSlipperySlopes && Grid[current.Y][current.X] == '<')
            mods = new List<(int X, int Y)> { (-1, 0) };
        
        return mods.Select(m => {
                var localX = current.X + m.X;
                var localY = current.Y + m.Y;
                return (localX, localY);
            }).Where(m => IsValid(m, previous))
            .ToList();
    }
}