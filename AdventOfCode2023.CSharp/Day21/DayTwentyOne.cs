using System.Diagnostics;
using System.Text;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day21;

public class DayTwentyOne : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Step Counter";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 21;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day21\DayTwentyOneInput.txt";
        var count = CountVisitedPlots(filePath, 64, false);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day21\DayTwentyOneInput.txt";
        //var sumPower = SumPowerOfLeastCubes(filePath);

        return "";
    }

    public int CountVisitedPlots(string filePath, int maxSteps, bool useInfiniteGarden) {
        var garden = new Garden(filePath, useInfiniteGarden);
        var visited = new HashSet<(int X, int Y)>() { (garden.Start.X, garden.Start.Y) };
        for (int i = 0; i < maxSteps; i++) {
            var newVisited = new HashSet<(int X, int Y)>();
            foreach(var position in visited) {
                foreach(var next in garden.GetNeighbors(position.X, position.Y)) {
                    newVisited.Add((next.X, next.Y));
                }
            }
            visited = newVisited;
        }
    
        garden.Print(visited);
        return visited.Count();
    }
}

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
        //if (!UseInfiniteGarden) {
            if (x < 0 || x > Grid[0].Count - 1 || y < 0 || y > Grid.Count - 1)
               return false;
            return Grid[y][x] != '#';
        //}
        /*
        // Use infinite garden
        if (x == -1) {
            x = Grid[0].Count - 1;
        }
        if (x == Grid[0].Count) {
            x = 0;
        }
        if (y == -1) {
            y = Grid.Count - 1;
        }
        if (y == Grid.Count) {
            y = 0;
        }
        return Grid[y][x] != '#';
        */
    }

    public List<(int X, int Y)> GetNeighbors(int x, int y) {
        var mods = new List<(int X, int Y)>() { (0, -1), (1, 0), (0, 1), (-1, 0) };
        
        
        return mods.Select(m => {
                var localX = x + m.X;
                var localY = y + m.Y;
                // TODO: refactor to pull out common code
                if (UseInfiniteGarden) {
                    if (localX == -1) {
                        localX = Grid[0].Count - 1;
                    }
                    if (localX == Grid[0].Count) {
                        localX = 0;
                    }
                    if (localY == -1) {
                        localY = Grid.Count - 1;
                    }
                    if (localY == Grid.Count) {
                        localY = 0;
                    }
                }
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
