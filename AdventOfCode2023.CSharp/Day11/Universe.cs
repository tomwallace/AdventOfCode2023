using System.Diagnostics;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day11;

public class Universe {
    public List<List<char>> Grid { get; set; }

    public List<Galaxy> Galaxies { get; set; }

    public List<int> ExpandRows { get; set; }

    public List<int> ExpandCols { get; set; }

    public int ExpandSteps { get; }

    public Universe(string filePath, int expandSteps) {
        Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());
        Galaxies = new List<Galaxy>();
        ExpandSteps = expandSteps;

        ExpandRows = new List<int>();
        for (int i = 0; i < Grid.Count; i++) {
            if (Grid[i].All(c => c == '.'))
                ExpandRows.Add(i);
        }

        ExpandCols = new List<int>();
        for (int i = 0; i < Grid[0].Count; i++) {
            if (Grid.All(c => c[i] == '.'))
                ExpandCols.Add(i);
        }

        //PrintGrid();

        var idCounter = 1;
        for (int y = 0; y < Grid.Count; y++) {
            for (int x = 0; x < Grid[0].Count; x++) {
                if (Grid[y][x] == '#') {
                    Galaxies.Add(new Galaxy(idCounter, x, y));
                    idCounter++;
                }
            }
        }
    }

    public long SumMinimumDistance() {
        var pairs = Galaxies.SelectMany(x => Galaxies, (x, y) => Tuple.Create(x, y))
                            .Where(t => t.Item1.Id < t.Item2.Id);
        long sumMinDist = 0;

        foreach(var pair in pairs) {
            // use Manhattan Distance - but account for expandSteps
            var xExpanders = ExpandCols.Count(e => (e >= pair.Item1.X && e <= pair.Item2.X) ||
                (e >= pair.Item2.X && e <= pair.Item1.X));
            var yExpanders = ExpandRows.Count(e => (e >= pair.Item1.Y && e <= pair.Item2.Y) ||
                (e >= pair.Item2.Y && e <= pair.Item1.Y));
            var dist = Math.Abs(pair.Item1.X - pair.Item2.X) + (xExpanders * ExpandSteps) 
                        + Math.Abs(pair.Item1.Y - pair.Item2.Y) + (yExpanders * ExpandSteps);
            sumMinDist += dist;
        }

        return sumMinDist;
    }

    public void PrintGrid() {
        foreach(var row in Grid) {
            var line = new string(row.ToArray());
            Debug.WriteLine(line);
        }
    }
}
