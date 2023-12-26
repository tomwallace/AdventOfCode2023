using System.Diagnostics;
using AdventOfCode2023.CSharp.Utility;
using Microsoft.VisualBasic;

namespace AdventOfCode2023.CSharp.Day20;

public class DayTwenty : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Pulse Propagation [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 20;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Day20\DayTwentyInput.txt";
        var totalPulses = CalculateTotalPulses(filePath);

        return totalPulses.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Day20\DayTwentyInput.txt";
        var presses = NumberOfButtonPressesUntilLowRx(filePath);

        return presses.ToString();
    }

    internal long CalculateTotalPulses(string filePath) {
        long totalLow = 0;
        long totalHigh = 0;
        
        var modules = new Dictionary<string, Module>();
        foreach(var line in FileUtility.ParseFileToList(filePath)) {
            var module = new Module(line);
            modules.Add(module.Id, module);
        }

        // Need to update the Conj-based modules
        foreach(var conjMod in modules.Where(m => m.Value.Op == '&')) {
            var id = conjMod.Value.Id;
            var sentTo = modules.Count(m => m.Value.SendsTo.Contains(id));
            conjMod.Value.ConjWatched = sentTo;
        }

        for (var button = 1; button <= 1000; button++) {
            var queue = new Queue<(string Id, bool IsHigh, string FromId)>();
            queue.Enqueue(("broadcaster", false, "button"));
            totalLow++;   // for button going to broadcaster

            do {
                var current = queue.Dequeue();
                //var debugText = current.IsHigh ? "high" : "low";
                //Debug.WriteLine($"{current.FromId} -{debugText}-> {current.Id}");
                if (!modules.ContainsKey(current.Id))
                    continue;
                var sent = modules[current.Id].BroadCast(current.IsHigh, current.FromId);
                foreach(var s in sent) {
                    if (s.IsHigh)
                        totalHigh++;
                    else
                        totalLow++;
                    queue.Enqueue((s.Id, s.IsHigh, current.Id));
                }
            } while (queue.Any());
        }

        return totalLow * totalHigh;
    }

    // TODO: Refactor common code out
    internal long NumberOfButtonPressesUntilLowRx(string filePath) {
        var modules = new Dictionary<string, Module>();
        foreach(var line in FileUtility.ParseFileToList(filePath)) {
            var module = new Module(line);
            modules.Add(module.Id, module);
        }

        // Need to update the Conj-based modules
        foreach(var conjMod in modules.Where(m => m.Value.Op == '&')) {
            var id = conjMod.Value.Id;
            var sentTo = modules.Count(m => m.Value.SendsTo.Contains(id));
            conjMod.Value.ConjWatched = sentTo;
        }

        long buttonPresses = 0;
        do {
            var queue = new Queue<(string Id, bool IsHigh, string FromId)>();
            queue.Enqueue(("broadcaster", false, "button"));

            do {
                buttonPresses++;
                
                var current = queue.Dequeue();
                if (current.Id == "rx" && current.IsHigh == false)
                    return buttonPresses;

                //var debugText = current.IsHigh ? "high" : "low";
                //Debug.WriteLine($"{current.FromId} -{debugText}-> {current.Id}");
                if (!modules.ContainsKey(current.Id))
                    continue;
                var sent = modules[current.Id].BroadCast(current.IsHigh, current.FromId);
                foreach(var s in sent) {
                    queue.Enqueue((s.Id, s.IsHigh, current.Id));
                }
            } while (queue.Any());
        } while (1 == 1);

        throw new Exception("Should never get here");
    }
    
}

public class Module {
    public string Id { get; }

    public char? Op { get; }

    public List<string> SendsTo { get; }

    public bool FlipFlop { get; set; }

    // bool = IsHigh
    public Dictionary<string, bool> Conj { get; set; }

    public int ConjWatched { get; set; }

    // Ex: broadcaster -> a, b, c
    // Ex: %a -> b
    public Module(string line) {
        var split = line.Split(new string[] { " -> " },StringSplitOptions.TrimEntries);
        SendsTo = split[1].Split(',', StringSplitOptions.TrimEntries).ToList();
        if (split[0][0] == '%') {
            Op = '%';
            Id = split[0].Substring(1);
        } else if (split[0][0] == '&') {
            Op = '&';
            Id = split[0].Substring(1);
        } else {
            // broadcast module
            Id = split[0];
        }

        FlipFlop = false;
        Conj = new Dictionary<string, bool>();
        ConjWatched = 0;
    }

    public List<(string Id, bool IsHigh)> BroadCast(bool isHighIncoming, string fromId) {
        var broadCast = new List<(string Id, bool IsHigh)>();
        switch(Op) {
            case '%':
                if (!isHighIncoming) {
                    FlipFlop = !FlipFlop;
                    foreach(var sends in SendsTo) {
                        broadCast.Add((sends, FlipFlop));
                    }
                }
                // Ignore high incoming
                break;
            case '&':
                if (Conj.ContainsKey(fromId))
                    Conj[fromId] = isHighIncoming;
                else
                    Conj.Add(fromId, isHighIncoming);
                
                var isHighSend = !(Conj.Count(c => c.Value == true) == ConjWatched);
                foreach(var sends in SendsTo) {
                    broadCast.Add((sends, isHighSend));
                }
                break;
            // broadcast
            default:
                foreach(var sends in SendsTo) {
                    broadCast.Add((sends, isHighIncoming));
                }
                break;
        }

        return broadCast;
    }
}
