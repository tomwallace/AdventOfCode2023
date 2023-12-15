using AdventOfCode2023.CSharp.Utility;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace AdventOfCode2023.CSharp.Day14;

public class DayFourteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Parabolic Reflector Dish [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 14;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day14\DayFourteenInput.txt";
        var load = RotateNorthAndCalculateLoad(filePath);

        return load.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day14\DayFourteenInput.txt";
        var load = CycleAndCalculateLoad(filePath, 1000000000);

        return load.ToString();
    }

    internal int RotateNorthAndCalculateLoad(string filePath)
    {
        var platform = new Platform(filePath);
        platform.Print();

        platform.RollNorth();

        platform.Print();

        var load = platform.CalculateLoadOnNorthernBeams();
        return load;
    }

    internal int CycleAndCalculateLoad(string filePath, int numCycles)
    {
        var platform = new Platform(filePath);
        var foundDup = false;

        var seenBefore = new List<string>();
        var loadValues = new Dictionary<string, int>();
        for (int i = 0; i < numCycles; i++)
        {
            for (int lean = 0; lean < 4; lean++)
            {
                platform.RollNorth();
                platform.Spin();
            }

            var asString = platform.ToString();
            if (seenBefore.Contains(asString))
            {
                var loc = seenBefore.IndexOf(asString);
                var rem = (numCycles - i) % (seenBefore.Count - loc) + 1;

                var loadValue = loadValues[seenBefore[rem]];
                return loadValue;
            } else
            {
                seenBefore.Add(asString);
                loadValues.Add(asString, platform.CalculateLoadOnNorthernBeams());
            }
        }

        // Should never get here
        throw new Exception("Should never get here");
    }
}

public class Platform
{
    public List<List<char>> Grid { get; set; }

    public List<string> AsRows { get; set; }

    public List<string> AsCols { get; set; }

    public int NumberRoundRocks { get; set; }

    public List<Rock> Rocks { get; set; }

    public Platform(string filePath)
    {
        Grid = new List<List<char>>();
        Rocks = new List<Rock>();
        AsCols = new List<string>();
        AsRows = new List<string>();

        var countRocks = 0;
        var lines = FileUtility.ParseFileToList(filePath);
        // rows
        foreach (var line in lines)
        {
            Grid.Add(line.ToCharArray().ToList());
            AsRows.Add(line);
            countRocks += line.Count(c => c == 'O');
        }

        NumberRoundRocks = countRocks;
        // cols
        AsCols = Rotate(AsRows).ToList();
    }

    public int CalculateLoadOnNorthernBeams()
    {
        var load = 0;
        var southEdge = AsCols.Count;
        for (int y = 0; y < AsCols.Count; y++)
        {
            for (int x = 0; x < AsCols[y].Length; x++)
            {
                if (AsCols[y][x] == 'O')
                {
                    var rockLoad = southEdge - x;
                    load += rockLoad; ;
                }
            }
        }

        return load;
    }

    public void RollNorth()
    {
        var newCols = new List<string>();
        for(int col = 0; col < AsCols.Count; col++)
        {
            var current = AsCols[col];
            var newCol = new string(current).Replace("O", ".").ToCharArray();
            var fixedIndexes = new List<int>() { -1 };
            fixedIndexes = fixedIndexes.Concat(current.AllIndexesOf("#").ToList())
                .Concat(new List<int>() { current.Length }).ToList();

            foreach (var idx in fixedIndexes.Pairwise())
            {
                var count = current.Where((c, i) => c == 'O' && i > idx.Item1 && i < idx.Item2).Count();
                for (int i = idx.Item1 + 1; i <= idx.Item1 + count; i++)
                    newCol[i] = 'O';
            }
            newCols.Add(new string(newCol));
        }

        // sanity check
        var newNumberRocks = newCols.Sum(str => str.Count(c => c == 'O'));
        if (newNumberRocks != NumberRoundRocks)
            throw new Exception("Number of rocks are different after roll");

        AsCols = newCols.ToList();

        // Recalc AsRows - TODO: needs work
        AsRows = RotateCounter(AsCols).ToList();
    }

    public void Print()
    {
        var builder = new StringBuilder();
        for(int y =  0; y < AsCols[0].Length; y++)
        {
            for(int x = 0; x < AsCols.Count; x++)
            {
                builder.Append(AsCols[x][y].ToString());
            }
            Debug.WriteLine(builder.ToString());
            builder.Clear();
        }

        Debug.WriteLine("");
        Debug.WriteLine("");
    }

    private List<string> Rotate(List<string> current)
    {
        var rotated = new List<string>();
        var builder = new StringBuilder();
        for (int i = 0; i < current[0].Length; i++)
        {
            foreach (var row in current)
            {
                builder.Append(row[i]);
            }
            rotated.Add(builder.ToString());
            builder.Clear();
        }

        return rotated;
    }

    public void Spin()
    {
        var rotated = new List<string>();
        var builder = new StringBuilder();
        
        for (int y = AsCols[0].Length - 1; y >= 0; y-- )
        {
            for (int x = 0; x < AsCols.Count; x++)
            {
                builder.Append(AsCols[x][y]);
            }
            rotated.Add(builder.ToString());
            builder.Clear();
        }

        AsCols = rotated.ToList();
    }


    private List<string> RotateCounter(List<string> current)
    {
        var rotated = new List<string>();
        var builder = new StringBuilder();
        for (int i = current[0].Length - 1; i >= 0; i--)
        {
            foreach (var row in AsRows)
            {
                builder.Append(row[i]);
            }
            rotated.Add(builder.ToString());
            builder.Clear();
        }

        return rotated;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        foreach(var col in AsCols)
            builder.Append(col);
        return builder.ToString();
    }
}

// TODO: Move to utility if used
public static class StringExtensions
{
    public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
    {
        int minIndex = str.IndexOf(searchstring);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
        }
    }
}


public class Rock
{
    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
