using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day05;

public class DayFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "If You Give A Seed A Fertilizer";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 5;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day05\DayFiveInput.txt";
        var farmingPlan = new FarmingPlan(filePath, false);

        return farmingPlan.FindLowestSeedAfterTransforms().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day05\DayFiveInput.txt";
        var farmingPlan = new FarmingPlan(filePath, true);

        return farmingPlan.FindLowestSeedAfterTransforms().ToString();
    }

    
    
}

public class FarmingPlan
{
    private Dictionary<long, long> cache = new Dictionary<long, long>();
    private bool useSeedRanges;
    
    public List<long> Seeds { get;}

    public List<List<Offset>> Maps { get; set; }

    public FarmingPlan(string filePath, bool useSeedRangesToggle)
    {
        cache = new Dictionary<long, long>();
        useSeedRanges = useSeedRangesToggle;
        Maps = new List<List<Offset>>();

        // Parse
        var lines = FileUtility.ParseFileToList(filePath);
        Seeds = lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(c => long.Parse(c)).ToList();
        
        
        var map = new List<Offset>();
        for(int i = 3; i < lines.Count; i++)
        {
            var line = lines[i];

            // Add to Maps if empty line or last line
            if (line == "")
            {
                Maps.Add(map);
                map = new List<Offset>();
                i++;
                continue;
            }

            // Process into a offset
            map.Add(new Offset(line));

            // Add to Maps if empty line or last line
            if (i == lines.Count - 1)
            {
                Maps.Add(map);
                map = new List<Offset>();
                i++;
                continue;
            }
        }
    }

    public long TransformSeedThroughMaps(long seed)
    {
        if (cache.ContainsKey(seed))
            return cache[seed];
        
        var current = seed;
        // Transverse the maps in order
        foreach(var map in Maps)
        {
            foreach(var offset in map)
            {
                // If seed never found it remains the same
                if (offset.Contains(current))
                {
                    current = offset.Transform(current);
                    break;
                }
            }
        }

        cache.Add(seed, current);
        return current;
    }

    public long FindLowestSeedAfterTransforms()
    {
        if (!useSeedRanges)
        {
            var finalSeeds = Seeds.Select(s => TransformSeedThroughMaps(s)).OrderBy(s => s).ToList();
            return finalSeeds[0];
        }

        // Now the numbers are paired, with the first as the starting number and the second as the number in the range
        long minimum = long.MaxValue;
        // TODO: move the cache here so not doubling up
        var alreadyDone = new Dictionary<long, string>();
        
        for (int p = 0; p < Seeds.Count; p += 2)
        {
            for (long i = Seeds[p]; i <= Seeds[p] + Seeds[p + 1] - 1; i++)
            {
                if (alreadyDone.ContainsKey(i))
                    continue;

                var value = TransformSeedThroughMaps(i);
                minimum = minimum < value ? minimum : value;
                alreadyDone.Add(i, "");
            }
        }
        
        return minimum;
    }
}

public class Offset
{
    public long FromStart { get;}

    public long FromEnd { get; }
    
    public long Adjuster { get; }

    public long ToStart { get; }

    public Offset(string line)
    {
        var entries = line.Split(' ');
        FromStart = long.Parse(entries[1]);
        Adjuster = long.Parse(entries[2]);
        FromEnd = FromStart + Adjuster - 1;
        ToStart = long.Parse(entries[0]);
    }

    public bool Contains(long input)
    {
        return input >= FromStart && input <= FromEnd;
    }

    public long Transform(long input)
    {
        var off = input - FromStart;
        if (input <= 0)
            throw new ArgumentException($"{input} is less than {FromStart}");

        return ToStart + off;
    }

    
}
