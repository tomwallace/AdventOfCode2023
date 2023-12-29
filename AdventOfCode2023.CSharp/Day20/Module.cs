namespace AdventOfCode2023.CSharp.Day20;

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
