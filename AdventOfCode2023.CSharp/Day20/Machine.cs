using AdventOfCode2023.CSharp.Utility;

namespace AdventOfCode2023.CSharp.Day20;

public class Machine {
    public Dictionary<string, Module> Modules { get; }

    public Machine(string filePath) {
        Modules = new Dictionary<string, Module>();
        foreach(var line in FileUtility.ParseFileToList(filePath)) {
            var module = new Module(line);
            Modules.Add(module.Id, module);
        }

        // Need to update the Conj-based modules
        foreach(var conjMod in Modules.Where(m => m.Value.Op == '&')) {
            var id = conjMod.Value.Id;
            var sentTo = Modules.Count(m => m.Value.SendsTo.Contains(id));
            conjMod.Value.ConjWatched = sentTo;
        }
    }

    public (long Low, long High) SendPulses(string? findModuleKey = null) {
        long totalLow = 0;
        long totalHigh = 0;

        var queue = new Queue<(string Id, bool IsHigh, string FromId)>();
        queue.Enqueue(("broadcaster", false, "button"));
        totalLow++;   // for button going to broadcaster

        do {
            var current = queue.Dequeue();
            //var debugText = current.IsHigh ? "high" : "low";
            //Debug.WriteLine($"{current.FromId} -{debugText}-> {current.Id}");

            // If we are searching for a key that will receive a low signal
            if (findModuleKey != null && !current.IsHigh && current.Id == findModuleKey)
                return (0, 0);

            if (!Modules.ContainsKey(current.Id))
                continue;

            var sent = Modules[current.Id].BroadCast(current.IsHigh, current.FromId);
            foreach(var s in sent) {
                if (s.IsHigh)
                    totalHigh++;
                else
                    totalLow++;
                queue.Enqueue((s.Id, s.IsHigh, current.Id));
            }
        } while (queue.Any());

        return (totalLow, totalHigh);
    }

    public List<string> GetPenultimateKeys(string keySearchingFor) {
        var penultimateKeys = new List<string>();
        foreach (var module in Modules.Where(m => m.Value.SendsTo.Contains(keySearchingFor)))
        {
            foreach (var item in Modules)
            {
                if (item.Value.SendsTo.Contains(module.Key))
                {
                    penultimateKeys.Add(item.Key);
                }
            }
        }

        return penultimateKeys;
    }

}
