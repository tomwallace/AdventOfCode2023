using System.Numerics;
using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day21;

public class DayTwentyOne : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Step Counter [HARD]";
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
        var count = PartTwo(filePath);

        return count.ToString();
    }

    internal int CountVisitedPlots(string filePath, int maxSteps, bool useInfiniteGarden) {
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

    // I had to go to Reddit for Part B.  My original solution for Part B worked, but took
    // 233 seconds for 1000 steps and we had to go up to 26501365.  So it was untenable.
    // The Reddit solutions took two forms, geometric and computational.
    // This solution, used observation to determine there are two states per tile, and derived
    // the formulas from that:  https://aoc.csokavar.hu/?day=21
    internal long PartTwo(string filePath) {
        // Exploiting some nice properties of the input it reduces to quadratic 
        // interpolation over 3 points: k * 131 + 65 for k = 0, 1, 2
        // I used the Newton method.
        var steps = Steps(ParseMap(filePath)).Take(328).ToArray();

        (decimal x0, decimal y0) = (65, steps[65]);
        (decimal x1, decimal y1) = (196, steps[196]);
        (decimal x2, decimal y2) = (327, steps[327]);

        decimal y01 = (y1 - y0) / (x1 - x0);
        decimal y12 = (y2 - y1) / (x2 - x1);
        decimal y012 = (y12 - y01) / (x2 - x0);

        var n = 26501365;
        return (long)(y0 + y01 * (n - x0) + y012 * (n - x0) * (n - x1));
    }

    // walks around and returns the number of available positions at each step
    private IEnumerable<long> Steps(HashSet<Complex> map) {
        var positions = new HashSet<Complex> { new Complex(65, 65) };
        while(true) {
            yield return positions.Count;
            positions = Step(map, positions);
        }
    }
    
    private HashSet<Complex> Step(HashSet<Complex> map, HashSet<Complex> positions) {
        Complex[] dirs = [1, -1, Complex.ImaginaryOne, -Complex.ImaginaryOne];

        var res = new HashSet<Complex>();
        foreach (var pos in positions) {
            foreach (var dir in dirs) {
                var posT = pos + dir;
                var tileCol = Mod(posT.Real, 131);
                var tileRow = Mod(posT.Imaginary, 131);
                if (map.Contains(new Complex(tileCol, tileRow))) {
                    res.Add(posT);
                }
            }
        }
        return res;
    }

    // the double % takes care of negative numbers
    private double Mod(double n, int m) => ((n % m) + m) % m;

    private HashSet<Complex> ParseMap(string filePath) {
        var lines = FileUtility.ParseFileToList(filePath);
        return (
            from irow in Enumerable.Range(0, lines.Count)
            from icol in Enumerable.Range(0, lines[0].Length)
            where lines[irow][icol] != '#'
            select new Complex(icol, irow)
        ).ToHashSet();
    }
}
