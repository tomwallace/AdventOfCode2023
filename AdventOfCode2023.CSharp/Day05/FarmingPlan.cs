using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day05;

public class FarmingPlan
{
    public List<long> Seeds { get;}

    public List<SeedRange> SeedRanges { get;  }

    public List<List<Offset>> Maps { get; set; }

    public List<List<Offset>> ReverseMaps { get; set; }

    public FarmingPlan(string filePath)
    {
        Maps = new List<List<Offset>>();

        // Parse
        var lines = FileUtility.ParseFileToList(filePath);
        Seeds = lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(c => long.Parse(c)).ToList();

        SeedRanges = new List<SeedRange>();
        for (int i = 0; i < Seeds.Count; i += 2)
        {
            var range = new SeedRange(Seeds[i], Seeds[i] + Seeds[i+1]);
            SeedRanges.Add(range);
        }
        
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

        var reverseMaps = Maps.ToList();
        reverseMaps.Reverse();
        ReverseMaps = reverseMaps;
    }

    public long TransformSeedThroughMaps(long seed)
    {
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

        return current;
    }

    public long TransformLocationThroughMaps(long location)
    {
        // Goes through maps in reverse to get the seed from a location
        var current = location;

        foreach (var map in ReverseMaps)
        {
            foreach (var offset in map)
            {
                // If seed never found it remains the same
                if (offset.ContainsReverse(current))
                {
                    current = offset.TransformReverse(current);
                    break;
                }
            }
        }

        return current;
    }

    public long FindLowestSeedAfterTransforms()
    {
        var finalSeeds = Seeds.Select(s => TransformSeedThroughMaps(s)).OrderBy(s => s).ToList();
        return finalSeeds[0];
    }

    public long FindLowestLocationAfterTransforms()
    {
        long current = 0;
        var found = false;
        while (!found)
        {
            current++;
            var seed = TransformLocationThroughMaps(current);
            if (IsSeedInSeedRanges(seed))
                return current;
        }

        throw new Exception("Should never get here");
    }

    internal bool IsSeedInSeedRanges(long seed)
    {
        return SeedRanges.Any(sr => sr.Contains(seed));
    }
}
