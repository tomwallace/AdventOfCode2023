using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day25;

public class DayTwentyFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Snowverload [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 25;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day25\DayTwentyFiveInput.txt";
        var product = FindProductOfGroupSizes(filePath);

        return product.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        return "Still need to complete all stars.";
    }

    
    internal int FindProductOfGroupSizes(string filePath) {
        Random r = new Random();
        // Need size of 3 from Karger's algorithm
        var result = ApplyCut(filePath, r);
        while (result.CutSize != 3) {
            result = ApplyCut(filePath, r);
        }

        return result.GroupOneSize * result.GroupTwoSize;
    }

    // I did not know how to approach this one and Reddit helped identify Karger's algorithm
    // https://en.wikipedia.org/wiki/Karger%27s_algorithm
    // It is a randomized algorithm that works through permutations to find the minimal "cut"
    // and return the sizes of the two components separated by the cut.
    internal (int CutSize, int GroupOneSize, int GroupTwoSize) ApplyCut(string filePath, Random r) {
        var components = new Dictionary<string, List<string>>();
        foreach(var line in FileUtility.ParseFileToList(filePath)) {
            var split = line.Split(':',StringSplitOptions.TrimEntries);
            var key = split[0];
            var connections = split[1].Split(' ').ToList();
            foreach(var connection in connections) {
                // forward
                if (components.ContainsKey(key))
                    components[key].Add(connection);
                else 
                    components.Add(key, new List<string>() { connection });
                
                // backward
                if (components.ContainsKey(connection))
                    components[connection].Add(key);
                else 
                    components.Add(connection, new List<string>() { key });
            }
        }

        var componentSize = components.Keys.ToDictionary(k => k, _ => 1);

        for (var i = 0; components.Count > 2; i++) {
            // Cut a random connection and put in a new connection that inherits the connections of all those
            // nodes, except the original severed connection.
            var randomNumber = r.Next(components.Count);
            var u = components.Keys.ElementAt(randomNumber);
            randomNumber = r.Next(components[u].Count);
            var v = components[u][randomNumber];
 
            var merged = $"replacement-{i}";
            var theirConnections = components[u].Where(c => c != v).ToList()
                    .Concat(components[v].Where(c => c != u).ToList());
            components.Add(merged, theirConnections.ToList());

            components = Rebind(components, u, merged, v);
            components = Rebind(components, v, merged, u);
 
            components.Remove(u);
            components.Remove(v);
 
            componentSize[merged] = componentSize[u] + componentSize[v];
        }

        // Should be down to just two keys remaining
        if (components.Count != 2)
            throw new Exception("Unexpected number of keys after processing");

        var a = components.Keys.First();
        var b = components.Keys.Last();
        return (components[a].Count(), componentSize[a], componentSize[b]);

    }

    // Mutates the component dictionary as we go
    private Dictionary<string, List<string>> Rebind(Dictionary<string, List<string>> components, string oldKey, string newKey, string ignoreKey) {
        foreach (var next in components[oldKey].ToList()) {
            if (next != ignoreKey) {
                components[next].Remove(oldKey);
                components[next].Add(newKey);
            }
        }

        return components;
    }
}